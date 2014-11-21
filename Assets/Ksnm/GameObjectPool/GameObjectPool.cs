/*
 Copyright (c) 2014 Takahiro Kasanami
 
 This software is provided 'as-is', without any express or implied
 warranty. In no event will the authors be held liable for any damages
 arising from the use of this software.
 
 Permission is granted to anyone to use this software for any purpose,
 including commercial applications, and to alter it and redistribute it
 freely, subject to the following restrictions:
 
    1. The origin of this software must not be misrepresented; you must not
    claim that you wrote the original software. If you use this software
    in a product, an acknowledgment in the product documentation would be
    appreciated but is not required.
 
    2. Altered source versions must be plainly marked as such, and must not be
    misrepresented as being the original software.
 
    3. This notice may not be removed or altered from any source
    distribution.
*/
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Ksnm
{
    /// <summary>
    /// ゲームオブジェクトのプーリングを行うクラス
    /// 公式のチュートリアルを参考に作成。
    /// </summary>
    public class GameObjectPool : MonoBehaviour
    {
        /// <summary>
        /// 行動
        /// </summary>
        public enum Action
        {
            /// <summary>
            /// 何もしない
            /// </summary>
            None = 0x00,
            /// <summary>
            /// 子オブジェクトを削除
            /// </summary>
            DestroyChildren = 0x01,
            /// <summary>
            /// 子オブジェクトを無効化
            /// </summary>
            InactivateChildren = 0x02,
        }
        /// <summary>
        /// Awake時に行う行動
        /// </summary>
        public Action awokenAction;
        /// <summary>
        /// Start時に行う行動
        /// </summary>
        public Action startAction;

        void Awake()
        {
            ManagedGameObjects = new Dictionary<int, List<GameObject>>();
            Act(awokenAction);
        }

        void Start()
        {
            Act(startAction);
        }

        void Act(Action action)
        {
            if (action != Action.None)
            {
                if (action == Action.DestroyChildren)
                {
                    for (int i = 0; i < transform.childCount; i++)
                    {
                        GameObject.Destroy(transform.GetChild(i).gameObject);
                    }
                }
                else if (action == Action.InactivateChildren)
                {
                    for (int i = 0; i < transform.childCount; i++)
                    {
                        Inactivate(transform.GetChild(i).gameObject);
                    }
                }
            }
        }

        /// <summary>
        /// 管理しているゲームオブジェクト
        /// </summary>
        public Dictionary<int, List<GameObject>> ManagedGameObjects { get; private set; }

        /// <summary>
        /// ゲームオブジェクトをプールから取得する。
        /// 必要であれば新たに生成する。
        /// </summary>
        /// <param name="prefab">プレハブ</param>
        /// <param name="localPosition">Transform.localPositionへ設定する値</param>
        /// <param name="localRotation">Transform.localRotationへ設定する値</param>
        /// <param name="localScale">Transform.localScaleへ設定する値</param>
        /// <returns></returns>
        public GameObject GetGameObject(GameObject prefab, Vector3 localPosition, Quaternion localRotation, Vector3 localScale)
        {
            // プレハブのインスタンスIDをkeyとする
            int key = prefab.GetInstanceID();
            // Dictionaryにkeyが存在しなければ作成する
            if (ManagedGameObjects.ContainsKey(key) == false)
            {
                ManagedGameObjects.Add(key, new List<GameObject>());
            }
            // プレハブのインスタンスID毎のリスト
            List<GameObject> gameObjects = ManagedGameObjects[key];
            // 使用されていないインスタンスを検索
            foreach (var gameObject in gameObjects)
            {
                // アクティブじゃない＝使用されていない
                if (gameObject.activeInHierarchy == false)
                {
                    // 設定しなおして返す。
                    gameObject.transform.localPosition = localPosition;
                    gameObject.transform.localRotation = localRotation;
                    gameObject.transform.localScale = localScale;
                    gameObject.SetActive(true);
                    return gameObject;
                }
            }
            // 以前作ったインスタンスが無いので新規作成
            {
                var gameObject = (GameObject)GameObject.Instantiate(prefab);
                gameObject.transform.parent = transform;
                gameObject.transform.localPosition = localPosition;
                gameObject.transform.localRotation = localRotation;
                gameObject.transform.localScale = localScale;
                // 追加
                gameObjects.Add(gameObject);
                return gameObject;
            }
        }
        /// <summary>
        /// ゲームオブジェクトをプールから取得する。
        /// 必要であれば新たに生成する。
        /// </summary>
        /// <param name="prefab">プレハブ</param>
        /// <param name="localPosition">Transform.localPositionへ設定する値</param>
        /// <param name="localRotation">Transform.localRotationへ設定する値</param>
        /// <returns></returns>
        public GameObject GetGameObject(GameObject prefab, Vector3 localPosition, Quaternion localRotation)
        {
            return GetGameObject(prefab, localPosition, localRotation, Vector3.one);
        }
        /// <summary>
        /// ゲームオブジェクトをプールから取得する。
        /// 必要であれば新たに生成する。
        /// </summary>
        /// <param name="prefab">プレハブ</param>
        /// <param name="localPosition">Transform.localPositionへ設定する値</param>
        /// <returns></returns>
        public GameObject GetGameObject(GameObject prefab, Vector3 localPosition)
        {
            return GetGameObject(prefab, localPosition, Quaternion.identity, Vector3.one);
        }
        /// <summary>
        /// ゲームオブジェクトをプールから取得する。
        /// 必要であれば新たに生成する。
        /// </summary>
        /// <param name="prefab"></param>
        /// <returns></returns>
        public GameObject GetGameObject(GameObject prefab)
        {
            return GetGameObject(prefab, Vector3.zero, Quaternion.identity, Vector3.one);
        }
        /// <summary>
        /// アクティブ/非アクティブに関わらず、管理下のゲームオブジェクトの数を取得
        /// </summary>
        /// <param name="prefab"></param>
        /// <returns></returns>
        public int GetCount(GameObject prefab)
        {
            // プレハブのインスタンスID
            int key = prefab.GetInstanceID();
            if (ManagedGameObjects.ContainsKey(key) == false)
                return 0;
            List<GameObject> gameObjects = ManagedGameObjects[key];
            return gameObjects.Count;
        }
        /// <summary>
        /// アクティブ/非アクティブに関わらず、管理下のゲームオブジェクトの数を取得
        /// </summary>
        /// <param name="prefab"></param>
        /// <returns></returns>
        public int GetCount()
        {
            int count = 0;
            foreach (var list in ManagedGameObjects.Values)
            {
                count += list.Count;
            }
            return count;
        }
        /// <summary>
        /// アクティブな、管理下のゲームオブジェクトの数を取得
        /// </summary>
        /// <param name="prefab"></param>
        /// <returns></returns>
        public int GetActiveCount(GameObject prefab)
        {
            // プレハブのインスタンスID
            int key = prefab.GetInstanceID();
            if (ManagedGameObjects.ContainsKey(key) == false)
                return 0;
            List<GameObject> gameObjects = ManagedGameObjects[key];
            return gameObjects.Where(item => item.activeSelf).Count();
        }
        /// <summary>
        /// アクティブな、管理下のゲームオブジェクトの数を取得
        /// </summary>
        /// <param name="prefab"></param>
        /// <returns></returns>
        public int GetActiveCount()
        {
            int count = 0;
            foreach (var list in ManagedGameObjects.Values)
            {
                count += list.Where(item => item.activeSelf).Count();
            }
            return count;
        }
        /// <summary>
        /// 非アクティブな、管理下のゲームオブジェクトの数を取得
        /// </summary>
        /// <param name="prefab"></param>
        /// <returns></returns>
        public int GetInactiveCount(GameObject prefab)
        {
            // プレハブのインスタンスID
            int key = prefab.GetInstanceID();
            if (ManagedGameObjects.ContainsKey(key) == false)
                return 0;
            List<GameObject> gameObjects = ManagedGameObjects[key];
            return gameObjects.Where(item => !item.activeSelf).Count();
        }
        /// <summary>
        /// 非アクティブな、管理下のゲームオブジェクトの数を取得
        /// </summary>
        /// <param name="prefab"></param>
        /// <returns></returns>
        public int GetInactiveCount()
        {
            int count = 0;
            foreach (var list in ManagedGameObjects.Values)
            {
                count += list.Where(item => !item.activeSelf).Count();
            }
            return count;
        }
        /// <summary>
        /// ゲームオブジェクトを非アクティブにする。
        /// </summary>
        /// <param name="gameObject"></param>
        public void Inactivate(GameObject gameObject)
        {
            gameObject.SetActive(false);
        }
        /// <summary>
        /// 特定のプレハブのゲームオブジェクトを非アクティブにします。
        /// </summary>
        /// <param name="gameObject"></param>
        public void InactivateFromPrefab(GameObject prefab)
        {
            // プレハブのインスタンスID
            int key = prefab.GetInstanceID();
            if (ManagedGameObjects.ContainsKey(key) == false)
                return;
            List<GameObject> gameObjects = ManagedGameObjects[key];
            foreach (var gameObject in gameObjects)
            {
                Inactivate(gameObject);
            }
        }
        /// <summary>
        /// 管理している全てのゲームオブジェクトを非アクティブにします。
        /// </summary>
        /// <param name="gameObject"></param>
        public void InactivateAll()
        {
            foreach (var list in ManagedGameObjects.Values)
            {
                foreach (var gameObject in list)
                {
                    Inactivate(gameObject);
                }
            }
        }
    }
}
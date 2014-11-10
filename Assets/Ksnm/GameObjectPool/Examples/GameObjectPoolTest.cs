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

namespace Ksnm.Examples
{
    /// <summary>
    /// GameObjectPoolをテスト
    /// </summary>
    public class GameObjectPoolTest : MonoBehaviour
    {
        public GameObjectPool targetPool;
        public GameObject hogePrefab;
        public GameObject piyoPrefab;
        void OnGUI()
        {
            GUILayout.BeginArea(new Rect(0, 0, Screen.width, Screen.height));
            GUILayout.Label("GameObjectPoolをテスト");
            {
                GUILayout.Label("hogePrefab");
                var count = targetPool.GetCount(hogePrefab);
                GUILayout.Label("インスタンスの数=" + count);
                count = targetPool.GetInactiveCount(hogePrefab);
                GUILayout.Label("非アクティブ数=" + count);
                count = targetPool.GetActiveCount(hogePrefab);
                GUILayout.Label("アクティブ数=" + count);
                if (GUILayout.Button("hogePrefabをインスタンス化"))
                {
                    targetPool.GetGameObject(hogePrefab, new Vector3((count % 10) * 2, -1, (count / 10) * 2));
                }
                if (GUILayout.Button("hogePrefabを非アクティブ化"))
                {
                    targetPool.InactivateFromPrefab(hogePrefab);
                }
            }
            {
                GUILayout.Label("piyoPrefab");
                var count = targetPool.GetCount(piyoPrefab);
                GUILayout.Label("インスタンスの数=" + count);
                count = targetPool.GetInactiveCount(piyoPrefab);
                GUILayout.Label("非アクティブ数=" + count);
                count = targetPool.GetActiveCount(piyoPrefab);
                GUILayout.Label("アクティブ数=" + count);
                if (GUILayout.Button("piyoPrefabをインスタンス化"))
                {
                    targetPool.GetGameObject(piyoPrefab, new Vector3((count % 10) * 2, +1, (count / 10) * 2));
                }
                if (GUILayout.Button("piyoPrefabを非アクティブ化"))
                {
                    targetPool.InactivateFromPrefab(piyoPrefab);
                }
            }
            if (GUILayout.Button("全て非アクティブ化"))
            {
                targetPool.InactivateAll();
            }
            GUILayout.EndArea();
        }
    }
}
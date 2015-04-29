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
using System.Collections;
using System.Collections.Generic;
using Original = UnityEngine;

namespace Ksnm.ExtensionMethods.UnityEngine
{
    public static class GameObject_
    {
        /// <summary>
        /// GetComponentInChildrenの親方向版
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="gameObject"></param>
        /// <param name="selfExclusion">呼び出したOriginal.GameObjectを含めるか否か</param>
        /// <returns></returns>
        public static T GetComponentInParent<T>(this Original.GameObject gameObject, bool selfExclusion = false) where T : Original.Component
        {
            if (selfExclusion == false)
            {
                var component = gameObject.GetComponent<T>();
                if (component != null)
                {
                    return component;
                }
            }
            if (gameObject.transform.parent != null)
            {
                return gameObject.transform.parent.gameObject.GetComponentInParent<T>();
            }
            return null;
        }
        /// <summary>
        /// ２つのコンポーネントを検索
        /// T1,T2→親のT1,T2→親のT1,T2→・・・という順番
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="gameObject"></param>
        /// <returns></returns>
        public static Original.GameObject GetComponentInParent<T1, T2>(this Original.GameObject gameObject, bool selfExclusion = false)
            where T1 : Original.Component
            where T2 : Original.Component
        {
            if (selfExclusion == false)
            {
                var component1 = gameObject.GetComponent<T1>();
                if (component1 != null)
                {
                    return component1.gameObject;
                }
                var component2 = gameObject.GetComponent<T1>();
                if (component2 != null)
                {
                    return component2.gameObject;
                }
            }
            if (gameObject.transform.parent != null)
            {
                return gameObject.transform.parent.gameObject.GetComponentInParent<T1, T2>();
            }
            return null;
        }
    }
}

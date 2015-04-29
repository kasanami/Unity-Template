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
using Array = System.Array;
using Original = UnityEngine;

namespace Ksnm.ExtensionMethods.UnityEngine
{
    public static class Transform_
    {
        /// <summary>
        /// Hierarchy上の親を持たないオブジェクトを取得
        /// </summary>
        /// <param name="transform"></param>
        /// <returns></returns>
        public static Original.GameObject[] FindRootObjects(this Original.Transform transform)
        {
            return Array.FindAll(Original.GameObject.FindObjectsOfType<Original.GameObject>(), (item) => item.transform.parent == null);
        }
        #region localEulerAngles
        /// <summary>
        /// localEulerAnglesのxだけを設定。
        /// </summary>
        public static void SetLocalEulerAnglesX(this Original.Transform transform, float value)
        {
            var temp = transform.localEulerAngles;
            temp.x = value;
            transform.localEulerAngles = temp;
        }
        /// <summary>
        /// localEulerAnglesのyだけを設定。
        /// </summary>
        public static void SetLocalEulerAnglesY(this Original.Transform transform, float value)
        {
            var temp = transform.localEulerAngles;
            temp.y = value;
            transform.localEulerAngles = temp;
        }
        /// <summary>
        /// localEulerAnglesのzだけを設定。
        /// </summary>
        public static void SetLocalEulerAnglesZ(this Original.Transform transform, float value)
        {
            var temp = transform.localEulerAngles;
            temp.z = value;
            transform.localEulerAngles = temp;
        }
        #endregion localEulerAngles
        #region localPosition
        /// <summary>
        /// localPositionのxだけを設定。
        /// </summary>
        public static void SetLocalPositionX(this Original.Transform transform, float value)
        {
            var temp = transform.localPosition;
            temp.x = value;
            transform.localPosition = temp;
        }
        /// <summary>
        /// localPositionのyだけを設定。
        /// </summary>
        public static void SetLocalPositionY(this Original.Transform transform, float value)
        {
            var temp = transform.localPosition;
            temp.y = value;
            transform.localPosition = temp;
        }
        /// <summary>
        /// localPositionのzだけを設定。
        /// </summary>
        public static void SetLocalPositionZ(this Original.Transform transform, float value)
        {
            var temp = transform.localPosition;
            temp.z = value;
            transform.localPosition = temp;
        }
        #endregion localPosition
        #region localScale
        /// <summary>
        /// localScaleのxだけを設定。
        /// </summary>
        public static void SetLocalScaleX(this Original.Transform transform, float value)
        {
            var temp = transform.localScale;
            temp.x = value;
            transform.localScale = temp;
        }
        /// <summary>
        /// localScaleのyだけを設定。
        /// </summary>
        public static void SetLocalScaleY(this Original.Transform transform, float value)
        {
            var temp = transform.localScale;
            temp.y = value;
            transform.localScale = temp;
        }
        /// <summary>
        /// localScaleのzだけを設定。
        /// </summary>
        public static void SetLocalScaleZ(this Original.Transform transform, float value)
        {
            var temp = transform.localScale;
            temp.z = value;
            transform.localScale = temp;
        }
        #endregion localScale
        /// <summary>
        /// 引数の名前とタグに合う、子供の数を計算します。
        /// nameまたはtagがnullの場合、条件から外します。
        /// </summary>
        /// <param name="transform"></param>
        /// <param name="name"></param>
        /// <param name="tag"></param>
        /// <returns></returns>
        public static int GetChildCount(this Original.Transform transform, string name, string tag = null)
        {
            int count = 0;
            foreach (Original.Transform child in transform)
            {
                if (name != null)
                {
                    if (tag != null)
                    {
                        if (child.name == name && child.tag == tag)
                        {
                            ++count;
                        }
                    }
                    else
                    {
                        if (child.name == name)
                        {
                            ++count;
                        }
                    }
                }
                else
                {
                    if (tag != null)
                    {
                        if (child.tag == tag)
                        {
                            ++count;
                        }
                    }
                }
            }
            return count;
        }
    }
}
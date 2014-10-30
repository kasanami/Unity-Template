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
    /// 表示したActivityIndicatorが、勝手に消されないように
    /// キーとフラグで表示状態を管理します。
    /// </summary>
    public static class HandheldActivityIndicator
    {
#if UNITY_ANDROID || UNITY_IPHONE
        public static Dictionary<object, bool> Flags { get; private set; }
        /// <summary>
        /// 所望のスタイルを設定します。
        /// </summary>
#if UNITY_IPHONE
        public static iOSActivityIndicatorStyle Style { get; set; }
#elif UNITY_ANDROID
        public static AndroidActivityIndicatorStyle Style { get; set; }
#endif
        /// <summary>
        /// コンストラクタ
        /// </summary>
        static HandheldActivityIndicator()
        {
            Flags = new Dictionary<object, bool>();
        }
        /// <summary>
        /// 表示開始
        /// </summary>
        public static void Start(object key)
        {
#if UNITY_IPHONE
            Handheld.SetActivityIndicatorStyle(Style);
#elif UNITY_ANDROID
            Handheld.SetActivityIndicatorStyle(AndroidActivityIndicatorStyle.Small);
#endif
            Flags[key] = true;
            Handheld.StartActivityIndicator();
        }
        /// <summary>
        /// 非表示
        /// </summary>
        public static void Stop(object key)
        {
            Flags[key] = false;
            if (Flags.Any() == true)
                return;// いづれか一つでもtrueなら消さない
            Handheld.StopActivityIndicator();
        }
        /// <summary>
        /// 強制的に非表示
        /// 全てのフラグはfalseになります。
        /// </summary>
        public static void Stop()
        {
            foreach (var key in Flags.Keys)
            {
                Flags[key] = false;
            }
            Handheld.StopActivityIndicator();
        }
#else
#endif// UNITY_ANDROID || UNITY_IPHONE
    }
}
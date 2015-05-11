/*
 Copyright (c) 2014-2015 Takahiro Kasanami
 
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
using System.Collections.Generic;

namespace Ksnm
{
    /// <summary>
    /// 表示したActivityIndicatorが、勝手に消されないように
    /// キーとフラグで表示状態を管理します。
    /// </summary>
    public static class HandheldActivityIndicator
    {
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
            Flags[key] = true;
#if UNITY_IPHONE || UNITY_ANDROID
            Handheld.SetActivityIndicatorStyle(Style);
            Handheld.StartActivityIndicator();
#endif
        }
        /// <summary>
        /// 非表示
        /// </summary>
        public static void Stop(object key)
        {
            Flags[key] = false;
#if UNITY_IPHONE || UNITY_ANDROID
            // いづれか一つでもtrueなら消さない
            if (Flags.Values.Any(item => item) == true)
                return;
            // 非表示
            Handheld.StopActivityIndicator();
#endif
        }
        /// <summary>
        /// 強制的に非表示
        /// 全てのフラグはfalseになります。
        /// </summary>
        public static void Stop()
        {
            // 全てにfalseを設定
            foreach (var key in Flags.Keys)
                Flags[key] = false;
#if UNITY_IPHONE || UNITY_ANDROID
            // 非表示
            Handheld.StopActivityIndicator();
#endif
        }
    }
}


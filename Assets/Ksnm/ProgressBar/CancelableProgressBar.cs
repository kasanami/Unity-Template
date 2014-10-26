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

namespace Ksnm
{
    /// <summary>
    /// UnityEditor.EditorUtility.DisplayCancelableProgressBarにネスト機能を追加したプログレスバー
    /// </summary>
    public class CancelableProgressBar : ProgressBar
    {
#if UNITY_EDITOR
        /// <summary>
        /// Updateでキャンセルボタンが押されるとtrueになります。
        /// </summary>
        public bool Canceled { get; private set; }
        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="title"></param>
        /// <param name="info"></param>
        /// <param name="countMax"></param>
        public CancelableProgressBar(string title)
            : base(title)
        {
            Canceled = false;
        }
        /// <summary>
        /// 更新
        /// </summary>
        /// <returns>キャンセルボタンが押された時はtrueを返します。</returns>
        public override bool Update(float add = 0)
        {
            UpdateProgress(add);
            Canceled = UnityEditor.EditorUtility.DisplayCancelableProgressBar(DisplayTitle, DisplayInfo, Progress);
            return Canceled;
        }
#endif// UNITY_EDITOR
    }
}
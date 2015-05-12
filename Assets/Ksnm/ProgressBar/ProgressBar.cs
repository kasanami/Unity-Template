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
using System;
using System.Collections;
using System.Collections.Generic;

#if Ksnm_Using_UniLinq
using UniLinq;
#else
using System.Linq;
#endif

namespace Ksnm
{
    /// <summary>
    /// UnityEditor.EditorUtility.DisplayProgressBarにネスト機能を追加したプログレスバー
    /// </summary>
#if UNITY_EDITOR
    public class ProgressBar : IDisposable
    {
        /// <summary>
        /// Updateでキャンセルボタンが押されるとtrueになります。
        /// ・for文内などで、Canceledを見てbreakする際、End関数を忘れないように注意してください。
        /// </summary>
        public bool Canceled { get; set; }
        protected class ProgressItem
        {
            public string info;
            public float position;
            public float size;
            public float Progress
            {
                get { return position / size; }
            }
            public ProgressItem(string info, float size)
            {
                this.info = info;
                this.position = 0;
                this.size = size;
            }
        }
        Stack<ProgressItem> ProgressStack = new Stack<ProgressItem>();
        protected ProgressItem CurrentItem;
        /// <summary>
        /// 進捗率(0.0～1.0)
        /// </summary>
        protected float Progress { get; private set; }
        /// <summary>
        /// Progressを更新します。
        /// </summary>
        /// <param name="add">更新されたプロセス数</param>
        protected void UpdateProgress(float add)
        {
            CurrentItem.position += add;
            float progress = 0;
            //ProgressStack.GetEnumerator()
            foreach (var item in ProgressStack)
            {
                var scale = 1.0f / item.size;
                progress *= scale;
                progress += item.Progress;
            }
            Progress = progress;
        }
        /// <summary>
        /// ウインドウに表示されるタイトル
        /// </summary>
        protected string title;
        /// <summary>
        /// 表示用title
        /// </summary>
        protected string DisplayTitle
        {
            get
            {
                return title + " " + (Progress * 100).ToString("0.00") + "％";
            }
        }
        /// <summary>
        /// 表示用info
        /// ・UnityEditor.EditorUtility.DisplayProgressBarが改行表示に対応していないので
        /// 　スタックの区切りは"\t"を使用します。
        /// </summary>
        protected string DisplayInfo
        {
            get
            {
                var info = "";
                foreach (var item in ProgressStack.Reverse())
                {
                    info += item.info + "(" + item.position + "／" + item.size + ")" + "\t";
                }
                return info;
            }
        }
        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="title">ウインドウに表示されるタイトル</param>
        public ProgressBar(string title)
        {
            this.title = title;
            Canceled = false;
        }
        /// <summary>
        /// 開始（ネスト深度を1段階深くします。）
        /// </summary>
        /// <returns>キャンセルを検知するための戻り値ですが、このクラスでは常にfalseを返します。</returns>
        public virtual bool Begin(float size, string info = "")
        {
            CurrentItem = new ProgressItem(info, size);
            ProgressStack.Push(CurrentItem);
            return Update(0);
        }
        /// <summary>
        /// 更新
        /// この関数の前に、Beginを呼ぶ必要があります。
        /// </summary>
        /// <param name="add">更新されたプロセス数</param>
        /// <returns>キャンセルされた場合trueを返します。</returns>
        public virtual bool Update(float add = 0)
        {
            UpdateProgress(add);
            UnityEditor.EditorUtility.DisplayProgressBar(DisplayTitle, DisplayInfo, Progress);
            return Canceled;
        }
        /// <summary>
        /// 終了（ネスト深度を1段階浅くします。）
        /// </summary>
        /// <returns>キャンセルされた場合trueを返します。</returns>
        public virtual bool End()
        {
            if (ProgressStack.Count > 0)
            {
                ProgressStack.Pop();
                if (ProgressStack.Count <= 0)
                {
                    CurrentItem = null;
                    UnityEditor.EditorUtility.ClearProgressBar();
                }
                else
                {
                    CurrentItem = ProgressStack.Peek();
                }
            }
            return Canceled;
        }
        #region IDisposable
        /// <summary>
        /// End()が呼ばれないままループを抜ける等しても
        /// 「using ステートメント」を使用していれば自動的に閉じる。
        /// </summary>
        public void Dispose()
        {
            if (ProgressStack.Count > 0)
            {
                ProgressStack.Clear();
                CurrentItem = null;
                UnityEditor.EditorUtility.ClearProgressBar();
            }
        }
        #endregion IDisposable
    }
#else
    public class ProgressBar
    {
    }
#endif// UNITY_EDITOR
}
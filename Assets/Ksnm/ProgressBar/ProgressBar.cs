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
    /// UnityEditor.EditorUtility.DisplayProgressBarにネスト機能を追加したプログレスバー
    /// </summary>
    public class ProgressBar
    {
#if UNITY_EDITOR
        protected string title;
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
        protected ProgressItem CurrentStack;
        protected float Progress { get; private set; }
        protected void UpdateProgress(float add)
        {
            CurrentStack.position += add;
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
                    var percent = (item.Progress * 100).ToString("0.00");
                    info += item.info + percent + "％" + "(" + item.position + "／" + item.size + ")" + "\t";
                }
                return info;
            }
        }
        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="title"></param>
        public ProgressBar(string title)
        {
            this.title = title;
        }
        /// <summary>
        /// 開始
        /// </summary>
        /// <returns>キャンセルを検知するための戻り値ですが、このクラスでは常にfalseを返します。</returns>
        public virtual bool Begin(float size, string info = "")
        {
            CurrentStack = new ProgressItem(info, size);
            ProgressStack.Push(CurrentStack);
            return Update(0);
        }
        /// <summary>
        /// 更新
        /// この関数の前に、Beginを呼ぶ必要があります。
        /// </summary>
        /// <param name="add">更新する</param>
        /// <returns>キャンセルを検知するための戻り値ですが、このクラスでは常にfalseを返します。</returns>
        public virtual bool Update(float add = 0)
        {
            UpdateProgress(add);
            UnityEditor.EditorUtility.DisplayProgressBar(DisplayTitle, DisplayInfo, Progress);
            return false;
        }
        /// <summary>
        /// 終了
        /// </summary>
        public virtual void End()
        {
            if (ProgressStack.Count > 0)
            {
                ProgressStack.Pop();
                if (ProgressStack.Count <= 0)
                {
                    CurrentStack = null;
                    UnityEditor.EditorUtility.ClearProgressBar();
                }
                else
                {
                    CurrentStack = ProgressStack.Peek();
                }
            }
        }
#endif// UNITY_EDITOR
    }
}
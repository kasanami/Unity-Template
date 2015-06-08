/*
The zlib License

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

3. This notice may not be removed or altered from any source distribution.
*/
using UnityEngine;
using System.Collections;

namespace Ksnm
{
    /// <summary>
    /// 条件に合えば、自ら消えるオブジェクト
    /// </summary>
    public class AutoDestroy : MonoBehaviour
    {
        [SerializeField, TooltipAttribute("消す対象")]
        public GameObject target;
        [SerializeField, TooltipAttribute("消す条件")]
        public Trigger trigger;
        [SerializeField, TooltipAttribute("有効になる環境")]
        public EnvironmentToEnable enableTo;
        /// <summary>
        /// 条件
        /// </summary>
        public enum Trigger
        {
            Awake,
            Start,
            FirstUpdate,
        }
        /// <summary>
        /// 有効にする環境
        /// </summary>
        [System.Serializable]
        public struct EnvironmentToEnable
        {
            public bool Always;
            public bool DebugBuild;
            public bool NotDebugBuild;
            public bool Editor;
            public bool EditorWin;
            public bool EditorOSX;
            public bool Standalone;
            public bool StandaloneWin;
            public bool StandaloneOSX;
            public bool StandaloneLinux;
            public bool WebPlayer;
            public bool iOS;
            public bool Android;
        }
        /// <summary>
        /// 現在の環境で有効にするか返します。
        /// </summary>
        bool Enabled
        {
            get
            {
                if (enableTo.Always) { return true; }
                if (Debug.isDebugBuild && enableTo.DebugBuild) { return true; }
                if (Debug.isDebugBuild && enableTo.DebugBuild == false) { return true; }

#if UNITY_EDITOR
                if (enableTo.Editor) { return true; }
#endif

#if UNITY_EDITOR_WIN
                if (enableTo.EditorWin) { return true; }
#endif

#if UNITY_EDITOR_OSX
                if (enableTo.EditorOSX) { return true; }
#endif

#if UNITY_STANDALONE
                if (enableTo.Standalone) { return true; }
#endif

#if UNITY_STANDALONE_WIN
                if (enableTo.StandaloneWin) { return true; }
#endif

#if UNITY_STANDALONE_OSX
                if (enableTo.StandaloneOSX) { return true; }
#endif

#if UNITY_STANDALONE_LINUX
                if (enableTo.StandaloneLinux) { return true; }
#endif

#if UNITY_WEBPLAYER
                if (enableTo.WebPlayer) { return true; }
#endif

#if UNITY_IOS
                if (enableTo.iOS) { return true; }
#endif

#if UNITY_ANDROID
                if (enableTo.Android) { return true; }
#endif

                return false;
            }
        }
        #region MonoBehaviour
        void Reset()
        {
            if (target == null)
            {
                target = this.gameObject;
            }
        }
        void Awake()
        {
            if (trigger == Trigger.Awake && Enabled)
            {
                Destroy(target);
            }
        }
        void Start()
        {
            if (trigger == Trigger.Start && Enabled)
            {
                Destroy(target);
            }
        }
        void Update()
        {
            if (trigger == Trigger.FirstUpdate && Enabled)
            {
                Destroy(target);
            }
        }
        #endregion
    }
}

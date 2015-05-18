/*
 Copyright (c) 2015 Takahiro Kasanami
 
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

namespace Ksnm.UI
{
    /// <summary>
    /// 
    /// </summary>
    public class QuitMessage : MonoBehaviour
    {
        // iPhoneでは、終了機能はリジェクトになる
#if UNITY_IOS
        void Awake()
        {
            Destroy(this.gameObject);
        }
        public void OnExitEnded()
        {
        }
#else
        Animator animator;
        /// <summary>
        /// メンバを初期化
        /// </summary>
        void Awake()
        {
            animator = GetComponent<Animator>();
        }
        /// <summary>
        /// キーの押下を監視
        /// </summary>
        void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                animator.SetTrigger("Escape");
            }
        }
        /// <summary>
        /// アニメーションのイベントから呼ばれる
        /// </summary>
        public void OnExitEnded()
        {
            Application.Quit();
        }
#endif
    }
}
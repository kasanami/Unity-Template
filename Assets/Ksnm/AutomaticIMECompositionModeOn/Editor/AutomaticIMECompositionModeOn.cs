﻿/*
 Copyright (c) 2014-2018 Takahiro Kasanami
 
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
#if Ksnm_Enable_AutomaticIMECompositionModeOn
using UnityEngine;
using UnityEditor;

namespace Ksnm
{
    /// <summary>
    /// 起動時エディタスクリプト実行時にIMEをON
    ///  Unity4.3にすると、Inspector上でIMEが切り替わらなく日本語入力できなかったり、
    ///  IMEの切り替えはできても変換途中で勝手にIMEが戻って確定されてしまうようになったので、その回避処理
    /// </summary>
    [InitializeOnLoad]
    public class AutomaticIMECompositionModeOn
    {
        static AutomaticIMECompositionModeOn()
        {
            Input.imeCompositionMode = IMECompositionMode.On;
        }
    }
}
#endif
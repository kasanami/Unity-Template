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
using UnityEditor;

namespace Ksnm
{
    /// <summary>
    /// スクリプトのコンパイルエラー確認の補助に
    /// コンパイルした時間をログ出力する。
    /// </summary>
    [InitializeOnLoad]
    public class CompiledTimeStamp
    {
        static CompiledTimeStamp()
        {
            var message = new System.Text.StringBuilder();
            message.AppendLine("CompiledTimeStamp " + System.DateTime.Now.ToLongTimeString());
            message.AppendLine("Defines:");
#if UNITY_EDITOR
            message.AppendLine("UNITY_EDITOR");
#endif
#if UNITY_EDITOR_WIN
            message.AppendLine("UNITY_EDITOR_WIN");
#endif
#if UNITY_EDITOR_OSX
            message.AppendLine("UNITY_EDITOR_OSX");
#endif
#if UNITY_STANDALONE_OSX
            message.AppendLine("UNITY_STANDALONE_OSX");
#endif
#if UNITY_STANDALONE_WIN
            message.AppendLine("UNITY_STANDALONE_WIN");
#endif
#if UNITY_STANDALONE_LINUX
            message.AppendLine("UNITY_STANDALONE_LINUX");
#endif
#if UNITY_STANDALONE
            message.AppendLine("UNITY_STANDALONE");
#endif
#if UNITY_WEBPLAYER
            message.AppendLine("UNITY_WEBPLAYER");
#endif
#if UNITY_WII
            message.AppendLine("UNITY_WII");
#endif
#if UNITY_IOS
            message.AppendLine("UNITY_IOS");
#endif
#if UNITY_IPHONE
            message.AppendLine("UNITY_IPHONE");
#endif
#if UNITY_ANDROID
            message.AppendLine("UNITY_ANDROID");
#endif
#if UNITY_PS3
            message.AppendLine("UNITY_PS3");
#endif
#if UNITY_PS4
            message.AppendLine("UNITY_PS4");
#endif
#if UNITY_XBOX360
            message.AppendLine("UNITY_XBOX360");
#endif
#if UNITY_XBOXONE
            message.AppendLine("UNITY_XBOXONE");
#endif
#if UNITY_BLACKBERRY
            message.AppendLine("UNITY_BLACKBERRY");
#endif
#if UNITY_WP8
            message.AppendLine("UNITY_WP8");
#endif
#if UNITY_METRO
            message.AppendLine("UNITY_METRO");
#endif
#if UNITY_WINRT
            message.AppendLine("UNITY_WINRT");
#endif
#if UNITY_WEBGL
            message.AppendLine("UNITY_WEBGL");
#endif
#if Ksnm_Using_UniLinq
            message.AppendLine("Ksnm_Using_UniLinq");
#endif
            Debug.Log(message.ToString());
        }
    }
}
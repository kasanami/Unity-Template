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
using System.Text;

namespace Ksnm.Examples
{
    public class HandheldActivityIndicatorExample : MonoBehaviour
    {
        void Start()
        {
#if UNITY_IPHONE
            HandheldActivityIndicator.Style = iOSActivityIndicatorStyle.WhiteLarge;
#elif UNITY_ANDROID
            HandheldActivityIndicator.Style = AndroidActivityIndicatorStyle.Large;
#endif
        }
        void OnGUI()
        {
            GUI.skin.button.fontSize = 40;
            GUI.skin.button.wordWrap = true;
            GUI.skin.label.fontSize = 40;
            GUI.skin.label.wordWrap = true;
            GUILayout.BeginArea(new Rect(0, 0, Screen.width, Screen.height));
            {
                GUILayout.BeginVertical();
                {
                    GUILayout.Label("通常のUnityEngine.Handheld.***ActivityIndicator()を使用した場合");
                    GUILayout.BeginHorizontal();
                    {
                        if (GUILayout.Button("Aさんが表示"))
                        {
                            Handheld.StartActivityIndicator();
                        }
                        if (GUILayout.Button("Aさんが非表示"))
                        {
                            Handheld.StopActivityIndicator();
                        }
                    }
                    GUILayout.EndHorizontal();
                    GUILayout.BeginHorizontal();
                    {
                        if (GUILayout.Button("Bさんが表示"))
                        {
                            Handheld.StartActivityIndicator();
                        }
                        if (GUILayout.Button("Bさんが非表示"))
                        {
                            Handheld.StopActivityIndicator();
                        }
                    }
                    GUILayout.EndHorizontal();
                    GUILayout.Label("Ksnm.HandheldActivityIndicatorを使用した場合");
                    GUILayout.BeginHorizontal();
                    {
                        if (GUILayout.Button("Aさんが表示"))
                        {
                            HandheldActivityIndicator.Start("A");
                            FlagsLog();
                        }
                        if (GUILayout.Button("Aさんが非表示"))
                        {
                            HandheldActivityIndicator.Stop("A");
                            FlagsLog();
                        }
                    }
                    GUILayout.EndHorizontal();
                    GUILayout.BeginHorizontal();
                    {
                        if (GUILayout.Button("Bさんが表示"))
                        {
                            HandheldActivityIndicator.Start("B");
                            FlagsLog();
                        }
                        if (GUILayout.Button("Bさんが非表示"))
                        {
                            HandheldActivityIndicator.Stop("B");
                            FlagsLog();
                        }
                    }
                    GUILayout.EndHorizontal();
                }
                GUILayout.EndVertical();
            }
            GUILayout.EndArea();
        }
        static void FlagsLog()
        {
            StringBuilder stringBuilder = new StringBuilder();
            foreach (var key in HandheldActivityIndicator.Flags.Keys)
            {
                stringBuilder.AppendLine("Flags[" + key + "]=" + HandheldActivityIndicator.Flags[key]);
            }
            Debug.Log(stringBuilder.ToString());
        }
    }
}
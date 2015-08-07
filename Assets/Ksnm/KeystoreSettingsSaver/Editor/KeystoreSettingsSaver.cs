/*
The zlib License

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

3. This notice may not be removed or altered from any source distribution.
*/
using UnityEngine;
using UnityEditor;
using UnityEditor.Callbacks;
using System.IO;
using System.Collections;
using System.Text;
using System.Linq;

namespace Ksnm
{
    [InitializeOnLoad]
    public class KeystoreSettingsSaver
    {
        public const string DataFileName = "Data.txt";
        public static string DataFilePath
        {
            get
            {
                var thisFileFullPath = new System.Diagnostics.StackTrace(true).GetFrame(0).GetFileName();
                thisFileFullPath = thisFileFullPath.Replace('\\', '/');
                var thisDirectoryName = Path.GetDirectoryName(thisFileFullPath);
                return thisDirectoryName + "/" + DataFileName;
            }
        }
        /// <summary>
        /// テキスト形式で、入出力する。
        /// 各行に対応する各値は固定。
        /// </summary>
        static string Text
        {
            get
            {
                var stringBuilder = new StringBuilder();
                stringBuilder.Append(PlayerSettings.Android.keystorePass + "\n");
                stringBuilder.Append(PlayerSettings.Android.keyaliasName + "\n");
                stringBuilder.Append(PlayerSettings.Android.keyaliasPass + "\n");
                return stringBuilder.ToString();
            }
            set
            {
                var lines = value.Split('\n');
                PlayerSettings.Android.keystorePass = lines.ElementAtOrDefault(0);
                PlayerSettings.Android.keyaliasName = lines.ElementAtOrDefault(1);
                PlayerSettings.Android.keyaliasPass = lines.ElementAtOrDefault(2);
            }
        }
        /// <summary>
        /// 静的コンストラクタ
        /// 
        /// エディタ起動時・コンパイル時に呼び出される。
        /// </summary>
        static KeystoreSettingsSaver()
        {
            // 起動直後（起動してから10秒未満）だけ処理する
            if (EditorApplication.timeSinceStartup < 10)
            {
                if (File.Exists(DataFilePath))
                {
                    Text = File.ReadAllText(DataFilePath);
                }
            }
        }
        [PostProcessBuild]
        static void OnPostProcessBuild(BuildTarget target, string path)
        {
            if (target == BuildTarget.Android)
            {
                Save();
            }
        }
        /// <summary>
        /// 現在の設定保存
        /// </summary>
        static void Save()
        {
            var text = Text;
            if (text.Length > 3)
            {
                File.WriteAllText(DataFilePath, Text, Encoding.UTF8);
            }
        }
    }
}

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
using UnityEditor;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Ksnm
{
    /// <summary>
    /// ファイルやフォルダ、エディタ機能のショートカット集
    /// </summary>
    public class ShortcutMenus
    {
#if UNITY_STANDALONE_WIN
        /// <summary>
        /// プロジェクトのフォルダを開く
        /// </summary>
        [MenuItem("Shortcut/Folders/Project Folder")]
        public static void OpenProjectFolder()
        {
            var path = ProjectFolderPath;
            OpenDirectory(path, "Open Project Folder");
        }
        /// <summary>
        /// Application.dataPath フォルダを開く
        /// </summary>
        [MenuItem("Shortcut/Folders/Data Path")]
        public static void OpenDataPath()
        {
            OpenDirectory(Application.dataPath, "Open Data Path");
        }
        /// <summary>
        /// Asset Store からダウンロードしたアセットの保存先フォルダを開く
        /// </summary>
        [MenuItem("Shortcut/Folders/Asset Store(アセットストアの保存先)")]
        public static void OpenAssetStore()
        {
            // 特殊フォルダのパスを得る。
            // 例：C:\Users\username\AppData\Roaming
            var path = System.Environment.GetFolderPath(System.Environment.SpecialFolder.ApplicationData);
            path += @"\Unity\Asset Store";
            OpenDirectory(path, "Open Asset Store");
        }
        /// <summary>
        /// CGIncludes フォルダを開く
        /// </summary>
        [MenuItem("Shortcut/Folders/CGIncludes")]
        public static void OpenCGIncludes()
        {
            // エディタのパスを得る
            // "C:/Program Files (x86)/Unity/Editor/Unity.exe"
            var path = EditorApplication.applicationPath;
            path = System.IO.Path.GetDirectoryName(path);
            OpenDirectory(path + "/Data/CGIncludes", "Open CGIncludes");
        }
        /// <summary>
        /// ログファイルが有るフォルダを開く
        /// http://docs-jp.unity3d.com/Documentation/Manual/LogFiles.html
        /// </summary>
        [MenuItem("Shortcut/Folders/Editor(ログファイルがあるフォルダ)")]
        public static void OpenEditor()
        {
            // 特殊フォルダのパスを得る。
            // 例：C:\Users\username\AppData\Local
            var path = System.Environment.GetFolderPath(System.Environment.SpecialFolder.LocalApplicationData);
            path += @"\Unity\Editor";
            OpenDirectory(path, "Open Editor");
        }
        /// <summary>
        /// ソリューションファルを開く
        /// </summary>
        [MenuItem("Shortcut/Files/*-csharp.sln")]
        public static void OpenCSharpSolution()
        {
            var path = ProjectFolderPath;
            var paths = System.IO.Directory.GetFiles(path, "*-csharp.sln");
            if (paths.Length > 0)
            {
                System.Diagnostics.Process.Start(paths[0]);
            }
            else
            {
                UnityEditor.EditorUtility.DisplayDialog("Open CSharp Solution", "*-csharp.sln\nが見つかりませんでした。", "OK");
            }
        }
        /// <summary>
        /// ログファイルを開く
        /// http://docs-jp.unity3d.com/Documentation/Manual/LogFiles.html
        /// </summary>
        [MenuItem("Shortcut/Files/Editor.log")]
        public static void OpenEditorLog()
        {
            // 特殊フォルダのパスを得る。
            // 例：C:\Users\username\AppData\Local
            var path = System.Environment.GetFolderPath(System.Environment.SpecialFolder.LocalApplicationData);
            path += @"\Unity\Editor\Editor.log";
            OpenFile(path, "Open Editor.log");
        }
        /// <summary>
        /// ログファイルを開く
        /// http://docs-jp.unity3d.com/Documentation/Manual/LogFiles.html
        /// </summary>
        [MenuItem("Shortcut/Files/Editor-prev.log")]
        public static void OpenEditorPrevLog()
        {
            // 特殊フォルダのパスを得る。
            // 例：C:\Users\username\AppData\Local
            var path = System.Environment.GetFolderPath(System.Environment.SpecialFolder.LocalApplicationData);
            path += @"\Unity\Editor\Editor-prev.log";
            OpenFile(path, "Open Editor-prev.log");
        }
        /// <summary>
        /// エディタ/スタンドアローンでの、PlayerPrefsの保存場所を開く
        /// </summary>
        [MenuItem("Shortcut/PlayerPrefs/Editor and Standalone")]
        public static void OpenEditorPlayerPrefs()
        {
            // レジストリエディターの、前回開いたキーの情報を書き換える。
            try
            {
                // note "HKCU"は有効じゃないので、"HKEY_CURRENT_USER"を使う。（以下の両方）
                var key = @"HKEY_CURRENT_USER\Software\" + PlayerSettings.companyName + @"\" + PlayerSettings.productName;
                Microsoft.Win32.Registry.SetValue(@"HKEY_CURRENT_USER\Software\Microsoft\Windows\CurrentVersion\Applets\Regedit", "LastKey", key, Microsoft.Win32.RegistryValueKind.String);
            }
            catch (System.Exception e)
            {
                Debug.LogException(e);
                return;
            }
            // レジストリエディターを開く
            OpenFile(@"C:\Windows\regedit.exe", "PlayerPrefs (Editor,Standalone)");
        }
        /// <summary>
        /// Web Player での、PlayerPrefsの保存場所を開く
        /// </summary>
        [MenuItem("Shortcut/PlayerPrefs/Web Player")]
        public static void OpenWebPlayerPrefs()
        {
            // 特殊フォルダのパスを得る。
            // 例：C:\Users\username\AppData\Roaming
            var path = System.Environment.GetFolderPath(System.Environment.SpecialFolder.ApplicationData);
            path += @"\Unity\WebPlayerPrefs";
            OpenDirectory(path, "PlayerPrefs (Web Player)");
        }

        /// <summary>
        /// Assetを即時保存
        /// プレハブの更新の保存は、シーンを保存しないとできないので
        /// </summary>
        [MenuItem("Shortcut/API/EditorApplication.SaveAssets()")]
        public static void EditorApplication_SaveAssets()
        {
            EditorApplication.SaveAssets();
        }

        /// <summary>
        /// スクリーンショットを撮影
        /// </summary>
        [MenuItem("Shortcut/API/Application.CaptureScreenshot()")]
        public static void Application_CaptureScreenshot()
        {
            var time = System.DateTime.Now;
            UnityEditor.EditorUtility.DisplayDialog("Application.CaptureScreenshot()", "Gameウインドウを表示してください。", "撮影");
            var fileName = "Screenshot_" + time.ToString("yyyyMMdd_HHmmss") + ".png";
            Application.CaptureScreenshot(fileName);
            Debug.Log("Screenshot save to " + fileName);
        }

        /// <summary>
        /// このファイルのパス
        /// </summary>
        [MenuItem("Shortcut/Editor Sample/ThisFilePath")]
        public static void ThisFilePath()
        {
            var thisFileFullPath = new System.Diagnostics.StackTrace(true).GetFrame(0).GetFileName();
            thisFileFullPath = thisFileFullPath.Replace('\\', '/');
            var thisFilePath = thisFileFullPath.Replace(Application.dataPath, string.Empty);
            thisFilePath = "Assets" + thisFilePath;
            Debug.Log("ThisFilePath=" + thisFilePath);
        }

        /// <summary>
        /// Dalvik Debug Monitor Serverを起動
        /// </summary>
        [MenuItem("Shortcut/Tools/Dalvik Debug Monitor Server")]
        public static void StartDalvikDebugMonitorServer()
        {
            // 環境変数からパスを得る。
            // 例：C:\Users\username\AppData\Local\Android\android-sdk\platform-tools
            var environmentVariable = GetEnvironmentVariable("Path", "Dalvik Debug Monitor Server");
            if (environmentVariable.Count() == 0)
                return;
            var paths = environmentVariable.Where((string pathItem) =>
                {
                    return pathItem.IndexOf(@"\android-sdk\") != -1;
                });
            if (paths.Count() == 0)
            {
                UnityEditor.EditorUtility.DisplayDialog("Dalvik Debug Monitor Server", "android-sdkの環境変数Pathが見つかりませんでした。", "OK");
                return;
            }
            var path = paths.ElementAt(0);
            // batのパスに変更
            // 例：C:\Users\username\AppData\Local\Android\android-sdk\tools\ddms.bat
            path = System.IO.Path.GetDirectoryName(path) + @"\tools\ddms.bat";
            OpenFile(path, "Dalvik Debug Monitor Server");
        }

        #region 内部関数
        static string ProjectFolderPath
        {
            get { return System.IO.Path.GetDirectoryName(Application.dataPath); }
        }
        static string[] GetEnvironmentVariable(string variable, string menuName)
        {
            var environmentVariables = new string[0];
            var environmentVariable = System.Environment.GetEnvironmentVariable(variable, System.EnvironmentVariableTarget.User);
            if (environmentVariable != null)
            {
                environmentVariables = environmentVariable.Split(';');
            }
            if (environmentVariables.Count() == 0)
            {
                UnityEditor.EditorUtility.DisplayDialog(menuName, "環境変数" + variable + "が見つかりませんでした。", "OK");
            }
            return environmentVariables;
        }
        static void OpenFile(string path, string menuName)
        {
            if (System.IO.File.Exists(path))
            {
                System.Diagnostics.Process.Start(path);
            }
            else
            {
                UnityEditor.EditorUtility.DisplayDialog(menuName, path + "\nが見つかりませんでした。", "OK");
            }
        }
        static void OpenDirectory(string path, string menuName)
        {
            if (System.IO.Directory.Exists(path))
            {
                System.Diagnostics.Process.Start(path);
            }
            else
            {
                UnityEditor.EditorUtility.DisplayDialog(menuName, path + "\nが見つかりませんでした。", "OK");
            }
        }
        #endregion 内部関数
#endif// UNITY_STANDALONE_WIN
    }

}
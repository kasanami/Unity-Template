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

namespace Ksnm.Randoms.Examples
{
    public class Xorshift128Example
    {
#if UNITY_EDITOR
        /// <summary>
        /// 0～2のシード値でテスト
        /// </summary>
        [UnityEditor.MenuItem("Ksnm/Examples/Xorshift128Example Test1")]
        public static void Test1()
        {
            var logText = "";
            for (int seed = 0; seed < 3; seed++)
            {
                var random = new Xorshift128(seed);
                logText += "seed=" + seed + "\n";
                for (int i = 0; i < 10; i++)
                {
                    var v = random.NextDouble();
                    if (v < 0 || v >= 1)
                        Debug.LogError("random.NextDouble()=" + v);
                    logText += "random.NextDouble() = " + random.NextDouble() + "\n";
                }
                for (int i = 0; i < 10; i++)
                {
                    var v = random.Next();
                    if (v < 0)
                        Debug.LogError("random.Next()=" + v);
                    logText += "random.Next() = " + v + "\n";
                }
                for (int i = 0; i < 10; i++)
                {
                    var v = random.Next(3);
                    if (v < 0 || v >= 3)
                        Debug.LogError("random.Next(3)=" + v);
                    logText += "random.Next(3) = " + v + "\n";
                }
                for (int i = 0; i < 10; i++)
                {
                    var v = random.Next(1, 3);
                    if (v < 1 || v >= 3)
                        Debug.LogError("random.Next(1, 3)=" + v);
                    logText += "random.Next(1, 3) = " + v + "\n";
                }
                logText += "random.NextBytes() = {\n";
                var bytes = new byte[16];
                random.NextBytes(bytes);
                foreach (var item in bytes)
                {
                    logText += item + "\n";
                }
                logText += "}\n";
            }
            Debug.Log(logText);
        }
        /// <summary>
        /// シードに負の値を指定しても絶対値が同じであれば、同じ結果になるかテスト。
        /// </summary>
        [UnityEditor.MenuItem("Ksnm/Examples/Xorshift128Example Test2")]
        public static void Test2()
        {
            var logText = "Xorshift128\n";
            for (int seed = 1; seed <= 5; seed++)
            {
                var random = new Xorshift128(seed);
                logText += "seed=" + seed + "\t";
                var x = random.Next();
                logText += "random.Next() = " + x + "\n";
                random = new Xorshift128(-seed);
                logText += "seed=" + -seed + "\t";
                var y = random.Next();
                logText += "random.Next() = " + y + "\n";
                if (x != y)
                    Debug.LogError("x != y");
            }
            // System.Randomも見てみる
            logText += "System.Random\n";
            for (int seed = 1; seed <= 5; seed++)
            {
                var random = new System.Random(seed);
                logText += "seed=" + seed + "\t";
                var x = random.Next();
                logText += "random.Next() = " + x + "\n";
                random = new System.Random(-seed);
                logText += "seed=" + -seed + "\t";
                var y = random.Next();
                logText += "random.Next() = " + y + "\n";
                if (x != y)
                    Debug.LogError("x != y");
            }
            Debug.Log(logText);
        }
        /// <summary>
        /// 短い時間に連続でコンストラクタを呼ぶと、同じ値が生成されるかテスト。
        /// </summary>
        [UnityEditor.MenuItem("Ksnm/Examples/Xorshift128Example Test3")]
        public static void Test3()
        {
            // 短い時間に連続でコンストラクタを呼ぶと、同じ値が生成される
            var logText = "Xorshift128\n";
            for (int seed = 0; seed < 3; seed++)
            {
                var random = new Xorshift128();
                logText += "random.Next() = " + random.Next() + "\n";
            }
            // System.Randomも見てみる
            logText += "System.Random\n";
            for (int seed = 0; seed < 3; seed++)
            {
                var random = new System.Random();
                logText += "random.Next() = " + random.Next() + "\n";
            }
            Debug.Log(logText);
        }
#endif// UNITY_EDITOR
    }
}
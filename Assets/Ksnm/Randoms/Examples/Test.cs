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
#if UNITY_EDITOR
using UnityEngine;
#endif
using System.Collections;
using System.Collections.Generic;

namespace Ksnm.Randoms.Examples
{
    /// <summary>
    /// Randoms内のクラスをテスト
    /// </summary>
    public class Test
    {
        /// <summary>
        /// 各種乱数ジェネレータクラスをテスト
        /// </summary>
#if UNITY_EDITOR
        [UnityEditor.MenuItem("Ksnm/Examples/Randoms/Test All", false, 10)]
#endif
        public static void TestAll()
        {
            TestSystemRandom();
            TestUnityRandom();
            TestXorshift128();
            //TestXorshift32();
            //TestXorshift8();
        }
        /// <summary>
        /// System.Randomをテスト
        /// </summary>
#if UNITY_EDITOR
        [UnityEditor.MenuItem("Ksnm/Examples/Randoms/Test SystemRandom", false, 10)]
#endif
        public static void TestSystemRandom()
        {
            _Test("Test System.Random", seed => new System.Random(seed));
        }
        /// <summary>
        /// UnityRandomをテスト
        /// </summary>
#if UNITY_EDITOR
        [UnityEditor.MenuItem("Ksnm/Examples/Randoms/Test UnityRandom", false, 10)]
#endif
        public static void TestUnityRandom()
        {
            _Test("Test UnityRandom", seed => new UnityRandom(seed));
        }
        /// <summary>
        /// Xorshift128をテスト
        /// </summary>
#if UNITY_EDITOR
        [UnityEditor.MenuItem("Ksnm/Examples/Randoms/Test Xorshift128", false, 10)]
#endif
        public static void TestXorshift128()
        {
            _Test("Test Xorshift128", seed => new Xorshift128(seed));
        }
        /// <summary>
        /// シードに負の値を指定しても絶対値が同じであれば、同じ結果になるかテスト。
        /// </summary>
#if UNITY_EDITOR
        [UnityEditor.MenuItem("Ksnm/Examples/Randoms/Test2")]
#endif
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
                    Utility.DebugLogError("x != y");
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
                    Utility.DebugLogError("x != y");
            }
            Utility.DebugLog(logText);
        }
        /// <summary>
        /// 短い時間に連続でコンストラクタを呼ぶと、同じ値が生成されるかテスト。
        /// </summary>
#if UNITY_EDITOR
        [UnityEditor.MenuItem("Ksnm/Examples/Randoms/Test3")]
#endif
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
            Utility.DebugLog(logText);
        }

        #region 共通処理
        /// <summary>
        /// 指定の乱数ジェネレータをテスト
        /// </summary>
        /// <param name="randomFactory">(シード値)=>乱数ジェネレーター</param>
        /// <returns></returns>
        static void _Test(string name, System.Func<int, System.Random> randomFactory)
        {
            var logText = new System.Text.StringBuilder();
            logText.AppendLine(name);
            for (int seed = 0; seed <= 3; seed++)
            {
                logText.AppendLine("seed=" + seed);
                var random = randomFactory(seed);
                var log = _Test(random);
                logText.AppendLine(log);
            }
            logText.AppendLine("シード値が同じであれば結果も同じかテスト");
            for (int seed = 0; seed <= 3; seed++)
            {
                logText.AppendLine("seed=" + seed);
                var randomA = randomFactory(seed);
                var randomB = randomFactory(seed);
                for (int i = 0; i < 10; i++)
                {
                    var vA = randomA.Next();
                    var vB = randomB.Next();
                    logText.AppendLine("randomA.Next() = " + vA);
                    logText.AppendLine("randomB.Next() = " + vB);
                    if (vA != vB)
                        Utility.DebugLogError("シード値が同じなのに、違う結果になった。");
                }
            }
            Utility.DebugLog(logText.ToString());
        }
        /// <summary>
        /// 指定の乱数ジェネレータをテスト
        /// </summary>
        /// <param name="random"></param>
        /// <returns></returns>
        static string _Test(System.Random random)
        {
            var logText = new System.Text.StringBuilder();
            for (int i = 0; i < 10; i++)
            {
                var v = random.NextDouble();
                if (v < 0 || v >= 1)
                    Utility.DebugLogError("random.NextDouble()=" + v);
                logText.AppendLine("random.NextDouble() = " + v);
            }
            for (int i = 0; i < 10; i++)
            {
                var v = random.Next();
                if (v < 0)
                    Utility.DebugLogError("random.Next()=" + v);
                logText.AppendLine("random.Next() = " + v);
            }
            for (int i = 0; i < 10; i++)
            {
                var v = random.Next(3);
                if (v < 0 || v >= 3)
                    Utility.DebugLogError("random.Next(3)=" + v);
                logText.AppendLine("random.Next(3) = " + v);
            }
            for (int i = 0; i < 10; i++)
            {
                var v = random.Next(1, 3);
                if (v < 1 || v >= 3)
                    Utility.DebugLogError("random.Next(1, 3)=" + v);
                logText.AppendLine("random.Next(1, 3) = " + v);
            }
            logText.AppendLine("random.NextBytes() = {");
            var bytes = new byte[16];
            random.NextBytes(bytes);
            foreach (var item in bytes)
            {
                logText.AppendLine(item.ToString());
            }
            logText.AppendLine("}");
            return logText.ToString();
        }
        #endregion
    }
}
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
using Original = System;

namespace Ksnm.ExtensionMethods.System
{
    public static class Random_
    {

        /// <summary>
        /// false と true の乱数を返します。
        /// </summary>
        public static bool NextBool(this Original.Random random)
        {
            return random.Next(2) == 1;
        }

        #region UnityEngine.Randomを再現

        /// <summary>
        /// 範囲を制限した乱数を生成
        /// <para>maxの値は含まれない</para>
        /// </summary>
        public static int Range(this Original.Random random, int min, int max)
        {
            return min + random.Next(max - min);
        }

        /// <summary>
        /// 範囲を制限した乱数を生成
        /// <para>maxの値は含まれる</para>
        /// </summary>
        public static float Range(this Original.Random random, float min, float max)
        {
            return (float)random.Range((double)min, (double)max);
        }

        /// <summary>
        /// 範囲を制限した乱数を生成
        /// <para>maxの値は含まれる</para>
        /// </summary>
        public static double Range(this Original.Random random, double min, double max)
        {
            // 0.0～1.0の値
            var t = random.Next() / ((double)int.MaxValue - 1);
            return min + t * (max - min);
        }

        #endregion UnityEngine.Randomを再現
    }
}

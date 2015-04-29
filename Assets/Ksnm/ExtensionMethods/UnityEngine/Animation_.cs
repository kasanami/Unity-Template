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
using System.Collections.Generic;
using Original = UnityEngine;

namespace Ksnm.ExtensionMethods.UnityEngine
{
    public static class Animation_
    {
        /// <summary>
        /// Animationの中に含まれるAnimationの名前の一覧を取得する。
        /// </summary>
        /// <param name="animation"></param>
        /// <returns></returns>
        public static List<string> GetAnimationNameList(this Original.Animation animation)
        {
            var list = new List<string>();
            foreach (Original.AnimationState state in animation)
            {
                list.Add(state.name);
            }
            return list;
        }
    }
}
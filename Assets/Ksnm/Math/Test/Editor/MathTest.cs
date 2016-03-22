/*
 Copyright (c) 2016 Takahiro Kasanami
 
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
#if Ksnm_EnableUnitTest
using UnityEngine;
using System.Collections.Generic;
using NUnit.Framework;

namespace Ksnm
{
    [TestFixture]
    public class MathTest
    {
        [Test]
        [TestCase(1, 2, -1.0f, 0.0f)]
        [TestCase(1, 2, 0.0f, 1.0f)]
        [TestCase(1, 2, 0.5f, 1.5f)]
        [TestCase(1, 2, 1.0f, 2.0f)]
        [TestCase(1, 2, 2.0f, 3.0f)]
        public void Lerp(float from, float to, float t, float value)
        {
            var value2 = Math.Lerp(from, to, t);
            Assert.AreEqual(value, value2);
        }
        [Test]
        [TestCase(1, 2, -1.0f, 0.0f)]
        [TestCase(1, 2, 0.0f, 1.0f)]
        [TestCase(1, 2, 0.5f, 1.5f)]
        [TestCase(1, 2, 1.0f, 2.0f)]
        [TestCase(1, 2, 2.0f, 3.0f)]
        public void InverseLerp(float from, float to, float t, float value)
        {
            var t2 = Math.InverseLerp(from, to, value);
            Assert.AreEqual(t, t2);
        }
    }
}
#endif

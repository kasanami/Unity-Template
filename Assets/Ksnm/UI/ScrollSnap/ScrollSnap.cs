/*
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
 
    3. This notice may not be removed or altered from any source
    distribution.
*/
using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Ksnm.ExtensionMethods.UnityEngine;

namespace Ksnm.UI
{
    /// <summary>
    /// ScrollRectでスナップを行う
    /// TODO:作りかけ
    /// </summary>
    [RequireComponent(typeof(ScrollRect))]
    public class ScrollSnap : UIBehaviour, IBeginDragHandler, IEndDragHandler
    {
        /// <summary>
        /// 操作対象
        /// </summary>
        public ScrollRect scrollRect;
        /// <summary>
        /// スナップする間隔数
        /// </summary>
        public Vector2 stepCount = Vector2.one * 5;
        /// <summary>
        /// 速度
        /// この速度以下になったら、移動処理を開始します。
        /// </summary>
        public float speed = 1;

        #region UIBehaviour
        /// <summary>
        /// パラメータが変更された
        /// </summary>
        protected override void OnValidate()
        {
            // 値を制限
            // TODO:RangeAttributeのVector2版があれば使いたいけど、まだ見つけてない。
            stepCount.x = Mathf.Clamp(stepCount.x, 0, float.MaxValue);
            stepCount.y = Mathf.Clamp(stepCount.y, 0, float.MaxValue);
        }
        protected override void Reset()
        {
            base.Reset();

            if (scrollRect == null)
            {
                scrollRect = GetComponent<ScrollRect>();
            }
        }
        #endregion UIBehaviour

        #region IBeginDragHandler
        public void OnBeginDrag(PointerEventData eventData)
        {
            //StopCoroutine(SnapRect());
        }
        #endregion IBeginDragHandler

        #region IEndDragHandler
        public void OnEndDrag(PointerEventData eventData)
        {
            if (stepCount.x > 1 && stepCount.y > 1)
            {
                StartCoroutine(FrameProcess(eventData));
            }
            else if (stepCount.x > 1)
            {
                StartCoroutine(FrameProcess(eventData, 0));
            }
            else if (stepCount.y > 1)
            {
                StartCoroutine(FrameProcess(eventData, 1));
            }
        }
        #endregion IEndDragHandler

        /// <summary>
        /// 
        /// </summary>
        /// <param name="eventData"></param>
        /// <param name="axisIdx">Vector2の軸を指定</param>
        /// <returns></returns>
        private IEnumerator FrameProcess(PointerEventData eventData, int axisIdx)
        {
            var canvasScaler = scrollRect.GetComponentInParent<CanvasScaler>();
            // 移動方向（時間が経つと値が変わるので今取得）
            var delta = (eventData.delta.normalized)[axisIdx];
            float target = float.NaN;
            var stepCount = this.stepCount[axisIdx] - 1;
            DebugLog("stepCount=" + stepCount);
            var stepInterval = 1.0f / stepCount;
            var rectTransform = scrollRect.content;
            var speed = this.speed;
            var speed2 = speed * canvasScaler.referencePixelsPerUnit;
            speed2 *= speed2;
            while (true)
            {
                var currentPosition = scrollRect.normalizedPosition;
                // targetが未設定なら、現在の速度を監視
                if (float.IsNaN(target))
                {
                    var velocity = scrollRect.velocity[axisIdx];
                    velocity /= stepCount;
                    var currentSpeed = Mathf.Abs(velocity);
                    if (currentSpeed <= speed2)
                    {
                        // 移動方向の、次の区切りを目的地とする。
                        target = currentPosition[axisIdx];
                        if (target % stepInterval == 0)
                        {
                            // 既に目的地なら終了
                            break;
                        }
                        target = NextTarget(target, delta, stepCount);
                        DebugLog("snap start " + axisIdx + " delta=" + delta + " target=" + target);
                    }
                }
                else
                {
                    currentPosition[axisIdx] = Move(currentPosition[axisIdx], target, stepInterval * (Time.deltaTime * speed));
                    scrollRect.normalizedPosition = currentPosition;
                    if (currentPosition[axisIdx] == target)
                    {
                        break;
                    }
                }
                yield return new WaitForEndOfFrame();
            }
            DebugLog("snap end");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="eventData"></param>
        /// <param name="axisIdx">Vector2の軸を指定</param>
        /// <returns></returns>
        private IEnumerator FrameProcess(PointerEventData eventData)
        {
            var canvasScaler = scrollRect.GetComponentInParent<CanvasScaler>();
            // 移動方向（時間が経つと値が変わるので今取得）
            var delta = eventData.delta.normalized;
            Vector2 target = new Vector2(float.NaN, float.NaN);
            var stepCount = this.stepCount;
            stepCount.x = Mathf.Max(1, stepCount.x - 1);
            stepCount.y = Mathf.Max(1, stepCount.y - 1);
            var stepInterval = Vector2.one;
            stepInterval.x /= stepCount.x;
            stepInterval.y /= stepCount.y;
            var rectTransform = scrollRect.content;
            var speed = this.speed;
            var speed2 = speed * canvasScaler.referencePixelsPerUnit;
            speed2 *= speed2;
            while (true)
            {
                var currentPosition = scrollRect.normalizedPosition;
                // targetが未設定なら、現在の速度を監視
                if (float.IsNaN(target.x))
                {
                    var velocity = scrollRect.velocity;
                    velocity.x /= stepCount.x;
                    velocity.y /= stepCount.y;
                    var currentSpeed = velocity.sqrMagnitude;
                    if (currentSpeed <= speed2)
                    {
                        // 移動方向の、次の区切りを目的地とする。
                        target = Vector2.zero;
                        target.x = NextTarget(currentPosition.x, delta.x, stepCount.x);
                        target.y = NextTarget(currentPosition.y, delta.y, stepCount.y);
                        DebugLog("snap start delta=" + delta + " target=" + target);
                    }
                }
                else
                {
                    //currentPosition[axisIdx] = Mathf.SmoothDamp(currentPosition[axisIdx], target, ref velocity, 1.0f);
                    //currentPosition.Move(target, stepInterval * speed * 0.1f);
                    currentPosition = Move(currentPosition, target, stepInterval * (Time.deltaTime * speed));
                    //currentPosition.x = Math.Move(currentPosition.x, target.x, stepInterval.x * Time.deltaTime);
                    //currentPosition.y = Math.Move(currentPosition.y, target.y, stepInterval.y * Time.deltaTime);
                    DebugLog("snap =" + currentPosition + " deltaTime=" + Time.deltaTime);
                    scrollRect.normalizedPosition = currentPosition;
                    if (currentPosition == target)
                    {
                        break;
                    }
                }
                yield return new WaitForEndOfFrame();
            }
            DebugLog("snap end");
        }

        float NextTarget(float current, float delta, float stepCount)
        {
            current *= stepCount;
            if (delta > 0.6f)
            {
                current = Mathf.FloorToInt(current);
            }
            else if (delta < -0.6f)
            {
                current = Mathf.CeilToInt(current);
            }
            else
            {
                current = Mathf.RoundToInt(current);
            }
            current /= stepCount;
            current = Mathf.Clamp01(current);
            return current;
        }

        /// <summary>
        /// 目的の値まで移動します。
        /// </summary>
        /// <param name="current"></param>
        /// <param name="target"></param>
        /// <param name="delta"></param>
        /// <returns></returns>
        public static float Move(float current, float target, float speed)
        {
            if (current > target)
            {
                current = System.Math.Max(current - speed, target);
            }
            else if (current < target)
            {
                current = System.Math.Min(current + speed, target);
            }
            return current;
        }

        /// <summary>
        /// 目的の値まで移動します。
        /// </summary>
        /// <param name="current"></param>
        /// <param name="target"></param>
        /// <param name="delta"></param>
        /// <returns></returns>
        public static UnityEngine.Vector2 Move(UnityEngine.Vector2 current, UnityEngine.Vector2 target, float speed)
        {
            UnityEngine.Vector2 velocity = (target - current).normalized * speed;
            velocity.x = Mathf.Abs(velocity.x);
            velocity.y = Mathf.Abs(velocity.y);
            return Move(current, target, velocity);
        }

        public static UnityEngine.Vector2 Move(UnityEngine.Vector2 current, UnityEngine.Vector2 target, UnityEngine.Vector2 speed)
        {
            current.x = Move(current.x, target.x, speed.x);
            current.y = Move(current.y, target.y, speed.y);
            return current;
        }

        void DebugLog(string message)
        {
            Debug.Log(message);
        }
    }
}
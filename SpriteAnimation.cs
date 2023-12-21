using System.Collections.Generic;
using UnityEngine;

// The MIT License (MIT)

// Copyright (c) 2023 Kim Pedersen

// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:

// The above copyright notice and this permission notice shall be included in all
// copies or substantial portions of the Software.

// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
// SOFTWARE.

namespace SpriteAnim {

    [CreateAssetMenu(fileName = "New Sprite Animation", menuName = "Sprite Animation", order = 1)]
    public class SpriteAnimation : ScriptableObject {

        [Tooltip("Set to the title of the animation")]
        public string title;

        [Tooltip("Frames in the animation")]
        public List<SpriteAnimationFrame> frames = new();

        [Tooltip("Is the animation looping?")]
        public bool looping;

        [Tooltip("Should the first frame of the animation be shown when the animation ends?")]
        public bool showFirstFrameAtEnd;



        /// <summary>
        /// Total duration of the animation
        /// </summary>
        public float TotalDuration {
            get {
                // Calculate the duration of the animation - if the animation is looping,
                // the duration is set to infinity
                float totalDuration = (looping) ? float.PositiveInfinity : 0f;

                if (!looping) {
                    foreach (SpriteAnimationFrame frame in frames) {
                        totalDuration += frame.duration;
                    }
                }

                return totalDuration;
            }
        }

    }


    [System.Serializable]
    public struct SpriteAnimationFrame {

        /// <summary>
        /// Sprite used for this frame
        /// </summary>
        public Sprite sprite;

        /// <summary>
        /// Duration of the frame in seconds
        /// </summary>
        public float duration;

    }

}
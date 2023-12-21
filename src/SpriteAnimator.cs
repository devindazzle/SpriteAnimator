using System;
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

    [RequireComponent(typeof(SpriteRenderer))]
    public class SpriteAnimator : MonoBehaviour {

        [Header("Animator")]

        [Tooltip("Should the default animation be played on Awake?")]
        [SerializeField] private bool playOnAwake;

        [Tooltip("Set to the sprite animation that will be played on Awake")]
        [field: SerializeField] public SpriteAnimation DefaultAnimation { get; private set; }



        /// <summary>
        /// The scale at which time passes
        /// </summary>
        public float TimeScale {
            get => m_TimeScale;
            set => m_TimeScale = value;
        }


        /// <summary>
        /// Is the animator playing an animation?
        /// </summary>
        public bool Playing => m_Playing;


        /// <summary>
        /// The Sprite Animation that is currently being played
        /// </summary>
        public SpriteAnimation CurrentAnimation => m_CurrentAnimation;


        /// <summary>
        /// SpriteRenderer component used for rendering the sprite animation
        /// </summary>
        public SpriteRenderer Renderer => m_Renderer;



        #region Privates

        private SpriteRenderer m_Renderer;

        private SpriteAnimation m_CurrentAnimation;

        private Action m_OnComplete;

        private bool m_Playing;

        private int m_FrameIndex;

        private float m_Timer;

        private float m_TimeScale = 1;

        #endregion

        #region MonoBehaviour

        private void Awake() {
            // Store a reference to frequently accessed components on same game object
            m_Renderer = GetComponent<SpriteRenderer>();

            // Play the default animation
            if (playOnAwake)
                Play(DefaultAnimation);
        }


        private void Update() {
            // Exit if no animation is playing
            if (!m_Playing) return;
            if (m_CurrentAnimation == null) return;

            // Advance the frame timer
            m_Timer += Time.deltaTime * m_TimeScale;

            // Should the next frame be shown?
            if (m_Timer >= m_CurrentAnimation.frames[m_FrameIndex].duration) {
                // Is this the last frame?
                if (m_FrameIndex + 1 >= m_CurrentAnimation.frames.Count) {
                    // Determine what frame to show at the last frame
                    if (m_CurrentAnimation.looping) {
                        // Animation is looping, so just show the first frame
                        ShowFrameAtIndex(0);
                    }
                    else if (m_CurrentAnimation.showFirstFrameAtEnd) {
                        // Animation should show the first frame at the end
                        ShowFrameAtIndex(0);
                        Stop();

                        // Perform OnComplete action
                        m_OnComplete?.Invoke();
                    }
                    else {
                        // Animation should just keep showing the last frame
                        Stop();

                        // Perform OnComplete action
                        m_OnComplete?.Invoke();
                    }
                }
                else {
                    // Not the last frame
                    ShowFrameAtIndex(m_FrameIndex + 1);
                }

            }
        }

        #endregion

        #region Methods

        /// <summary>
        /// Play a sprite animation
        /// </summary>
        public void Play(SpriteAnimation animation, int startFrameIndex = 0, bool force = false, Action onComplete = null) {
            // Exit if the animation is null
            if (animation == null) return;

            // Exit if the animation is the same as the current animation and not forced
            if (animation.Equals(m_CurrentAnimation) && !force) return;

            // Stop the previous animation - if any
            if (m_Playing && m_OnComplete != null) {
                m_OnComplete.Invoke();
            }

            // Reset animation state
            m_CurrentAnimation = animation;
            m_OnComplete = onComplete;

            // Show the first frame
            ShowFrameAtIndex(startFrameIndex);

            // Start playing the animation
            m_Playing = true;
        }


        /// <summary>
        /// Stop playing an animation
        /// </summary>
        public void Stop() {
            m_Playing = false;
            m_FrameIndex = 0;
            m_Timer = 0;
        }


        /// <summary>
        /// Displays a frame in the animation
        /// </summary>
        private void ShowFrameAtIndex(int frameIndex) {
            // Update the frame
            m_FrameIndex = frameIndex;
            m_Timer = 0f;
            m_Renderer.sprite = m_CurrentAnimation.frames[m_FrameIndex].sprite;
        }

        #endregion

    }


}
﻿using UnityEngine;
using UnityEngine.UI;

namespace MoreMountains.Feedbacks
{
	/// <summary>
	///     This feedback lets you control the contents of a target Text over time
	/// </summary>
	[AddComponentMenu("")]
    [FeedbackHelp("This feedback lets you control the contents of a target Text over time.")]
    [FeedbackPath("UI/Text")]
    public class MMF_Text : MMF_Feedback
    {
        public enum ColorModes { Instant, Gradient, Interpolate }
        /// a static bool used to disable all feedbacks of this type at once
        public static bool FeedbackTypeAuthorized = true;

        protected string _initialText;
        /// the new text to replace the old one with
        [Tooltip("the new text to replace the old one with")]
        [TextArea]
        public string NewText = "Hello World";

        [MMFInspectorGroup("Text", true, 76, true)]

        /// the Text component to control
        [Tooltip(" Text component to control")]
        public Text TargetText;
        public override bool HasAutomatedTargetAcquisition => true;

        protected override void AutomateTargetAcquisition()
        {
            TargetText = FindAutomatedTarget<Text>();
        }

        /// <summary>
        ///     On play we change the text of our target TMPText
        /// </summary>
        /// <param name="position"></param>
        /// <param name="feedbacksIntensity"></param>
        protected override void CustomPlayFeedback(Vector3 position, float feedbacksIntensity = 1.0f)
        {
            if (!Active || !FeedbackTypeAuthorized)
            {
                return;
            }

            if (TargetText == null)
            {
                return;
            }

            _initialText = TargetText.text;
            TargetText.text = NewText;
        }

        /// <summary>
        ///     On restore, we put our object back at its initial position
        /// </summary>
        protected override void CustomRestoreInitialValues()
        {
            if (!Active || !FeedbackTypeAuthorized)
            {
                return;
            }

            TargetText.text = _initialText;
        }

        /// sets the inspector color for this feedback
#if UNITY_EDITOR
        public override Color FeedbackColor
        {
            get
            {
                return MMFeedbacksInspectorColors.UIColor;
            }
        }

        public override bool EvaluateRequiresSetup()
        {
            return TargetText == null;
        }

        public override string RequiredTargetText => TargetText != null ? TargetText.name : "";
        public override string RequiresSetupText =>
            "This feedback requires that a TargetText be set to be able to work properly. You can set one below.";
#endif
    }
}
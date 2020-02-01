using System;
using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using System.Reflection;

// This will add a tab to the Stock Settings in the Difficulty settings called "SimpleLogistics"
// To use, reference the setting using the following:
//
//  HighLogic.CurrentGame.Parameters.CustomParams<SimpleLogistics_Options>().requireLanded
//
// As it is set up, the option is disabled, so in order to enable it, the player would have
// to deliberately go in and change it
//
namespace SimpleLogistics
{
    // http://forum.kerbalspaceprogram.com/index.php?/topic/147576-modders-notes-for-ksp-12/#comment-2754813
    // search for "Mod integration into Stock Settings

    public class SimpleLogistics_Options : GameParameters.CustomParameterNode
    {
        public override string Title { get { return "Default Settings"; } }
        public override GameParameters.GameMode GameMode { get { return GameParameters.GameMode.ANY; } }
        public override string Section { get { return "SimpleLogistics"; } }
        public override string DisplaySection { get { return "SimpleLogistics"; } }
        public override int SectionOrder { get { return 1; } }

        /// <summary>
        /// The needs EC to start in GameParameters
        /// </summary>
        [GameParameters.CustomParameterUI("Required Landed",
            toolTip = "if set to yes, vessel must be landed in order to access the logistics network.",
            newGameOnly = false,
            unlockedDuringMission = true
            )]
        public bool requireLanded = true;

        /// <summary>
        /// The automatic switch in GameParameters
        /// </summary>
        [GameParameters.CustomParameterUI("Allow Splashed",
            toolTip = "if yes, allow splashed vessels to access the logistics network.",
            newGameOnly = false,
            unlockedDuringMission = true)]
        public bool allowSplashed = true;

        /// <summary>
        /// The automatic switch in GameParameters
        /// </summary>
        [GameParameters.CustomParameterUI("Allow Pre-Launch",
            toolTip = "if yes, allow pre-launch vessels to access the logistics network.",
            newGameOnly = false,
            unlockedDuringMission = true)]
        public bool allowPreLaunch = true;

        /// <summary>
        /// The colored paw
        /// </summary>
        [GameParameters.CustomParameterUI("PAW Color",
            toolTip = "allow color coding in ODFC PAW (part action window) / part RMB (right menu button).",
            newGameOnly = false,
            unlockedDuringMission = true)]
        public bool coloredPAW = true;

        /// <summary>
        /// Sets the globalScalingFactor in GameParameters
        /// </summary>
        [GameParameters.CustomFloatParameterUI("Global Range",
            toolTip = "Scales production and consumption Globally on all ODFC modules.",
            newGameOnly = false,
            unlockedDuringMission = true,
            minValue = 0.05f,
            maxValue = 5.0f,
            stepCount = 101,
            displayFormat = "F2",
            asPercentage = false)]
        public float globalLogisticsRange = 1.0f;

        // If you want to have some of the game settings default to enabled,  change 
        // the "if false" to "if true" and set the values as you like


#if true        
        /// <summary>
        /// Gets a value indicating whether this instance has presets.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance has presets; otherwise, <c>false</c>.
        /// </value>
        public override bool HasPresets { get { return true; } }
        /// <summary>
        /// Sets the difficulty preset.
        /// </summary>
        /// <param name="preset">The preset.</param>
        public override void SetDifficultyPreset(GameParameters.Preset preset)
        {
            Debug.Log("Setting difficulty preset");
            switch (preset)
            {
                case GameParameters.Preset.Easy:
                    requireLanded = false;
                    allowSplashed = true;
                    allowPreLaunch = true;
                    globalLogisticsRange = 1.5f;
                    break;

                case GameParameters.Preset.Normal:
                    requireLanded = true;
                    allowSplashed = true;
                    allowPreLaunch = true;
                    globalLogisticsRange = 1.0f;
                    break;

                case GameParameters.Preset.Moderate:
                    requireLanded = true;
                    allowSplashed = false;
                    allowPreLaunch = true;
                    globalLogisticsRange = 0.75f;
                    break;

                case GameParameters.Preset.Hard:
                    requireLanded = true;
                    allowSplashed = false;
                    allowPreLaunch = false;
                    globalLogisticsRange = 0.5f;
                    break;
            }
        }

#else
        public override bool HasPresets { get { return false; } }
        public override void SetDifficultyPreset(GameParameters.Preset preset) { }
#endif

        public override bool Enabled(MemberInfo member, GameParameters parameters) { return true; }
        public override bool Interactible(MemberInfo member, GameParameters parameters) { return true; }
        public override IList ValidValues(MemberInfo member) { return null; }
    }
}

   
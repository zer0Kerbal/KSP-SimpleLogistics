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
        public override string Title { get { return "[WIP] Default Settings"; } }
        public override GameParameters.GameMode GameMode { get { return GameParameters.GameMode.ANY; } }
        public override string Section { get { return "[WIP] SimpleLogistics"; } }
        public override string DisplaySection { get { return "[WIP] SimpleLogistics"; } }
        public override int SectionOrder { get { return 1; } }


        [GameParameters.CustomParameterUI("SimpleLogistics! enabled?",
            toolTip = "enables and disables entire network (ON/off).",
            newGameOnly = false,
            unlockedDuringMission = true)]
        internal bool enabled = true;


        [GameParameters.CustomParameterUI("Landed vessels may connect to SimpleLogistics network",
            toolTip = "if yes, landed vessels connect to and use the SimpleLogistics Network?",
            newGameOnly = false,
            unlockedDuringMission = true)]
         internal bool yesLanded = true;


        [GameParameters.CustomParameterUI("Splashed vessels may connect to SimpleLogistics network",
            toolTip = "if yes, allow splashed vessels to access the SimpleLogistics network.",
            newGameOnly = false,
            unlockedDuringMission = true)]
        internal bool yesSplashed = true;


        [GameParameters.CustomParameterUI("Pre-Launch vessels may connect to SimpleLogistics network",
            toolTip = "if yes, allow pre-launch vessels to access the SimpleLogistics network.",
            newGameOnly = false,
            unlockedDuringMission = true)]
        internal bool yesPreLaunch = true;


        [GameParameters.CustomParameterUI("Orbiting vessels may connect to SimpleLogistics network",
            toolTip = "if yes, allow Orbiting  vessels to access the SimpleLogistics network.",
            newGameOnly = false,
            unlockedDuringMission = true)]
        internal bool yesOrbiting = false;


        [GameParameters.CustomParameterUI("Flying vessels may connect to SimpleLogistics network",
            toolTip = "if yes, allow Flying  vessels to access the SimpleLogistics network.",
            newGameOnly = false,
            unlockedDuringMission = true)]
        internal bool yesFlying = false;


        [GameParameters.CustomParameterUI("SubOrbital vessels may connect to SimpleLogistics network",
            toolTip = "if yes, allow SubOrbital  vessels to access the SimpleLogistics network.",
            newGameOnly = false,
            unlockedDuringMission = true)]
        internal bool yesSubOrbital = false;


        [GameParameters.CustomParameterUI("Escaping vessels may connect to SimpleLogistics network",
            toolTip = "if yes, allow Escaping  vessels to access the SimpleLogistics network.",
            newGameOnly = false,
            unlockedDuringMission = true)]
        internal bool yesEscaping = false;


        [GameParameters.CustomParameterUI("Docked vessels may connect to SimpleLogistics network",
            toolTip = "if yes, allow Docked vessels to access the SimpleLogistics network.",
            newGameOnly = false,
            unlockedDuringMission = true)]
        internal bool yesDocked = false;


        [GameParameters.CustomParameterUI("Control: Full",
            toolTip = "allow logisics network access from vessels with full control.",
            newGameOnly = false,
            unlockedDuringMission = true)]
        internal bool yesFull = true;

        [GameParameters.CustomParameterUI("Control: Partial Manned",
            toolTip = "allow logisics network access from vessels with Partial Manned control.",
            newGameOnly = false,
            unlockedDuringMission = true)]
        internal bool yesPartialManned = true;

        [GameParameters.CustomParameterUI("Control: Partial Unmanned",
            toolTip = "allow logisics network access from vessels with Partial Unmanned control.",
            newGameOnly = false,
            unlockedDuringMission = true)]
        internal bool yesPartialUnmaned = true;
               

        [GameParameters.CustomParameterUI("Control: None (debris)",
            toolTip = "allow logisics network access from vessels with no control.",
            newGameOnly = false,
            unlockedDuringMission = true)]
        internal bool yesNone = false;
        
        /// <summary>
        /// The colored paw
        /// </summary>
        [GameParameters.CustomParameterUI("PAW Color",
            toolTip = "allow color coding in ODFC PAW (part action window) / part RMB (right menu button).",
            newGameOnly = false,
            unlockedDuringMission = true)]
        internal bool coloredPAW = true;

        [GameParameters.CustomFloatParameterUI("Global Range Max (0 for limitied only by game physics range",
            toolTip = "Max range of Logistics Network (in meters).",
            newGameOnly = false,
            unlockedDuringMission = true,
            minValue = 10.0f,
            maxValue = 2400.0f,
            stepCount = 100,
            displayFormat = "F2",
            asPercentage = false)]
        internal float maxRange = 1000.0f; // 0 for physics range ~2400

        [GameParameters.CustomFloatParameterUI("Global Range",
            toolTip = "Max range of Logistics Network (in meters).",
            newGameOnly = false,
            unlockedDuringMission = true,
            minValue = 10.0f,
            maxValue = 1000.0f,
            stepCount = 10,
            displayFormat = "F2",
            asPercentage = false)]
        internal float maxAltitude = 500.0f;

        [GameParameters.CustomFloatParameterUI("Global Range",
            toolTip = "Max range of Logistics Network (in meters).",
            newGameOnly = false,
            unlockedDuringMission = true,
            minValue = 1.0f,
            maxValue = 100.0f,
            stepCount = 1,
            displayFormat = "F2",
            asPercentage = false)]
        internal float maxGroundSpeed = 20.0f;

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
                    yesLanded = true;
                    yesSplashed = true;
                    yesPreLaunch = true;
                    yesFlying = false;
                    yesSubOrbital = false;
                    yesOrbiting = false;
                    yesEscaping = false;
                    yesDocked = false;
                    yesNone = true;
                    yesPartialUnmaned = true;
                    yesPartialManned = true;
                    yesFull = true;
                    maxRange = 0f;
                    maxAltitude = 500f;
                    maxGroundSpeed = 100f;
                    break;

                case GameParameters.Preset.Normal:
                    yesLanded = true;
                    yesSplashed = true;
                    yesPreLaunch = true;
                    yesFlying = false;
                    yesSubOrbital = false;
                    yesOrbiting = false;
                    yesEscaping = false;
                    yesDocked = false;
                    yesNone = false;
                    yesPartialUnmaned = true;
                    yesPartialManned = true;
                    yesFull = true;
                    maxRange = 1000f;
                    maxAltitude = 250f;
                    maxGroundSpeed = 40f;
                    break;

                case GameParameters.Preset.Moderate:
                    yesLanded = true;
                    yesSplashed = true;
                    yesPreLaunch = true;
                    yesFlying = false;
                    yesSubOrbital = false;
                    yesOrbiting = false;
                    yesEscaping = false;
                    yesDocked = false;
                    yesNone = false;
                    yesPartialUnmaned = false;
                    yesPartialManned = true;
                    yesFull = true;
                    maxRange = 500f;
                    maxAltitude = 100f;
                    maxGroundSpeed = 20f;
                    break;

                case GameParameters.Preset.Hard:
                    yesLanded = true;
                    yesSplashed = true;
                    yesPreLaunch = true;
                    yesFlying = false;
                    yesSubOrbital = false;
                    yesOrbiting = false;
                    yesEscaping = false;
                    yesDocked = false;
                    yesNone = false;
                    yesPartialUnmaned = false;
                    yesPartialManned = false;
                    yesFull = true;
                    maxRange = 250f;
                    maxAltitude = 50f;
                    maxGroundSpeed = 10f;
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

   
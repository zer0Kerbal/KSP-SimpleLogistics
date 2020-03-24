using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using UnityEngine;
using KSP.UI.Screens;
using KSP.IO;
using KSP.Localization;
using KSPAssets;
using System.Linq;

namespace SimpleLogistics
{
    [KSPAddon(KSPAddon.Startup.Flight, false)]
    internal class InSituation : MonoBehaviour
    {
        #region Enums Vars
        //? either set in part.cfg or in game.settings
        /* TODO: 
         * need a way to trap changes to conditionPerissions in game.settins; 
         * InRange()
         * AppropriateCondition()
         * AppropriateState()
         * AppropriateAltitude()
         * NetworkEligible()
         * IsActive needs to be read in from part and set IsActive flag
     */

        /*
           //! from KSP
           internal enum sits : byte { LANDED, SPLASHED, PRELAUNCH, FLYING, SUB_ORBITAL, ORBITING, ESCAPING, DOCKED };

           //! from KSP
           internal enum cLevel : byte { NONE, PARTIAL_UNMANNED, PARTIAL_MANNED, FULL };
   */
#endregion

        internal static bool NetworkEligible(Vessel vessel)
        {
            if (SituationEligible(vessel) && ControlEligible(vessel) && InRange(vessel)) return true;
            else return false;
        }
        
        internal static bool InRange(Vessel vessel)
        {
                   Transform partTransform = FlightGlobals.ActiveVessel.transform;
                   Vector3 directionToTarget = partTransform.position - vessel.transform.position;
                   //double angle = Vector3.Angle(GetPortDirection(), directionToTarget);
                   double distance = directionToTarget.magnitude;

                   //Log.dbg("Angle " + angle);
                   // LGG commented out the following to avoid compile error
                   //Log.dbg("{0} Range {1}", vessel.name, distance);

                   // if (Mathf.Abs(angle) < 35 && distance < range) return true;
                   if (distance <= HighLogic.CurrentGame.Parameters.CustomParams<OptionsA>().maxRange) return true;
            return false;
        }

        internal static bool InAltitude()
        {
            if (FlightGlobals.ActiveVessel.radarAltitude <= HighLogic.CurrentGame.Parameters.CustomParams<OptionsA>().maxAltitude) return true;
            else return true;
        }

        internal static bool InSpeed()
        {
            if (FlightGlobals.ActiveVessel.srfSpeed <= HighLogic.CurrentGame.Parameters.CustomParams<OptionsA>().maxGroundSpeed) return true;
            else return false;
        }

        internal static bool ControlEligible(Vessel vessel)
        {
            Vessel.ControlLevel controlLevel = vessel.CurrentControlLevel;
            //Log.dbg("control level: {0}", controlLevel.ToString());
            switch (controlLevel)
            {
                case Vessel.ControlLevel.FULL:
                    // Log.dbg("control level: {0}", controlLevel.ToString());
                    if (HighLogic.CurrentGame.Parameters.CustomParams<OptionsC>().yesFull) return true;
                    break;
                case Vessel.ControlLevel.PARTIAL_MANNED:
                    // Log.dbg("control level: {0}", controlLevel.ToString());
                    if (HighLogic.CurrentGame.Parameters.CustomParams<OptionsC>().yesPartialManned) return true;
                    break;
                case Vessel.ControlLevel.PARTIAL_UNMANNED:
                    // Log.dbg("control level: {0}", controlLevel.ToString());
                    if (HighLogic.CurrentGame.Parameters.CustomParams<OptionsC>().yesPartialUnmaned) return true;
                    break;
                case Vessel.ControlLevel.NONE:
                    // Log.dbg("control level: {0}", controlLevel.ToString());
                    if (HighLogic.CurrentGame.Parameters.CustomParams<OptionsC>().yesNone) return true;
                    break;
            }
            return false;
        }

        internal static bool SituationEligible(Vessel vessel)
        {
            Vessel.Situations situation = vessel.situation;
            //Log.dbg("{0} situation: {1} ", vessel.name, situation.ToString());
            switch (situation)
            {
                case Vessel.Situations.LANDED:
                    // Log.dbg("situation {0} permission {1}", situation.ToString(), HighLogic.CurrentGame.Parameters.CustomParams<OptionsA>().yesLanded.ToString());
                    if (HighLogic.CurrentGame.Parameters.CustomParams<OptionsB>().yesLanded) return true;
                    break;
                case Vessel.Situations.SPLASHED:
                    // Log.dbg("situation {0} permission {1}", situation.ToString(), HighLogic.CurrentGame.Parameters.CustomParams<OptionsA>().yesSplashed.ToString());
                    if (HighLogic.CurrentGame.Parameters.CustomParams<OptionsB>().yesSplashed) return true;
                    break;
                case Vessel.Situations.PRELAUNCH:
                    // Log.dbg("situation {0} permission {1}", situation.ToString(), HighLogic.CurrentGame.Parameters.CustomParams<OptionsA>().yesPreLaunch.ToString());
                    if (HighLogic.CurrentGame.Parameters.CustomParams<OptionsB>().yesPreLaunch) return true;
                    break;
                case Vessel.Situations.ORBITING:
                    // Log.dbg("situation {0} permission {1}", situation.ToString(), HighLogic.CurrentGame.Parameters.CustomParams<OptionsA>().yesOrbiting.ToString());
                    if (HighLogic.CurrentGame.Parameters.CustomParams<OptionsB>().yesOrbiting) return true;
                    break;
                case Vessel.Situations.FLYING:
                    // Log.dbg("situation {0} permission {1}", situation.ToString(), HighLogic.CurrentGame.Parameters.CustomParams<OptionsA>().yesFlying.ToString());
                    if (HighLogic.CurrentGame.Parameters.CustomParams<OptionsB>().yesFlying) return true;
                    break;
                case Vessel.Situations.SUB_ORBITAL:
                    // Log.dbg("situation {0} permission {1}", situation.ToString(), HighLogic.CurrentGame.Parameters.CustomParams<OptionsA>().yesSubOrbital.ToString());
                    if (HighLogic.CurrentGame.Parameters.CustomParams<OptionsB>().yesSubOrbital) return true;
                    break;
                case Vessel.Situations.ESCAPING:
                    // Log.dbg("situation {0} permission {1}", situation.ToString(), HighLogic.CurrentGame.Parameters.CustomParams<OptionsA>().yesEscaping.ToString());
                    if (HighLogic.CurrentGame.Parameters.CustomParams<OptionsB>().yesEscaping) return true;
                    break;
                case Vessel.Situations.DOCKED:
                    // Log.dbg("situation {0} permission {1}", situation.ToString(), HighLogic.CurrentGame.Parameters.CustomParams<OptionsA>().yesDocked.ToString());
                    if (HighLogic.CurrentGame.Parameters.CustomParams<OptionsB>().yesLanded) { return true; }
                    break;
            }
            return false;
        }
    }
}
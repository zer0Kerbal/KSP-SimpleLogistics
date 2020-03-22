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
        // consitionPermissions
        internal static bool yesLanded = true,
                             yesSplashed = true,
                             yesPreLaunch = true,
                             yesFlying = false,
                             yesSubOrbital = false,
                             yesOrbiting = false,
                             yesEscaping = false,
                             yesDocked = true,

                             yesNone = false,
                             yesPartialUnmaned = true,
                             yesPartialManned = true,
                             yesFull = true;

        internal static double  maxRange = 2400,
                                maxAltitude = 500,
                                maxGroundSpeed = 20;
        /*
           //! from KSP
           internal enum sits : byte { LANDED, SPLASHED, PRELAUNCH, FLYING, SUB_ORBITAL, ORBITING, ESCAPING, DOCKED };

           //! from KSP
           internal enum cLevel : byte { NONE, PARTIAL_UNMANNED, PARTIAL_MANNED, FULL };
   */
        #endregion
        internal static bool InRange(Vessel vessel)
        {
            /*       Transform partTransform = part.transform;
                   Vector3 directionToTarget = partTransform.position - kerbalOnEva.vessel.transform.position;
                   double angle = Vector3.Angle(GetPortDirection(), directionToTarget);
                   double distance = directionToTarget.magnitude;

                   Log.dbg("Angle " + angle);
                   Log.dbg("Range " + distance);

                   // if (Mathf.Abs(angle) < 35 && distance < range) return true;
                   if (distance < maxRange) return true;*/
            return true;
        }

        internal static bool AppropriateRange()
        {
            /* // FlightGlobals.ActiveVessel.vesselRanges .situation != Vessel.Situations.LANDED)
            if (nearestVessel <= maxRange) return true;
            else return false;*/
            return true;
        }

        internal static bool InAltitude()
        {
            if (FlightGlobals.ActiveVessel.radarAltitude <= maxAltitude) return true;
            else return true;
        }

        internal static bool InSpeed()
        {
            if (FlightGlobals.ActiveVessel.srfSpeed <= maxGroundSpeed) return true;
            else return false;
        }

        internal static bool InControl()
        {
            Vessel.ControlLevel controlLevel = FlightGlobals.ActiveVessel.CurrentControlLevel;
            switch (controlLevel)
            {
                case Vessel.ControlLevel.FULL:
                    Log.dbg("control level: {0}", controlLevel.ToString());
                    if (yesFull) return true;
                    break;
                case Vessel.ControlLevel.PARTIAL_MANNED:
                    Log.dbg("control level: {0}", controlLevel.ToString());
                    if (yesPartialManned) return true;
                    break;
                case Vessel.ControlLevel.PARTIAL_UNMANNED:
                    Log.dbg("control level: {0}", controlLevel.ToString());
                    if (yesPartialUnmaned) return true;
                    break;
                case Vessel.ControlLevel.NONE:
                    Log.dbg("control level: {0}", controlLevel.ToString());
                    if (yesNone) return true;
                    break;
            }
            return false;
        }

        internal static bool NetworkEligible()
        {
            Vessel.Situations situation = FlightGlobals.ActiveVessel.situation;
            switch (situation)
            {
                case Vessel.Situations.LANDED:
                    Log.dbg("situation {0} permission {1}", situation.ToString(), yesLanded.ToString());
                    if (yesLanded) return true;
                    break;
                case Vessel.Situations.SPLASHED:
                    Log.dbg("situation {0} permission {1}", situation.ToString(), yesSplashed.ToString());
                    if (yesSplashed) return true;
                    break;
                case Vessel.Situations.PRELAUNCH:
                    Log.dbg("situation {0} permission {1}", situation.ToString(), yesPreLaunch.ToString());
                    if (yesPreLaunch) return true;
                    break;
                case Vessel.Situations.ORBITING:
                    Log.dbg("situation {0} permission {1}", situation.ToString(), yesOrbiting.ToString());
                    if (yesOrbiting) return true;
                    break;
                case Vessel.Situations.FLYING:
                    Log.dbg("situation {0} permission {1}", situation.ToString(), yesFlying.ToString());
                    if (yesFlying) return true;
                    break;
                case Vessel.Situations.SUB_ORBITAL:
                    Log.dbg("situation {0} permission {1}", situation.ToString(), yesSubOrbital.ToString());
                    if (yesSubOrbital) return true;
                    break;
                case Vessel.Situations.ESCAPING:
                    Log.dbg("situation {0} permission {1}", situation.ToString(), yesEscaping.ToString());
                    if (yesEscaping) return true;
                    break;
                case Vessel.Situations.DOCKED:
                    Log.dbg("situation {0} permission {1}", situation.ToString(), yesDocked.ToString());
                    if (yesLanded) { return true; }
                    break;
            }
            return false;
        }
    }
}
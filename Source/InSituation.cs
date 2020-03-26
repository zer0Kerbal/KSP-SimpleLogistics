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
        #region TODO
        //? either set in part.cfg or in game.settings
        /* TODO: 
         * need a way to trap changes to conditionPerissions in game.settins; 
         * [x] InRange()
         * [x] AppropriateCondition()
         * [x] AppropriateState()
         * AppropriateAltitude()
         * [x] NetworkEligible()
         * IsActive needs to be read in from part and set IsActive flag
     */
        #endregion
        /// <summary>
        /// Queries all the conditions that determine elibability/suitablitliy to access logicstics network.
        /// If all conditions are met, passes an String.Empty;
        /// else passes back the error message built from al the testing conditions.
        /// </summary>
        /// <param name="vessel">The vessel.</param>
        /// <returns></returns>
        internal static string NetworkEligible(Vessel vessel)
        {
            double distance = InRange(vessel),
                   altitude = InAltitude(vessel),
                   groundSpeed = InSpeed(vessel);

            string msgSit = SituationEligible(vessel),
                   msgCntr = ControlEligible(vessel),
                   msgDistance = Localizer.Format("#SimpleLogistics_msgRange", distance.ToString(), HighLogic.CurrentGame.Parameters.CustomParams<OptionsC>().maxDistance.ToString()),
                   msgAlt = Localizer.Format("#SimpleLogistics_msgAltitude", altitude.ToString(), HighLogic.CurrentGame.Parameters.CustomParams<OptionsC>().maxAltitude.ToString()),
                   msgSpd = Localizer.Format("#SimpleLogistics_msgGroundSpeed", groundSpeed.ToString(), HighLogic.CurrentGame.Parameters.CustomParams<OptionsC>().maxGroundSpeed.ToString()),
                   msgDistanceAlt = Localizer.Format("#SimpleLogistics_msgDistanceAlt", distance.ToString(), HighLogic.CurrentGame.Parameters.CustomParams<OptionsC>().maxDistance.ToString()),
                   msgAltAlt = Localizer.Format("#SimpleLogistics_msgAltitudeAlt", altitude.ToString(), HighLogic.CurrentGame.Parameters.CustomParams<OptionsC>().maxAltitude.ToString()),
                   msgSpdAlt = Localizer.Format("#SimpleLogistics_msgGroundspeedAlt", groundSpeed.ToString(), HighLogic.CurrentGame.Parameters.CustomParams<OptionsC>().maxGroundSpeed.ToString()),
                   returnMsg = String.Empty;


            if (!String.IsNullOrEmpty(msgSit)) returnMsg += msgSit + "\n";
            if (!String.IsNullOrEmpty(msgCntr)) returnMsg += msgCntr + "\n";

            if (distance != 0) returnMsg += msgDistance;
                else returnMsg += msgDistanceAlt;
            returnMsg += "\n";

            if (altitude != 0) returnMsg += msgAlt;
                else returnMsg += msgAltAlt;
            returnMsg += "\n";
            
            if (groundSpeed != 0) returnMsg += msgSpd;
                else returnMsg += msgSpdAlt;
            
            returnMsg += "\n";

            // Logs.DebugLog(returnMsg);

            if (String.IsNullOrEmpty(msgSit)
                && String.IsNullOrEmpty(msgCntr)
                && distance == 0
                && altitude == 0
                && groundSpeed == 0)
                return String.Empty;
            else return returnMsg;
        }
        
        internal static double InRange(Vessel vessel)
        {
            double distance = 0;
            if (HighLogic.CurrentGame.Parameters.CustomParams<OptionsC>().maxDistance == 0f) return 0;
            else
            { 
                   Transform partTransform = FlightGlobals.ActiveVessel.transform;
                   Vector3 directionToTarget = partTransform.position - vessel.transform.position;
                   //double angle = Vector3.Angle(GetPortDirection(), directionToTarget);
                   distance = directionToTarget.magnitude;

                   //Log.msg("Angle " + angle);
                   // LGG commented out the following to avoid compile error
                   //Log.msg("{0} Range {1}", vessel.name, distance);

                   // if (Mathf.Abs(angle) < 35 && distance < range) return true;
                   if (distance <= HighLogic.CurrentGame.Parameters.CustomParams<OptionsC>().maxDistance) return 0;
            }
            return distance;
        }

        internal static double InAltitude(Vessel vessel)
        {
            if (HighLogic.CurrentGame.Parameters.CustomParams<OptionsC>().maxAltitude == 0f 
                || vessel.radarAltitude <= HighLogic.CurrentGame.Parameters.CustomParams<OptionsC>().maxAltitude) return 0f;
            else return vessel.radarAltitude;
        }

        internal static double InSpeed(Vessel vessel)
        {

            if (HighLogic.CurrentGame.Parameters.CustomParams<OptionsC>().maxDistance == 0f 
                || vessel.srfSpeed <= HighLogic.CurrentGame.Parameters.CustomParams<OptionsC>().maxGroundSpeed) return 0f;
            else return vessel.srfSpeed;
        }

        internal static string ControlEligible(Vessel vessel)
        {
            Vessel.ControlLevel controlLevel = vessel.CurrentControlLevel;
            switch (controlLevel)
            {
                case Vessel.ControlLevel.FULL:
                    if (!HighLogic.CurrentGame.Parameters.CustomParams<OptionsC>().yesFull) return Localizer.Format("#SimpleLogistics_msgControl", Localizer.Format("#SimpleLogistics_Full"));
                    break;
                case Vessel.ControlLevel.PARTIAL_MANNED:
                    if (!HighLogic.CurrentGame.Parameters.CustomParams<OptionsC>().yesPartialManned) return Localizer.Format("#SimpleLogistics_msgControl", Localizer.Format("#SimpleLogistics_PartialManned"));
                    break;
                case Vessel.ControlLevel.PARTIAL_UNMANNED:
                    if (!HighLogic.CurrentGame.Parameters.CustomParams<OptionsC>().yesPartialUnmaned) return Localizer.Format("#SimpleLogistics_msgControl", Localizer.Format("#SimpleLogistics_PartialUnmanned"));
                    break;
                case Vessel.ControlLevel.NONE:
                    if (!HighLogic.CurrentGame.Parameters.CustomParams<OptionsC>().yesNone) return Localizer.Format("#SimpleLogistics_msgControl", Localizer.Format("#SimpleLogistics_None"));
                    break;
            }
            return String.Empty;
        }

        internal static string SituationEligible(Vessel vessel)
        {
            Vessel.Situations situation = vessel.situation;
            switch (situation)
            {
                case Vessel.Situations.LANDED:
                    if (!HighLogic.CurrentGame.Parameters.CustomParams<OptionsB>().yesLanded) return Localizer.Format("#SimpleLogistics_msgSituation", Localizer.Format("#SimpleLogistics_Landed"));
                    break;
                case Vessel.Situations.PRELAUNCH:
                    if (!HighLogic.CurrentGame.Parameters.CustomParams<OptionsB>().yesPreLaunch) return Localizer.Format("#SimpleLogistics_msgSituation", Localizer.Format("#SimpleLogistics_PreLaunch"));
                    break;
                case Vessel.Situations.SPLASHED:
                    if (!HighLogic.CurrentGame.Parameters.CustomParams<OptionsB>().yesSplashed) return Localizer.Format("#SimpleLogistics_msgSituation", Localizer.Format("#SimpleLogistics_Splashed"));
                    break;
                case Vessel.Situations.ORBITING:
                    if (!HighLogic.CurrentGame.Parameters.CustomParams<OptionsB>().yesOrbiting) return Localizer.Format("#SimpleLogistics_msgSituation", Localizer.Format("#SimpleLogistics_Orbiting"));
                    break;
                case Vessel.Situations.FLYING:
                    if (!HighLogic.CurrentGame.Parameters.CustomParams<OptionsB>().yesFlying) return Localizer.Format("#SimpleLogistics_msgSituation", Localizer.Format("#SimpleLogistics_Flying"));
                    break;
                case Vessel.Situations.SUB_ORBITAL:
                    if (!HighLogic.CurrentGame.Parameters.CustomParams<OptionsB>().yesSubOrbital) return Localizer.Format("#SimpleLogistics_msgSituation", Localizer.Format("#SimpleLogistics_SubOrbital"));
                    break;
                case Vessel.Situations.ESCAPING:
                    if (!HighLogic.CurrentGame.Parameters.CustomParams<OptionsB>().yesEscaping) return Localizer.Format("#SimpleLogistics_msgSituation", Localizer.Format("#SimpleLogistics_Escaping"));
                    break;
                case Vessel.Situations.DOCKED:
                    if (!HighLogic.CurrentGame.Parameters.CustomParams<OptionsB>().yesLanded) return Localizer.Format("#SimpleLogistics_msgSituation", Localizer.Format("#SimpleLogistics_Docked"));
                    break;
            }
            return String.Empty;
        }
    }
}
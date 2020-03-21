using System;
using UnityEngine;

namespace simplelogistics
{
  //? either set in part.cfg or in game.settings
  // consitionPermissions
  const yesLanded = true,
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

  double  maxRange = 2400,
          maxAltitude = 500,
          maxGroundSpeed = 20;

  //TODO: need a way to trap changes to conditionPerissions in game.settins; 

  //! from KSP
  internal enum Situations
  {
    LANDED,
    SPLASHED,
    PRELAUNCH,
    FLYING,
    SUB_ORBITAL,
    ORBITING,
    ESCAPING,
    DOCKED
  };

  //! from KSP
  internal enum ControlLevel
  {
    NONE,
    PARTIAL_UNMANNED,
    PARTIAL_MANNED,
    FULL
  };

  internal boolean appropriateRange
  {
    if ( nearestVessel <= maxRange) return true
    else return false;
  }

  internal boolean appropriateAltitude
  {
    if (vessel.radarAltitude <= maxAltitude) return true
    else return false;
  }

  internal boolean appropriateGroundSpeed
  {
    if (vessel.groundSpeed <= maxGroundSpeed) return true
    else return false;
  }

  internal boolean appropriateControlLevel
  {
    switch (ControlLevel controlLevel = Vessel.ControlLevel)
    {
      case controlLevel.FULL:
      Log.msg("control level: {0}", controlLevel.ToString());
        if (yesFULL) return True;
        break;
      case controlLevel.PARTIAL_MANNED:
      Log.msg("control level: {0}", controlLevel.ToString());
        if (yesFULL) return True;
        break;
      case controlLevel.PARTIAL_UNMANNED:
      Log.msg("control level: {0}", controlLevel.ToString());
        if (yesFULL) return True;
        break;
      case controlLevel.NONE:
      Log.msg("control level: {0}", controlLevel.ToString());
        if (yesFULL) return True;
        break;
    }
    return false;
  }

  boolean appropriateSituation
  {
    switch (Situations situation = Vessel.Situation)
    {
      case situation.LANDED:
        Log.msg("situation {0} permission {1}", situation.ToString(), yesLanded.ToString());
        if (yesLanded) return True;
        break;
      case situation.SPLASHED:
        Log.msg("situation {0} permission {1}", situation.ToString(), yesSplashed.ToString());
        if (yesLanded) return True;
        break;
      case situation.PRELAUNCH:
        Log.msg("situation {0} permission {1}", situation.ToString(), yesPreLaunch.ToString());
        if (yesLanded) return True;
        break;
      case situation.FLYING:
        Log.msg("situation {0} permission {1}", situation.ToString(), yesFlying.ToString());
        if (yesLanded) return True;
        break;
      case situation.SUB_ORBITAL:
        Log.msg("situation {0} permission {1}", situation.ToString(), yesSubOrbital.ToString());
        if (yesLanded) return True;
        break;
      case situation.ORBITING:
        Log.msg("situation {0} permission {1}", situation.ToString(), yesOrbiting.ToString());
        if (yesLanded) return True;
        break;
      case situation.ESCAPING:
        Log.msg("situation {0} permission {1}", situation.ToString(), yesEscaping.ToString());
        if (yesLanded) return True;
        break;
      case situation.DOCKED:
        Log.msg("situation {0} permission {1}", situation.ToString(), yesDocked.ToString());
        if (yesLanded) return True;
        break;
    }
  }
}

  //   if (!vessel.LandedOrSplashed && vessel.radarAltitude > 500d)
  //   {
  //       //ScreenMessages.PostScreenMessage("Heat Pumps must be landed to use geothermal wells!", 5f,
  //       //    ScreenMessageStyle.UPPER_CENTER);
  //       //Shutdown();
  //       return True;
  //   }
  //   else return False;
  // } 

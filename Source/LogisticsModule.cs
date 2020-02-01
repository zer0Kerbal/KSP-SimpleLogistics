using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using KSP.Localization;

namespace SimpleLogistics
{
	public class LogisticsModule: PartModule
	{
		[KSPField(isPersistant = true, guiName = "#SimpleLogistics_Module_Plugged", guiActive = true)] //Plugged In?
        private bool isActive = false;

		public bool IsActive { get { return isActive; } }

		[KSPEvent(guiActive = true, guiName = "#SimpleLogistics_Module_PluggedNet")] //Plug into Network
        private void Toggle() {
			isActive = !isActive;
		}

		public void Set(bool status) {
			isActive = status;
		}

		public override string GetInfo()
		{
			return Localizer.Format("#SimpleLogistics_Module_Getinfo"); //"Logistics Module for easy resource sharing."
        }

		public override void OnStart(PartModule.StartState state) {
		}
	}
}

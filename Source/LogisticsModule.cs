using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace SimpleLogistics
{
	public class LogisticsModule: PartModule
	{
		[KSPField(isPersistant = true, guiName = "Plugged In?", guiActive = true)]
		private bool isActive = false;

		public bool IsActive { get { return isActive; } }

		[KSPEvent(guiActive = true, guiName = "Plug into Network")]
		private void Toggle() {
			isActive = !isActive;
		}

		public void Set(bool status) {
			isActive = status;
		}

		public override string GetInfo()
		{
			return "Logistics Module for easy resource sharing.";
		}

		public override void OnStart(PartModule.StartState state) {
		}
	}
}

using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using KSP.Localization;

namespace SimpleLogistics
{
	[Serializable]
	public class LogisticsModule: PartModule
	{
		[KSPField(isPersistant = true, guiName = "#SimpleLogistics_Module_Plugged", guiActive = true)] //Plugged In?
        private bool isActive = false;

		[SerializeField] 
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

        // LGG
        public new void Load(ConfigNode node)
        {
            bool b;
            if (node.HasValue("IsActive") && bool.TryParse(node.GetValue("IsActive"), out b))
            {
                isActive = b;
            }
            base.Load(node); // LGG
        }

        // LGG
        public new void Save(ConfigNode node)
        {
            node.AddValue("IsActive", isActive);
            base.Save(node); // LGG
        }

        public override string ToString()
        {
            return IsActive ? "false" : "true";
            
        }
    }
}

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
        private bool _isActive = false;

		[SerializeField]
        public bool IsActive { get { return _isActive; } }

		[KSPEvent(guiActive = true, guiName = "#SimpleLogistics_Module_PluggedNet")] //Plug into Network
        private void Toggle() {
			_isActive = !_isActive;
		}

		public void Set(bool status) {
			_isActive = status;
        }
 
        /// <summary>Module information shown in editors</summary>
        private string info = string.Empty;

        public override string GetInfo()
        {
            //? this is what is show in the editor
            //? As annoying as it is, pre-parsing the config MUST be done here, because this is called during part loading.
            //? The config is only fully parsed after everything is fully loaded (which is why it's in OnStart())
            if (info == string.Empty)
            {
                info += Localizer.Format("#SimpleLogistics_manu"); // #SimpleLogistics_manu = KerGuise Experimental Logistics
                info += "\n v" + Version.Text; // mod Version Number text
                info += "\n<color=#b4d455FF>" + Localizer.Format("#SimpleLogistics_Module_Getinfo"); // #SimpleLogistics_Module_Getinfo = Logistics Module for easy resource sharing.
            }
            return info;
        }

		public override void OnStart(PartModule.StartState state) 
        {
            Logs.dbg("On Start");
		}

        // LGG
        public new void Load(ConfigNode node)
        {
            bool b;
            if (node.HasValue("isActive") && bool.TryParse(node.GetValue("isActive"), out b))
            {
                Set(b);
            }
            base.Load(node); // LGG
        }

        // LGG
        public new void Save(ConfigNode node)
        {
            node.AddValue("isActive", _isActive);
            base.Save(node); // LGG
        }

        public override string ToString()
        {
            return IsActive ? "true" : "false";
            
        }
    }
}

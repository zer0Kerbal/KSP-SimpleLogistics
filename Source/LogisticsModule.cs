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
        [KSPField(isPersistant = true, guiActive = true, guiActiveEditor = true, guiName = "#SLOG-Module-Plugged", groupName = "SimpleLogistics", //Plugged In?
            groupDisplayName = "SimpleLogistics! v " + Version.SText, groupStartCollapsed = true)]
        public bool _isActive = false;

        [SerializeField]
        public bool IsActive { get { return _isActive; } }

        public void Set(bool status)
        { _isActive = status; }

		[KSPEvent(guiActive = true, guiActiveEditor = true, guiName = "#SLOG-Module-PluggedNet", groupName = "SimpleLogistics")] //Plug into Network
        internal void Toggle()
        { _isActive = !_isActive; }

        public override string ToString()
        { return _isActive ? "false" : "true"; }

        #region Var Const Enums
        /// <summary>Module information shown in editors</summary>
        private string info = string.Empty;

        #endregion
        public override string GetInfo()
        {
            //? this is what is shown in the editor
            //? As annoying as it is, pre-parsing the config MUST be done here, because this is called during part loading.
            //? The config is only fully parsed after everything is fully loaded (which is why it's in OnStart())
            if (info == string.Empty)
            {
                info += Localizer.Format("#SLOG-manu"); // #SLOG-manu = KerGuise Experimental Logistics
                info += "\n v" + Version.SText; // mod Version Number text
                info += "\n<color=#b4d455FF>" + Localizer.Format("#SLOG-Module-Getinfo"); // #SLOG-Module-Getinfo = Logistics Module for easy resource sharing.
            }
            return info;
        }
        #region On Event
        public override void OnStart(PartModule.StartState state)
        {
            Logs.dbg("On Start " + state.ToString());
        }

        // LGG
        public new void Load(ConfigNode node)
        {
            bool b = false;
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
        #endregion
    }
}

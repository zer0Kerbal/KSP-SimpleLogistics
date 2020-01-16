using UnityEngine;
using ToolbarControl_NS;

namespace SimpleLogistics
{
	[KSPAddon(KSPAddon.Startup.MainMenu, true)]
	public class RegisterToolbar : MonoBehaviour
	{
		void Start()
		{
			ToolbarControl.RegisterMod(SimpleLogistics.MODID, SimpleLogistics.MODNAME);
		}
	}
}

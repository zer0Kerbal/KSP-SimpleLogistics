using System;
using UnityEngine;
using ToolbarControl_NS;

namespace SimpleLogistics
{
	public struct Palette
	{
		public static Texture2D tTransparent = new Texture2D(1, 1);
		public static Texture2D tWindowBack = new Texture2D(8, 8);
		public static Texture2D tButtonBack = new Texture2D(8, 8);
		public static Texture2D tButtonHover = new Texture2D(8, 8);


		internal static void LoadTextures()
		{
			var bytes = System.IO.File.ReadAllBytes("GameData/SimpleLogistics/Plugins/window-back.png");
			//var bytes = System.IO.File.ReadAllBytes("GameData/SimpleLogistics/Plugins/PluginData/Textures/window-back.png");
			tWindowBack.LoadImage(bytes);

			bytes = System.IO.File.ReadAllBytes("GameData/SimpleLogistics/Plugins/button-back.png");
			//bytes = System.IO.File.ReadAllBytes("GameData/SimpleLogistics/Plugins/PluginData/Textures/button-back.png");
			tButtonBack.LoadImage(bytes);

			bytes = System.IO.File.ReadAllBytes("GameData/SimpleLogistics/Plugins/button-hover-back.png");
			//bytes = System.IO.File.ReadAllBytes("GameData/SimpleLogistics/Plugins/PluginData/Textures/button-hover-back.png");
			tButtonHover.LoadImage(bytes);
		}
	}

	//[KSPAddon(KSPAddon.Startup.MainMenu, true)]
	[KSPAddon(KSPAddon.Startup.Instantly, true)]
	public class GUICore : MonoBehaviour
	{
		void Start()
		{
			Debug.Log("SimpleLogistics.RegisterToolbar");
			ToolbarControl.RegisterMod(SimpleLogistics.MODID, SimpleLogistics.MODNAME);
		}
		//}

		//public class Core : MonoBehaviour
		//{
		static private GUICore _instance = null;
		static public GUICore Instance
		{
			get { return _instance; }
		}

		static private bool skinInitialized = false;

		public void Awake()
		{
			if (_instance != null)
			{
				Destroy(this);
				return;
			}
			_instance = this;
		}

		public void OnDestroy()
		{
			if (_instance == this)
				_instance = null;
		}

		public void OnGUI()
		{
			if (skinInitialized)
				return;
			Palette.LoadTextures();
			skinInitialized = true;
			Destroy(this); // Quit after initialized
		}
	}
}

/*
 * UI Framework licensed under BSD 3-clause license
 * https://github.com/Real-Gecko/Unity5-UIFramework/blob/master/LICENSE.md
*/

using System;
using UnityEngine;

namespace SimpleLogistics
{
	public struct Palette {
		// Colors
		public static Color white = new Color (1.0f, 1.0f, 1.0f);
		public static Color dimWhite = new Color (0.9f, 0.9f, 0.9f, 0.9f);
		public static Color black = new Color (0.0f, 0.0f, 0.0f, 1.0f);
		public static Color red = new Color (1.0f, 0.8f, 0.8f);
		public static Color darkRed = new Color (0.7f, 0.4f, 0.4f);
		public static Color green = new Color (0.6f, 1.0f, 0.8f);
		public static Color darkGreen = new Color (0.4f, 0.7f, 0.4f);
		public static Color blue = new Color (0.7f, 0.7f, 1.0f);
		public static Color yellow = new Color (1.0f, 1.0f, 0.5f);
		public static Color gray60 = new Color (0.6f, 0.6f, 0.6f, 0.85f);
		public static Color gray50 = new Color (0.5f, 0.5f, 0.5f);
		public static Color gray40 = new Color (0.4f, 0.4f, 0.4f);
		public static Color gray30 = new Color (0.3f, 0.3f, 0.3f, 0.85f);
		public static Color gray20 = new Color (0.2f, 0.2f, 0.2f);
		public static Color gray10 = new Color (0.1f, 0.1f, 0.1f);
		public static Color transparent = new Color (0.0f, 0.0f, 0.0f, 0.0f);
		public static Color windowBack = new Color (0.39f, 0.41f, 0.46f, 0.85f);
		public static Color windowBorder = new Color (0.66f, 0.73f, 0.78f, 0.85f);

		// Color filled textures
		public static Texture2D tBlack = new Texture2D (1, 1);
		public static Texture2D tDarkRed = new Texture2D (1, 1);
		public static Texture2D tDarkGreen = new Texture2D (1, 1);
		public static Texture2D tGray60 = new Texture2D (1, 1);
		public static Texture2D tGray50 = new Texture2D (1, 1);
		public static Texture2D tGray40 = new Texture2D (1, 1);
		public static Texture2D tGray30 = new Texture2D (1, 1);
		public static Texture2D tGray20 = new Texture2D (1, 1);
		public static Texture2D tGray10 = new Texture2D (1, 1);
		public static Texture2D tTransparent = new Texture2D (1, 1);
		public static Texture2D tWindowBack = new Texture2D (8, 8);
		public static Texture2D tButtonBack = new Texture2D (8, 8);
		public static Texture2D tButtonHover = new Texture2D (8, 8);

		internal static void InitPalette() {
			tBlack.SetPixel (0, 0, black);
			tBlack.Apply ();
//
			tDarkRed.SetPixel (0, 0, darkRed);
			tDarkRed.Apply ();
//
			tDarkGreen.SetPixel (0, 0, darkGreen);
			tDarkGreen.Apply ();

			tGray60.SetPixel(0, 0, gray60);
			tGray60.Apply();

			tGray50.SetPixel(0, 0, gray50);
			tGray50.Apply();

			tGray40.SetPixel (0, 0, gray40);
			tGray40.Apply ();

			tGray30.SetPixel(0, 0, gray30);
			tGray30.Apply();

			tGray40.SetPixel (0, 0, gray40);
			tGray40.Apply ();

			tGray20.SetPixel(0, 0, gray20);
			tGray20.Apply();

			tGray10.SetPixel(0, 0, gray10);
			tGray10.Apply();

			tTransparent.SetPixel (0, 0, transparent);
			tTransparent.Apply ();

		}
		internal static void LoadTextures() {
			var bytes = System.IO.File.ReadAllBytes ("GameData/SimpleLogistics/Plugins/Textures/window-back.png");
			tWindowBack.LoadImage (bytes);

			bytes = System.IO.File.ReadAllBytes ("GameData/SimpleLogistics/Plugins/Textures/button-back.png");
			tButtonBack.LoadImage (bytes);

			bytes = System.IO.File.ReadAllBytes ("GameData/SimpleLogistics/Plugins/Textures/button-hover-back.png");
			tButtonHover.LoadImage (bytes);
		}
	}
}


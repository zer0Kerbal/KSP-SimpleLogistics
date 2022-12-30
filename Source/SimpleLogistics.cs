﻿using System;
using System.Collections.Generic;
using UnityEngine;
using KSP.UI.Screens;
using KSP.IO;
using KSP.Localization;
using ClickThroughFix;
using ToolbarControl_NS;

namespace SimpleLogistics
{
	[KSPAddon(KSPAddon.Startup.Flight, false)]
	public class SimpleLogistics: MonoBehaviour
	{
		private static SimpleLogistics instance;
		public static SimpleLogistics Instance { get { return instance; } }

		// So many lists...
		private SortedList<string, double> resourcePool;
		private SortedList<string, double> requestPool;
		private SortedList<string, double> vesselSpareSpace;

		private List<PartResource> partResources;

		private bool requested;

		private PluginConfiguration config;

		// GUI vars
		private Rect windowRect;
		private int windowId;
		private bool gamePaused;
		private bool GUIglobalHidden;
		private bool GUIactive;
		private bool refreshGUI;

		private static Color titleColor = new Color(45f, 145f, 17f, 1f);
		private static Color titleBackColor = new Color(45f, 145f, 17f, 0.45f);

		private ToolbarControl toolbarControl;

		// Same as Debug Toolbar lock mask
		private const ulong lockMask = 900719925474097919;

#region On Events
		private void Awake() 
		{
			if (instance != null) 
			{
				Destroy (this);
				return;
			}
			instance = this;
		}

		private void Start() 
		{
			resourcePool = new SortedList<string, double> ();
			requestPool = new SortedList<string, double> ();
			vesselSpareSpace = new SortedList<string, double> ();

			partResources = new List<PartResource> ();

			config = PluginConfiguration.CreateForType<SimpleLogistics> ();
			config.load ();

			windowRect = config.GetValue<Rect>(this.name, new Rect (0, 0, 400, 400));

			windowId = GUIUtility.GetControlID(FocusType.Passive);

			GUIglobalHidden = false;
			gamePaused = false;
			GUIactive = false;
			refreshGUI = true;

			requested = false;
			CreateLauncher();

			GameEvents.onVesselChange.Add (onVesselChange);
			GameEvents.onLevelWasLoaded.Add (onLevelWasLoaded);
			GameEvents.onHideUI.Add(onHideUI);
			GameEvents.onShowUI.Add(onShowUI);
			GameEvents.onGamePause.Add (onGamePause);
			GameEvents.onGameUnpause.Add (onGameUnpause);
		}

		private void OnDestroy() 
		{
			config.SetValue (this.name, windowRect);
			config.save ();

			GameEvents.onLevelWasLoaded.Remove (onLevelWasLoaded);
			GameEvents.onVesselChange.Remove (onVesselChange);
			GameEvents.onHideUI.Remove(onHideUI);
			GameEvents.onShowUI.Remove(onShowUI);
			GameEvents.onGamePause.Remove (onGamePause);
			GameEvents.onGameUnpause.Remove (onGameUnpause);

			UnlockControls ();
			DestroyLauncher ();

			if (instance == this)
				instance = null;
		}
#endregion

#region Game Events
        private void onVesselChange(Vessel vessel) 
		{
			requestPool.Clear ();
			vesselSpareSpace.Clear ();
			foreach(Part part in vessel.parts) 
			{
				foreach (var resource in part.Resources) 
				{
					if (!requestPool.ContainsKey (resource.info.name)) 
					{
						requestPool.Add (resource.info.name, 0);
						vesselSpareSpace.Add (resource.info.name, resource.maxAmount);
					} 
					else
						vesselSpareSpace [resource.info.name] += resource.maxAmount;
				}
			}
		}

		public void onLevelWasLoaded(GameScenes scene)
		{
			onVesselChange(FlightGlobals.ActiveVessel);
		}

#endregion

#region UI Functions

		public const string MODID = "SimpleLogisticsUI";
		public const string MODNAME = "SimpleLogistics!";
		private void CreateLauncher() 
		{
			toolbarControl = gameObject.AddComponent<ToolbarControl>();
			toolbarControl.AddToAllToolbars(onAppTrue, onAppFalse,	
				ApplicationLauncher.AppScenes.FLIGHT,
				 MODID,
				"SIButton",
				"SimpleLogistics/Plugins/PluginData/Textures/simple-logistics-icon",
				"SimpleLogistics/Plugins/PluginData/Textures/simple-logistics-icon-toolbar",
				MODNAME
			);
		}

		public void DestroyLauncher()
		{
			if (toolbarControl != null)
			{
				toolbarControl.OnDestroy();
				Destroy(toolbarControl);
			}
		}

		public void OnGUI()
		{
			if (gamePaused || GUIglobalHidden || !GUIactive) return;

			string IsEligible = InSituation.NetworkEligible(FlightGlobals.ActiveVessel);
			if (!String.IsNullOrEmpty(IsEligible))
			{
				toolbarControl.SetFalse();
				Logs.msg(IsEligible);
			}

			if (refreshGUI)
			{
				windowRect.height = 0;
				refreshGUI = false;
			}

			if (HighLogic.CurrentGame.Parameters.CustomParams<OptionsA>().useAlternateSkin) GUI.skin = HighLogic.Skin;
			GUI.backgroundColor = titleBackColor;
			GUI.contentColor = titleColor;
			windowRect = ClickThruBlocker.GUILayoutWindow(
				windowId,
				windowRect,
				DrawGUI,
                Localizer.Format("#SLOG-WindowTitle", Version.Text), //"Logistics Network v "
                GUILayout.ExpandWidth(true),
				GUILayout.ExpandHeight(true)
			);
			if (windowRect.Contains (Event.current.mousePosition)){
				LockControls ();
			} else {
				UnlockControls();
			}
		}

		// It's a mess
		private void DrawGUI(int windowId)
		{
			GUILayout.BeginVertical ();
			GUI.contentColor = Color.blue;
				GUILayout.Label(Localizer.Format("#SLOG-VesselName", FlightGlobals.ActiveVessel.GetDisplayName()));
				GUILayout.Label(Localizer.Format("#SLOG-Status", FlightGlobals.ActiveVessel.SituationString));
				
				bool ableToRequest = false;
				LogisticsModule lm = FlightGlobals.ActiveVessel.FindPartModuleImplementing<LogisticsModule> ();
				if (lm != null)
				{
				GUILayout.BeginHorizontal();
					GUILayout.Label(
						lm.IsActive ? Localizer.Format("#SLOG-Label2") : Localizer.Format("#SLOG-Label3") //, //"Pluged In""Unplugged"
						//lm.IsActive ? GUI.contentColor = Color.green : GUI.contentColor = Color.red
					);
				GUILayout.FlexibleSpace();
					GUI.contentColor = Color.yellow;
					if (GUILayout.Button(Localizer.Format("#SLOG-Label4"))) // "Toggle Plug"
					{
						lm.Toggle();// Set (!lm.IsActive);
						refreshGUI = true;
					}
				GUILayout.FlexibleSpace();
					GUI.contentColor = Color.white;
				GUILayout.EndHorizontal();
				// if plugged in - not able to request
					ableToRequest = !lm.IsActive;
				}

			//if (ableToRequest)
				GetVesselSpareSpace();

				GUI.contentColor = Color.yellow;
				GUILayout.Label(Localizer.Format("#SLOG-Label5")); //"Resource Pool:"

			foreach (var resource in resourcePool)
			{
				GUILayout.BeginHorizontal();
					GUILayout.Label(resource.Key, GUILayout.Width(170));
					if (ableToRequest && requestPool.ContainsKey(resource.Key))
						GUILayout.Label(requestPool[resource.Key].ToString("0.00") + " / " + resource.Value.ToString ("0.00"));
					else GUILayout.Label(resource.Value.ToString("0.00"));
				//TODO: *knocking on wood that this woorks.
				/*					if (GUILayout.Button(resourcePool[resource.Key].ToString("0.00")))
											// depositResource(PartResourceLibrary.Instance.GetDefinition(resource.Key.ToString()) );
											depositResource(PartResourceLibrary.Instance.GetDefinition(resource.Key.ToString()) );*//*
			*/
				GUILayout.EndHorizontal();
				if (ableToRequest && requestPool.ContainsKey(resource.Key))
				{
					GUILayout.BeginHorizontal();
						if (GUILayout.Button("0", GUILayout.Width(20)))
							requestPool[resource.Key] = 0;

						requestPool[resource.Key] = GUILayout.HorizontalSlider (
							(float)requestPool[resource.Key],
							0,
							(float)Math.Min (vesselSpareSpace [resource.Key], resource.Value),
							GUILayout.Width (280)
						);

						if (GUILayout.Button(vesselSpareSpace[resource.Key].ToString("0.00")))
								requestPool[resource.Key] = Math.Min (vesselSpareSpace [resource.Key], resource.Value);

					GUILayout.EndHorizontal();
				}
			}

			if (ableToRequest)
				if (GUILayout.Button(Localizer.Format("#SLOG-Label6"))) // "Request Resources"
				{
					requested = true;
				}
			GUILayout.FlexibleSpace(); 
			if (GUILayout.Button("<color=red>X</color>", GUILayout.Width(20))) toolbarControl.SetFalse();

			GUILayout.EndVertical();

			GUI.DragWindow();
		}

		public void onGamePause()
		{
			gamePaused = true;
			UnlockControls ();
		}

		public void onGameUnpause()
		{
			gamePaused = false;
		}

		private void onHideUI()
		{
			GUIglobalHidden = true;
			UnlockControls ();
		}

		private void onShowUI()
		{
			GUIglobalHidden = false;
		}

		public void onAppTrue()
		{
			string errMsg = InSituation.NetworkEligible(FlightGlobals.ActiveVessel);
			if (!String.IsNullOrEmpty(errMsg))
			{
                Logs.msg(errMsg); //"Must be landed to use logistics"
                return;
			}

			GUIactive = true;
		}

		public void onAppFalse()
		{
			GUIactive = false;
			refreshGUI = true;
			UnlockControls ();
		}

		internal virtual void onToggle()
		{
			string errMsg = InSituation.NetworkEligible(FlightGlobals.ActiveVessel);
			if (!String.IsNullOrEmpty(errMsg))
			{
				Logs.msg(errMsg); //"Must be landed to use logistics"
				return;
			}

			GUIactive = !GUIactive;
			if (!GUIactive)
			{
				refreshGUI = true;
				UnlockControls ();
			}
		}

		private ControlTypes LockControls()
		{
			return InputLockManager.SetControlLock ((ControlTypes)lockMask, this.name);
		}

		private void UnlockControls()
		{
			InputLockManager.RemoveControlLock(this.name);
		}
#endregion
#region Resource Sharing
        private void FixedUpdate()
		{
			// Find all resources in the network
			partResources.Clear ();
			foreach (Vessel vessel in FlightGlobals.VesselsLoaded)
			{
				if (!String.IsNullOrEmpty(InSituation.NetworkEligible(vessel)))
				{
					Logs.dbg("{0} ineligible\n", vessel.GetDisplayName());
                    continue;
				}

				LogisticsModule lm = vessel.FindPartModuleImplementing<LogisticsModule> ();
				if (lm != null && !lm.IsActive)
					{
					Logs.dbg("{0} not pluged in\n", vessel.GetDisplayName());
					continue;
					}
				
				foreach (Part part in vessel.parts)
				{
					if (part.State == PartStates.DEAD)
					{
						Logs.dbg("{0} is dead on {1}\n", part.partName, vessel.GetDisplayName());
						continue;
					}	
					
					foreach (PartResource resource in part.Resources)
					{
						if (resource.info.resourceTransferMode == ResourceTransferMode.NONE ||
							resource._flowMode == PartResource.FlowMode.None ||
							!resource._flowState)
						{
							Logs.dbg("{3}'s {2}'s {1} can't flow", resource.resourceName, part.partName,vessel.vesselName);
							continue;
						}
						
						partResources.Add (resource);
					}
				}
			}

			// Create a resource pool
			resourcePool.Clear ();
			foreach (var resource in partResources)
			{
				if (!resourcePool.ContainsKey (resource.info.name))
					resourcePool.Add (resource.info.name, resource.amount);
				else
					resourcePool [resource.info.name] += resource.amount;
			}

			// Spread resources evenly
			foreach (var resource in resourcePool)
			{
				double value = resource.Value;
				if (requested)
				{
					if (requestPool.ContainsKey (resource.Key))
					{
						value -= requestPool [resource.Key];
					}
				}

				var resList = partResources.FindAll (r => r.info.name == resource.Key);

				// Don't waste time on single one
//				if (resList.Count == 1)
//					continue;

				ShareResource (resList, value);
			}

			if (requested)
			{
				TransferResources ();
				requested = false;
			}
		}

		/// <summary>
		/// Distributes resource evenly across every capacitor with priority to low capacity first
		/// </summary>
		/// <param name="resources">List of resources</param>
		/// <param name="amount">Overall amount</param>
		private void ShareResource(List<PartResource> resources, double amount)
		{
			// Portion each may potentially receive
			double portion = amount / resources.Count;

			// Those who may not grab whole portion
			var minors = resources.FindAll (r => r.maxAmount < portion);

			// Those who may grab whole portion and even ask for more :D
			var majors = resources.FindAll (r => r.maxAmount >= portion);

			if (minors.Count > 0)
			{
				// Some may not handle this much
				foreach (var minor in minors)
				{
					minor.amount = minor.maxAmount;
					amount -= minor.maxAmount;
				}
				// Love recursion :D
				if (amount > 0)
					ShareResource (majors, amount);
			} else {
				// Portion size is good for everybody
				foreach (var major in majors)
				{
					major.amount = portion;
				}
			}
		}

		/// <summary>
		/// Get the amount of spare resource space. Calling every physics frame is stupid, but who cares :D
		/// </summary>
		/// <param name="vessel">Vessel.</param>
		private void GetVesselSpareSpace()
		{
			vesselSpareSpace.Clear ();
			foreach(Part part in FlightGlobals.ActiveVessel.parts)
			{
				foreach (var resource in part.Resources)
				{
					if (!vesselSpareSpace.ContainsKey (resource.info.name))
						vesselSpareSpace.Add (resource.info.name, resource.maxAmount - resource.amount);
					else
						vesselSpareSpace [resource.info.name] += resource.maxAmount - resource.amount;
				}
			}
		}

		// Code duplication? No way!
		private void TransferResources()
		{
			List<PartResource> AVResources = new List<PartResource> ();
			SortedList<string, double> AVPool = new SortedList<string, double> ();

			foreach (Part part in FlightGlobals.ActiveVessel.parts)
			{
				if (part.State == PartStates.DEAD)
					continue;

				foreach (PartResource resource in part.Resources)
				{
					if (resource.info.resourceTransferMode == ResourceTransferMode.NONE ||
						resource._flowMode == PartResource.FlowMode.None ||
						!resource._flowState)
						continue;

					AVResources.Add (resource);
				}
			}

			foreach (var resource in AVResources)
			{
				if (!AVPool.ContainsKey (resource.info.name))
					AVPool.Add (resource.info.name, resource.amount);
				else
					AVPool [resource.info.name] += resource.amount;
			}

			// Spread resources evenly
			foreach (var resource in AVPool)
			{
				double value = resource.Value;
				var resList = AVResources.FindAll (r => r.info.name == resource.Key);
				value += requestPool [resource.Key];
				requestPool [resource.Key] = 0;

				ShareResource (resList, value);
			}
		}
#endregion
#region zed'K new code
        private void depositResource(PartResource resource)
		{
			List<PartResource> AVResources = new List<PartResource>();
			
			foreach (Part part in FlightGlobals.ActiveVessel.parts)
				if (part.State == PartStates.DEAD ||
					resource.info.resourceTransferMode == ResourceTransferMode.NONE ||
					resource._flowMode == PartResource.FlowMode.None ||
					!resource._flowState)
					continue;

			AVResources.Add(resource);
			ShareResource(AVResources, resource.amount);
		}
#endregion
#region On Event
        
#endregion
    }
}


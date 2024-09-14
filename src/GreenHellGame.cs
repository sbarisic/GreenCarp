using HarmonyLib;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace GreenCarp {
	delegate void SetStateFunc(MainMenuState State);

	[HarmonyPatch(typeof(MainMenu), "Awake")]
	class GreenHellGame_Awake {
		static void Postfix(MainMenu __instance) {
			Console.WriteLine("GreenHellGame.Awake");

			Type T = __instance.GetType();
			T.GetField("m_FadeInDuration", BindingFlags.Instance | BindingFlags.NonPublic).SetValue(__instance, 0.0f);
			T.GetField("m_FadeOutDuration", BindingFlags.Instance | BindingFlags.NonPublic).SetValue(__instance, 0.0f);
			T.GetField("m_FadeOutSceneDuration", BindingFlags.Instance | BindingFlags.NonPublic).SetValue(__instance, 0.0f);
			T.GetField("m_CompanyLogoDuration", BindingFlags.Instance | BindingFlags.NonPublic).SetValue(__instance, 0.0f);
			T.GetField("m_GameLogoDuration", BindingFlags.Instance | BindingFlags.NonPublic).SetValue(__instance, 0.0f);
			T.GetField("m_BlackScreenDuration", BindingFlags.Instance | BindingFlags.NonPublic).SetValue(__instance, 0.0f);

			MethodInfo SetStateInfo = AccessTools.Method(typeof(MainMenu), "SetState", new[] { typeof(MainMenuState) });
			SetStateFunc SetState = AccessTools.MethodDelegate<SetStateFunc>(SetStateInfo, __instance);
			SetState(MainMenuState.MainMenu);
		}
	}

	[HarmonyPatch(typeof(GreenHellGame), "InitializeRemoteStorage")]
	class GreenHellGame_InitializeRemoteStorage {
		static bool Prefix(GreenHellGame __instance) {
			Console.WriteLine("GreenHellGame.InitializeRemoteStorage - " + nameof(LocalCarpStorage));
			__instance.m_RemoteStorage = new LocalCarpStorage();
			return false;
		}
	}

	[HarmonyPatch(typeof(GreenHellGame), "Initialize")]
	class GreenHellGame_Initialize {
		static void Postfix(GreenHellGame __instance) {
			Plugin.OnGameEngineStart(__instance);
		}
	}

	[HarmonyPatch(typeof(GreenHellGame), "TryToMoveSettingsToRemoteStorage")]
	class GreenHellGame_TryToMoveSettingsToRemoteStorage {
		static bool Prefix(GreenHellGame __instance) {
			Console.WriteLine("GreenHellGame.TryToMoveSettingsToRemoteStorage");
			return false;
		}
	}

	[HarmonyPatch(typeof(GreenHellGame), "TryToMoveSavesToRemoteStorage")]
	class GreenHellGame_TryToMoveSavesToRemoteStorage {
		static bool Prefix(GreenHellGame __instance) {
			Console.WriteLine("GreenHellGame.TryToMoveSavesToRemoteStorage");
			__instance.m_RemoteStorage = new LocalCarpStorage();
			return false;
		}
	}
}

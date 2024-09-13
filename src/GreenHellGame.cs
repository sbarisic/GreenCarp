using HarmonyLib;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GreenCarp {

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

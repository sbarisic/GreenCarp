using HarmonyLib;

using Steamworks;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GreenCarp {
	[HarmonyPatch(typeof(MainLevel), "StartLevel")]
	class MainLevel_StartLevel {
		static void Postfix(MainLevel __instance) {
			Console.WriteLine("Starting level");

			if (__instance.m_TODTime != null) {
				__instance.m_TODTime.m_DayLengthInMinutes = 40f;
				__instance.m_TODTime.m_NightLengthInMinutes = 20f;
			}
		}
	}

	static class SaveGameUtils {
		public static void SetAllCheats(bool Enabled) {
			Cheats.m_GodMode = Enabled;
			//Cheats.m_ImmortalItems = Enabled;
			Cheats.m_GhostMode = Enabled;
			Cheats.m_OneShotAI = Enabled;
			Cheats.m_OneShotConstructions = Enabled;
			Cheats.m_InstantBuild = Enabled;
			Cheats.m_InstantCraftingDecorations = Enabled;

			/*if (Enabled) {
				ItemsManager ItmMgr = ItemsManager.Get();

				if (ItmMgr != null) {
					ItmMgr.UnlockWholeNotepad();
					ItmMgr.UnlockAllItemsInNotepad();
				}

				Player Ply = Player.Get();
				if (Ply != null) {
					Ply.m_MapUnlocked = true;
				}
			}*/
		}
	}

	[HarmonyPatch(typeof(SaveGame), "LoadCoop", new Type[] { })]
	class SaveGame_LoadCoop {
		static void Postfix() {
			Console.WriteLine("SaveGame.LoadCoop");
			SaveGameUtils.SetAllCheats(false);
		}
	}

	[HarmonyPatch(typeof(SaveGame), "FullLoad", new Type[] { })]
	class SaveGame_FullLoad {
		static void Postfix() {
			Console.WriteLine("SaveGame.FullLoad");
			SaveGameUtils.SetAllCheats(false);

			if (ReplTools.AmIMaster()) {
				Console.WriteLine("Host mode - enabling cheats");

				SaveGameUtils.SetAllCheats(true);
			}
		}
	}
}

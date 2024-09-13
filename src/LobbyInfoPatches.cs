using HarmonyLib;

using Steamworks;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GreenCarp {
	[HarmonyPatch(typeof(SteamAchievementsManager), "IsAchievedInCloud")]
	class SteamAchievementsManagerManager_IsAchievedInCloud {
		static bool Prefix(SteamAchievementsManager __instance, string api_name, ref bool __result) {
			__result = true;
			return false;
		}
	}

	[HarmonyPatch(typeof(SteamAchievementsManager), "LoadAchievementsFromSteam")]
	class SteamAchievementsManagerManager_LoadAchievementsFromSteam {
		static bool Prefix(SteamAchievementsManager __instance) {
			return false;
		}
	}

	[HarmonyPatch(typeof(SteamAchievementsManager), "LoadStatsFromSteam")]
	class SteamAchievementsManagerManager_LoadStatsFromSteam {
		static bool Prefix(SteamAchievementsManager __instance) {
			return false;
		}
	}
}

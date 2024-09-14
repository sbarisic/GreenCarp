using HarmonyLib;

using Steamworks;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GreenCarp {
	/*[HarmonyPatch(typeof(Player), "TakeDamage")]
	class Player_TakeDamage {
		static bool Prefix(Player __instance, ref bool __result) {
			// Master is invincible
			if (P2PSession.Instance != null && P2PSession.Instance.AmIMaster()) {
				__result = false;
				return false;
			}

			return true;
		}
	}

	[HarmonyPatch(typeof(Player), "CanReceiveDamageOfType")]
	class Player_CanReceiveDamageOfType {
		static bool Prefix(Player __instance, ref bool __result) {
			// Master is invincible
			if (P2PSession.Instance != null && P2PSession.Instance.AmIMaster()) {
				__result = false;
				return false;
			}

			return true;
		}
	}*/
}

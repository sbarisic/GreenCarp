using HarmonyLib;

using Steamworks;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace GreenCarp {
	/*[HarmonyPatch(typeof(LobbyInfo), "Start")]
	class LobbyInfo_Start {
		static bool Prefix(LobbyInfo __instance) {
			for (int i = 0; i < P2PSession.MAX_PLAYERS - 4; i++) {
				__instance.AddComponentWithEvent<LobbyMemberListElement>();
			}

			return true;
		}

		static void Postfix(LobbyInfo __instance) {
			FieldInfo MembersField = __instance.GetType().GetField("m_Members", BindingFlags.Instance | BindingFlags.NonPublic);
			LobbyMemberListElement[] Members = (LobbyMemberListElement[])MembersField.GetValue(__instance);

			Members = new LobbyMemberListElement[P2PSession.MAX_PLAYERS];
			for (int i = 0; i < Members.Length; i++) {
				Members[i] = new LobbyMemberListElement();
			}

			MembersField.SetValue(__instance, Members);
		}
	}*/
}

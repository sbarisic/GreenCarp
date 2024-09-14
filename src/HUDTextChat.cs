using HarmonyLib;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using UnityEngine;

namespace GreenCarp {
	[HarmonyPatch(typeof(HUDTextChat), "SendTextMessage")]
	class HUDTextChat_SendTextMessage {
		static bool Prefix(HUDTextChat __instance) {
			if (__instance == null || __instance.m_Field == null)
				return true;

			string ChatMsg = __instance.m_Field.text.Trim();

			if (ChatCommands.OnChatCommand("", ChatMsg, (Msg) => {
				__instance.m_History.StoreMessage(Msg, null, Color.yellow);
			})) {
				return false;
			}

			return true;
		}
	}

	[HarmonyPatch(typeof(HUDTextChatHistory), "OnTextChat")]
	class HUDTextChatHistory_OnTextChat {
		static bool Prefix(HUDTextChatHistory __instance, P2PNetworkMessage net_msg) {
			if (P2PSession.Instance == null || (P2PSession.Instance != null && !P2PSession.Instance.AmIMaster()))
				return true;

			ReplicatedLogicalPlayer playerComponent = net_msg.m_Connection.m_Peer.GetPlayerComponent<ReplicatedLogicalPlayer>();
			string Msg = net_msg.m_Reader.ReadString();
			string DisplayName = net_msg.m_Connection.m_Peer.GetDisplayName();

			__instance.StoreMessage(Msg, DisplayName, new Color?(playerComponent ? playerComponent.GetPlayerColor() : __instance.m_NormalColor));

			ChatCommands.OnChatCommand(DisplayName, Msg, (CmdMsg) => {
				__instance.StoreMessage(CmdMsg, null, Color.yellow);
				SendTextChatMessage(net_msg.m_Connection.m_Peer, CmdMsg);
			});

			return false;
		}

		static void SendTextChatMessage(P2PPeer Peer, string message) {
			P2PNetworkWriter p2PNetworkWriter = new P2PNetworkWriter();
			p2PNetworkWriter.StartMessage(10);
			p2PNetworkWriter.Write(message);
			p2PNetworkWriter.FinishMessage();
			P2PSession.Instance.SendWriterTo(Peer, p2PNetworkWriter, 1);
		}
	}
}

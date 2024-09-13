using BepInEx;
using BepInEx.Logging;
using System.Reflection;
using HarmonyLib;
using System;
using UnityEngine.Rendering;
using System.Collections;
using System.Globalization;

namespace GreenCarp {
	[BepInPlugin(MyPluginInfo.PLUGIN_GUID, MyPluginInfo.PLUGIN_NAME, MyPluginInfo.PLUGIN_VERSION)]
	public class Plugin : BaseUnityPlugin {
		internal static new ManualLogSource Logger;
		internal static Harmony HarmonyInstance;

		private void Awake() {
			// Plugin startup logic
			Logger = base.Logger;
			Logger.LogInfo($"Plugin {MyPluginInfo.PLUGIN_GUID} is loaded!");

			HarmonyInstance = Harmony.CreateAndPatchAll(Assembly.GetExecutingAssembly());

			P2PSession.MAX_PLAYERS = 8;
			GreenHellGame.DEBUG_LOAD_SAVE_ACHIEVEMENTS = true;
		}

		public static void OnGameEngineStart(GreenHellGame Game) {
			Console.WriteLine("Game engine started!");
			//CursorManager.Get().ShowCursor(true);
		}
	}

	delegate void PrintLocalFunc(string Msg);

	static class ChatCommands {
		public static bool OnChatCommand(string Sender, string Command, PrintLocalFunc Print) {
			if (Command != null)
				Command = Command.Trim();
			else
				return false;

			if (Command.Length < 2)
				return false;

			if (P2PSession.Instance == null)
				return false;

			if (Sender == "") {
				if (OnLocalCommand(Command, Print))
					return true;
			}

			if (!P2PSession.Instance.AmIMaster())
				return false;


			if (TryParseCommand(Command, out string[] Tok)) {
				if (Tok[0] == "save") {
					SaveGame.Save();
				} else {
					Print("Uknown command: " + Tok[0]);
				}

				return true;
			}

			return false;
		}

		public static bool OnLocalCommand(string Command, PrintLocalFunc Print) {
			if (TryParseCommand(Command, out string[] Tok)) {
				if (Tok[0] == "tp" && Tok.Length == 2) {
					foreach (ReplicatedLogicalPlayer player in ReplicatedLogicalPlayer.s_AllLogicalPlayers) {
						if (player.GetP2PPeer().GetDisplayName().StartsWith(Tok[1])) {
							Player.Get().Teleport(player.gameObject, false);
							break;
						}
					}

					return true;
				}
			}

			return false;
		}

		static bool TryParseCommand(string Command, out string[] Tokens) {
			if (Command.StartsWith("/")) {
				Tokens = Command.Substring(1).Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

				return Tokens.Length > 0;
			}

			Tokens = null;
			return false;
		}
	}
}

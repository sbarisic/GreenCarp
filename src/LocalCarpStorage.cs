using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;

using UnityEngine;
using System.Diagnostics;
using HarmonyLib;

namespace GreenCarp {
	// Stores fresh carp
	public class LocalCarpStorage : LocalStorage {
		public LocalCarpStorage() {

		}

		protected override string GetPath() {
			return GetBasePath();
		}

		public static string GetBasePath() {
			string Pth = Application.dataPath + "/Saves/";

			if (!Directory.Exists(Pth))
				Directory.CreateDirectory(Pth);

			return Pth;
		}
	}

	class LocalCarpStorage_Patches {
		[HarmonyPatch(typeof(LocalCarpStorage), "GetFileSize")]
		[HarmonyPrefix]
		static bool GetFileSize(LocalCarpStorage __instance, string file_name, ref int __result) {
			string full_file_name = LocalCarpStorage.GetBasePath() + file_name;

			if (!File.Exists(full_file_name)) {
				__result = 0;
				return false;
			}

			return true;
		}
	}
}

using System;
using System.Composition;
using System.Threading.Tasks;
using ArchiSteamFarm;
using ArchiSteamFarm.Plugins;
using JetBrains.Annotations;

namespace Ryzhehvost.CommandlessRedeem {
	[Export(typeof(IPlugin))]
	public sealed class CommandlessRedeem : IBotMessage {
		public string Name => nameof(CommandlessRedeem);
		public Version Version => typeof(CommandlessRedeem).Assembly.GetName().Version;

		public void OnLoaded() {
			ASF.ArchiLogger.LogGenericInfo("Commandless Redeem Plugin by Ryzhehvost, powered by ginger cats");
		}

		public async Task<string> OnBotMessage([NotNull] Bot bot, ulong steamID, [NotNull] string message) {
			if (!bot.HasPermission(steamID,BotConfig.EPermission.Operator)) {
				return null;
			}

			if (!Utilities.IsValidCdKey(message.Split((char[]) null, StringSplitOptions.RemoveEmptyEntries)[0])) {
				return null;
			}

			return await bot.Commands.Response(steamID, "r " + bot.BotName + " " + message, false).ConfigureAwait(false); 
		}
	}
}

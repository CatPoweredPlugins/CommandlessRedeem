using System;
using System.Composition;
using System.Threading.Tasks;
using ArchiSteamFarm.Core;
using ArchiSteamFarm.Steam;
using ArchiSteamFarm.Plugins.Interfaces;

namespace CommandlessRedeem {
	[Export(typeof(IPlugin))]
	public sealed class CommandlessRedeem : IBotMessage, IBotCommand2 {
		public string Name => nameof(CommandlessRedeem);
		public Version Version => typeof(CommandlessRedeem).Assembly.GetName().Version ?? new Version("0");

		public Task OnLoaded() {
			ASF.ArchiLogger.LogGenericInfo("Commandless Redeem Plugin by Ryzhehvost, powered by ginger cats");
			return Task.CompletedTask;
		}

		public async Task<string?> OnBotMessage(Bot bot, ulong steamID, string message) {
			EAccess access = bot.GetAccess(steamID);
			if (access < EAccess.Operator) {
				return null;
			}

			if (!Utilities.IsValidCdKey(message.Split((char[]?) null, StringSplitOptions.RemoveEmptyEntries)[0])) {
				return null;
			}

			return await bot.Commands.Response(access, "r " + bot.BotName + " " + message).ConfigureAwait(false);
		}

		public async Task<string?> OnBotCommand(Bot bot, EAccess access, string message, string[] args, ulong steamID = 0) => await OnBotMessage(bot, steamID, string.Join(" ", args)).ConfigureAwait(false);
	}
}

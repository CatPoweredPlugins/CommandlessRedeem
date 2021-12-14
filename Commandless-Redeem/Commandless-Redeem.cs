using System;
using System.Composition;
using System.Threading.Tasks;
using ArchiSteamFarm.Core;
using ArchiSteamFarm.Steam;
using ArchiSteamFarm.Steam.Storage;
using ArchiSteamFarm.Plugins.Interfaces;
using JetBrains.Annotations;

namespace CommandlessRedeem {
	[Export(typeof(IPlugin))]
	public sealed class CommandlessRedeem : IBotMessage, IBotCommand {
		public string Name => nameof(CommandlessRedeem);
		public Version Version => typeof(CommandlessRedeem).Assembly.GetName().Version ?? new Version("0");

		public Task OnLoaded() {
			ASF.ArchiLogger.LogGenericInfo("Commandless Redeem Plugin by Ryzhehvost, powered by ginger cats");
			return Task.CompletedTask;
		}

		public async Task<string?> OnBotMessage(Bot bot, ulong steamID, string message) {
			if (!bot.HasAccess(steamID, BotConfig.EAccess.Operator)) {
				return null;
			}

			if (!Utilities.IsValidCdKey(message.Split((char[]?) null, StringSplitOptions.RemoveEmptyEntries)[0])) {
				return null;
			}

			return await bot.Commands.Response(steamID, "r " + bot.BotName + " " + message).ConfigureAwait(false);
		}

		public async Task<string?> OnBotCommand(Bot bot, ulong steamID, string message, string[] args) => await OnBotMessage(bot, steamID, string.Join(" ", args)).ConfigureAwait(false);
	}
}

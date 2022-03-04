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

		public Task<string?> OnBotMessage(Bot bot, ulong steamID, string message) => HandleMessageInternal(bot, bot.GetAccess(steamID), message);

		public static async Task<string?> HandleMessageInternal(Bot bot, EAccess access, string message) {
			if (access < EAccess.Operator) {
				return null;
			}

			if (!Utilities.IsValidCdKey(message.Split((char[]?) null, StringSplitOptions.RemoveEmptyEntries)[0])) {
				return null;
			}

			return await bot.Commands.Response(access, "r " + bot.BotName + " " + message).ConfigureAwait(false);
		}

		public Task<string?> OnBotCommand(Bot bot, EAccess access, string message, string[] args, ulong steamID = 0) => HandleMessageInternal(bot, access, string.Join(" ", args));
	}
}

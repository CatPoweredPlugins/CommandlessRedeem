using System;
using System.Composition;
using System.Threading.Tasks;
using ArchiSteamFarm.Core;
using ArchiSteamFarm.Steam;
using ArchiSteamFarm.Localization;
using ArchiSteamFarm.Plugins.Interfaces;
using ArchiSteamFarm.Web.GitHub.Data;
using ArchiSteamFarm.Web.GitHub;
using System.Globalization;

namespace CommandlessRedeem;
[Export(typeof(IPlugin))]
internal sealed class CommandlessRedeem : IBotMessage, IBotCommand2, IGitHubPluginUpdates {
	public string Name => nameof(CommandlessRedeem);
	public Version Version => typeof(CommandlessRedeem).Assembly.GetName().Version ?? new Version("0");

	public string RepositoryName => "CatPoweredPlugins/CommandlessRedeem";

	public async Task<Uri?> GetTargetReleaseURL(Version asfVersion, string asfVariant, bool asfUpdate, bool stable, bool forced) {
		ArgumentNullException.ThrowIfNull(asfVersion);
		ArgumentException.ThrowIfNullOrEmpty(asfVariant);

		if (string.IsNullOrEmpty(RepositoryName)) {
			ASF.ArchiLogger.LogGenericError(string.Format(CultureInfo.CurrentCulture, Strings.WarningFailedWithError, nameof(RepositoryName)));

			return null;
		}

		ReleaseResponse? releaseResponse = await GitHubService.GetLatestRelease(RepositoryName, stable).ConfigureAwait(false);

		if (releaseResponse == null) {
			return null;
		}

		Version newVersion = new(releaseResponse.Tag);

		if (!(Version.Major == newVersion.Major && Version.Minor == newVersion.Minor && Version.Build == newVersion.Build) && !(asfUpdate || forced)) {
			ASF.ArchiLogger.LogGenericInfo(string.Format(CultureInfo.CurrentCulture, "New {0} plugin version {1} is only compatible with latest ASF version", Name, newVersion));
			return null;
		}


		if (Version >= newVersion & !forced) {
			ASF.ArchiLogger.LogGenericInfo(string.Format(CultureInfo.CurrentCulture, Strings.PluginUpdateNotFound, Name, Version, newVersion));

			return null;
		}

		if (releaseResponse.Assets.Count == 0) {
			ASF.ArchiLogger.LogGenericWarning(string.Format(CultureInfo.CurrentCulture, Strings.PluginUpdateNoAssetFound, Name, Version, newVersion));

			return null;
		}

		ReleaseAsset? asset = await ((IGitHubPluginUpdates) this).GetTargetReleaseAsset(asfVersion, asfVariant, newVersion, releaseResponse.Assets).ConfigureAwait(false);

		if ((asset == null) || !releaseResponse.Assets.Contains(asset)) {
			ASF.ArchiLogger.LogGenericWarning(string.Format(CultureInfo.CurrentCulture, Strings.PluginUpdateNoAssetFound, Name, Version, newVersion));

			return null;
		}

		ASF.ArchiLogger.LogGenericInfo(string.Format(CultureInfo.CurrentCulture, Strings.PluginUpdateFound, Name, Version, newVersion));

		return asset.DownloadURL;
	}

	public Task OnLoaded() {
		ASF.ArchiLogger.LogGenericInfo("Commandless Redeem Plugin by Rudokhvist, powered by ginger cats");
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

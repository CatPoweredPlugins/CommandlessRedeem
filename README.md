# ASF Commandless Redeem Plugin
This plugin for [ASF](https://github.com/JustArchiNET/ArchiSteamFarm/) allows key redeeming without `!r` (or `!redeem`) command, by simply sending them to ASF via steam chat. Works only with ASF v4.0+ (make sure to check actual required version in release notes). 

**Warning:** This plugin also works for IPC, but be extremely careful when using it this way - make sure to set [Default bot](https://github.com/JustArchiNET/ArchiSteamFarm/wiki/Configuration#defaultbot) before that, or keys will be redeemed on random bot.

To use:
- download .zip file from [latest release](https://github.com/Rudokhvist/Commandless-Redeem/releases/latest), in most cases you need `Commandless-Redeem.zip`, but if you use ASF-generic-netf.zip (you really need a strong reason to do that) download `Commandless-Redeem-netf.zip`.
- unpack downloaded .zip file to separate folder (for example `ComandlessRedeem`) inside of `plugins` folder of your ASF setup.
- (re)start ASF, you should get a message indicating that plugin loaded successfully. 


![downloads](https://img.shields.io/github/downloads/Rudokhvist/Commandless-Redeem/total.svg?style=social)

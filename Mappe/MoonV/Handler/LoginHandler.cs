using AltV.Net;
using AltV.Net.Async;
using AltV.Net.Data;
using AltV.Net.EntitySync;
using MoonV.dbmodels;
using MoonV.Factories;
using MoonV.Models;
using MoonV.Utils;
using System;
using System.Threading.Tasks;

namespace MoonV.Handler
{
    class LoginHandler : IScript
    {
        [ScriptEvent(ScriptEventType.PlayerConnect)]
        public static void Connecthandler(ClassicPlayer player, string reason)
        {
            if (player == null || !player.Exists) return;
            if (Main.serverIsStarting) { player.Kick("Server is booting..."); return; }
            player.Model = 0x3D843282;
            player.accountId = Accounts.GetAccountId(player.SocialClubId);
            player.Emit("Client:syncTime", DateTime.Now.Hour, DateTime.Now.Minute);
        }

        [ScriptEvent(ScriptEventType.PlayerDisconnect)]
        public static void Disconnecthandler(ClassicPlayer player, string reason)
        {
            if (player == null || !player.Exists) return;
            Accounts.ChangeLastPosition(player.accountId, player.Position, player.Dimension);
            player.Emit("Client:VoiceSystem:voiceConnect", false, player.accountId);
        }

        [AsyncClientEvent("Server:Login:ValidateLoginCredentials")]
        public static void ClientEvent_ValidateLoginCredentials(ClassicPlayer player, string username, string password)
        {
            if (player == null || !player.Exists || string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password)) return;

            if (!BCrypt.Net.BCrypt.Verify(password, Accounts.GetAccountPassword(username)))
            {
                HelperMethods.TriggerClientEvent(player, "Client:Login:showError", $"Das eingegebene Passwort ist falsch.");
                return;
            }

            if (Accounts.GetAccountSocialClub(username) != player.SocialClubId)
            {
                HelperMethods.TriggerClientEvent(player, "Client:Login:showError", $"Dieser Account gehört nicht dir.");
                return;
            }
            Accounts.SetCustomPlayerValues(player, username);
            player.Position = new Position(83, 813, 214);
            player.Spawn(new Position(83, 813, 214), 0);
            player.Dimension = 0;
            player.EmitLocked("Client:Login:showArea", 2);
            HelperMethods.TriggerClientEvent(player, "Client:Login:loginSuccess", AccountsSkin.ExistSkin(player.accountId), 0);
        }

        [AsyncClientEvent("Server:Charselector:loadCharacter")]
        public static async Task ClientEvent_loadCharacter(ClassicPlayer player)
        {
            if (player == null || !player.Exists || player.accountId <= 0) return;
            if (Accounts.IsAccountFirstLogin(player.accountName))
            {
                //Erster Login
                Accounts.CreateLastPosition(player.accountId, Constants.Positions.spawnPosition_Airport, 0);
                Accounts.SetAccountFirstLogin(player.accountId, false);
            }

            if (Characters.GetCharacterGender(player.accountId) == 0) player.Model = 1885233650;
            else if (Characters.GetCharacterGender(player.accountId) == 1) player.Model = 2627665880;
            HelperMethods.TriggerClientEvent(player, "Client:Charselector:setCorrectSkin", AccountsSkin.GetFacefeatures(player.accountId), AccountsSkin.GetHeadblend(player.accountId), Models.AccountsSkin.GetHeadoverlay(player.accountId));
            HelperMethods.TriggerClientEvent(player, "Client:Charselector:spawnCharacterFinal");
            Characters.SetPlayerValues(player);
            await HelperMethods.ClientEvent_setCefStatus(player, false);
            player.positionShouldBeSaved = true;
            HelperMethods.SendDiscordLog("Login", $"{Characters.GetCharacterName(player.accountId)} hat sich erfolgreich eingeloggt. AccountID: {player.accountId}", "green");
        }

        [AsyncClientEvent("Server:Register:RegisterPlayer")]
        public async void RegisterNewPlayer(ClassicPlayer player, string username, string pass, string passrepeat, string alphakey)
        {
            if (player == null || !player.Exists) return;

            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(alphakey) || string.IsNullOrWhiteSpace(pass) || string.IsNullOrWhiteSpace(passrepeat))
            {
                player.EmitLocked("Client:Login:showError", "Eines der Felder wurde nicht ordnungsgemäß ausgefüllt.");
                return;
            }

            if (Accounts.ExistAccount(username))
            {
                player.EmitLocked("Client:Login:showError", "Der eingegebene Benutzername ist bereits vergeben.");
                return;
            }

            if (pass != passrepeat)
            {
                player.EmitLocked("Client:Login:showError", "Die eingegebenen Passwörter stimmen nicht überein.");
                return;
            }

            if (Accounts.ExistSocialIdInDB(player.SocialClubId))
            {
                player.EmitLocked("Client:Login:showError", "Es existiert bereits ein Konto mit deiner Socialclub ID.");
                return;
            }

            if(!Accounts.IsAlphaKeyValid(alphakey))
            {
                player.EmitLocked("Client:Login:showError", "Dieser Alphakey existiert nicht.");
                return;
            }
            player.EmitLocked("Client:Login:showArea", 1);
            await Task.Delay(new Random().Next(2000, 2500));
            if (Accounts.ExistSocialIdInDB(player.SocialClubId) || Accounts.ExistAccount(username)) return;
            Accounts.RegisterAccount(username, pass, player.SocialClubId);
            Accounts.RemoveAlphaKey(alphakey);
        }

        [AsyncClientEvent("Server:Login:ChangePassword")]
        public static void ClientEvent_ChangePassword(ClassicPlayer player, string pass, string passrepeat)
        {
            Alt.Log($"{player.SocialClubId}");
            if (string.IsNullOrWhiteSpace(pass) || string.IsNullOrWhiteSpace(passrepeat))
            {
                player.EmitLocked("Client:Login:showError", "Eines der Felder wurde nicht ordnungsgemäß ausgefüllt.");
                return;
            }
            if (pass != passrepeat)
            {
                player.EmitLocked("Client:Login:showError", "Die eingegebenen Passwörter stimmen nicht überein.");
                return;
            }
            if (!Accounts.ExistSocialIdInDB(player.SocialClubId))
            {
                player.EmitLocked("Client:Login:showError", "Du besitzt keinen Account.");
                return;
            }
            Accounts.ChangeAccountPassword(player.SocialClubId, pass);
            player.EmitLocked("Client:Login:showError", "Das Password wurde erfolgreich geändert.");

        }
    }
}

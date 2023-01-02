using AltV.Net;
using AltV.Net.Async;
using AltV.Net.Data;
using MoonV.Factories;
using MoonV.Utils;
using System;

namespace MoonV.Handler
{
    class CharcreatorHandler : IScript
    {
        [AsyncClientEvent("Server:Charcreator:prepare")]
        public static void ClientEvent_PrepareCharcreator(ClassicPlayer player)
        {
            if (player == null || !player.Exists) return;
            player.Position = new Position((float)402.778, (float)-996.9758, (float)-96);
            player.Rotation = new Rotation(0, 0, (float)3.1168559);
        }

        [AsyncClientEvent("Server:Charcreator:CreateCharacter")]
        public static void ClientEvent_CreateCharacter(ClassicPlayer player, string firstname, string lastname, string birthday, int gender, string facefeatures, string headblend, string headoverlay)
        {
            if (player == null || !player.Exists || player.accountId <= 0) return;
            Models.Characters.CreateCharacter(player.accountId, firstname, lastname, gender, birthday);
            Models.AccountsSkin.CreateNewEntry(new dbmodels.Account_Skin { accId = player.accountId, facefeatures = facefeatures, headblendsdata = headblend, headoverlays = headoverlay, clothesArmor = "None", clothesBag = "None", clothesBracelet = "None", clothesDecal = "None", clothesEarring = "None", clothesFeet = "None", clothesGlass = "None", clothesHat = "None", clothesLeg = "None", clothesMask = "None", clothesNecklace = "None", clothesTop = "None", clothesTorso = "None", clothesUndershirt = "None", clothesWatch = "None" });
            HelperMethods.TriggerClientEvent(player, "Client:Login:loginSuccess", true, 1);
        }
    }
}
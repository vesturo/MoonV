using MoonV.dbmodels;
using MoonV.EntityStreamer;
using MoonV.Models;
using MoonV.Utils;
using System.Collections.Generic;

namespace MoonV.Database
{
    class DbHandler
    {
        public static void LoadDatabase()
        {
            LoadAllAccounts();
            LoadAllCharacters();
        }

        public static void LoadAllAccounts()
		{
			using (var db = new gtaContext())
			{
			    Accounts.Accounts_ = new List<Account>(db.Account);
                Accounts.Alphakeys_ = new List<Alphakeys>(db.Alphakeys);
                AccountsSkin.AccountsSkin_ = new List<Account_Skin>(db.Account_Skin);
				Accounts.AccountPositions_ = new List<Account_Position>(db.Account_Position);
			}
			HelperMethods.LogColored($"~lc~[MoonV]~w~ {Accounts.Accounts_.Count} Accounts geladen..");
            HelperMethods.LogColored($"~lc~[MoonV]~w~ {Accounts.AccountPositions_.Count} Account-Positions geladen..");
            HelperMethods.LogColored($"~lc~[MoonV]~w~ {AccountsSkin.AccountsSkin_.Count} Account-Skins geladen..");
		}

        public static void LoadAllCharacters()
        {
            using (var db = new gtaContext())
            {
                Characters.Characters_ = new List<Character>(db.Character);
            }
            HelperMethods.LogColored($"~lc~[MoonV]~w~ {Characters.Characters_.Count} Characters geladen..");
        }
    }
}

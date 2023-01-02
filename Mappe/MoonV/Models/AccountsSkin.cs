using AltV.Net;
using MoonV.Database;
using MoonV.dbmodels;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MoonV.Models
{
    class AccountsSkin : IScript
    {
        public static List<Account_Skin> AccountsSkin_ = new List<Account_Skin>();


        public static void CreateNewEntry(Account_Skin skin)
        {
            AccountsSkin_.Add(skin);
            using (var db = new gtaContext())
            {
                db.Account_Skin.Add(skin);
                db.SaveChanges();
            }
        }

        public static string GetFacefeatures(int accId)
        {
            var i = AccountsSkin_.ToList().FirstOrDefault(x => x.accId == accId);
            if (i != null) return i.facefeatures;
            return "[]";
        }

        public static string GetHeadblend(int accId)
        {
            var i = AccountsSkin_.ToList().FirstOrDefault(x => x.accId == accId);
            if (i != null) return i.headblendsdata;
            return "[]";
        }

        public static string GetHeadoverlay(int accId)
        {
            var i = AccountsSkin_.ToList().FirstOrDefault(x => x.accId == accId);
            if (i != null) return i.headoverlays;
            return "[]";
        }

        public static bool ExistSkin(int accId)
        {
            return AccountsSkin_.ToList().Exists(x => x.accId == accId);
        }
    }
}

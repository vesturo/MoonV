using AltV.Net;
using AltV.Net.Data;
using MoonV.Database;
using MoonV.dbmodels;
using MoonV.Factories;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MoonV.Models
{
    class Accounts
    {
        public static List<Account> Accounts_ = new List<Account>();
        public static List<Alphakeys> Alphakeys_ = new List<Alphakeys>();
        public static List<Account_Position> AccountPositions_ = new List<Account_Position>();

        public static void SetCustomPlayerValues(ClassicPlayer player, string username)
        {
            if (player == null || !player.Exists) return;
            Account account = Accounts_.ToList().FirstOrDefault(x => x.username == username);
            if (account == null) return;
            player.accountName = username;
            player.adminlevel = account.adminlevel;

        }

        public static bool ExistAccount(string username)
        {
            return Accounts_.ToList().Exists(x => x.username == username);
        }

        public static bool IsAccountFirstLogin(string name)
        {
            Account account = Accounts_.ToList().FirstOrDefault(x => x.username == name);
            if (account != null) return account.isFirstLogin;
            return false;
        }

        public static bool ExistAccountName(string name)
        {
            return Accounts_.ToList().Exists(x => x.username == name);
        }

        public static bool ExistSocialIdInDB(ulong socialId)
        {
            return Accounts_.ToList().Exists(x => x.socialId == socialId);
        }

        public static int GetAccountId(ulong socialId)
        {
            Account account = Accounts_.ToList().FirstOrDefault(x => x.socialId == socialId);
            if (account != null) return account.id;
            return 0;
        }

        public static string GetAccountPassword(string name)
        {
            Account account = Accounts_.ToList().FirstOrDefault(x => x.username == name);
            if (account != null) return account.password;
            return "";
        }

        public static ulong GetAccountSocialClub(string username)
        {
            Account account = Accounts_.ToList().FirstOrDefault(x => x.username == username);
            if (account != null) return account.socialId;
            return 0;
        }

        public static void SetAccountFirstLogin(int accId, bool isFirstLogin)
        {
            Account account = Accounts_.ToList().FirstOrDefault(x => x.id == accId);
            if (account == null) return;
            account.isFirstLogin = isFirstLogin;
            using (var db = new gtaContext())
            {
                db.Account.Update(account);
                db.SaveChanges();
            }
        }

        public static void RegisterAccount(string name, string password, ulong socialId)
        {
            if (ExistAccount(name)) return;
            Account account = new Account
            {
                username = name,
                password = BCrypt.Net.BCrypt.HashPassword(password),
                socialId = socialId,
                isFirstLogin = true,
                adminlevel = 0
            };
            Accounts_.Add(account);

            using (var db = new gtaContext())
            {
                db.Account.Add(account);
                db.SaveChanges();
            }
        }

        public static void CreateLastPosition(int accId, Position pos, int dimension)
        {
            Account_Position account_Position = new Account_Position
            {
                accId = accId,
                position = pos,
                dimension = dimension
            };
            AccountPositions_.Add(account_Position);

            using (var db = new gtaContext())
            {
                db.Account_Position.Add(account_Position);
                db.SaveChanges();
            }
        }

        public static void ChangeLastPosition(int accId, Position pos, int dimension)
        {
            Account_Position account_Position = AccountPositions_.ToList().FirstOrDefault(x => x.accId == accId);
            if (account_Position == null) return;
            account_Position.position = pos;
            account_Position.dimension = dimension;

            using (var db = new gtaContext())
            {
                db.Account_Position.Update(account_Position);
                db.SaveChanges();
            }
        }

        public static Position getLastPosition(int accId)
        {
            Account_Position account_Position = AccountPositions_.ToList().FirstOrDefault(x => x.accId == accId);
            if (account_Position != null) return account_Position.position;
            return new Position(0, 0, 0);
        }

        public static int getLastPosDimension(int accId)
        {
            Account_Position account_Position = AccountPositions_.ToList().FirstOrDefault(x => x.accId == accId);
            if (account_Position != null) return account_Position.dimension;
            return 0;
        }

        public static bool IsAlphaKeyValid(string alphakey)
        {
            var AlphaKeyArray = Alphakeys_.FirstOrDefault(x => x.Alphakey == alphakey);
            if (AlphaKeyArray != null) return true;
            return false;
        }

        public static void RemoveAlphaKey(string alphakey)
        {
            if (string.IsNullOrWhiteSpace(alphakey)) return;
            var AlphaKey = Alphakeys_.FirstOrDefault(x => x.Alphakey == alphakey);
            if (AlphaKey != null)
            {
                Alphakeys_.Remove(AlphaKey);
                using (gtaContext db = new gtaContext())
                {
                    db.Alphakeys.Remove(AlphaKey);
                    db.SaveChanges();
                }

            }
        }

        public static void ChangeAccountPassword(ulong socialId, string password)
        {
            Account account = Accounts_.ToList().FirstOrDefault(x => x.socialId == socialId);
            if (account == null) return;
            account.password = BCrypt.Net.BCrypt.HashPassword(password);
            using (var db = new gtaContext())
            {
                db.Account.Update(account);
                db.SaveChanges();
            }
        }
    }
}

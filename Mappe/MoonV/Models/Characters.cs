using AltV.Net;
using AltV.Net.Async;
using AltV.Net.Data;
using AltV.Net.Elements.Entities;
using MoonV.Database;
using MoonV.dbmodels;
using MoonV.Factories;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MoonV.Models
{
    class Characters
    {
        public static List<Character> Characters_ = new List<Character>();

        public static void CreateCharacter(int accountId, string firstname, string lastname, int gender, string birthday)
        {
            Character character = new Character
            {
                accountId = accountId,
                firstname = firstname,
                lastname = lastname,
                gender = gender,
                birthday = birthday,
                cash = 5000,
                bank = 10000,
                health = 100,
                armor = 0
            };
            Characters_.Add(character);

            using (var db = new gtaContext())
            {
                db.Character.Add(character);
                db.SaveChanges();
            }
        }

        public static string GetCharacterName(int accId)
        {
            Character character = Characters_.ToList().FirstOrDefault(x => x.accountId == accId);
            if (character != null) return $"{character.firstname} {character.lastname}";
            return "";
        }

        public static void SetPlayerValues(ClassicPlayer player)
        {
            if (player == null || player.accountId <= 0) return;
            Position pos = Accounts.getLastPosition(player.accountId);
            player.Spawn(pos, 0);
            player.Position = pos;
            player.Dimension = Accounts.getLastPosDimension(player.accountId);
            player.Health = (ushort)(GetCharacterHealth(player.accountId) + 100);
            player.Armor = (ushort)GetCharacterArmor(player.accountId);
        }

        public static void SetCharacterCash(ClassicPlayer player, int amount)
        {
            if (player == null || !player.Exists) return;
            Character character = Characters_.ToList().FirstOrDefault(x => x.accountId == player.accountId);
            if (character == null) return;
            character.cash -= amount;
        }

        public static int GetCharacterGender(int accId)
        {
            Character character = Characters_.ToList().FirstOrDefault(x => x.accountId == accId);
            if (character != null) return character.gender;
            return 0;
        }

        public static int GetCharacterCash(int accId)
        {
            Character character = Characters_.ToList().FirstOrDefault(x => x.accountId == accId);
            if (character != null) return character.cash;
            return 0;
        }

        public static int GetCharacterHealth(int accId)
        {
            Character character = Characters_.ToList().FirstOrDefault(x => x.accountId == accId);
            if (character != null) return character.health;
            return 0;
        }

        public static int GetCharacterArmor(int accId)
        {
            Character character = Characters_.ToList().FirstOrDefault(x => x.accountId == accId);
            if (character != null) return character.armor;
            return 0;
        }

        public static void SetCharacterHealth(int accId, int health)
        {
            Character character = Characters_.FirstOrDefault(p => p.accountId == accId);
            if (character != null)
            {
                character.health = health;
                using (gtaContext db = new gtaContext())
                {
                    db.Character.Update(character);
                    db.SaveChanges();
                }
            }
        }

        public static void SetCharacterArmor(int accId, int armor)
        {
            Character character = Characters_.FirstOrDefault(p => p.accountId == accId);
            if (character != null)
            {
                character.armor = armor;
                using (gtaContext db = new gtaContext())
                {
                    db.Character.Update(character);
                    db.SaveChanges();
                }
            }
        }
    }
}

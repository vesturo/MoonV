using AltV.Net.Data;
using Microsoft.EntityFrameworkCore;
using MoonV.dbmodels;
using MoonV.Utils;
using Newtonsoft.Json;

namespace MoonV.Database
{
    public partial class gtaContext : DbContext
    {
        public gtaContext() { }
        public gtaContext(DbContextOptions<gtaContext> options) : base(options) { }

        public virtual DbSet<Account> Account { get; set; }
        public virtual DbSet<Alphakeys> Alphakeys { get; set; }
        public virtual DbSet<Account_Skin> Account_Skin { get; set; }
        public virtual DbSet<Account_Position> Account_Position { get; set; }
        public virtual DbSet<Character> Character { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                //Lokal
                string connectionStr = $"server={Constants.DatabaseConfig.Host};port={Constants.DatabaseConfig.Port};user={Constants.DatabaseConfig.User};password={Constants.DatabaseConfig.Password};database={Constants.DatabaseConfig.Database}";
                optionsBuilder.UseMySql(connectionStr, ServerVersion.AutoDetect(connectionStr));
                optionsBuilder.EnableSensitiveDataLogging();
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("ProductVersion", "2.2.4-servicing-10062");

            modelBuilder.Entity<Account>(entity =>
            {
                entity.HasKey(e => e.id);
                entity.ToTable("accounts", Constants.db);
                entity.HasIndex(e => e.id).HasDatabaseName("id");
                entity.Property(e => e.id).HasColumnName("id").HasColumnType("int(11)");
                entity.Property(e => e.username).HasColumnName("username").HasMaxLength(64);
                entity.Property(e => e.password).HasColumnName("password").HasMaxLength(128);
                entity.Property(e => e.socialId).HasColumnName("socialId");
                entity.Property(e => e.adminlevel).HasColumnName("adminlevel");
                entity.Property(e => e.isFirstLogin).HasColumnName("isFirstLogin");
            });

            modelBuilder.Entity<Alphakeys>(entity =>
            {
                entity.HasKey(e => e.id);
                entity.ToTable("alphakeys", Constants.db);
                entity.HasIndex(e => e.id).HasDatabaseName("id");
                entity.Property(e => e.id).HasColumnName("id").HasColumnType("int(11)");
                entity.Property(e => e.Alphakey).HasColumnName("alphakey").HasMaxLength(64);
            });

            modelBuilder.Entity<Account_Skin>(entity =>
            {
                entity.HasKey(e => e.id);
                entity.ToTable("accounts_skin", Constants.db);
                entity.HasIndex(e => e.id).HasDatabaseName("id");
                entity.Property(e => e.id).HasColumnName("id").HasColumnType("int(11)");
                entity.Property(e => e.accId).HasColumnName("accId").HasColumnType("int(11)");
                entity.Property(e => e.facefeatures).HasColumnName("facefeatures").HasMaxLength(256);
                entity.Property(e => e.headblendsdata).HasColumnName("headblendsdata").HasMaxLength(256);
                entity.Property(e => e.headoverlays).HasColumnName("headoverlays").HasMaxLength(256);
                entity.Property(e => e.clothesTop).HasColumnName("clothesTop").HasMaxLength(128);
                entity.Property(e => e.clothesTorso).HasColumnName("clothesTorso").HasMaxLength(128);
                entity.Property(e => e.clothesLeg).HasColumnName("clothesLeg").HasMaxLength(128);
                entity.Property(e => e.clothesFeet).HasColumnName("clothesFeet").HasMaxLength(128);
                entity.Property(e => e.clothesHat).HasColumnName("clothesHat").HasMaxLength(128);
                entity.Property(e => e.clothesGlass).HasColumnName("clothesGlass").HasMaxLength(128);
                entity.Property(e => e.clothesEarring).HasColumnName("clothesEarring").HasMaxLength(128);
                entity.Property(e => e.clothesNecklace).HasColumnName("clothesNecklace").HasMaxLength(128);
                entity.Property(e => e.clothesMask).HasColumnName("clothesMask").HasMaxLength(128);
                entity.Property(e => e.clothesArmor).HasColumnName("clothesArmor").HasMaxLength(128);
                entity.Property(e => e.clothesUndershirt).HasColumnName("clothesUndershirt").HasMaxLength(128);
                entity.Property(e => e.clothesBracelet).HasColumnName("clothesBracelet").HasMaxLength(128);
                entity.Property(e => e.clothesWatch).HasColumnName("clothesWatch").HasMaxLength(128);
                entity.Property(e => e.clothesBag).HasColumnName("clothesBag").HasMaxLength(128);
                entity.Property(e => e.clothesDecal).HasColumnName("clothesDecal").HasMaxLength(128);
            });

            modelBuilder.Entity<Account_Position>(entity =>
            {
                entity.HasKey(e => e.id);
                entity.ToTable("accounts_position", Constants.db);
                entity.HasIndex(e => e.id).HasDatabaseName("id");
                entity.Property(e => e.id).HasColumnName("id").HasColumnType("int(11)");
                entity.Property(e => e.accId).HasColumnName("accId").HasColumnType("int(11)");
                entity.Property(e => e.position).HasColumnName("position").HasConversion(
                    v => JsonConvert.SerializeObject(v),
                    v => JsonConvert.DeserializeObject<Position>(v));
                entity.Property(e => e.dimension).HasColumnName("dimension").HasColumnType("int(11)");
            });

            modelBuilder.Entity<Character>(entity =>
            {
                entity.HasKey(e => e.id);
                entity.ToTable("accounts_characters", Constants.db);
                entity.HasIndex(e => e.id).HasDatabaseName("id");
                entity.Property(e => e.id).HasColumnName("id").HasColumnType("int(11)");
                entity.Property(e => e.accountId).HasColumnName("accountId").HasColumnType("int(11)");
                entity.Property(e => e.firstname).HasColumnName("firstname").HasMaxLength(64);
                entity.Property(e => e.lastname).HasColumnName("lastname").HasMaxLength(64);
                entity.Property(e => e.gender).HasColumnName("gender").HasColumnType("int(11)");
                entity.Property(e => e.birthday).HasColumnName("birthday").HasMaxLength(64);
                entity.Property(e => e.cash).HasColumnName("cash").HasColumnType("int(11)");
                entity.Property(e => e.bank).HasColumnName("bank").HasColumnType("int(11)");
                entity.Property(e => e.health).HasColumnName("health").HasColumnType("int(11)");
                entity.Property(e => e.armor).HasColumnName("armor").HasColumnType("int(11)");
            });
        }
    }
}
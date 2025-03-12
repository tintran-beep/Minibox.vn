﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Minibox.Core.Data.Database.Main;

#nullable disable

namespace Minibox.Core.Data.Database.Main.MigrationHistory
{
    [DbContext(typeof(MainDbContext))]
    [Migration("20250312074512_AddLanguage")]
    partial class AddLanguage
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "9.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Minibox.Core.Data.Database.Main.Entity.Auth.Claim", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(500)
                        .HasColumnType("nvarchar(500)");

                    b.Property<string>("Type")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("Value")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("Id");

                    b.ToTable("Claim", "auth");
                });

            modelBuilder.Entity("Minibox.Core.Data.Database.Main.Entity.Auth.Role", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(500)
                        .HasColumnType("nvarchar(500)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("Id");

                    b.ToTable("Role", "auth");
                });

            modelBuilder.Entity("Minibox.Core.Data.Database.Main.Entity.Auth.RoleClaim", b =>
                {
                    b.Property<Guid>("RoleId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("ClaimId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("RoleId", "ClaimId");

                    b.HasIndex("ClaimId");

                    b.ToTable("RoleClaim", "auth");
                });

            modelBuilder.Entity("Minibox.Core.Data.Database.Main.Entity.Auth.User", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<int?>("AccessFailedCount")
                        .HasColumnType("int");

                    b.Property<Guid?>("AvatarId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("ConcurrencyStamp")
                        .IsRequired()
                        .HasMaxLength(2147483647)
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("CreatedDate_Utc")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("DateOfBirth")
                        .HasColumnType("datetime2");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<bool>("EmailConfirmed")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bit")
                        .HasDefaultValue(false);

                    b.Property<string>("Fullname")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<DateTime?>("LockoutEndDate_Utc")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("ModifiedDate_Utc")
                        .HasColumnType("datetime2");

                    b.Property<string>("NormalizedEmail")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("NormalizedFullname")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("NormalizedUsername")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("PasswordHash")
                        .IsRequired()
                        .HasMaxLength(2147483647)
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .IsRequired()
                        .HasMaxLength(15)
                        .HasColumnType("nvarchar(15)");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bit")
                        .HasDefaultValue(false);

                    b.Property<string>("SecurityStamp")
                        .IsRequired()
                        .HasMaxLength(2147483647)
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.Property<bool>("TwoFactorEnabled")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bit")
                        .HasDefaultValue(false);

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("Id");

                    b.HasIndex("AvatarId");

                    b.ToTable("User", "auth");
                });

            modelBuilder.Entity("Minibox.Core.Data.Database.Main.Entity.Auth.UserClaim", b =>
                {
                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("ClaimId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("UserId", "ClaimId");

                    b.HasIndex("ClaimId");

                    b.ToTable("UserClaim", "auth");
                });

            modelBuilder.Entity("Minibox.Core.Data.Database.Main.Entity.Auth.UserLogin", b =>
                {
                    b.Property<string>("Provider")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("ProviderKey")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Provider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("UserLogin", "auth");
                });

            modelBuilder.Entity("Minibox.Core.Data.Database.Main.Entity.Auth.UserRole", b =>
                {
                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("RoleId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("UserRole", "auth");
                });

            modelBuilder.Entity("Minibox.Core.Data.Database.Main.Entity.Auth.UserToken", b =>
                {
                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Provider")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("TokenName")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("TokenValue")
                        .IsRequired()
                        .HasMaxLength(2147483647)
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserId", "Provider", "TokenName");

                    b.ToTable("UserToken", "auth");
                });

            modelBuilder.Entity("Minibox.Core.Data.Database.Main.Entity.Default.Media", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("Type")
                        .HasColumnType("int");

                    b.Property<string>("Url")
                        .IsRequired()
                        .HasMaxLength(1000)
                        .HasColumnType("nvarchar(1000)");

                    b.HasKey("Id");

                    b.ToTable("Media", "dbo");
                });

            modelBuilder.Entity("Minibox.Core.Data.Database.Main.Entity.Lang.Language", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Code")
                        .IsRequired()
                        .HasMaxLength(4)
                        .HasColumnType("nvarchar(4)");

                    b.Property<string>("Value")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("Id");

                    b.ToTable("Language", "lang");
                });

            modelBuilder.Entity("Minibox.Core.Data.Database.Main.Entity.Lang.LanguageKey", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("DefaultValue")
                        .IsRequired()
                        .HasMaxLength(1000)
                        .HasColumnType("nvarchar(1000)");

                    b.Property<string>("Key")
                        .IsRequired()
                        .HasMaxLength(250)
                        .HasColumnType("nvarchar(250)");

                    b.HasKey("Id");

                    b.HasIndex("Key");

                    b.ToTable("LanguageKey", "lang");
                });

            modelBuilder.Entity("Minibox.Core.Data.Database.Main.Entity.Lang.LanguageTranslation", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Code")
                        .IsRequired()
                        .HasMaxLength(4)
                        .HasColumnType("nvarchar(4)");

                    b.Property<Guid>("LanguageKeyId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Value")
                        .IsRequired()
                        .HasMaxLength(1000)
                        .HasColumnType("nvarchar(1000)");

                    b.HasKey("Id");

                    b.HasIndex("LanguageKeyId");

                    b.ToTable("LanguageTranslation", "lang");
                });

            modelBuilder.Entity("Minibox.Core.Data.Database.Main.Entity.Auth.RoleClaim", b =>
                {
                    b.HasOne("Minibox.Core.Data.Database.Main.Entity.Auth.Claim", "Claim")
                        .WithMany("RoleClaims")
                        .HasForeignKey("ClaimId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Minibox.Core.Data.Database.Main.Entity.Auth.Role", "Role")
                        .WithMany("RoleClaims")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Claim");

                    b.Navigation("Role");
                });

            modelBuilder.Entity("Minibox.Core.Data.Database.Main.Entity.Auth.User", b =>
                {
                    b.HasOne("Minibox.Core.Data.Database.Main.Entity.Default.Media", "Avartar")
                        .WithMany("Users")
                        .HasForeignKey("AvatarId")
                        .OnDelete(DeleteBehavior.SetNull);

                    b.Navigation("Avartar");
                });

            modelBuilder.Entity("Minibox.Core.Data.Database.Main.Entity.Auth.UserClaim", b =>
                {
                    b.HasOne("Minibox.Core.Data.Database.Main.Entity.Auth.Claim", "Claim")
                        .WithMany("UserClaims")
                        .HasForeignKey("ClaimId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Minibox.Core.Data.Database.Main.Entity.Auth.User", "User")
                        .WithMany("UserClaims")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Claim");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Minibox.Core.Data.Database.Main.Entity.Auth.UserLogin", b =>
                {
                    b.HasOne("Minibox.Core.Data.Database.Main.Entity.Auth.User", "User")
                        .WithMany("UserLogins")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("Minibox.Core.Data.Database.Main.Entity.Auth.UserRole", b =>
                {
                    b.HasOne("Minibox.Core.Data.Database.Main.Entity.Auth.Role", "Role")
                        .WithMany("UserRoles")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Minibox.Core.Data.Database.Main.Entity.Auth.User", "User")
                        .WithMany("UserRoles")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Role");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Minibox.Core.Data.Database.Main.Entity.Auth.UserToken", b =>
                {
                    b.HasOne("Minibox.Core.Data.Database.Main.Entity.Auth.User", "User")
                        .WithMany("UserTokens")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("Minibox.Core.Data.Database.Main.Entity.Lang.LanguageTranslation", b =>
                {
                    b.HasOne("Minibox.Core.Data.Database.Main.Entity.Lang.LanguageKey", "LanguageKey")
                        .WithMany("LanguageTranslations")
                        .HasForeignKey("LanguageKeyId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("LanguageKey");
                });

            modelBuilder.Entity("Minibox.Core.Data.Database.Main.Entity.Auth.Claim", b =>
                {
                    b.Navigation("RoleClaims");

                    b.Navigation("UserClaims");
                });

            modelBuilder.Entity("Minibox.Core.Data.Database.Main.Entity.Auth.Role", b =>
                {
                    b.Navigation("RoleClaims");

                    b.Navigation("UserRoles");
                });

            modelBuilder.Entity("Minibox.Core.Data.Database.Main.Entity.Auth.User", b =>
                {
                    b.Navigation("UserClaims");

                    b.Navigation("UserLogins");

                    b.Navigation("UserRoles");

                    b.Navigation("UserTokens");
                });

            modelBuilder.Entity("Minibox.Core.Data.Database.Main.Entity.Default.Media", b =>
                {
                    b.Navigation("Users");
                });

            modelBuilder.Entity("Minibox.Core.Data.Database.Main.Entity.Lang.LanguageKey", b =>
                {
                    b.Navigation("LanguageTranslations");
                });
#pragma warning restore 612, 618
        }
    }
}

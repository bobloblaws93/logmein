﻿// <auto-generated />
using System;
using CardsAPI.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace CardsAPI.Migrations
{
    [DbContext(typeof(CardsContext))]
    [Migration("20190721060543_added-autogenkey-card")]
    partial class addedautogenkeycard
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.11-servicing-32099")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("CardsAPI.models.Card", b =>
                {
                    b.Property<int>("card_id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("deck_id");

                    b.Property<int>("face");

                    b.Property<int?>("player_id");

                    b.Property<int>("position");

                    b.Property<int>("suit");

                    b.Property<int>("value");

                    b.HasKey("card_id");

                    b.HasIndex("deck_id");

                    b.HasIndex("player_id");

                    b.ToTable("Cards");
                });

            modelBuilder.Entity("CardsAPI.models.Deck", b =>
                {
                    b.Property<int>("deck_id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("game_id");

                    b.HasKey("deck_id");

                    b.HasIndex("game_id");

                    b.ToTable("Decks");
                });

            modelBuilder.Entity("CardsAPI.models.Game", b =>
                {
                    b.Property<int>("game_id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.HasKey("game_id");

                    b.ToTable("Games");
                });

            modelBuilder.Entity("CardsAPI.models.Player", b =>
                {
                    b.Property<int>("player_id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name");

                    b.Property<int?>("game_id");

                    b.Property<int>("value");

                    b.HasKey("player_id");

                    b.HasIndex("game_id");

                    b.ToTable("Players");
                });

            modelBuilder.Entity("CardsAPI.models.Card", b =>
                {
                    b.HasOne("CardsAPI.models.Deck", "deck")
                        .WithMany("cards")
                        .HasForeignKey("deck_id")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("CardsAPI.models.Player", "player")
                        .WithMany("cards")
                        .HasForeignKey("player_id");
                });

            modelBuilder.Entity("CardsAPI.models.Deck", b =>
                {
                    b.HasOne("CardsAPI.models.Game", "game")
                        .WithMany("decks")
                        .HasForeignKey("game_id")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("CardsAPI.models.Player", b =>
                {
                    b.HasOne("CardsAPI.models.Game")
                        .WithMany("players")
                        .HasForeignKey("game_id");
                });
#pragma warning restore 612, 618
        }
    }
}

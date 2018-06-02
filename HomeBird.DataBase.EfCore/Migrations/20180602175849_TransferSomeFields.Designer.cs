﻿// <auto-generated />
using HomeBird.Common;
using HomeBird.DataBase.EfCore.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore.Storage.Internal;
using System;

namespace HomeBird.DataBase.EfCore.Migrations
{
    [DbContext(typeof(HomeBirdContext))]
    [Migration("20180602175849_TransferSomeFields")]
    partial class TransferSomeFields
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.0.1-rtm-125")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("HomeBird.DataBase.EfCore.Models.HbBroods", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTimeOffset>("BroodDate");

                    b.Property<int>("Count");

                    b.Property<DateTimeOffset>("CreationDate");

                    b.Property<int>("DeadCount");

                    b.Property<decimal>("DeadPercent");

                    b.Property<int>("EmptyCount");

                    b.Property<decimal>("EmptyPercent");

                    b.Property<bool>("IsDeleted");

                    b.Property<int>("LotId");

                    b.Property<decimal>("Percent");

                    b.Property<decimal>("PlacePrice");

                    b.HasKey("Id");

                    b.HasIndex("LotId");

                    b.ToTable("Broods");
                });

            modelBuilder.Entity("HomeBird.DataBase.EfCore.Models.HbIncubators", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<bool>("IsDeleted");

                    b.Property<string>("Title")
                        .HasMaxLength(1024);

                    b.HasKey("Id");

                    b.ToTable("Incubators");
                });

            modelBuilder.Entity("HomeBird.DataBase.EfCore.Models.HbLayings", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("Count");

                    b.Property<DateTimeOffset>("CreationDate");

                    b.Property<int>("IncubatorId");

                    b.Property<bool>("IsDeleted");

                    b.Property<DateTimeOffset>("LayingDate");

                    b.Property<int>("LotId");

                    b.HasKey("Id");

                    b.HasIndex("IncubatorId");

                    b.HasIndex("LotId");

                    b.ToTable("Layings");
                });

            modelBuilder.Entity("HomeBird.DataBase.EfCore.Models.HbLots", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<decimal?>("AvgAdultPrice");

                    b.Property<decimal?>("AvgDailyPrice");

                    b.Property<DateTime>("CreationDate");

                    b.Property<decimal?>("EggPrice");

                    b.Property<string>("Identifier")
                        .HasMaxLength(1024);

                    b.Property<bool>("IsDeleted");

                    b.Property<int?>("Loses");

                    b.Property<decimal?>("Profit");

                    b.Property<int?>("SoldCount");

                    b.Property<int>("Year");

                    b.HasKey("Id");

                    b.ToTable("Lots");
                });

            modelBuilder.Entity("HomeBird.DataBase.EfCore.Models.HbOverheads", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<decimal>("Amount");

                    b.Property<string>("Comment")
                        .HasMaxLength(2048);

                    b.Property<DateTimeOffset>("CreationDate");

                    b.Property<bool>("IsDeleted");

                    b.Property<int>("LotId");

                    b.Property<DateTimeOffset>("OverheadDate");

                    b.HasKey("Id");

                    b.HasIndex("LotId");

                    b.ToTable("Overheads");
                });

            modelBuilder.Entity("HomeBird.DataBase.EfCore.Models.HbPurchases", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Address")
                        .HasMaxLength(2048);

                    b.Property<decimal>("Amount");

                    b.Property<int>("Count");

                    b.Property<DateTimeOffset>("CreationDate");

                    b.Property<bool>("IsDeleted");

                    b.Property<int>("LotId");

                    b.Property<DateTimeOffset>("PurchaseDate");

                    b.HasKey("Id");

                    b.HasIndex("LotId");

                    b.ToTable("Purchases");
                });

            modelBuilder.Entity("HomeBird.DataBase.EfCore.Models.HbSales", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<decimal>("Amount");

                    b.Property<string>("Buyer")
                        .HasMaxLength(2048);

                    b.Property<string>("Comment")
                        .HasMaxLength(2048);

                    b.Property<int>("Count");

                    b.Property<bool>("IsDeleted");

                    b.Property<int>("LotId");

                    b.Property<DateTimeOffset>("SaleDate");

                    b.Property<int>("Type");

                    b.HasKey("Id");

                    b.HasIndex("LotId");

                    b.ToTable("Sales");
                });

            modelBuilder.Entity("HomeBird.DataBase.EfCore.Models.HbBroods", b =>
                {
                    b.HasOne("HomeBird.DataBase.EfCore.Models.HbLots", "Lot")
                        .WithMany("Broods")
                        .HasForeignKey("LotId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("HomeBird.DataBase.EfCore.Models.HbLayings", b =>
                {
                    b.HasOne("HomeBird.DataBase.EfCore.Models.HbIncubators", "Incubator")
                        .WithMany()
                        .HasForeignKey("IncubatorId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("HomeBird.DataBase.EfCore.Models.HbLots", "Lot")
                        .WithMany("Layings")
                        .HasForeignKey("LotId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("HomeBird.DataBase.EfCore.Models.HbOverheads", b =>
                {
                    b.HasOne("HomeBird.DataBase.EfCore.Models.HbLots", "Lot")
                        .WithMany("Overheads")
                        .HasForeignKey("LotId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("HomeBird.DataBase.EfCore.Models.HbPurchases", b =>
                {
                    b.HasOne("HomeBird.DataBase.EfCore.Models.HbLots", "Lot")
                        .WithMany("Purchases")
                        .HasForeignKey("LotId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("HomeBird.DataBase.EfCore.Models.HbSales", b =>
                {
                    b.HasOne("HomeBird.DataBase.EfCore.Models.HbLots", "Lot")
                        .WithMany("Sales")
                        .HasForeignKey("LotId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}

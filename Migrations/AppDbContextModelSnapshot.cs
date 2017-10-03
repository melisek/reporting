﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore.Storage.Internal;
using System;
using szakdoga.Data;

namespace szakdoga.Migrations
{
    [DbContext(typeof(AppDbContext))]
    partial class AppDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.0.0-rtm-26452")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("szakdoga.Models.Dashboard", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("CreationDate");

                    b.Property<DateTime>("ModifyDate");

                    b.Property<string>("Name");

                    b.Property<string>("Style");

                    b.HasKey("Id");

                    b.ToTable("Dashboards");
                });

            modelBuilder.Entity("szakdoga.Models.Query", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("NextUpdating");

                    b.Property<int>("ResultTableName");

                    b.Property<string>("SQL");

                    b.Property<string>("TranslatedColumnNames");

                    b.Property<long>("UpdatePeriodTicks");

                    b.HasKey("Id");

                    b.ToTable("Query");
                });

            modelBuilder.Entity("szakdoga.Models.RiporDashboardRel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int?>("DashboardId");

                    b.Property<string>("Position");

                    b.Property<int?>("RiportId");

                    b.HasKey("Id");

                    b.HasIndex("DashboardId");

                    b.HasIndex("RiportId");

                    b.ToTable("RiporDashboardRel");
                });

            modelBuilder.Entity("szakdoga.Models.Riport", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("CreationDate");

                    b.Property<DateTime>("ModifyDate");

                    b.Property<string>("Name");

                    b.Property<int?>("QueryId");

                    b.Property<string>("Style");

                    b.HasKey("Id");

                    b.HasIndex("QueryId");

                    b.ToTable("Riport");
                });

            modelBuilder.Entity("szakdoga.Models.RiportUserRel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("AuthoryLayer");

                    b.Property<int?>("RiportId");

                    b.Property<int?>("UserId");

                    b.HasKey("Id");

                    b.HasIndex("RiportId");

                    b.HasIndex("UserId");

                    b.ToTable("RiportUserRel");
                });

            modelBuilder.Entity("szakdoga.Models.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("EmailAddress");

                    b.Property<string>("Name");

                    b.Property<string>("Password");

                    b.HasKey("Id");

                    b.ToTable("User");
                });

            modelBuilder.Entity("szakdoga.Models.RiporDashboardRel", b =>
                {
                    b.HasOne("szakdoga.Models.Dashboard", "Dashboard")
                        .WithMany()
                        .HasForeignKey("DashboardId");

                    b.HasOne("szakdoga.Models.Riport", "Riport")
                        .WithMany()
                        .HasForeignKey("RiportId");
                });

            modelBuilder.Entity("szakdoga.Models.Riport", b =>
                {
                    b.HasOne("szakdoga.Models.Query", "Query")
                        .WithMany()
                        .HasForeignKey("QueryId");
                });

            modelBuilder.Entity("szakdoga.Models.RiportUserRel", b =>
                {
                    b.HasOne("szakdoga.Models.Riport", "Riport")
                        .WithMany()
                        .HasForeignKey("RiportId");

                    b.HasOne("szakdoga.Models.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId");
                });
#pragma warning restore 612, 618
        }
    }
}

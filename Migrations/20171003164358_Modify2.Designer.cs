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
    [Migration("20171003164358_Modify2")]
    partial class Modify2
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.0.0-rtm-26452")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("szakdoga.Models.Dashboard", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("CreationDate")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("ModifyDate")
                        .ValueGeneratedOnAddOrUpdate();

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

            modelBuilder.Entity("szakdoga.Models.Report", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("CreationDate")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("ModifyDate")
                        .ValueGeneratedOnAddOrUpdate();

                    b.Property<string>("Name");

                    b.Property<int?>("QueryId");

                    b.Property<string>("Style");

                    b.HasKey("Id");

                    b.HasIndex("QueryId");

                    b.ToTable("Riport");
                });

            modelBuilder.Entity("szakdoga.Models.ReportDashboardRel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int?>("DashboardId");

                    b.Property<string>("Position");

                    b.Property<int?>("ReportId");

                    b.HasKey("Id");

                    b.HasIndex("DashboardId");

                    b.HasIndex("ReportId");

                    b.ToTable("RiporDashboardRel");
                });

            modelBuilder.Entity("szakdoga.Models.ReportUserRel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("AuthoryLayer");

                    b.Property<int?>("ReportId");

                    b.Property<int?>("UserId");

                    b.HasKey("Id");

                    b.HasIndex("ReportId");

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

            modelBuilder.Entity("szakdoga.Models.Report", b =>
                {
                    b.HasOne("szakdoga.Models.Query", "Query")
                        .WithMany()
                        .HasForeignKey("QueryId");
                });

            modelBuilder.Entity("szakdoga.Models.ReportDashboardRel", b =>
                {
                    b.HasOne("szakdoga.Models.Dashboard", "Dashboard")
                        .WithMany()
                        .HasForeignKey("DashboardId");

                    b.HasOne("szakdoga.Models.Report", "Report")
                        .WithMany()
                        .HasForeignKey("ReportId");
                });

            modelBuilder.Entity("szakdoga.Models.ReportUserRel", b =>
                {
                    b.HasOne("szakdoga.Models.Report", "Report")
                        .WithMany()
                        .HasForeignKey("ReportId");

                    b.HasOne("szakdoga.Models.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId");
                });
#pragma warning restore 612, 618
        }
    }
}

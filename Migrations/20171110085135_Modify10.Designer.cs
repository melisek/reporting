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
    [Migration("20171110085135_Modify10")]
    partial class Modify10
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
                        .ValueGeneratedOnAdd()
                        .HasDefaultValueSql("getdate()");

                    b.Property<bool>("Deleted")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValue(false);

                    b.Property<string>("GUID")
                        .IsRequired();

                    b.Property<DateTime>("ModifyDate")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValueSql("getdate()");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(200);

                    b.Property<string>("Style");

                    b.HasKey("Id");

                    b.HasAlternateKey("GUID")
                        .HasName("AlternateKey_DashBoard_GUID");

                    b.ToTable("Dashboards");
                });

            modelBuilder.Entity("szakdoga.Models.Query", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("CreationDate")
                        .ValueGeneratedOnAdd();

                    b.Property<bool>("Deleted")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValue(false);

                    b.Property<string>("GUID")
                        .IsRequired();

                    b.Property<DateTime>("ModifyDate")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(200);

                    b.Property<DateTime>("NextUpdating");

                    b.Property<string>("ResultTableName")
                        .HasMaxLength(200);

                    b.Property<string>("SQL");

                    b.Property<string>("TranslatedColumnNames");

                    b.Property<long>("UpdatePeriodTicks");

                    b.HasKey("Id");

                    b.HasAlternateKey("GUID")
                        .HasName("AlternateKey_Query_GUID");

                    b.ToTable("Query");
                });

            modelBuilder.Entity("szakdoga.Models.Report", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Columns");

                    b.Property<DateTime>("CreationDate")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValueSql("getdate()");

                    b.Property<bool>("Deleted")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValue(false);

                    b.Property<string>("Filter");

                    b.Property<string>("GUID")
                        .IsRequired();

                    b.Property<DateTime>("ModifyDate")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValueSql("getdate()");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(200);

                    b.Property<int?>("QueryId");

                    b.Property<int>("Rows");

                    b.Property<string>("Sort");

                    b.Property<string>("Style");

                    b.HasKey("Id");

                    b.HasAlternateKey("GUID")
                        .HasName("AlternateKey_Report_GUID");

                    b.HasIndex("QueryId");

                    b.ToTable("Report");
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

                    b.ToTable("ReportDashboardRel");
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

                    b.ToTable("ReportUserRel");
                });

            modelBuilder.Entity("szakdoga.Models.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("CreationDate")
                        .ValueGeneratedOnAdd();

                    b.Property<bool>("Deleted")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValue(false);

                    b.Property<string>("EmailAddress");

                    b.Property<string>("GUID")
                        .IsRequired();

                    b.Property<DateTime>("ModifyDate")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(200);

                    b.Property<string>("Password")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasAlternateKey("GUID")
                        .HasName("AlternateKey_User_GUID");

                    b.ToTable("User");
                });

            modelBuilder.Entity("szakdoga.Models.UserDashboardRel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("AuthoryLayer");

                    b.Property<int?>("DashboardId");

                    b.Property<int?>("UserId");

                    b.HasKey("Id");

                    b.HasIndex("DashboardId");

                    b.HasIndex("UserId");

                    b.ToTable("UserDashboardRel");
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

            modelBuilder.Entity("szakdoga.Models.UserDashboardRel", b =>
                {
                    b.HasOne("szakdoga.Models.Dashboard", "Dashboard")
                        .WithMany()
                        .HasForeignKey("DashboardId");

                    b.HasOne("szakdoga.Models.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId");
                });
#pragma warning restore 612, 618
        }
    }
}

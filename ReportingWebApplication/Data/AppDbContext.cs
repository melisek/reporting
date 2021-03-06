﻿using Microsoft.EntityFrameworkCore;
using szakdoga.Models;
using szakdoga.Models.Entities;

namespace szakdoga.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        { }

        /// <summary>
        /// https://docs.microsoft.com/en-us/ef/core/modeling/relational/default-values
        /// </summary>
        /// <param name="modelBuilder"></param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Report>().Property(b => b.CreationDate).HasDefaultValueSql("getdate()");
            modelBuilder.Entity<Report>().Property(b => b.ModifyDate).HasDefaultValueSql("getdate()");
            modelBuilder.Entity<Dashboard>().Property(b => b.CreationDate).HasDefaultValueSql("getdate()");
            modelBuilder.Entity<Dashboard>().Property(b => b.ModifyDate).HasDefaultValueSql("getdate()");
            modelBuilder.Entity<User>().Property(b => b.CreationDate).HasDefaultValueSql("getdate()");
            modelBuilder.Entity<User>().Property(b => b.ModifyDate).HasDefaultValueSql("getdate()");
            modelBuilder.Entity<Query>().Property(b => b.CreationDate).HasDefaultValueSql("getdate()");
            modelBuilder.Entity<Query>().Property(b => b.ModifyDate).HasDefaultValueSql("getdate()");

            modelBuilder.Entity<Dashboard>().Property(b => b.Deleted).HasDefaultValue(false);
            modelBuilder.Entity<Query>().Property(b => b.Deleted).HasDefaultValue(false);
            modelBuilder.Entity<Report>().Property(b => b.Deleted).HasDefaultValue(false);
            modelBuilder.Entity<User>().Property(b => b.Deleted).HasDefaultValue(false);

            // unique constrain
            modelBuilder.Entity<Dashboard>().HasAlternateKey(x => x.DashBoardGUID).HasName("AlternateKey_DashBoard_GUID");
            modelBuilder.Entity<Query>().HasAlternateKey(x => x.QueryGUID).HasName("AlternateKey_Query_GUID");
            modelBuilder.Entity<Report>().HasAlternateKey(x => x.ReportGUID).HasName("AlternateKey_Report_GUID");
            modelBuilder.Entity<User>().HasAlternateKey(x => x.UserGUID).HasName("AlternateKey_User_GUID");
            modelBuilder.Entity<User>().HasAlternateKey(x => x.EmailAddress).HasName("AlternateKey_User_EmailAddress");
        }

        public DbSet<Dashboard> Dashboards { get; set; }
        public DbSet<Query> Query { get; set; }
        public DbSet<ReportDashboardRel> ReportDashboardRel { get; set; }
        public DbSet<Report> Report { get; set; }
        public DbSet<ReportUserRel> ReportUserRel { get; set; }
        public DbSet<User> User { get; set; }
        public DbSet<UserDashboardRel> UserDashboardRel { get; set; }
        public DbSet<UserJwtMap> UserJwtMap { get; set; }
    }
}
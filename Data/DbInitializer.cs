﻿using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Generic;
using System.Linq;
using szakdoga.Models;

namespace szakdoga.Data
{
    public class DbInitializer
    {
        public static void Seed(IApplicationBuilder applicationBuilder)
        {
            //TODO: megnézni h ezzel mért nem működik //AppDbContext context = applicationBuilder.ApplicationServices.GetRequiredService<AppDbContext>();
            using (var serviceScope = applicationBuilder.ApplicationServices.GetRequiredService<IServiceScopeFactory>()
                .CreateScope())
            {
                AppDbContext context = serviceScope.ServiceProvider.GetService<AppDbContext>();

                CleanAllTables(context);

                if (!context.User.Any())
                {
                    context.User.AddRange(Users);
                }
                context.SaveChanges();
            }
        }

        static List<User> Users
        {
            get
            {
                return new List<User>
                {
                    new User{ Name="Admin", Password="admin", EmailAddress="asd@asd.com"},
                    new User{ Name="Teszt", Password="teszt",EmailAddress="teszt@teszt.com"}
                };
            }
        }

        //static Query Query1 { get =>  new Query{SQL="select * from User",  }; }



        private static void CleanAllTables(AppDbContext context)
        {
            context.ReportDashboardRel.RemoveRange(context.ReportDashboardRel.ToList());
            context.ReportUserRel.RemoveRange(context.ReportUserRel.ToList());
            context.UserDashboardRel.RemoveRange(context.UserDashboardRel.ToList());
            context.Dashboards.RemoveRange(context.Dashboards.ToList());
            context.Report.RemoveRange(context.Report.ToList());
            context.User.RemoveRange(context.User.ToList());
            context.Query.RemoveRange(context.Query.ToList());
        }
    }
}
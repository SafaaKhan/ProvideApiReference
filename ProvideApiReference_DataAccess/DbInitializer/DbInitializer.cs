﻿using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using ProvideApiReference_DataAccess.Data;
using ProvideApiReference_Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProvideApiReference_DataAccess.DbInitializer
{
    public class DbInitializer : IDbInitializer
    {
        private readonly ApplicationDbContext _db;
        private readonly ILogger<DbInitializer> _logger;
        private readonly UserManager<ApplicationUser> _userManager;

        public DbInitializer(ApplicationDbContext db, ILogger<DbInitializer> logger,UserManager<ApplicationUser> userManager)
        {
            _db = db;
           _logger = logger;
            _userManager = userManager;
        }
        public async Task Initialize()
        {
            try
            {
                // migrations if they are not applied
                if (_db.Database.GetPendingMigrations().Count() > 0)
                {
                    _db.Database.Migrate();
                }
                await Seed.SeedDataAsync(_db,_userManager);
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, "An error occured during the migration");
            }

            //create roles if they are not created
            //if roles are not created, we will create the admin role as well
            return;

        }
    }
}

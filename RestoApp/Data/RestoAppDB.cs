using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using RestoApp.Models;

namespace RestoApp.Data
{
    public class RestoAppDB: DbContext
    {
        public RestoAppDB(DbContextOptions<RestoAppDB> options) : base(options) 
        { 


        }

       public DbSet<Employee> Employees { get; set; }

        public DbSet<Area> Areas { get; set; }

       public DbSet<Task> Tasks { get; set; }

    }
}

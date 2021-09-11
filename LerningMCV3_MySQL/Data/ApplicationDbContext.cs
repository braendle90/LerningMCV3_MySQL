using LerningMCV3_MySQL.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using LerningMCV3_MySQL.ViewModel;

namespace LerningMCV3_MySQL.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<Person> Persons{ get; set; }
        public DbSet<Adress> Adresses { get; set; }
        public DbSet<Pet> Pets { get; set; }
        public DbSet<LerningMCV3_MySQL.ViewModel.PersonViewModel> PersonViewModel { get; set; }

    }
}

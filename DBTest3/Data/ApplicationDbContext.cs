﻿using DBTest3.Data.Entity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace DBTest3.Data
{
    public class ApplicationDbContext : IdentityDbContext<User, Role, string>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<User> users { get; set; }
        public DbSet<Role> roles { get; set; }
        public DbSet<Location> locations { get; set; }
        public DbSet<Companies> companies { get; set; }
        public DbSet<Projects> projects { get; set; }
        public DbSet<Tickets> tickets { get; set; }
        public DbSet<TicketStatus> ticketStatuses { get; set; }
    }
}
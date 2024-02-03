﻿using IdentityF.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace IdentityF.Data
{
    public class IdentityFContext : DbContext
    {
        public IdentityFContext(DbContextOptions<IdentityFContext> contextOptions) : base(contextOptions)
        {

        }

        public DbSet<User> Users { get; set; }
        public DbSet<Password> Passwords { get; set; }
        public DbSet<Block> Blocks { get; set; }
        public DbSet<Mfa> Mfas { get; set; }
        public DbSet<Confirm> Confirms { get; set; }
        public DbSet<Contact> Contacts { get; set; }
    }
}

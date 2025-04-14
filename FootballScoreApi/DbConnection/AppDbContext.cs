﻿using FootballScoreApp.Entities;
using Microsoft.EntityFrameworkCore;

namespace FootballScoreApp.DbConnection
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) {}

        public DbSet<User> Users { get; set; }
    }
}

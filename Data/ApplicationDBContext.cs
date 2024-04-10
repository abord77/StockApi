﻿using System;
using LearningApi.Models;
using Microsoft.EntityFrameworkCore;


namespace LearningApi.Data {
    public class ApplicationDBContext : DbContext {
        public ApplicationDBContext(DbContextOptions dbContextOptions) : base(dbContextOptions) {
            
        }

        public DbSet<Stock> Stocks { get; set; }
        public DbSet<Comment> Comments { get; set; }
    }
}

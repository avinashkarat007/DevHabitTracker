﻿using DevHabitTracker.Entities;
using Microsoft.EntityFrameworkCore;

namespace DevHabitTracker.Database
{
    public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options): DbContext(options)
    {
        public DbSet<Habit> Habits { get; set; }

        public DbSet<Tag> Tags { get; set; }

        public DbSet<HabitTag> HabitTags { get; set; }

        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Tag>()
                        .HasIndex(t => t.Name)
                        .IsUnique();

            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<HabitTag>()
                .HasKey(ht => new { ht.HabitId, ht.TagId }); // 👈 Composite key

            modelBuilder.Entity<HabitTag>()
                .HasOne(ht => ht.Habit)
                .WithMany(h => h.HabitTags)
                .HasForeignKey(ht => ht.HabitId);

            modelBuilder.Entity<HabitTag>()
                .HasOne(ht => ht.Tag)
                .WithMany(t => t.HabitTags)
                .HasForeignKey(ht => ht.TagId);
        }

    }
}

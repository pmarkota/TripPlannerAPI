using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace API.Models
{
    public partial class postgresContext : DbContext
    {
        public postgresContext()
        {
        }

        public postgresContext(DbContextOptions<postgresContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Activity> Activities { get; set; } = null!;
        public virtual DbSet<AppUser> AppUsers { get; set; } = null!;
        public virtual DbSet<Budget> Budgets { get; set; } = null!;
        public virtual DbSet<Itinerary> Itineraries { get; set; } = null!;
        public virtual DbSet<PackingList> PackingLists { get; set; } = null!;
        public virtual DbSet<Trip> Trips { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseNpgsql("User Id=postgres.gbjswwsxpabhtelxlgdd;Password=YoIfGlvckU2r3RH0;Server=aws-0-eu-central-1.pooler.supabase.com;Port=5432;Database=postgres;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasPostgresEnum("auth", "aal_level", new[] { "aal1", "aal2", "aal3" })
                .HasPostgresEnum("auth", "code_challenge_method", new[] { "s256", "plain" })
                .HasPostgresEnum("auth", "factor_status", new[] { "unverified", "verified" })
                .HasPostgresEnum("auth", "factor_type", new[] { "totp", "webauthn" })
                .HasPostgresEnum("pgsodium", "key_status", new[] { "default", "valid", "invalid", "expired" })
                .HasPostgresEnum("pgsodium", "key_type", new[] { "aead-ietf", "aead-det", "hmacsha512", "hmacsha256", "auth", "shorthash", "generichash", "kdf", "secretbox", "secretstream", "stream_xchacha20" })
                .HasPostgresExtension("extensions", "pg_stat_statements")
                .HasPostgresExtension("extensions", "pgcrypto")
                .HasPostgresExtension("extensions", "pgjwt")
                .HasPostgresExtension("extensions", "uuid-ossp")
                .HasPostgresExtension("graphql", "pg_graphql")
                .HasPostgresExtension("pgsodium", "pgsodium")
                .HasPostgresExtension("vault", "supabase_vault");

            modelBuilder.Entity<Activity>(entity =>
            {
                entity.HasComment("activity table");
            });

            modelBuilder.Entity<AppUser>(entity =>
            {
                entity.HasKey(e => e.UserId)
                    .HasName("AppUsers_pkey");

                entity.HasComment("users table");

                entity.Property(e => e.CreatedAt).HasDefaultValueSql("now()");
            });

            modelBuilder.Entity<Budget>(entity =>
            {
                entity.HasComment("budgets table");

                entity.HasOne(d => d.Trip)
                    .WithMany(p => p.Budgets)
                    .HasForeignKey(d => d.TripId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("public_Budgets_trip_id_fkey");
            });

            modelBuilder.Entity<Itinerary>(entity =>
            {
                entity.HasComment("Itinerary Table");

                entity.HasOne(d => d.Activity)
                    .WithMany(p => p.Itineraries)
                    .HasForeignKey(d => d.ActivityId)
                    .HasConstraintName("public_Itineraries_activity_id_fkey");

                entity.HasOne(d => d.Trip)
                    .WithMany(p => p.Itineraries)
                    .HasForeignKey(d => d.TripId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("public_Itineraries_trip_id_fkey");
            });

            modelBuilder.Entity<PackingList>(entity =>
            {
                entity.HasKey(e => e.PackingId)
                    .HasName("PackingList_pkey");

                entity.HasOne(d => d.Trip)
                    .WithMany(p => p.PackingLists)
                    .HasForeignKey(d => d.TripId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("public_PackingList_trip_id_fkey");
            });

            modelBuilder.Entity<Trip>(entity =>
            {
                entity.HasComment("trips table");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Trips)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("public_Trips_user_id_fkey");
            });

            modelBuilder.HasSequence<int>("seq_schema_version", "graphql").IsCyclic();

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}

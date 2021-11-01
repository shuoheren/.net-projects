﻿using System;
using System.ComponentModel.DataAnnotations.Schema;
using ApplicationCore.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data
{
    public class MovieShopDbContext : DbContext
    {
        
        public DbSet<Genre> Genres { get; set; }
        public DbSet<Movie> Movies { get; set; }
        public DbSet<Cast> Casts { get; set; }
        public DbSet<Crew> Crews { get; set; }
        public DbSet<Favorite> Favorites { get; set; }
        public DbSet<Purchase> Purchases { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Trailer> Trailers { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<MovieGenre> MovieGenres { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }
        public DbSet<MovieCast> MovieCasts { get; set; }
        public DbSet<MovieCrew> MovieCrews { get; set; }

        public MovieShopDbContext(DbContextOptions<MovieShopDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Movie>(ConfigureMovie);
            modelBuilder.Entity<Genre>(ConfigureGenre);
            modelBuilder.Entity<Cast>(ConfigureCast);
            modelBuilder.Entity<Crew>(ConfigureCrew);
            modelBuilder.Entity<Purchase>(ConfigurePurchase);
            modelBuilder.Entity<Review>(ConfigureReview);
            modelBuilder.Entity<Role>(ConfigureRole);
            modelBuilder.Entity<Trailer>(ConfigureTrailer);
            modelBuilder.Entity<User>(CongigureUser);
            modelBuilder.Entity<Favorite>(ConfigureFavorite);
            modelBuilder.Entity<MovieGenre>(ConfigureMovieGenre);
            modelBuilder.Entity<UserRole>(ConfigureUserRole);
            modelBuilder.Entity<MovieCast>(ConfigureMovieCast);
            modelBuilder.Entity<MovieCrew>(ConfigureMovieCrew);
        }

        private void ConfigureFavorite(EntityTypeBuilder<Favorite> builder)
        {
            builder.ToTable("Favorite");
            builder.HasKey(f => f.Id);
            builder.HasOne(f => f.Movie).WithMany(m => m.FavorUsers)
                .HasForeignKey(f => f.MovieId);
            builder.HasOne(f => f.User).WithMany(u => u.FavorMovies)
                .HasForeignKey(f => f.UserId);
        }

        private void ConfigureTrailer(EntityTypeBuilder<Trailer> builder)
        {
            builder.ToTable("Trailer");
            builder.HasKey(t => t.Id);
            builder.Property(t => t.TrailerUrl).HasMaxLength(2084);
            builder.Property(t => t.Name).HasMaxLength(2084);
            builder.HasOne(t => t.Movie).WithMany(m => m.Trailers)
                .HasForeignKey(T => T.MovieId);
        }

        private void ConfigureUserRole(EntityTypeBuilder<UserRole> builder)
        {
            builder.ToTable("UserRole");
            builder.HasKey(ur => new { ur.UserId, ur.RoleId });
            builder.HasOne(ur => ur.User).WithMany(u => u.Roles)
                .HasForeignKey(ur => ur.UserId);
            builder.HasOne(ur => ur.Role).WithMany(r => r.Users)
                .HasForeignKey(ur => ur.RoleId);
        }

        private void CongigureUser(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("User");
            builder.HasKey(u => u.Id);
            builder.Property(u => u.FirstName).HasMaxLength(128);
            builder.Property(u => u.LastName).HasMaxLength(128);
            builder.Property(u => u.Email).HasMaxLength(256);
            builder.Property(u => u.HashedPassword).HasMaxLength(1024);
            builder.Property(u => u.Salt).HasMaxLength(1024);
            builder.Property(u => u.PhoneNumber).HasMaxLength(16);
            builder.Property(u => u.DateOfBirth).HasColumnType("datetime2(7)");
            builder.Property(u => u.LockoutEndDate).HasColumnType("datetime2(7)");
            builder.Property(u => u.LastLoginDateTime).HasColumnType("datetime2(7)");
        }

        private void ConfigureRole(EntityTypeBuilder<Role> builder)
        {
            builder.ToTable("Role");
            builder.HasKey(r => r.Id);
            builder.Property(r => r.Name).HasMaxLength(20);
        }

        private void ConfigureReview(EntityTypeBuilder<Review> builder)
        {
            builder.ToTable("Review");
            builder.HasKey(r => new { r.UserId, r.MovieId });
            builder.Property(r => r.Rating).HasColumnType("decimal(3,2").IsRequired();
            builder.HasOne(r => r.User).WithMany((u => u.Reviews))
                .HasForeignKey(r => r.UserId);
            builder.HasOne(r => r.Movie).WithMany(m => m.Reviews)
                .HasForeignKey(r => r.MovieId);
        }

        private void ConfigurePurchase(EntityTypeBuilder<Purchase> builder)
        {
            builder.ToTable("Purchase");
            builder.HasKey(p => p.Id);
            builder.Property(p => p.PurchaseNumber).HasDefaultValueSql("NEWID()").IsRequired();
            builder.Property(p => p.TotalPrice).HasColumnType("decimal(18,2)").IsRequired();
            builder.Property(p => p.PurchaseDateTime).HasColumnType("datetime2(7)").IsRequired();

            builder.HasOne(p => p.User).WithMany(u => u.Purchases)
                .HasForeignKey(p => p.UserId);
            builder.HasOne(p => p.Movie).WithMany(m => m.Purchases)
                .HasForeignKey(p => p.MovieId);
        }

        private void ConfigureMovieCrew(EntityTypeBuilder<MovieCrew> builder)
        {
            builder.ToTable("MovieCrew");
            builder.HasKey(mc => new {mc.MovieId, mc.CrewId});
            builder.Property(mc => mc.Department).HasMaxLength(128);
            builder.Property(mc => mc.Job).HasMaxLength(128);
            builder.HasOne(mc => mc.Movie).WithMany(m => m.Crews)
                .HasForeignKey(mc => mc.MovieId);
            builder.HasOne(mc => mc.Crew).WithMany(c => c.Movies)
                .HasForeignKey(mc => mc.CrewId);
        }

        private void ConfigureMovieCast(EntityTypeBuilder<MovieCast> builder)
        {
            builder.ToTable("MovieCast");
            builder.HasKey(mc => new {mc.MovieId, mc.CastId});
            builder.Property(mc => mc.Character).HasMaxLength(450);
            builder.HasOne(mc => mc.Movie).WithMany(m => m.Casts)
                .HasForeignKey(mc => mc.MovieId);
            builder.HasOne(mc => mc.Cast).WithMany(c => c.Movies)
                .HasForeignKey(mc => mc.CastId);
        }
        
        private void ConfigureCrew(EntityTypeBuilder<Crew> builder)
        {
            builder.ToTable("Crew");
            builder.HasKey(c => c.Id);
            builder.Property(c => c.Name).HasMaxLength(128);
            builder.Property(c => c.ProfilePath).HasMaxLength(2084);
        }

        private void ConfigureCast(EntityTypeBuilder<Cast> builder)
        {
            builder.ToTable("Cast");
            builder.HasKey(c => c.Id);
            builder.Property(c => c.Name).HasMaxLength(128);
            builder.Property(c => c.ProfilePath).HasMaxLength(2084);
        }

        private void ConfigureMovieGenre(EntityTypeBuilder<MovieGenre> builder)
        {
            builder.ToTable("MovieGenre");
            builder.HasKey(mg => new {mg.MovieId, mg.GenreId});
            builder.HasOne(mg => mg.Movie).WithMany(m => m.Genres)
                .HasForeignKey(mg => mg.MovieId);
            builder.HasOne(mg => mg.Genre).WithMany(g => g.Movies)
                .HasForeignKey(mg => mg.GenreId);
        }
        
        private void ConfigureGenre(EntityTypeBuilder<Genre> builder)
        {
            builder.ToTable("Genre");
            builder.HasKey(g => g.Id);
            builder.Property(g => g.Name).HasMaxLength(64);
        }

        private void ConfigureMovie(EntityTypeBuilder<Movie> builder)
        {
            builder.ToTable("Movie");
            builder.HasKey(m => m.Id);
            builder.Property(m => m.Title).HasMaxLength(256);
            builder.Property(m => m.Overview).HasMaxLength(4096);
            builder.Property(m => m.Tagline).HasMaxLength(512);
            builder.Property(m => m.ImdbUrl).HasMaxLength(2084);
            builder.Property(m => m.TmdbUrl).HasMaxLength(2084);
            builder.Property(m => m.PosterUrl).HasMaxLength(2084);
            builder.Property(m => m.BackdropUrl).HasMaxLength(2084);
            builder.Property(m => m.OriginalLanguage).HasMaxLength(64);
            builder.Property(m => m.Price).HasColumnType("decimal(5,2)").HasDefaultValue(9.9m);
            builder.Property(m => m.Budget).HasColumnType("decimal(18,2)").HasDefaultValue(9.9m);
            builder.Property(m => m.Revenue).HasColumnType("decimal(18,2)").HasDefaultValue(9.9m);
            builder.Property(m => m.ReleaseDate).HasColumnType("datetime2(7)").HasDefaultValueSql("getdate()");
            builder.Property(m => m.CreatedDate).HasColumnType("datetime2(7)").HasDefaultValueSql("getdate()");
            builder.Property(m => m.UpdatedDate).HasColumnType("datetime2(7)").HasDefaultValueSql("getdate()");
            builder.Ignore(m => m.Rating);
        }
    }
}

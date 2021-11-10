using ApplicationCore.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Data
{
    public class MovieShopDbContext : DbContext
    {
        // get the connection string into constructor
        public MovieShopDbContext(DbContextOptions<MovieShopDbContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // specify Fluent API rules for your Entities

            modelBuilder.Entity<Movie>(ConfigureMovie);
            modelBuilder.Entity<Trailer>(ConfigureTrailer);
            modelBuilder.Entity<MovieGenre>(ConfigureMovieGenre);
            modelBuilder.Entity<MovieCast>(ConfigureMovieCast);
            modelBuilder.Entity<MovieCrew>(ConfigureMovieCrew);
            modelBuilder.Entity<Role>(ConfigureRole);
            modelBuilder.Entity<User>(ConfigureUser);
            modelBuilder.Entity<Purchase>(ConfigurePurchase);
            modelBuilder.Entity<UserRole>(ConfigureUserRole);
            modelBuilder.Entity<Favorite>(ConfigureFavorite);
            modelBuilder.Entity<Review>(ConfigureReview);
            modelBuilder.Entity<Cast>(ConfigureCast);
        }

        private void ConfigureCast(EntityTypeBuilder<Cast> builder)
        {
            builder.ToTable("Cast");
        }

        private void ConfigureFavorite(EntityTypeBuilder<Favorite> builder)
        {
            builder.ToTable("Favorite");
            builder.HasKey(f => new { f.Id });
        }

        private void ConfigureUserRole(EntityTypeBuilder<UserRole> builder)
        {
            builder.ToTable("UserRole");
            builder.HasKey(ur => new { ur.UserId, ur.RoleId });
        }

        private void ConfigurePurchase(EntityTypeBuilder<Purchase> builder)
        {
            builder.ToTable("Purchase");
            builder.HasKey(p => new { p.Id });
            builder.HasOne(m => m.Movie).WithMany(m => m.Purchases).HasForeignKey(m => m.MovieId);
            builder.HasOne(u => u.User).WithMany(u => u.Purchases).HasForeignKey(u => u.UserId);
            builder.Property(p => p.TotalPrice).HasColumnType("decimal(18, 2)");
        }

        private void ConfigureUser(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("User");
            builder.HasKey(u => new { u.Id });
        }

        private void ConfigureRole(EntityTypeBuilder<Role> builder)
        {
            builder.ToTable("Role");
        }

        private void ConfigureReview(EntityTypeBuilder<Review> builder)
        {
            builder.ToTable("Review");
            builder.HasKey(r => new { r.MovieId, r.UserId });
            builder.HasOne(m => m.Movie).WithMany(m => m.Reviews).HasForeignKey(m => m.MovieId);
            builder.HasOne(u => u.User).WithMany(u => u.Reviews).HasForeignKey(u => u.UserId);
            builder.Property(m => m.Rating).HasColumnType("decimal(3, 2)");
        }

        private void ConfigureMovieCrew(EntityTypeBuilder<MovieCrew> builder)
        {
            builder.ToTable("MovieCrew");
            builder.HasKey(mc => new { mc.MovieId, mc.CrewId, mc.Department, mc.Job });
            builder.HasOne(m => m.Movie).WithMany(m => m.Crews).HasForeignKey(m => m.MovieId);
            builder.HasOne(c => c.Crew).WithMany(c => c.Movies).HasForeignKey(c => c.CrewId);
        }

        private void ConfigureMovieCast(EntityTypeBuilder<MovieCast> builder)
        {
            builder.ToTable("MovieCast");
            builder.HasKey(mc => new { mc.MovieId, mc.CastId, mc.Character });
            builder.HasOne(m => m.Movie).WithMany(m => m.Casts).HasForeignKey(m => m.MovieId);
            builder.HasOne(c => c.Cast).WithMany(c => c.Movies).HasForeignKey(c => c.CastId);
          
        }

        private void ConfigureMovieGenre(EntityTypeBuilder<MovieGenre> builder)
        {
            builder.ToTable("MovieGenre");
            builder.HasKey(mg => new { mg.MovieId, mg.GenreId });
            builder.HasOne(m => m.Movie).WithMany(m => m.Genres).HasForeignKey(m => m.MovieId);
            builder.HasOne(g => g.Genre).WithMany(g => g.Movies).HasForeignKey(g => g.GenreId);

        }

        private void ConfigureTrailer(EntityTypeBuilder<Trailer> builder)
        {
            //change the name to singular
            builder.ToTable("Trailer");
            builder.HasKey(t => t.Id);
            builder.Property(t => t.TrailerUrl).HasMaxLength(2084);
            builder.Property(t => t.Name).HasMaxLength(2084);

        }

        private void ConfigureMovie(EntityTypeBuilder<Movie> builder)
        {
            // specify all the constarints and rules for Movie Table/Entity
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
            builder.Property(m => m.Price).HasColumnType("decimal(5, 2)").HasDefaultValue(9.9m);
            builder.Property(m => m.Budget).HasColumnType("decimal(18, 2)").HasDefaultValue(9.9m);
            builder.Property(m => m.Revenue).HasColumnType("decimal(18, 2)").HasDefaultValue(9.9m);
            builder.Property(m => m.CreatedDate).HasDefaultValueSql("getdate()");

            //we wanna tell EF to ignore Rating property and not create the columns
            builder.Ignore(m => m.Rating);
        }
        // make suer our entity classes are represented as DbSets
        public DbSet<Genre> Genres { get; set; }

        public DbSet<Movie> Movies { get; set; }

        public DbSet<Cast> Casts { get; set; }

        public DbSet<Role> Roles { get; set; }

        public DbSet<User> Users { get; set; }

        public DbSet<Trailer> Trailers { get; set; }

        public DbSet<MovieGenre> MovieGenres { get; set; }

        public DbSet<MovieCast> MovieCasts { get; set; }

        public DbSet<MovieCrew> MovieCrews { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<Purchase> Purchases { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }
        public DbSet<Favorite> Favorites { get; set; }
    }
}

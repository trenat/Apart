using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace test3.Data
{
    public partial class eadiApartDbContext : DbContext
    {
        public virtual DbSet<ApartImage> ApartImage { get; set; }
        public virtual DbSet<ApartOption> ApartOption { get; set; }
        public virtual DbSet<Apartment> Apartment { get; set; }
        public virtual DbSet<Option> Option { get; set; }
        public virtual DbSet<Rate> Rate { get; set; }
        public virtual DbSet<Reservation> Reservation { get; set; }
        public virtual DbSet<User> User { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            #warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
            optionsBuilder.UseSqlServer(@"Server=tcp:eadiapartdb.database.windows.net,1433;Initial Catalog=eadiApartDb;Persist Security Info=False;User ID=eadi13;Password=AaBbCcDdEe1;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ApartImage>(entity =>
            {
                entity.HasKey(e => e.ImageId)
                    .HasName("PK_ApartImage");

                entity.HasIndex(e => e.ApartmentId)
                    .HasName("IX_ApartImage_ApartmentID");

                entity.Property(e => e.ImageId).HasColumnName("ImageID");

                entity.Property(e => e.ApartmentId).HasColumnName("ApartmentID");

                entity.Property(e => e.ImagePath)
                    .IsRequired()
                    .HasColumnType("varchar(200)");

                entity.HasOne(d => d.Apartment)
                    .WithMany(p => p.ApartImage)
                    .HasForeignKey(d => d.ApartmentId)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FK_ApartImage_Apartment");
            });

            modelBuilder.Entity<ApartOption>(entity =>
            {
                entity.HasKey(e => e.ApartOptionsId)
                    .HasName("PK_ApartOption");

                entity.HasIndex(e => e.ApartmentId)
                    .HasName("IX_ApartOption_ApartmentID");

                entity.HasIndex(e => e.OptionId)
                    .HasName("IX_ApartOption_OptionID");

                entity.Property(e => e.ApartOptionsId).HasColumnName("ApartOptionsID");

                entity.Property(e => e.ApartmentId).HasColumnName("ApartmentID");

                entity.Property(e => e.OptionId).HasColumnName("OptionID");

                entity.HasOne(d => d.Apartment)
                    .WithMany(p => p.ApartOption)
                    .HasForeignKey(d => d.ApartmentId)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FK_ApartOption_Apartment");

                entity.HasOne(d => d.Option)
                    .WithMany(p => p.ApartOption)
                    .HasForeignKey(d => d.OptionId)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FK_ApartOption_Option");
            });

            modelBuilder.Entity<Apartment>(entity =>
            {
                entity.HasIndex(e => e.OwnerId)
                    .HasName("IX_Apartment_OwnerID");

                entity.Property(e => e.ApartmentId).HasColumnName("ApartmentID");

                entity.Property(e => e.Adress)
                    .IsRequired()
                    .HasColumnType("nchar(100)");

                entity.Property(e => e.Location)
                    .IsRequired()
                    .HasColumnType("nchar(100)");

                entity.Property(e => e.OwnerId).HasColumnName("OwnerID");

                entity.Property(e => e.PriceBasic).HasColumnType("money");

                entity.HasOne(d => d.Owner)
                    .WithMany(p => p.Apartment)
                    .HasForeignKey(d => d.OwnerId)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FK_Apartment_User");
            });

            modelBuilder.Entity<Option>(entity =>
            {
                entity.Property(e => e.OptionId).HasColumnName("OptionID");

                entity.Property(e => e.ImagePath).HasColumnType("nchar(10)");

                entity.Property(e => e.Name).HasColumnType("nchar(30)");
            });

            modelBuilder.Entity<Rate>(entity =>
            {
                entity.Property(e => e.RateId).HasColumnName("RateID");

                entity.Property(e => e.RateLevel).HasColumnType("numeric");
            });

            modelBuilder.Entity<Reservation>(entity =>
            {
                entity.HasIndex(e => e.ApartmentId)
                    .HasName("IX_Reservation_ApartmentID");

                entity.HasIndex(e => e.ClientId)
                    .HasName("IX_Reservation_ClientID");

                entity.HasIndex(e => e.RateId)
                    .HasName("IX_Reservation_RateID");

                entity.Property(e => e.ReservationId).HasColumnName("ReservationID");

                entity.Property(e => e.ApartmentId).HasColumnName("ApartmentID");

                entity.Property(e => e.ClientId).HasColumnName("ClientID");

                entity.Property(e => e.FromDate).HasColumnType("date");

                entity.Property(e => e.RateId).HasColumnName("RateID");

                entity.Property(e => e.Status).HasColumnType("nchar(30)");

                entity.Property(e => e.ToDate).HasColumnType("date");

                entity.Property(e => e.TotalCost).HasColumnType("money");

                entity.HasOne(d => d.Apartment)
                    .WithMany(p => p.Reservation)
                    .HasForeignKey(d => d.ApartmentId)
                    .HasConstraintName("FK_Reservation_Apartment");

                entity.HasOne(d => d.Client)
                    .WithMany(p => p.Reservation)
                    .HasForeignKey(d => d.ClientId)
                    .HasConstraintName("FK_Reservation_User");

                entity.HasOne(d => d.Rate)
                    .WithMany(p => p.Reservation)
                    .HasForeignKey(d => d.RateId)
                    .HasConstraintName("FK_Reservation_Rate");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.Property(e => e.UserId).HasColumnName("UserID");

                entity.Property(e => e.DateOfBirth).HasColumnType("date");

                entity.Property(e => e.Email).HasColumnType("nchar(50)");

                entity.Property(e => e.EmailNormalized).HasColumnType("nchar(50)");

                entity.Property(e => e.HashSalt).HasColumnType("nchar(128)");

                entity.Property(e => e.Name).HasColumnType("nchar(30)");

                entity.Property(e => e.Password).HasColumnType("nchar(128)");

                entity.Property(e => e.PhoneNumber).HasColumnType("char(9)");

                entity.Property(e => e.Surname).HasColumnType("nchar(30)");
            });
        }
    }
}
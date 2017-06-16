using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using test3.Data;

namespace test3.Migrations
{
    [DbContext(typeof(eadiApartDbContext))]
    [Migration("20170411181635_eee")]
    partial class eee
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.1.1")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("test3.Data.ApartImage", b =>
                {
                    b.Property<int>("ImageId")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("ImageID");

                    b.Property<int>("ApartmentId")
                        .HasColumnName("ApartmentID");

                    b.Property<string>("ImagePath")
                        .IsRequired()
                        .HasColumnType("varchar(200)");

                    b.HasKey("ImageId")
                        .HasName("PK_ApartImage");

                    b.HasIndex("ApartmentId");

                    b.ToTable("ApartImage");
                });

            modelBuilder.Entity("test3.Data.Apartment", b =>
                {
                    b.Property<int>("ApartmentId")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("ApartmentID");

                    b.Property<string>("Adress")
                        .IsRequired()
                        .HasColumnType("nchar(100)");

                    b.Property<string>("Description");

                    b.Property<string>("Location")
                        .IsRequired()
                        .HasColumnType("nchar(100)");

                    b.Property<int>("OwnerId")
                        .HasColumnName("OwnerID");

                    b.Property<decimal>("PriceBasic")
                        .HasColumnType("money");

                    b.Property<int>("RoomSize");

                    b.HasKey("ApartmentId");

                    b.HasIndex("OwnerId");

                    b.ToTable("Apartment");
                });

            modelBuilder.Entity("test3.Data.ApartOption", b =>
                {
                    b.Property<int>("ApartOptionsId")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("ApartOptionsID");

                    b.Property<int>("ApartmentId")
                        .HasColumnName("ApartmentID");

                    b.Property<int>("OptionId")
                        .HasColumnName("OptionID");

                    b.HasKey("ApartOptionsId")
                        .HasName("PK_ApartOption");

                    b.HasIndex("ApartmentId");

                    b.HasIndex("OptionId");

                    b.ToTable("ApartOption");
                });

            modelBuilder.Entity("test3.Data.Option", b =>
                {
                    b.Property<int>("OptionId")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("OptionID");

                    b.Property<string>("ImagePath")
                        .HasColumnType("nchar(10)");

                    b.Property<int?>("Name");

                    b.HasKey("OptionId");

                    b.ToTable("Option");
                });

            modelBuilder.Entity("test3.Data.Rate", b =>
                {
                    b.Property<int>("RateId")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("RateID");

                    b.Property<decimal>("RateLevel")
                        .HasColumnType("numeric");

                    b.HasKey("RateId");

                    b.ToTable("Rate");
                });

            modelBuilder.Entity("test3.Data.Reservation", b =>
                {
                    b.Property<int>("ReservationId")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("ReservationID");

                    b.Property<int?>("ApartmentId")
                        .HasColumnName("ApartmentID");

                    b.Property<int?>("ClientId")
                        .HasColumnName("ClientID");

                    b.Property<string>("Comment")
                        .IsRequired();

                    b.Property<DateTime?>("FromDate")
                        .HasColumnType("date");

                    b.Property<string>("OwnerReply")
                        .IsRequired();

                    b.Property<int>("RateId")
                        .HasColumnName("RateID");

                    b.Property<string>("Status")
                        .HasColumnType("nchar(30)");

                    b.Property<DateTime?>("ToDate")
                        .HasColumnType("date");

                    b.Property<decimal?>("TotalCost")
                        .HasColumnType("money");

                    b.HasKey("ReservationId");

                    b.HasIndex("ApartmentId");

                    b.HasIndex("ClientId");

                    b.HasIndex("RateId");

                    b.ToTable("Reservation");
                });

            modelBuilder.Entity("test3.Data.User", b =>
                {
                    b.Property<int>("UserId")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("UserId");

                    b.Property<bool?>("Admin");

                    b.Property<DateTime?>("DateOfBirth")
                        .HasColumnType("date");

                    b.Property<string>("Email")
                        .HasColumnType("nchar(50)");

                    b.Property<string>("EmailNormalized")
                        .HasColumnType("nchar(50)");

                    b.Property<string>("HashSalt")
                        .HasColumnType("nchar(128)");

                    b.Property<string>("Name")
                        .HasColumnType("nchar(30)");

                    b.Property<string>("Password")
                        .HasColumnType("nchar(128)");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("char(9)");

                    b.Property<string>("Surname")
                        .HasColumnType("nchar(30)");

                    b.HasKey("UserId");

                    b.ToTable("User");
                });

            modelBuilder.Entity("test3.Data.ApartImage", b =>
                {
                    b.HasOne("test3.Data.Apartment", "Apartment")
                        .WithMany("ApartImage")
                        .HasForeignKey("ApartmentId")
                        .HasConstraintName("FK_ApartImage_Apartment");
                });

            modelBuilder.Entity("test3.Data.Apartment", b =>
                {
                    b.HasOne("test3.Data.User", "Owner")
                        .WithMany("Apartment")
                        .HasForeignKey("OwnerId")
                        .HasConstraintName("FK_Apartment_User");
                });

            modelBuilder.Entity("test3.Data.ApartOption", b =>
                {
                    b.HasOne("test3.Data.Apartment", "Apartment")
                        .WithMany("ApartOption")
                        .HasForeignKey("ApartmentId")
                        .HasConstraintName("FK_ApartOption_Apartment");

                    b.HasOne("test3.Data.Option", "Option")
                        .WithMany("ApartOption")
                        .HasForeignKey("OptionId")
                        .HasConstraintName("FK_ApartOption_Option");
                });

            modelBuilder.Entity("test3.Data.Reservation", b =>
                {
                    b.HasOne("test3.Data.Apartment", "Apartment")
                        .WithMany("Reservation")
                        .HasForeignKey("ApartmentId")
                        .HasConstraintName("FK_Reservation_Apartment");

                    b.HasOne("test3.Data.User", "Client")
                        .WithMany("Reservation")
                        .HasForeignKey("ClientId")
                        .HasConstraintName("FK_Reservation_User");

                    b.HasOne("test3.Data.Rate", "Rate")
                        .WithMany("Reservation")
                        .HasForeignKey("RateId")
                        .HasConstraintName("FK_Reservation_Rate");
                });
        }
    }
}

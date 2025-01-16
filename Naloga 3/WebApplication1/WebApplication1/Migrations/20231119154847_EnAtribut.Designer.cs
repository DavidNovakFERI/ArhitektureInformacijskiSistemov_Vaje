﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using WebApplication1;

#nullable disable

namespace WebApplication1.Migrations
{
    [DbContext(typeof(RacingDbContext))]
    [Migration("20231119154847_EnAtribut")]
    partial class EnAtribut
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "7.0.13");

            modelBuilder.Entity("WebApplication1.Models.Razredi+Ekipa", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Drzava")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Ime")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int>("letoUstanovitve")
                        .HasColumnType("INTEGER")
                        .HasColumnName("Leto");

                    b.HasKey("Id");

                    b.ToTable("Ekipe");
                });

            modelBuilder.Entity("WebApplication1.Models.Razredi+Voznik", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("ImePriimek")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<bool>("Novinec")
                        .HasColumnType("INTEGER");

                    b.Property<int>("StevilkaAvta")
                        .HasColumnType("INTEGER")
                        .HasColumnName("Stevilka");

                    b.Property<int>("letoRojstva")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER")
                        .HasDefaultValue(1990);

                    b.HasKey("Id");

                    b.ToTable("Vozniki");
                });

            modelBuilder.Entity("WebApplication1.Models.Razredi+VoznikVEkipi", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("EkipaId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("VoznikId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("letoIzpada")
                        .HasColumnType("INTEGER");

                    b.Property<int>("letoVstopa")
                        .HasColumnType("INTEGER");

                    b.Property<int>("steviloDirk")
                        .HasColumnType("INTEGER");

                    b.Property<int>("steviloZmag")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("EkipaId");

                    b.HasIndex("VoznikId");

                    b.ToTable("VoznikVEkipi");
                });

            modelBuilder.Entity("WebApplication1.Models.Razredi+VoznikVEkipi", b =>
                {
                    b.HasOne("WebApplication1.Models.Razredi+Ekipa", "Ekipa")
                        .WithMany()
                        .HasForeignKey("EkipaId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("WebApplication1.Models.Razredi+Voznik", "Voznik")
                        .WithMany()
                        .HasForeignKey("VoznikId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Ekipa");

                    b.Navigation("Voznik");
                });
#pragma warning restore 612, 618
        }
    }
}
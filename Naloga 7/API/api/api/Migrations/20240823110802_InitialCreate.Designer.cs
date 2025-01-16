﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using api;

#nullable disable

namespace api.Migrations
{
    [DbContext(typeof(Formula1Context))]
    [Migration("20240823110802_InitialCreate")]
    partial class InitialCreate
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "8.0.8");

            modelBuilder.Entity("api.Ekipa", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Drzava")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int>("LetoUstanovitve")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Naziv")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Ekipe");
                });

            modelBuilder.Entity("api.Voznik", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Ime")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int>("LetoRojstva")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Priimek")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Vozniki");
                });

            modelBuilder.Entity("api.VoznikVEkipi", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("EkipaId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("LetoDo")
                        .HasColumnType("INTEGER");

                    b.Property<int>("LetoOd")
                        .HasColumnType("INTEGER");

                    b.Property<int>("SteviloZmag")
                        .HasColumnType("INTEGER");

                    b.Property<int>("VoznikId")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("EkipaId");

                    b.HasIndex("VoznikId");

                    b.ToTable("VoznikiVEkipi");
                });

            modelBuilder.Entity("api.VoznikVEkipi", b =>
                {
                    b.HasOne("api.Ekipa", "Ekipa")
                        .WithMany()
                        .HasForeignKey("EkipaId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("api.Voznik", "Voznik")
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
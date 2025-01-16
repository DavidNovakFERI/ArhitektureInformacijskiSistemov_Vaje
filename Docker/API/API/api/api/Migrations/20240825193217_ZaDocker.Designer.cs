﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using api;

#nullable disable

namespace api.Migrations
{
    [DbContext(typeof(Formula1Context))]
    [Migration("20240825193217_ZaDocker")]
    partial class ZaDocker
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("api.Ekipa", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Drzava")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("LetoUstanovitve")
                        .HasColumnType("integer");

                    b.Property<string>("Naziv")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Ekipe");
                });

            modelBuilder.Entity("api.Voznik", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Ime")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("LetoRojstva")
                        .HasColumnType("integer");

                    b.Property<string>("Priimek")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Vozniki");
                });

            modelBuilder.Entity("api.VoznikVEkipi", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("EkipaId")
                        .HasColumnType("integer");

                    b.Property<int>("LetoDo")
                        .HasColumnType("integer");

                    b.Property<int>("LetoOd")
                        .HasColumnType("integer");

                    b.Property<int>("SteviloZmag")
                        .HasColumnType("integer");

                    b.Property<int>("VoznikId")
                        .HasColumnType("integer");

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

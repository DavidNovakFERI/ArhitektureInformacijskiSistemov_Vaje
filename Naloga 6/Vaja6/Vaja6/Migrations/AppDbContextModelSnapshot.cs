﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Vaja6.Data;

#nullable disable

namespace Vaja6.Migrations
{
    [DbContext(typeof(AppDbContext))]
    partial class AppDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "8.0.0");

            modelBuilder.Entity("Vaja6.Vozniki", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Ime")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Priimek")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int>("Starost")
                        .HasColumnType("INTEGER");

                    b.Property<int>("ŠtevilkaFormule")
                        .HasColumnType("INTEGER");

                    b.Property<int>("ŠteviloZmag")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.ToTable("Vozniki");
                });
#pragma warning restore 612, 618
        }
    }
}

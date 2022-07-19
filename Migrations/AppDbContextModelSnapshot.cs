﻿// <auto-generated />
using Dotnet.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Dotnet.Migrations
{
    [DbContext(typeof(AppDbContext))]
    partial class AppDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.7")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("Dotnet.Models.Student", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Address")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Students");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Address = "Rajabagicha",
                            Name = "Najish Eqbal"
                        },
                        new
                        {
                            Id = 2,
                            Address = "Daudnagar",
                            Name = "Danish Eqbal"
                        },
                        new
                        {
                            Id = 3,
                            Address = "Ranchi",
                            Name = "Yasin Ekbal"
                        },
                        new
                        {
                            Id = 4,
                            Address = "MaharajGanj",
                            Name = "Taukir Khan"
                        },
                        new
                        {
                            Id = 5,
                            Address = "Pakistan",
                            Name = "Bilal Shaeed"
                        });
                });
#pragma warning restore 612, 618
        }
    }
}

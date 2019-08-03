﻿// <auto-generated />
using FileStorage;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace FileStorage.Migrations
{
    [DbContext(typeof(AppContext))]
    [Migration("20190803104018_SizeTypeChanged")]
    partial class SizeTypeChanged
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.0-rtm-35687")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("FileStorage.DbModels.FileInfo", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Cateory")
                        .IsRequired();

                    b.Property<string>("Index")
                        .IsRequired();

                    b.Property<int>("Level");

                    b.Property<string>("Name")
                        .IsRequired();

                    b.Property<long>("Size");

                    b.HasKey("Id");

                    b.HasIndex("Index")
                        .IsUnique();

                    b.HasIndex("Level");

                    b.ToTable("Files");
                });
#pragma warning restore 612, 618
        }
    }
}

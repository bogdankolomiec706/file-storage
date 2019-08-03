﻿// <auto-generated />
using System;
using FileStorage;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace FileStorage.Migrations
{
    [DbContext(typeof(AppContext))]
    [Migration("20190803132608_Categories")]
    partial class Categories
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.0-rtm-35687")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("FileStorage.DbModels.FileCategory", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .HasMaxLength(255);

                    b.HasKey("Id");

                    b.HasIndex("Name")
                        .IsUnique()
                        .HasFilter("[Name] IS NOT NULL");

                    b.ToTable("FileCategories");
                });

            modelBuilder.Entity("FileStorage.DbModels.FileInfo", b =>
                {
                    b.Property<int>("Level");

                    b.Property<int>("SubIndex");

                    b.Property<int?>("CategoryId");

                    b.Property<int>("CateoryId");

                    b.Property<string>("FullIndex")
                        .IsRequired()
                        .HasMaxLength(500);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(255);

                    b.Property<long>("Size");

                    b.HasKey("Level", "SubIndex");

                    b.HasIndex("CategoryId");

                    b.HasIndex("FullIndex")
                        .IsUnique();

                    b.ToTable("Files");
                });

            modelBuilder.Entity("FileStorage.DbModels.FileInfo", b =>
                {
                    b.HasOne("FileStorage.DbModels.FileCategory", "Category")
                        .WithMany()
                        .HasForeignKey("CategoryId");
                });
#pragma warning restore 612, 618
        }
    }
}

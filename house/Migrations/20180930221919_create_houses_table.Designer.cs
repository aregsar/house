﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using house.Data;

namespace house.Migrations
{
    [DbContext(typeof(HouseDbContext))]
    [Migration("20180930221919_create_houses_table")]
    partial class create_houses_table
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.3-rtm-32065");

            modelBuilder.Entity("house.Data.House", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Zip");

                    b.HasKey("Id");

                    b.ToTable("House");
                });
#pragma warning restore 612, 618
        }
    }
}

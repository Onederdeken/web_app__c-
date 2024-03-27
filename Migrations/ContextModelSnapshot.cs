﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using db;

#nullable disable

namespace Application2.Migrations
{
    [DbContext(typeof(Context))]
    partial class ContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("Application2.Models.Buy", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<Guid>("IdPerson")
                        .HasColumnType("char(36)");

                    b.Property<Guid>("IdProduct")
                        .HasColumnType("char(36)");

                    b.Property<Guid>("IdSeller")
                        .HasColumnType("char(36)");

                    b.Property<int>("cost")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Buys");
                });

            modelBuilder.Entity("Application2.Models.Category", b =>
                {
                    b.Property<string>("NameCategory")
                        .HasColumnType("varchar(255)");

                    b.HasKey("NameCategory");

                    b.ToTable("Categories");
                });

            modelBuilder.Entity("Application2.Models.Person", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<int>("CMoney")
                        .HasColumnType("int");

                    b.Property<string>("DirectoryPhotoUser")
                        .HasColumnType("longtext");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("NickName")
                        .IsRequired()
                        .HasMaxLength(16)
                        .HasColumnType("varchar(16)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasMaxLength(25)
                        .HasColumnType("varchar(25)");

                    b.Property<string>("Telephone")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.ToTable("Persons");
                });

            modelBuilder.Entity("Application2.Models.Product", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<string>("CategoryNameCategory")
                        .IsRequired()
                        .HasColumnType("varchar(255)");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("varchar(100)");

                    b.Property<string>("DirectoryPhotoProduct")
                        .HasColumnType("longtext");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<Guid?>("SellerId")
                        .HasColumnType("char(36)");

                    b.Property<int>("cost")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("CategoryNameCategory");

                    b.HasIndex("SellerId");

                    b.ToTable("Products");
                });

            modelBuilder.Entity("Application2.Models.Seller", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<int>("CountMoney")
                        .HasColumnType("int");

                    b.Property<string>("DirectoryPhotoUser")
                        .HasColumnType("longtext");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("NickName")
                        .IsRequired()
                        .HasMaxLength(16)
                        .HasColumnType("varchar(16)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasMaxLength(25)
                        .HasColumnType("varchar(25)");

                    b.Property<string>("Telephone")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.ToTable("Sellers");
                });

            modelBuilder.Entity("Application2.Models.User", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<string>("DirectoryPhotoUser")
                        .HasColumnType("longtext");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("NickName")
                        .IsRequired()
                        .HasMaxLength(16)
                        .HasColumnType("varchar(16)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasMaxLength(25)
                        .HasColumnType("varchar(25)");

                    b.Property<string>("Telephone")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("role")
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("PersonProduct", b =>
                {
                    b.Property<Guid>("PersonId")
                        .HasColumnType("char(36)");

                    b.Property<Guid>("ProductsId")
                        .HasColumnType("char(36)");

                    b.HasKey("PersonId", "ProductsId");

                    b.HasIndex("ProductsId");

                    b.ToTable("PersonProduct");
                });

            modelBuilder.Entity("Application2.Models.Product", b =>
                {
                    b.HasOne("Application2.Models.Category", "Category")
                        .WithMany("Products")
                        .HasForeignKey("CategoryNameCategory")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Application2.Models.Seller", "Seller")
                        .WithMany("Products")
                        .HasForeignKey("SellerId");

                    b.Navigation("Category");

                    b.Navigation("Seller");
                });

            modelBuilder.Entity("PersonProduct", b =>
                {
                    b.HasOne("Application2.Models.Person", null)
                        .WithMany()
                        .HasForeignKey("PersonId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Application2.Models.Product", null)
                        .WithMany()
                        .HasForeignKey("ProductsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Application2.Models.Category", b =>
                {
                    b.Navigation("Products");
                });

            modelBuilder.Entity("Application2.Models.Seller", b =>
                {
                    b.Navigation("Products");
                });
#pragma warning restore 612, 618
        }
    }
}

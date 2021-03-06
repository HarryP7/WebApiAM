﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using WebApiAM.Models;

namespace WebApiAM.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20190203144831_Service")]
    partial class Service
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.4-rtm-31024")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("WebApiAM.Models.Event", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Comment")
                        .HasColumnType("varchar(MAX)");

                    b.Property<decimal>("Cost")
                        .HasColumnType("decimal(6, 2)");

                    b.Property<DateTime>("EvDate");

                    b.Property<int>("Fk_service");

                    b.Property<int>("Fk_user");

                    b.Property<int>("Status");

                    b.Property<string>("Uid");

                    b.HasKey("Id");

                    b.HasIndex("Fk_service");

                    b.HasIndex("Fk_user");

                    b.ToTable("Events");
                });

            modelBuilder.Entity("WebApiAM.Models.Role", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.ToTable("Roles");
                });

            modelBuilder.Entity("WebApiAM.Models.Service", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("DatePlace");

                    b.Property<string>("ImageUrl");

                    b.Property<decimal>("Lat")
                        .HasColumnType("decimal(10, 6)");

                    b.Property<decimal>("Lng")
                        .HasColumnType("decimal(10, 6)");

                    b.Property<string>("Title")
                        .HasColumnType("varchar(100)");

                    b.HasKey("Id");

                    b.ToTable("Service");
                });

            modelBuilder.Entity("WebApiAM.Models.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Email")
                        .HasColumnType("varchar(50)");

                    b.Property<string>("FullName")
                        .HasColumnType("varchar(100)");

                    b.Property<string>("Login")
                        .HasColumnType("varchar(30)");

                    b.Property<byte[]>("PasswordHash");

                    b.Property<byte[]>("PasswordSalt");

                    b.Property<string>("Phone")
                        .HasColumnType("varchar(16)");

                    b.Property<int?>("RoleId");

                    b.Property<string>("Uid");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("WebApiAM.Models.Event", b =>
                {
                    b.HasOne("WebApiAM.Models.Service", "Service")
                        .WithMany("Applications")
                        .HasForeignKey("Fk_service")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("WebApiAM.Models.User", "User")
                        .WithMany("Events")
                        .HasForeignKey("Fk_user")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("WebApiAM.Models.User", b =>
                {
                    b.HasOne("WebApiAM.Models.Role", "Role")
                        .WithMany()
                        .HasForeignKey("RoleId");
                });
#pragma warning restore 612, 618
        }
    }
}

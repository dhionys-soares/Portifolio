﻿// <auto-generated />
using DesafioIbge.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace DesafioIbge.Migrations
{
    [DbContext(typeof(DataContext))]
    [Migration("20240414190007_createdatabase")]
    partial class createdatabase
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.4")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("DesafioIbge.Models.Ibge", b =>
                {
                    b.Property<int>("Id")
                        .HasMaxLength(7)
                        .HasColumnType("int")
                        .IsFixedLength();

                    b.Property<string>("City")
                        .HasMaxLength(80)
                        .HasColumnType("NVARCHAR");

                    b.Property<string>("State")
                        .HasMaxLength(2)
                        .HasColumnType("VARCHAR")
                        .IsFixedLength();

                    b.HasKey("Id");

                    b.HasIndex(new[] { "City" }, "IX_IBGE_City");

                    b.HasIndex(new[] { "Id" }, "IX_IBGE_Id");

                    b.HasIndex(new[] { "State" }, "IX_IBGE_State");

                    b.ToTable("Ibge", (string)null);
                });

            modelBuilder.Entity("DesafioIbge.Models.Usuario", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(160)
                        .HasColumnType("VARCHAR");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(80)
                        .HasColumnType("NVARCHAR");

                    b.Property<string>("Senha")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("VARCHAR");

                    b.HasKey("Id");

                    b.ToTable("Usuario", (string)null);
                });
#pragma warning restore 612, 618
        }
    }
}
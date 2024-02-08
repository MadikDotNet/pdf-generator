﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using PdfGenerator.Infrastructure.DataAccess;

#nullable disable

namespace PdfGenerator.Infrastructure.DataAccess.Migrations
{
    [DbContext(typeof(PdfGeneratorDb))]
    partial class PdfGeneratorDbModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.1")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("PdfGenerator.Domain.PdfConversions.PdfConversion", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uuid");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("OriginFileName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("OriginFilePath")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("ResultPath")
                        .HasColumnType("text");

                    b.Property<int>("Status")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("OriginFilePath")
                        .IsUnique();

                    b.HasIndex("ResultPath")
                        .IsUnique();

                    b.HasIndex("Status");

                    b.ToTable("PdfConversions");
                });
#pragma warning restore 612, 618
        }
    }
}

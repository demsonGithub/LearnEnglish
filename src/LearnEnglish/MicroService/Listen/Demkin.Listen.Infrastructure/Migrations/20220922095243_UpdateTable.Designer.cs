﻿// <auto-generated />
using System;
using Demkin.Listen.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Demkin.Listen.Infrastructure.Migrations
{
    [DbContext(typeof(ListenDbContext))]
    [Migration("20220922095243_UpdateTable")]
    partial class UpdateTable
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.9")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("Demkin.Listen.Domain.AggregateModels.Category", b =>
                {
                    b.Property<long>("Id")
                        .HasColumnType("bigint");

                    b.Property<string>("CoverUrl")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("CreateTime")
                        .HasColumnType("datetime2");

                    b.Property<int>("SequenceNumber")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Category", (string)null);
                });

            modelBuilder.Entity("Demkin.Listen.Domain.AggregateModels.Category", b =>
                {
                    b.OwnsOne("Demkin.Listen.Domain.ValueObjects.MultipleLanguageTitle", "MultipleLanguageTitle", b1 =>
                        {
                            b1.Property<long>("CategoryId")
                                .HasColumnType("bigint");

                            b1.Property<string>("ChineseTitle")
                                .IsRequired()
                                .HasMaxLength(50)
                                .HasColumnType("nvarchar(50)")
                                .HasColumnName("ChineseTitle");

                            b1.Property<string>("EnglishTitle")
                                .IsRequired()
                                .HasMaxLength(50)
                                .HasColumnType("nvarchar(50)")
                                .HasColumnName("EnglishTitle");

                            b1.HasKey("CategoryId");

                            b1.ToTable("Category");

                            b1.WithOwner()
                                .HasForeignKey("CategoryId");
                        });

                    b.Navigation("MultipleLanguageTitle")
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}

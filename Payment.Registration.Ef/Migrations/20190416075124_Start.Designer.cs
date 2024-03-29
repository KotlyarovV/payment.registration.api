﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Payment.Registration.Ef.DbContexts;

namespace Payment.Registration.Ef.Migrations
{
    [DbContext(typeof(PaymentFormDbContext))]
    [Migration("20190416075124_Start")]
    partial class Start
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.4-servicing-10062")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Payment.Registration.Domain.Models.Applicant", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("LastName");

                    b.Property<string>("Name");

                    b.Property<string>("Surname");

                    b.HasKey("Id");

                    b.ToTable("Applicant");
                });

            modelBuilder.Entity("Payment.Registration.Domain.Models.File", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<Guid?>("PaymentPositionId");

                    b.Property<string>("WayToFile");

                    b.HasKey("Id");

                    b.HasIndex("PaymentPositionId");

                    b.ToTable("File");
                });

            modelBuilder.Entity("Payment.Registration.Domain.Models.PaymentForm", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<Guid>("ApplicantId");

                    b.Property<DateTime>("Date");

                    b.Property<int>("Number");

                    b.Property<int>("Type");

                    b.HasKey("Id");

                    b.HasIndex("ApplicantId")
                        .IsUnique();

                    b.ToTable("PaymentForms");
                });

            modelBuilder.Entity("Payment.Registration.Domain.Models.PaymentPosition", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Comment");

                    b.Property<Guid?>("PaymentFormId");

                    b.Property<int>("SortOrder");

                    b.Property<decimal>("Sum")
                        .HasColumnType("decimal(5, 2)");

                    b.HasKey("Id");

                    b.HasIndex("PaymentFormId");

                    b.ToTable("PaymentPosition");
                });

            modelBuilder.Entity("Payment.Registration.Domain.Models.File", b =>
                {
                    b.HasOne("Payment.Registration.Domain.Models.PaymentPosition")
                        .WithMany("Files")
                        .HasForeignKey("PaymentPositionId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Payment.Registration.Domain.Models.PaymentForm", b =>
                {
                    b.HasOne("Payment.Registration.Domain.Models.Applicant", "Applicant")
                        .WithOne()
                        .HasForeignKey("Payment.Registration.Domain.Models.PaymentForm", "ApplicantId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Payment.Registration.Domain.Models.PaymentPosition", b =>
                {
                    b.HasOne("Payment.Registration.Domain.Models.PaymentForm")
                        .WithMany("Items")
                        .HasForeignKey("PaymentFormId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}

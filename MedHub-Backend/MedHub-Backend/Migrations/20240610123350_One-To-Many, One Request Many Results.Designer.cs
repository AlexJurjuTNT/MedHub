﻿// <auto-generated />
using System;
using MedHub_Backend.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace MedHub_Backend.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20240610123350_One-To-Many, One Request Many Results")]
    partial class OneToManyOneRequestManyResults
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.6")
                .HasAnnotation("Proxies:ChangeTracking", false)
                .HasAnnotation("Proxies:CheckEquality", false)
                .HasAnnotation("Proxies:LazyLoading", true)
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("MedHub_Backend.Model.Clinic", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Location")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("location");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("name");

                    b.Property<string>("SendgridApiKey")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("sendgrid_api_key");

                    b.HasKey("Id");

                    b.ToTable("clinic");
                });

            modelBuilder.Entity("MedHub_Backend.Model.Patient", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Cnp")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("cnp");

                    b.Property<DateOnly>("DateOfBirth")
                        .HasColumnType("date")
                        .HasColumnName("date_of_birth");

                    b.Property<string>("Gender")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("gender");

                    b.Property<int>("Height")
                        .HasColumnType("integer")
                        .HasColumnName("height_cm");

                    b.Property<int>("UserId")
                        .HasColumnType("integer")
                        .HasColumnName("user_id");

                    b.Property<int>("Weight")
                        .HasColumnType("integer")
                        .HasColumnName("weight");

                    b.HasKey("Id");

                    b.HasIndex("UserId")
                        .IsUnique();

                    b.ToTable("patient");
                });

            modelBuilder.Entity("MedHub_Backend.Model.Role", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("name");

                    b.HasKey("Id");

                    b.ToTable("role");
                });

            modelBuilder.Entity("MedHub_Backend.Model.TestRequest", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("DoctorId")
                        .HasColumnType("integer")
                        .HasColumnName("doctor_id");

                    b.Property<int>("PatientId")
                        .HasColumnType("integer")
                        .HasColumnName("patient_id1");

                    b.Property<DateTime>("RequestDate")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("request_date");

                    b.HasKey("Id");

                    b.HasIndex("DoctorId");

                    b.HasIndex("PatientId");

                    b.ToTable("test_request");
                });

            modelBuilder.Entity("MedHub_Backend.Model.TestResult", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("CompletionDate")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("completion_date");

                    b.Property<string>("FilePath")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("file_path");

                    b.Property<int>("TestRequestId")
                        .HasColumnType("integer")
                        .HasColumnName("test_request_id");

                    b.HasKey("Id");

                    b.HasIndex("TestRequestId");

                    b.ToTable("test_result");
                });

            modelBuilder.Entity("MedHub_Backend.Model.TestType", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("name");

                    b.HasKey("Id");

                    b.ToTable("test_type");
                });

            modelBuilder.Entity("MedHub_Backend.Model.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("ClinicId")
                        .HasColumnType("integer")
                        .HasColumnName("clinic_id");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("email");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("password_hash");

                    b.Property<string>("PasswordResetCode")
                        .HasColumnType("text")
                        .HasColumnName("password_reset_code");

                    b.Property<int>("RoleId")
                        .HasColumnType("integer")
                        .HasColumnName("role_id");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("username");

                    b.HasKey("Id");

                    b.HasIndex("ClinicId");

                    b.HasIndex("RoleId");

                    b.ToTable("user");
                });

            modelBuilder.Entity("TestRequestTestType", b =>
                {
                    b.Property<int>("TestRequestsId")
                        .HasColumnType("integer");

                    b.Property<int>("TestTypesId")
                        .HasColumnType("integer");

                    b.HasKey("TestRequestsId", "TestTypesId");

                    b.HasIndex("TestTypesId");

                    b.ToTable("TestRequestTestType");
                });

            modelBuilder.Entity("MedHub_Backend.Model.Patient", b =>
                {
                    b.HasOne("MedHub_Backend.Model.User", "User")
                        .WithOne("Patient")
                        .HasForeignKey("MedHub_Backend.Model.Patient", "UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("MedHub_Backend.Model.TestRequest", b =>
                {
                    b.HasOne("MedHub_Backend.Model.User", "Doctor")
                        .WithMany()
                        .HasForeignKey("DoctorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("MedHub_Backend.Model.User", "Patient")
                        .WithMany()
                        .HasForeignKey("PatientId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Doctor");

                    b.Navigation("Patient");
                });

            modelBuilder.Entity("MedHub_Backend.Model.TestResult", b =>
                {
                    b.HasOne("MedHub_Backend.Model.TestRequest", "TestRequest")
                        .WithMany("TestResults")
                        .HasForeignKey("TestRequestId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("TestRequest");
                });

            modelBuilder.Entity("MedHub_Backend.Model.User", b =>
                {
                    b.HasOne("MedHub_Backend.Model.Clinic", "Clinic")
                        .WithMany("Users")
                        .HasForeignKey("ClinicId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("MedHub_Backend.Model.Role", "Role")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Clinic");

                    b.Navigation("Role");
                });

            modelBuilder.Entity("TestRequestTestType", b =>
                {
                    b.HasOne("MedHub_Backend.Model.TestRequest", null)
                        .WithMany()
                        .HasForeignKey("TestRequestsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("MedHub_Backend.Model.TestType", null)
                        .WithMany()
                        .HasForeignKey("TestTypesId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("MedHub_Backend.Model.Clinic", b =>
                {
                    b.Navigation("Users");
                });

            modelBuilder.Entity("MedHub_Backend.Model.TestRequest", b =>
                {
                    b.Navigation("TestResults");
                });

            modelBuilder.Entity("MedHub_Backend.Model.User", b =>
                {
                    b.Navigation("Patient")
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}

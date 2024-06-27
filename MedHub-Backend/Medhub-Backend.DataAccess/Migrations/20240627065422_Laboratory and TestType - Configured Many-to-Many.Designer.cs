﻿// <auto-generated />
using System;
using Medhub_Backend.DataAccess.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Medhub_Backend.DataAccess.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20240627065422_Laboratory and TestType - Configured Many-to-Many")]
    partial class LaboratoryandTestTypeConfiguredManytoMany
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

            modelBuilder.Entity("LaboratoryTestType", b =>
                {
                    b.Property<int>("LaboratoriesId")
                        .HasColumnType("integer");

                    b.Property<int>("TestTypesId")
                        .HasColumnType("integer");

                    b.HasKey("LaboratoriesId", "TestTypesId");

                    b.HasIndex("TestTypesId");

                    b.ToTable("LaboratoryTestType");
                });

            modelBuilder.Entity("Medhub_Backend.Domain.Entities.Clinic", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("email");

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

            modelBuilder.Entity("Medhub_Backend.Domain.Entities.Laboratory", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("ClinicId")
                        .HasColumnType("integer")
                        .HasColumnName("clinic_id");

                    b.Property<string>("Location")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("location");

                    b.HasKey("Id");

                    b.HasIndex("ClinicId");

                    b.ToTable("laboratory");
                });

            modelBuilder.Entity("Medhub_Backend.Domain.Entities.Patient", b =>
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

            modelBuilder.Entity("Medhub_Backend.Domain.Entities.Role", b =>
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

            modelBuilder.Entity("Medhub_Backend.Domain.Entities.TestRequest", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("ClinicId")
                        .HasColumnType("integer")
                        .HasColumnName("clinic_id");

                    b.Property<int>("DoctorId")
                        .HasColumnType("integer")
                        .HasColumnName("doctor_id");

                    b.Property<int>("LaboratoryId")
                        .HasColumnType("integer")
                        .HasColumnName("laboratory_id");

                    b.Property<int>("PatientId")
                        .HasColumnType("integer")
                        .HasColumnName("patient_id1");

                    b.Property<DateTime>("RequestDate")
                        .HasColumnType("timestamp without time zone")
                        .HasColumnName("request_date");

                    b.HasKey("Id");

                    b.HasIndex("ClinicId");

                    b.HasIndex("DoctorId");

                    b.HasIndex("LaboratoryId");

                    b.HasIndex("PatientId");

                    b.ToTable("test_request");
                });

            modelBuilder.Entity("Medhub_Backend.Domain.Entities.TestResult", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("CompletionDate")
                        .HasColumnType("timestamp without time zone")
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

            modelBuilder.Entity("Medhub_Backend.Domain.Entities.TestType", b =>
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

                    b.Property<int?>("TestResultId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("TestResultId");

                    b.ToTable("test_type");
                });

            modelBuilder.Entity("Medhub_Backend.Domain.Entities.User", b =>
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

                    b.Property<bool>("HasToResetPassword")
                        .HasColumnType("boolean")
                        .HasColumnName("has_to_reset_password");

                    b.Property<string>("Password")
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

            modelBuilder.Entity("LaboratoryTestType", b =>
                {
                    b.HasOne("Medhub_Backend.Domain.Entities.Laboratory", null)
                        .WithMany()
                        .HasForeignKey("LaboratoriesId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Medhub_Backend.Domain.Entities.TestType", null)
                        .WithMany()
                        .HasForeignKey("TestTypesId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Medhub_Backend.Domain.Entities.Laboratory", b =>
                {
                    b.HasOne("Medhub_Backend.Domain.Entities.Clinic", "Clinic")
                        .WithMany("Laboratories")
                        .HasForeignKey("ClinicId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Clinic");
                });

            modelBuilder.Entity("Medhub_Backend.Domain.Entities.Patient", b =>
                {
                    b.HasOne("Medhub_Backend.Domain.Entities.User", "User")
                        .WithOne("Patient")
                        .HasForeignKey("Medhub_Backend.Domain.Entities.Patient", "UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("Medhub_Backend.Domain.Entities.TestRequest", b =>
                {
                    b.HasOne("Medhub_Backend.Domain.Entities.Clinic", "Clinic")
                        .WithMany()
                        .HasForeignKey("ClinicId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Medhub_Backend.Domain.Entities.User", "Doctor")
                        .WithMany()
                        .HasForeignKey("DoctorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Medhub_Backend.Domain.Entities.Laboratory", "Laboratory")
                        .WithMany()
                        .HasForeignKey("LaboratoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Medhub_Backend.Domain.Entities.User", "Patient")
                        .WithMany()
                        .HasForeignKey("PatientId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Clinic");

                    b.Navigation("Doctor");

                    b.Navigation("Laboratory");

                    b.Navigation("Patient");
                });

            modelBuilder.Entity("Medhub_Backend.Domain.Entities.TestResult", b =>
                {
                    b.HasOne("Medhub_Backend.Domain.Entities.TestRequest", "TestRequest")
                        .WithMany("TestResults")
                        .HasForeignKey("TestRequestId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("TestRequest");
                });

            modelBuilder.Entity("Medhub_Backend.Domain.Entities.TestType", b =>
                {
                    b.HasOne("Medhub_Backend.Domain.Entities.TestResult", null)
                        .WithMany("TestTypes")
                        .HasForeignKey("TestResultId");
                });

            modelBuilder.Entity("Medhub_Backend.Domain.Entities.User", b =>
                {
                    b.HasOne("Medhub_Backend.Domain.Entities.Clinic", "Clinic")
                        .WithMany("Users")
                        .HasForeignKey("ClinicId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Medhub_Backend.Domain.Entities.Role", "Role")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Clinic");

                    b.Navigation("Role");
                });

            modelBuilder.Entity("TestRequestTestType", b =>
                {
                    b.HasOne("Medhub_Backend.Domain.Entities.TestRequest", null)
                        .WithMany()
                        .HasForeignKey("TestRequestsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Medhub_Backend.Domain.Entities.TestType", null)
                        .WithMany()
                        .HasForeignKey("TestTypesId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Medhub_Backend.Domain.Entities.Clinic", b =>
                {
                    b.Navigation("Laboratories");

                    b.Navigation("Users");
                });

            modelBuilder.Entity("Medhub_Backend.Domain.Entities.TestRequest", b =>
                {
                    b.Navigation("TestResults");
                });

            modelBuilder.Entity("Medhub_Backend.Domain.Entities.TestResult", b =>
                {
                    b.Navigation("TestTypes");
                });

            modelBuilder.Entity("Medhub_Backend.Domain.Entities.User", b =>
                {
                    b.Navigation("Patient")
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}

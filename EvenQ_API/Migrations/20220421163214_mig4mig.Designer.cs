﻿// <auto-generated />
using System;
using EvenQ_API.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace EvenQ_API.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20220421163214_mig4mig")]
    partial class mig4mig
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.10")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("EvenQ_API.Model.Event", b =>
                {
                    b.Property<int>("IDEvent")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("LocationId")
                        .HasColumnType("int");

                    b.Property<byte[]>("PosterImage")
                        .HasColumnType("varbinary(max)");

                    b.Property<float>("TicketPrice")
                        .HasColumnType("real");

                    b.Property<string>("Title")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("IDEvent");

                    b.HasIndex("LocationId");

                    b.ToTable("Events");
                });

            modelBuilder.Entity("EvenQ_API.Model.Location", b =>
                {
                    b.Property<int>("IDLocation")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("City")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Coordinates")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Street")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("IDLocation");

                    b.ToTable("Locations");
                });

            modelBuilder.Entity("EvenQ_API.Model.Member", b =>
                {
                    b.Property<string>("UID")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("FirstName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsAdmin")
                        .HasColumnType("bit");

                    b.Property<string>("LastName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("MembershipValid")
                        .HasColumnType("bit");

                    b.Property<int>("NumberOfRefferals")
                        .HasColumnType("int");

                    b.Property<string>("RefferalCode")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UID");

                    b.ToTable("Members");
                });

            modelBuilder.Entity("EvenQ_API.Model.Refferal", b =>
                {
                    b.Property<int>("IDRefferal")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime2");

                    b.Property<string>("InviteeId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("InviterId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("IDRefferal");

                    b.HasIndex("InviteeId");

                    b.HasIndex("InviterId");

                    b.ToTable("Refferals");
                });

            modelBuilder.Entity("EvenQ_API.Model.Ticket", b =>
                {
                    b.Property<int>("IDTicket")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("EventId")
                        .HasColumnType("int");

                    b.Property<bool>("IsValid")
                        .HasColumnType("bit");

                    b.Property<string>("MemberId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("TicketQR")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("IDTicket");

                    b.HasIndex("EventId");

                    b.HasIndex("MemberId");

                    b.ToTable("Tickets");
                });

            modelBuilder.Entity("EvenQ_API.Model.Event", b =>
                {
                    b.HasOne("EvenQ_API.Model.Location", "Location")
                        .WithMany()
                        .HasForeignKey("LocationId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Location");
                });

            modelBuilder.Entity("EvenQ_API.Model.Refferal", b =>
                {
                    b.HasOne("EvenQ_API.Model.Member", "Invitee")
                        .WithMany()
                        .HasForeignKey("InviteeId");

                    b.HasOne("EvenQ_API.Model.Member", "Inviter")
                        .WithMany()
                        .HasForeignKey("InviterId");

                    b.Navigation("Invitee");

                    b.Navigation("Inviter");
                });

            modelBuilder.Entity("EvenQ_API.Model.Ticket", b =>
                {
                    b.HasOne("EvenQ_API.Model.Event", "Event")
                        .WithMany()
                        .HasForeignKey("EventId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("EvenQ_API.Model.Member", "Member")
                        .WithMany()
                        .HasForeignKey("MemberId");

                    b.Navigation("Event");

                    b.Navigation("Member");
                });
#pragma warning restore 612, 618
        }
    }
}

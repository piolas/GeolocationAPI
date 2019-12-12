﻿// <auto-generated />
using System;
using Geolocation.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Geolocation.Infrastructure.Migrations
{
    [DbContext(typeof(GeolocationDbContext))]
    partial class GeolocationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.11-servicing-32099")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Geolocation.Domain.Domain.Connection", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("asn");

                    b.Property<string>("isp");

                    b.HasKey("Id");

                    b.ToTable("Connection");
                });

            modelBuilder.Entity("Geolocation.Domain.Domain.Currency", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("code");

                    b.Property<string>("name");

                    b.Property<string>("plural");

                    b.Property<string>("symbol");

                    b.Property<string>("symbol_native");

                    b.HasKey("Id");

                    b.ToTable("Currency");
                });

            modelBuilder.Entity("Geolocation.Domain.Domain.Language", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<Guid?>("LocationId");

                    b.Property<string>("code");

                    b.Property<string>("name");

                    b.Property<string>("native");

                    b.HasKey("Id");

                    b.HasIndex("LocationId");

                    b.ToTable("Language");
                });

            modelBuilder.Entity("Geolocation.Domain.Domain.Location", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("calling_code");

                    b.Property<string>("capital");

                    b.Property<string>("country_flag");

                    b.Property<string>("country_flag_emoji");

                    b.Property<string>("country_flag_emoji_unicode");

                    b.Property<int>("geoname_id");

                    b.Property<bool>("is_eu");

                    b.HasKey("Id");

                    b.ToTable("Location");
                });

            modelBuilder.Entity("Geolocation.Domain.Domain.RootObject", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("URLValue");

                    b.Property<string>("city");

                    b.Property<Guid?>("connectionId");

                    b.Property<string>("continent_code");

                    b.Property<string>("continent_name");

                    b.Property<string>("country_code");

                    b.Property<string>("country_name");

                    b.Property<Guid?>("currencyId");

                    b.Property<string>("ip");

                    b.Property<double>("latitude");

                    b.Property<Guid?>("locationId");

                    b.Property<double>("longitude");

                    b.Property<string>("region_code");

                    b.Property<string>("region_name");

                    b.Property<string>("time_zoneid");

                    b.Property<string>("type");

                    b.Property<string>("zip");

                    b.HasKey("Id");

                    b.HasIndex("connectionId");

                    b.HasIndex("currencyId");

                    b.HasIndex("locationId");

                    b.HasIndex("time_zoneid");

                    b.ToTable("Geolocations");
                });

            modelBuilder.Entity("Geolocation.Domain.Domain.TimeZone", b =>
                {
                    b.Property<string>("id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("code");

                    b.Property<DateTime>("current_time");

                    b.Property<int>("gmt_offset");

                    b.Property<bool>("is_daylight_saving");

                    b.HasKey("id");

                    b.ToTable("TimeZone");
                });

            modelBuilder.Entity("Geolocation.Domain.Domain.Language", b =>
                {
                    b.HasOne("Geolocation.Domain.Domain.Location")
                        .WithMany("languages")
                        .HasForeignKey("LocationId");
                });

            modelBuilder.Entity("Geolocation.Domain.Domain.RootObject", b =>
                {
                    b.HasOne("Geolocation.Domain.Domain.Connection", "connection")
                        .WithMany()
                        .HasForeignKey("connectionId");

                    b.HasOne("Geolocation.Domain.Domain.Currency", "currency")
                        .WithMany()
                        .HasForeignKey("currencyId");

                    b.HasOne("Geolocation.Domain.Domain.Location", "location")
                        .WithMany()
                        .HasForeignKey("locationId");

                    b.HasOne("Geolocation.Domain.Domain.TimeZone", "time_zone")
                        .WithMany()
                        .HasForeignKey("time_zoneid");
                });
#pragma warning restore 612, 618
        }
    }
}

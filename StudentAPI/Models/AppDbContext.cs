using System;
using System.Collections.Generic;
using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Data.SqlClient;
using System.Runtime.Serialization;

namespace Entities
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions options) : base(options) { }

        public DbSet<Student>? Students { get; set; }
        public DbSet<Address>? Addresses { get; set; }
        public DbSet<Contact>? Contacts { get; set; }
        public DbSet<Code>? Codes { get; set; }
        public DbSet<CodeValue>? CodeValues { get; set; }
        public DbSet<Country>? Countries { get; set; }
        public DbSet<Region>? Regions { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Table Namings
            modelBuilder.Entity<Student>().ToTable("Student");
            modelBuilder.Entity<Address>().ToTable("Address");
            modelBuilder.Entity<Contact>().ToTable("Contact");
            modelBuilder.Entity<Code>().ToTable("Code");
            modelBuilder.Entity<CodeValue>().ToTable("CodeValue");
            modelBuilder.Entity<Country>().ToTable("Countries");
            modelBuilder.Entity<Region>().ToTable("Region");

            // Adding Foreign Keys for the tables

            // StudentId for address
            modelBuilder.Entity<Address>()
                .HasOne<Student>()
                .WithMany(s => s.Addresses)
                .HasForeignKey(a => a.StudentId)
                .OnDelete(DeleteBehavior.Cascade);

            // StudentId for Contact
            modelBuilder.Entity<Contact>()
                .HasOne<Student>()
                .WithMany(s => s.Contacts)
                .HasForeignKey(c => c.StudentId)
                .OnDelete(DeleteBehavior.Cascade);

            // CodeId for CodeValue
            modelBuilder.Entity<CodeValue>()
                .HasOne<Code>()
                .WithMany()
                .HasForeignKey(c => c.CodeId)
                .OnDelete(DeleteBehavior.Cascade);


            /*
             * Seeding Data to Tables on Creation *
            */

            // Seed Data to Code Table

            // First Read the Data from the json files

            // Code File
            string codeJson = System.IO.File.ReadAllText("DummyData/Code.json");
            List<Code>? codes = System.Text.Json.JsonSerializer.Deserialize<List<Code>>(codeJson);

            // CodeValues File
            string codeValuesJson = System.IO.File.ReadAllText("DummyData/CodeValues.json");
            List<CodeValue>? codeValues = System.Text.Json.JsonSerializer.Deserialize<List<CodeValue>>(codeValuesJson);

            // Countries File
            string countriesJson = System.IO.File.ReadAllText("DummyData/Countries.json");
            List<Country>? countries = System.Text.Json.JsonSerializer.Deserialize<List<Country>>(countriesJson);

            // Regions File
            string regionJson = System.IO.File.ReadAllText("DummyData/Regions.json");
            List<Region>? regions = System.Text.Json.JsonSerializer.Deserialize<List<Region>>(regionJson);

			// Students File
			string studentsJson = System.IO.File.ReadAllText("DummyData/Students.json");
            List<Student>? students = System.Text.Json.JsonSerializer.Deserialize<List<Student>>(studentsJson);

            // Addresses File
			string addressJson = System.IO.File.ReadAllText("DummyData/Addresses.json");
            List<Address>? addresses = System.Text.Json.JsonSerializer.Deserialize<List<Address>>(addressJson);

			// Contacts File
			string contactJson = System.IO.File.ReadAllText("DummyData/Contacts.json");
			List<Contact>? contacts = System.Text.Json.JsonSerializer.Deserialize<List<Contact>>(contactJson);


			foreach (var code in codes)
            {
                modelBuilder.Entity<Code>().HasData(code);
            }

            foreach (var codeValue in codeValues)
            {
                modelBuilder.Entity<CodeValue>().HasData(codeValue);
            }

            foreach(Country country in countries)
            {
                modelBuilder.Entity<Country>().HasData(country);
            }

            foreach (Region region in regions)
            {
                modelBuilder.Entity<Region>().HasData(region);
            }

			foreach(Student student in students)
            {
                modelBuilder.Entity<Student>().HasData(student);
            }

			foreach (Address address in addresses)
			{
				modelBuilder.Entity<Address>().HasData(address);
			}

			foreach (Contact contact in contacts)
			{
				modelBuilder.Entity<Contact>().HasData(contact);
			}

		}
    }
}

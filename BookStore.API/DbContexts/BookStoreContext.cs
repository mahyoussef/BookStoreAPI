using BookStore.API.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.API.DbContexts
{
    public class BookStoreContext : DbContext
    {
        public BookStoreContext(DbContextOptions<BookStoreContext> options)
            : base(options)
        {
        }
        public DbSet<Author> Authors { get; set; }
        public DbSet<Book> Books { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<ApplicationUser> ApplicationUsers { get; set; }
        public DbSet<ShoppingCart> ShoppingCarts { get; set; }
        public DbSet<OrderHeader> OrderHeaders { get; set; }
        public DbSet<OrderDetails> OrderDetails { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }
        public DbSet<BookCategories> BookCategories { get; set; }
        public DbSet<ApplicationUserCategories> ApplicationUserCategories { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<BookCategories>().HasKey(sc => new { sc.BookId, sc.CategoryId });
            modelBuilder.Entity<ApplicationUserCategories>().HasKey(sc => new { sc.ApplicationUserId, sc.CategoryId });

            modelBuilder.Entity<ApplicationUserCategories>()
           .HasOne<ApplicationUser>(sc => sc.ApplicationUser)
           .WithMany(s => s.ApplicationUserCategories)
           .HasForeignKey(sc => sc.ApplicationUserId);
            modelBuilder.Entity<ApplicationUserCategories>()
                .HasOne<Category>(sc => sc.Category)
                .WithMany(s => s.ApplicationUserCategories)
                .HasForeignKey(sc => sc.CategoryId);

            modelBuilder.Entity<BookCategories>()
          .HasOne<Book>(sc => sc.Book)
          .WithMany(s => s.BookCategories)
          .HasForeignKey(sc => sc.BookId);
            modelBuilder.Entity<BookCategories>()
                .HasOne<Category>(sc => sc.Category)
                .WithMany(s => s.BookCategories)
                .HasForeignKey(sc => sc.CategoryId);


            modelBuilder.Entity<ApplicationUser>()
           .HasIndex(u => u.Email)
           .IsUnique();

            modelBuilder.Entity<Category>().HasData(
                new
                {
                    Id = Guid.Parse("b044828c-5f73-437e-81c8-089b85d81f94"),
                    Name = "Thriller"
                },
                 new
                 {
                     Id = Guid.Parse("4106f579-7696-4613-bfc4-498a0f78e921"),
                     Name = "Classics"
                 },
                  new
                  {
                      Id = Guid.Parse("2688fe22-29c6-4372-bb88-3c1fe45a8f02"),
                      Name = "Self-Development"
                  },
                   new
                   {
                       Id = Guid.Parse("69a4ddde-d290-42db-b035-96afa747396d"),
                       Name = "Romance"
                   },
                    new
                    {
                        Id = Guid.Parse("20442e97-6f21-4d0e-8a1f-434351238ca2"),
                        Name = "Mystery"
                    }
                );


            modelBuilder.Entity<Author>().HasData(
                new Author()
                {
                    Id = Guid.Parse("d28888e9-2ba9-473a-a40f-e38cb54f9b35"),
                    FirstName = "Berry",
                    LastName = "Griffin Beak Eldritch",
                    DateOfBirth = new DateTime(1980, 7, 23),
                    City = "London"
                },
                new Author()
                {
                    Id = Guid.Parse("da2fd609-d754-4feb-8acd-c4f9ff13ba96"),
                    FirstName = "Nancy",
                    LastName = "Swashbuckler Rye",
                    DateOfBirth = new DateTime(1970, 5, 21),
                    City = "Canda"
                },
                new Author()
                {
                    Id = Guid.Parse("2902b665-1190-4c70-9915-b9c2d7680450"),
                    FirstName = "Eli",
                    LastName = "Ivory Bones Sweet",
                    DateOfBirth = new DateTime(1995, 12, 16),
                    City = "Singaphore"
                },
                new Author()
                {
                    Id = Guid.Parse("102b566b-ba1f-404c-b2df-e2cde39ade09"),
                    FirstName = "Arnold",
                    LastName = "The Unseen Stafford",
                    DateOfBirth = new DateTime(1978, 3, 6),
                    City = "Tottenham"
                },
                new Author()
                {
                    Id = Guid.Parse("5b3621c0-7b12-4e80-9c8b-3398cba7ee05"),
                    FirstName = "Seabury",
                    LastName = "Toxic Reyson",
                    DateOfBirth = new DateTime(1988, 11, 23),
                    City = "Paris"
                },
                new Author()
                {
                    Id = Guid.Parse("2aadd2df-7caf-45ab-9355-7f6332985a87"),
                    FirstName = "Rutherford",
                    LastName = "Fearless Cloven",
                    DateOfBirth = new DateTime(1966, 4, 5),
                    City = "Madrid"
                },
                new Author()
                {
                    Id = Guid.Parse("2ee49fe3-edf2-4f91-8409-3eb25ce6ca51"),
                    FirstName = "Atherton",
                    LastName = "Crow Ridley",
                    DateOfBirth = new DateTime(1976, 10, 11),
                    City = "München"
                }
                );

            modelBuilder.Entity<ApplicationUser>()
                .HasIndex(u => u.Email)
                .IsUnique();

            modelBuilder.Entity<ApplicationUser>().HasData(
                new
                {
                    Id = Guid.Parse("6f48ae73-28bc-40f4-aadb-2cca2c7399e6"),
                    FirstName = "Mahmoud",
                    LastName = "Youssef",
                    Email = "Mahmoud@gmail.com",
                    Password = "Mm@123456",
                    PhoneNumber = "01229728943",
                    Address = "14th Hegazy St",
                    UserRoleId = Guid.Parse("b4ad8066-9164-45f9-9653-44c2deaf7098")
                },
                new
                {
                    Id = Guid.Parse("d6d95f7b-4245-496f-b769-b7db0affa1e1"),
                    FirstName = "Ahmed",
                    LastName = "Samy",
                    Email = "Ahmed@gmail.com",
                    Password = "Aa@123456",
                    PhoneNumber = "01229728943",
                    Address = "14th Hegazy St",
                    UserRoleId = Guid.Parse("78c2f354-a8bf-4554-ae9d-6b08d7b82bab")
                }
                );

            modelBuilder.Entity<UserRole>().HasData(
                new
                {
                    Id = Guid.Parse("78c2f354-a8bf-4554-ae9d-6b08d7b82bab"),
                    Name = "Admin"
                },
                 new
                 {
                     Id = Guid.Parse("b4ad8066-9164-45f9-9653-44c2deaf7098"),
                     Name = "Customer"
                 }
                );

            modelBuilder.Entity<ApplicationUserCategories>().HasData(
                new
                {
                    Id = Guid.Parse("5fe22ac5-6151-4961-8fe1-2e76b40a70b6"),
                    ApplicationUserId = Guid.Parse("6f48ae73-28bc-40f4-aadb-2cca2c7399e6"),
                    CategoryId = Guid.Parse("4106f579-7696-4613-bfc4-498a0f78e921")
                },
                new
                {
                    Id = Guid.Parse("91486cb9-96e8-4ecb-af36-767ab431e4ac"),
                    ApplicationUserId = Guid.Parse("d6d95f7b-4245-496f-b769-b7db0affa1e1"),
                    CategoryId = Guid.Parse("b044828c-5f73-437e-81c8-089b85d81f94")
                }
                );

            
            modelBuilder.Entity<Book>().HasData(
              new Book
              {
                  Id = Guid.Parse("5b1c2b4d-48c7-402a-80c3-cc796ad49c6b"),
                  AuthorId = Guid.Parse("2ee49fe3-edf2-4f91-8409-3eb25ce6ca51"),
                  PublisherId = Guid.Parse("9571262f-094c-43ec-b1e6-4bd6201cfd0c"),
                  Title = "Eloquent JavaScript, Second Edition",
                  Description = "JavaScript lies at the heart of almost every modern web application, from social apps to the newest browser-based.",
                  TotalNumberOfPages = 472,
                  ISBN = "9781593275846"
              },
              new Book
              {
                  Id = Guid.Parse("d8663e5e-7494-4f81-8739-6e0de1bea7ee"),
                  AuthorId = Guid.Parse("2ee49fe3-edf2-4f91-8409-3eb25ce6ca51"),
                  PublisherId = Guid.Parse("1135cd78-f7dd-402d-948b-4da57348b2f9"),
                  Title = "Learning JavaScript Design Patterns",
                  Description = "JavaScript lies at the heart of almost every modern web application, from social apps to the newest browser-based.",
                  TotalNumberOfPages = 254,
                  ISBN = "9781449331818"
              },
              new Book
              {
                  Id = Guid.Parse("d173e20d-159e-4127-9ce9-b0ac2564ad97"),
                  AuthorId = Guid.Parse("da2fd609-d754-4feb-8acd-c4f9ff13ba96"),
                  PublisherId = Guid.Parse("1135cd78-f7dd-402d-948b-4da57348b2f9"),
                  Title = "Speaking JavaScript",
                  Description = "JavaScript lies at the heart of almost every modern web application, from social apps to the newest browser-based.",
                  TotalNumberOfPages = 460,
                  ISBN = "9781449365035"
              },
              new Book
              {
                  Id = Guid.Parse("40ff5488-fdab-45b5-bc3a-14302d59869a"),
                  AuthorId = Guid.Parse("2902b665-1190-4c70-9915-b9c2d7680450"),
                  PublisherId = Guid.Parse("02906a11-0b84-47c6-a9f8-2db076fc9dae"),
                  Title = "Programming JavaScript Applications",
                  Description = "Robust Web Architecture with Node, HTML5, and Modern JS Libraries, from social apps to the newest browser-based.",
                  TotalNumberOfPages = 254,
                  ISBN = "9781491950296"
              },
              new Book
              {
                  Id = Guid.Parse("40ff5488-fdab-45b5-bc3a-14302d59869c"),
                  AuthorId = Guid.Parse("2902b665-1190-4c70-9915-b9c2d7680450"),
                  PublisherId = Guid.Parse("02906a11-0b84-47c6-a9f8-2db076fc9dae"),
                  Title = "Programming Node.Js Applications",
                  Description = "Robust Web Architecture with Node, HTML5, and Modern JS Libraries, from social apps to the newest browser-based.",
                  TotalNumberOfPages = 180,
                  ISBN = "9781491904244"
              }
              );

            modelBuilder.Entity<Publisher>().HasData(
                new
                {
                    Id = Guid.Parse("1135cd78-f7dd-402d-948b-4da57348b2f9"),
                    Name = "El-Rawaq"
                },
                new
                {
                    Id = Guid.Parse("9571262f-094c-43ec-b1e6-4bd6201cfd0c"),
                    Name = "Dawen"
                },
                new
                {
                    Id = Guid.Parse("02906a11-0b84-47c6-a9f8-2db076fc9dae"),
                    Name = "El-Shorouk"
                }
                );

            modelBuilder.Entity<BookCategories>().HasData(
                new
                {
                    Id = Guid.Parse("b8814929-f9ec-4a88-9b69-5e1f62d53056"),
                    BookId = Guid.Parse("40ff5488-fdab-45b5-bc3a-14302d59869c"),
                    CategoryId = Guid.Parse("20442e97-6f21-4d0e-8a1f-434351238ca2")
                },
                new
                {
                    Id = Guid.Parse("dbba41cb-4ac4-4587-b490-6186ab39a4ea"),
                    BookId = Guid.Parse("40ff5488-fdab-45b5-bc3a-14302d59869a"),
                    CategoryId = Guid.Parse("20442e97-6f21-4d0e-8a1f-434351238ca2")
                },
                new
                {
                    Id = Guid.Parse("91bd26e3-faf9-43eb-a4ad-1bd5dade738a"),
                    BookId = Guid.Parse("40ff5488-fdab-45b5-bc3a-14302d59869c"),
                    CategoryId = Guid.Parse("4106f579-7696-4613-bfc4-498a0f78e921")
                },
                new
                {
                    Id = Guid.Parse("5ff09974-9d37-44b1-ba3e-4c6a89d0b07c"),
                    BookId = Guid.Parse("d8663e5e-7494-4f81-8739-6e0de1bea7ee"),
                    CategoryId = Guid.Parse("b044828c-5f73-437e-81c8-089b85d81f94")
                }
                );

            modelBuilder.Entity<ShoppingCart>().HasData(
                new
                {
                    Id = Guid.Parse("79404b3f-53bb-4687-b836-87b318e841fd"),
                    BookId = Guid.Parse("d8663e5e-7494-4f81-8739-6e0de1bea7ee"),
                    ApplicationUserId = Guid.Parse("d6d95f7b-4245-496f-b769-b7db0affa1e1"),
                    Quantity = 3
                },
                new
                {
                    Id = Guid.Parse("ca909b9b-881e-4f77-9532-3c2c94b82700"),
                    BookId = Guid.Parse("40ff5488-fdab-45b5-bc3a-14302d59869c"),
                    ApplicationUserId = Guid.Parse("d6d95f7b-4245-496f-b769-b7db0affa1e1"),
                    Quantity = 2
                },
                new
                {
                    Id = Guid.Parse("cf816876-db46-4f9c-9962-c9a39493e8a7"),
                    BookId = Guid.Parse("40ff5488-fdab-45b5-bc3a-14302d59869a"),
                    ApplicationUserId = Guid.Parse("6f48ae73-28bc-40f4-aadb-2cca2c7399e6"),
                    Quantity = 1
                },
                new
                {
                    Id = Guid.Parse("78f10e08-fa49-4ef0-9789-59c1e90eeea1"),
                    BookId = Guid.Parse("5b1c2b4d-48c7-402a-80c3-cc796ad49c6b"),
                    ApplicationUserId = Guid.Parse("6f48ae73-28bc-40f4-aadb-2cca2c7399e6"),
                    Quantity = 1
                }
                );
        }
    }
}

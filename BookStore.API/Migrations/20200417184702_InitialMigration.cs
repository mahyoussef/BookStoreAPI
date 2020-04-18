using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BookStore.API.Migrations
{
    public partial class InitialMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Authors",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    FirstName = table.Column<string>(maxLength: 150, nullable: false),
                    LastName = table.Column<string>(maxLength: 150, nullable: false),
                    City = table.Column<string>(nullable: false),
                    DateOfBirth = table.Column<DateTimeOffset>(nullable: false),
                    DateOfDeath = table.Column<DateTimeOffset>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Authors", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Publisher",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Publisher", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UserRoles",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Books",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Title = table.Column<string>(maxLength: 150, nullable: false),
                    ISBN = table.Column<string>(maxLength: 15, nullable: false),
                    Description = table.Column<string>(maxLength: 150, nullable: false),
                    PublisherId = table.Column<Guid>(nullable: false),
                    TotalNumberOfPages = table.Column<int>(nullable: false),
                    AuthorId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Books", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Books_Authors_AuthorId",
                        column: x => x.AuthorId,
                        principalTable: "Authors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Books_Publisher_PublisherId",
                        column: x => x.PublisherId,
                        principalTable: "Publisher",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ApplicationUsers",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    FirstName = table.Column<string>(maxLength: 150, nullable: false),
                    LastName = table.Column<string>(maxLength: 150, nullable: false),
                    Email = table.Column<string>(nullable: false),
                    PhoneNumber = table.Column<string>(nullable: false),
                    Address = table.Column<string>(nullable: true),
                    UserRoleId = table.Column<Guid>(nullable: false),
                    Password = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApplicationUsers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ApplicationUsers_UserRoles_UserRoleId",
                        column: x => x.UserRoleId,
                        principalTable: "UserRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BookCategories",
                columns: table => new
                {
                    BookId = table.Column<Guid>(nullable: false),
                    CategoryId = table.Column<Guid>(nullable: false),
                    Id = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BookCategories", x => new { x.BookId, x.CategoryId });
                    table.ForeignKey(
                        name: "FK_BookCategories_Books_BookId",
                        column: x => x.BookId,
                        principalTable: "Books",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BookCategories_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ApplicationUserCategories",
                columns: table => new
                {
                    ApplicationUserId = table.Column<Guid>(nullable: false),
                    CategoryId = table.Column<Guid>(nullable: false),
                    Id = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApplicationUserCategories", x => new { x.ApplicationUserId, x.CategoryId });
                    table.ForeignKey(
                        name: "FK_ApplicationUserCategories_ApplicationUsers_ApplicationUserId",
                        column: x => x.ApplicationUserId,
                        principalTable: "ApplicationUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ApplicationUserCategories_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "OrderHeaders",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    ApplicationUserId = table.Column<Guid>(nullable: false),
                    Price = table.Column<double>(nullable: false),
                    TotalOrderPrice = table.Column<double>(nullable: false),
                    ShippingName = table.Column<string>(maxLength: 40, nullable: false),
                    ShippingAddress = table.Column<string>(nullable: false),
                    PhoneNumber = table.Column<string>(nullable: false),
                    OrderDate = table.Column<DateTimeOffset>(nullable: false),
                    CoupounCode = table.Column<string>(nullable: true),
                    CouponCodeDiscount = table.Column<double>(nullable: false),
                    Comments = table.Column<string>(nullable: true),
                    PaymentStatus = table.Column<string>(nullable: true),
                    TransactionId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderHeaders", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OrderHeaders_ApplicationUsers_ApplicationUserId",
                        column: x => x.ApplicationUserId,
                        principalTable: "ApplicationUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ShoppingCarts",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    BookId = table.Column<Guid>(nullable: false),
                    ApplicationUserId = table.Column<Guid>(nullable: false),
                    Quantity = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ShoppingCarts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ShoppingCarts_ApplicationUsers_ApplicationUserId",
                        column: x => x.ApplicationUserId,
                        principalTable: "ApplicationUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ShoppingCarts_Books_BookId",
                        column: x => x.BookId,
                        principalTable: "Books",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "OrderDetails",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    OrderHeaderId = table.Column<Guid>(nullable: false),
                    BookId = table.Column<Guid>(nullable: false),
                    Quantity = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderDetails", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OrderDetails_Books_BookId",
                        column: x => x.BookId,
                        principalTable: "Books",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OrderDetails_OrderHeaders_OrderHeaderId",
                        column: x => x.OrderHeaderId,
                        principalTable: "OrderHeaders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Authors",
                columns: new[] { "Id", "City", "DateOfBirth", "DateOfDeath", "FirstName", "LastName" },
                values: new object[,]
                {
                    { new Guid("d28888e9-2ba9-473a-a40f-e38cb54f9b35"), "London", new DateTimeOffset(new DateTime(1980, 7, 23, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 3, 0, 0, 0)), null, "Berry", "Griffin Beak Eldritch" },
                    { new Guid("da2fd609-d754-4feb-8acd-c4f9ff13ba96"), "Canda", new DateTimeOffset(new DateTime(1970, 5, 21, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 3, 0, 0, 0)), null, "Nancy", "Swashbuckler Rye" },
                    { new Guid("2902b665-1190-4c70-9915-b9c2d7680450"), "Singaphore", new DateTimeOffset(new DateTime(1995, 12, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 2, 0, 0, 0)), null, "Eli", "Ivory Bones Sweet" },
                    { new Guid("102b566b-ba1f-404c-b2df-e2cde39ade09"), "Tottenham", new DateTimeOffset(new DateTime(1978, 3, 6, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 2, 0, 0, 0)), null, "Arnold", "The Unseen Stafford" },
                    { new Guid("5b3621c0-7b12-4e80-9c8b-3398cba7ee05"), "Paris", new DateTimeOffset(new DateTime(1988, 11, 23, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 2, 0, 0, 0)), null, "Seabury", "Toxic Reyson" },
                    { new Guid("2aadd2df-7caf-45ab-9355-7f6332985a87"), "Madrid", new DateTimeOffset(new DateTime(1966, 4, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 2, 0, 0, 0)), null, "Rutherford", "Fearless Cloven" },
                    { new Guid("2ee49fe3-edf2-4f91-8409-3eb25ce6ca51"), "München", new DateTimeOffset(new DateTime(1976, 10, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 2, 0, 0, 0)), null, "Atherton", "Crow Ridley" }
                });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { new Guid("20442e97-6f21-4d0e-8a1f-434351238ca2"), "Mystery" },
                    { new Guid("69a4ddde-d290-42db-b035-96afa747396d"), "Romance" },
                    { new Guid("4106f579-7696-4613-bfc4-498a0f78e921"), "Classics" },
                    { new Guid("b044828c-5f73-437e-81c8-089b85d81f94"), "Thriller" },
                    { new Guid("2688fe22-29c6-4372-bb88-3c1fe45a8f02"), "Self-Development" }
                });

            migrationBuilder.InsertData(
                table: "Publisher",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { new Guid("1135cd78-f7dd-402d-948b-4da57348b2f9"), "El-Rawaq" },
                    { new Guid("9571262f-094c-43ec-b1e6-4bd6201cfd0c"), "Dawen" },
                    { new Guid("02906a11-0b84-47c6-a9f8-2db076fc9dae"), "El-Shorouk" }
                });

            migrationBuilder.InsertData(
                table: "UserRoles",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { new Guid("78c2f354-a8bf-4554-ae9d-6b08d7b82bab"), "Admin" },
                    { new Guid("b4ad8066-9164-45f9-9653-44c2deaf7098"), "Customer" }
                });

            migrationBuilder.InsertData(
                table: "ApplicationUsers",
                columns: new[] { "Id", "Address", "Email", "FirstName", "LastName", "Password", "PhoneNumber", "UserRoleId" },
                values: new object[,]
                {
                    { new Guid("d6d95f7b-4245-496f-b769-b7db0affa1e1"), "14th Hegazy St", "Ahmed@gmail.com", "Ahmed", "Samy", "Aa@123456", "01229728943", new Guid("78c2f354-a8bf-4554-ae9d-6b08d7b82bab") },
                    { new Guid("6f48ae73-28bc-40f4-aadb-2cca2c7399e6"), "14th Hegazy St", "Mahmoud@gmail.com", "Mahmoud", "Youssef", "Mm@123456", "01229728943", new Guid("b4ad8066-9164-45f9-9653-44c2deaf7098") }
                });

            migrationBuilder.InsertData(
                table: "Books",
                columns: new[] { "Id", "AuthorId", "Description", "ISBN", "PublisherId", "Title", "TotalNumberOfPages" },
                values: new object[,]
                {
                    { new Guid("d8663e5e-7494-4f81-8739-6e0de1bea7ee"), new Guid("2ee49fe3-edf2-4f91-8409-3eb25ce6ca51"), "JavaScript lies at the heart of almost every modern web application, from social apps to the newest browser-based.", "9781449331818", new Guid("1135cd78-f7dd-402d-948b-4da57348b2f9"), "Learning JavaScript Design Patterns", 254 },
                    { new Guid("d173e20d-159e-4127-9ce9-b0ac2564ad97"), new Guid("da2fd609-d754-4feb-8acd-c4f9ff13ba96"), "JavaScript lies at the heart of almost every modern web application, from social apps to the newest browser-based.", "9781449365035", new Guid("1135cd78-f7dd-402d-948b-4da57348b2f9"), "Speaking JavaScript", 460 },
                    { new Guid("5b1c2b4d-48c7-402a-80c3-cc796ad49c6b"), new Guid("2ee49fe3-edf2-4f91-8409-3eb25ce6ca51"), "JavaScript lies at the heart of almost every modern web application, from social apps to the newest browser-based.", "9781593275846", new Guid("9571262f-094c-43ec-b1e6-4bd6201cfd0c"), "Eloquent JavaScript, Second Edition", 472 },
                    { new Guid("40ff5488-fdab-45b5-bc3a-14302d59869a"), new Guid("2902b665-1190-4c70-9915-b9c2d7680450"), "Robust Web Architecture with Node, HTML5, and Modern JS Libraries, from social apps to the newest browser-based.", "9781491950296", new Guid("02906a11-0b84-47c6-a9f8-2db076fc9dae"), "Programming JavaScript Applications", 254 },
                    { new Guid("40ff5488-fdab-45b5-bc3a-14302d59869c"), new Guid("2902b665-1190-4c70-9915-b9c2d7680450"), "Robust Web Architecture with Node, HTML5, and Modern JS Libraries, from social apps to the newest browser-based.", "9781491904244", new Guid("02906a11-0b84-47c6-a9f8-2db076fc9dae"), "Programming Node.Js Applications", 180 }
                });

            migrationBuilder.InsertData(
                table: "ApplicationUserCategories",
                columns: new[] { "ApplicationUserId", "CategoryId", "Id" },
                values: new object[,]
                {
                    { new Guid("d6d95f7b-4245-496f-b769-b7db0affa1e1"), new Guid("b044828c-5f73-437e-81c8-089b85d81f94"), new Guid("91486cb9-96e8-4ecb-af36-767ab431e4ac") },
                    { new Guid("6f48ae73-28bc-40f4-aadb-2cca2c7399e6"), new Guid("4106f579-7696-4613-bfc4-498a0f78e921"), new Guid("5fe22ac5-6151-4961-8fe1-2e76b40a70b6") }
                });

            migrationBuilder.InsertData(
                table: "BookCategories",
                columns: new[] { "BookId", "CategoryId", "Id" },
                values: new object[,]
                {
                    { new Guid("d8663e5e-7494-4f81-8739-6e0de1bea7ee"), new Guid("b044828c-5f73-437e-81c8-089b85d81f94"), new Guid("5ff09974-9d37-44b1-ba3e-4c6a89d0b07c") },
                    { new Guid("40ff5488-fdab-45b5-bc3a-14302d59869a"), new Guid("20442e97-6f21-4d0e-8a1f-434351238ca2"), new Guid("dbba41cb-4ac4-4587-b490-6186ab39a4ea") },
                    { new Guid("40ff5488-fdab-45b5-bc3a-14302d59869c"), new Guid("20442e97-6f21-4d0e-8a1f-434351238ca2"), new Guid("b8814929-f9ec-4a88-9b69-5e1f62d53056") },
                    { new Guid("40ff5488-fdab-45b5-bc3a-14302d59869c"), new Guid("4106f579-7696-4613-bfc4-498a0f78e921"), new Guid("91bd26e3-faf9-43eb-a4ad-1bd5dade738a") }
                });

            migrationBuilder.InsertData(
                table: "ShoppingCarts",
                columns: new[] { "Id", "ApplicationUserId", "BookId", "Quantity" },
                values: new object[,]
                {
                    { new Guid("79404b3f-53bb-4687-b836-87b318e841fd"), new Guid("d6d95f7b-4245-496f-b769-b7db0affa1e1"), new Guid("d8663e5e-7494-4f81-8739-6e0de1bea7ee"), 3 },
                    { new Guid("ca909b9b-881e-4f77-9532-3c2c94b82700"), new Guid("d6d95f7b-4245-496f-b769-b7db0affa1e1"), new Guid("40ff5488-fdab-45b5-bc3a-14302d59869c"), 2 },
                    { new Guid("cf816876-db46-4f9c-9962-c9a39493e8a7"), new Guid("6f48ae73-28bc-40f4-aadb-2cca2c7399e6"), new Guid("40ff5488-fdab-45b5-bc3a-14302d59869a"), 1 },
                    { new Guid("78f10e08-fa49-4ef0-9789-59c1e90eeea1"), new Guid("6f48ae73-28bc-40f4-aadb-2cca2c7399e6"), new Guid("5b1c2b4d-48c7-402a-80c3-cc796ad49c6b"), 1 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_ApplicationUserCategories_CategoryId",
                table: "ApplicationUserCategories",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_ApplicationUsers_Email",
                table: "ApplicationUsers",
                column: "Email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ApplicationUsers_UserRoleId",
                table: "ApplicationUsers",
                column: "UserRoleId");

            migrationBuilder.CreateIndex(
                name: "IX_BookCategories_CategoryId",
                table: "BookCategories",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Books_AuthorId",
                table: "Books",
                column: "AuthorId");

            migrationBuilder.CreateIndex(
                name: "IX_Books_PublisherId",
                table: "Books",
                column: "PublisherId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderDetails_BookId",
                table: "OrderDetails",
                column: "BookId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderDetails_OrderHeaderId",
                table: "OrderDetails",
                column: "OrderHeaderId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderHeaders_ApplicationUserId",
                table: "OrderHeaders",
                column: "ApplicationUserId");

            migrationBuilder.CreateIndex(
                name: "IX_ShoppingCarts_ApplicationUserId",
                table: "ShoppingCarts",
                column: "ApplicationUserId");

            migrationBuilder.CreateIndex(
                name: "IX_ShoppingCarts_BookId",
                table: "ShoppingCarts",
                column: "BookId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ApplicationUserCategories");

            migrationBuilder.DropTable(
                name: "BookCategories");

            migrationBuilder.DropTable(
                name: "OrderDetails");

            migrationBuilder.DropTable(
                name: "ShoppingCarts");

            migrationBuilder.DropTable(
                name: "Categories");

            migrationBuilder.DropTable(
                name: "OrderHeaders");

            migrationBuilder.DropTable(
                name: "Books");

            migrationBuilder.DropTable(
                name: "ApplicationUsers");

            migrationBuilder.DropTable(
                name: "Authors");

            migrationBuilder.DropTable(
                name: "Publisher");

            migrationBuilder.DropTable(
                name: "UserRoles");
        }
    }
}

using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Migrations;

namespace RazorPagesMovie1.Migrations.ApplicationDb
{
    public partial class SeedAdminOnly : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // Seed admin user
            var hasher = new PasswordHasher<IdentityUser>();
            var admin = new IdentityUser
            {
                Id = "admin-id",
                UserName = "admin@blockbusters.com",
                NormalizedUserName = "ADMIN@BLOCKBUSTERS.COM",
                Email = "admin@blockbusters.com",
                NormalizedEmail = "ADMIN@BLOCKBUSTERS.COM",
                EmailConfirmed = true,
                SecurityStamp = Guid.NewGuid().ToString("D")
            };

            admin.PasswordHash = hasher.HashPassword(admin, "AdminPassword123!");

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "UserName", "NormalizedUserName", "Email", "NormalizedEmail", "EmailConfirmed", "PasswordHash", "SecurityStamp" },
                values: new object[] { admin.Id, admin.UserName, admin.NormalizedUserName, admin.Email, admin.NormalizedEmail, admin.EmailConfirmed, admin.PasswordHash, admin.SecurityStamp }
            );
        }
    }
}
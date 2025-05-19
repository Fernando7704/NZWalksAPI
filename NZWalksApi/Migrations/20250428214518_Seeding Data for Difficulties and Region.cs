using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace NZWalksApi.Migrations
{
    /// <inheritdoc />
    public partial class SeedingDataforDifficultiesandRegion : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "difficulties",
                columns: new[] { "id", "Name" },
                values: new object[,]
                {
                    { new Guid("284a9d17-9e07-4796-a0c8-199cb2ebd637"), "Dificil" },
                    { new Guid("55a87c60-33b4-4bcf-96a8-76c644dd7e20"), "Normal" },
                    { new Guid("752f441b-3268-4729-845a-02df3c0e7e13"), "Facil" }
                });

            migrationBuilder.InsertData(
                table: "regions",
                columns: new[] { "Id", "Code", "Name", "RegionImagenUrl" },
                values: new object[,]
                {
                    { new Guid("470b646a-063e-4a94-8346-f50135212f6d"), "WGN", "Wellington", "https://www.google.com/url?sa=i&url=https%3A%2F%2Fes.pngtree.com%2Ffree-backgrounds-photos%2Ffotos-anchas-pictures&psig=AOvVaw18mVrfLuAspgyKVvaKJG9A&ust=1745962585637000&source=images&cd=vfe&opi=89978449&ved=0CBQQjRxqFwoTCPDtstPX-4wDFQAAAAAdAAAAABAE" },
                    { new Guid("8f4eacfc-eaab-4cde-b87d-2b4544b8a84a"), "BOP", "Bay of Plenty", "https://www.google.com/url?sa=i&url=https%3A%2F%2Fes.pngtree.com%2Ffree-backgrounds-photos%2Ffotos-anchas-pictures&psig=AOvVaw18mVrfLuAspgyKVvaKJG9A&ust=1745962585637000&source=images&cd=vfe&opi=89978449&ved=0CBQQjRxqFwoTCPDtstPX-4wDFQAAAAAdAAAAABAE" },
                    { new Guid("e5973695-f958-41d3-8b68-8ec2d89cb159"), "MSN", "Nelson", "https://www.google.com/url?sa=i&url=https%3A%2F%2Fes.pngtree.com%2Ffree-backgrounds-photos%2Ffotos-anchas-pictures&psig=AOvVaw18mVrfLuAspgyKVvaKJG9A&ust=1745962585637000&source=images&cd=vfe&opi=89978449&ved=0CBQQjRxqFwoTCPDtstPX-4wDFQAAAAAdAAAAABAE" },
                    { new Guid("fc6d6315-4fff-41b7-8a3b-53d0224a477d"), "STL", "Southland", "https://www.google.com/url?sa=i&url=https%3A%2F%2Fes.pngtree.com%2Ffree-backgrounds-photos%2Ffotos-anchas-pictures&psig=AOvVaw18mVrfLuAspgyKVvaKJG9A&ust=1745962585637000&source=images&cd=vfe&opi=89978449&ved=0CBQQjRxqFwoTCPDtstPX-4wDFQAAAAAdAAAAABAE" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "difficulties",
                keyColumn: "id",
                keyValue: new Guid("284a9d17-9e07-4796-a0c8-199cb2ebd637"));

            migrationBuilder.DeleteData(
                table: "difficulties",
                keyColumn: "id",
                keyValue: new Guid("55a87c60-33b4-4bcf-96a8-76c644dd7e20"));

            migrationBuilder.DeleteData(
                table: "difficulties",
                keyColumn: "id",
                keyValue: new Guid("752f441b-3268-4729-845a-02df3c0e7e13"));

            migrationBuilder.DeleteData(
                table: "regions",
                keyColumn: "Id",
                keyValue: new Guid("470b646a-063e-4a94-8346-f50135212f6d"));

            migrationBuilder.DeleteData(
                table: "regions",
                keyColumn: "Id",
                keyValue: new Guid("8f4eacfc-eaab-4cde-b87d-2b4544b8a84a"));

            migrationBuilder.DeleteData(
                table: "regions",
                keyColumn: "Id",
                keyValue: new Guid("e5973695-f958-41d3-8b68-8ec2d89cb159"));

            migrationBuilder.DeleteData(
                table: "regions",
                keyColumn: "Id",
                keyValue: new Guid("fc6d6315-4fff-41b7-8a3b-53d0224a477d"));
        }
    }
}

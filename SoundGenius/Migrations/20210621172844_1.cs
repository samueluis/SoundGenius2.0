using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SoundGenius.Migrations
{
    public partial class _1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Artista",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(maxLength: 40, nullable: false),
                    Sexo = table.Column<string>(maxLength: 1, nullable: true),
                    FicheiroImg = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Artista", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    Name = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    UserName = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(maxLength: 256, nullable: true),
                    Email = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(nullable: false),
                    PasswordHash = table.Column<string>(nullable: true),
                    SecurityStamp = table.Column<string>(nullable: true),
                    ConcurrencyStamp = table.Column<string>(nullable: true),
                    PhoneNumber = table.Column<string>(nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(nullable: false),
                    TwoFactorEnabled = table.Column<bool>(nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(nullable: true),
                    LockoutEnabled = table.Column<bool>(nullable: false),
                    AccessFailedCount = table.Column<int>(nullable: false),
                    Nome = table.Column<string>(nullable: true),
                    Timestamp = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Faixas",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Titulo = table.Column<string>(nullable: true),
                    Genero = table.Column<string>(nullable: true),
                    FicheiroImg = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Faixas", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Funcionarios",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(maxLength: 70, nullable: false),
                    Password = table.Column<string>(nullable: true),
                    Email = table.Column<string>(nullable: true),
                    Telefone = table.Column<string>(maxLength: 9, nullable: false),
                    NumFuncionario = table.Column<int>(nullable: false),
                    TipoFuncionario = table.Column<string>(nullable: true),
                    UserId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Funcionarios", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "IdentityUser",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    UserName = table.Column<string>(nullable: true),
                    NormalizedUserName = table.Column<string>(nullable: true),
                    Email = table.Column<string>(nullable: true),
                    NormalizedEmail = table.Column<string>(nullable: true),
                    EmailConfirmed = table.Column<bool>(nullable: false),
                    PasswordHash = table.Column<string>(nullable: true),
                    SecurityStamp = table.Column<string>(nullable: true),
                    ConcurrencyStamp = table.Column<string>(nullable: true),
                    PhoneNumber = table.Column<string>(nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(nullable: false),
                    TwoFactorEnabled = table.Column<bool>(nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(nullable: true),
                    LockoutEnabled = table.Column<bool>(nullable: false),
                    AccessFailedCount = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IdentityUser", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Utilizadores",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(maxLength: 70, nullable: false),
                    Email = table.Column<string>(nullable: true),
                    Telefone = table.Column<string>(maxLength: 9, nullable: false),
                    Morada = table.Column<string>(nullable: false),
                    CodigoPostal = table.Column<string>(nullable: false),
                    UserId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Utilizadores", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Albuns",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Titulo = table.Column<string>(nullable: true),
                    Genero = table.Column<string>(nullable: true),
                    Data = table.Column<DateTime>(nullable: false),
                    FicheiroImg = table.Column<string>(nullable: true),
                    ArtistaFK = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Albuns", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Albuns_Artista_ArtistaFK",
                        column: x => x.ArtistaFK,
                        principalTable: "Artista",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<string>(nullable: false),
                    ClaimType = table.Column<string>(nullable: true),
                    ClaimValue = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(nullable: false),
                    ClaimType = table.Column<string>(nullable: true),
                    ClaimValue = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(maxLength: 128, nullable: false),
                    ProviderKey = table.Column<string>(maxLength: 128, nullable: false),
                    ProviderDisplayName = table.Column<string>(nullable: true),
                    UserId = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<string>(nullable: false),
                    RoleId = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<string>(nullable: false),
                    LoginProvider = table.Column<string>(maxLength: 128, nullable: false),
                    Name = table.Column<string>(maxLength: 128, nullable: false),
                    Value = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AlbumFaixas",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AlbumFK = table.Column<int>(nullable: false),
                    FaixaFK = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AlbumFaixas", x => x.ID);
                    table.ForeignKey(
                        name: "FK_AlbumFaixas_Albuns_AlbumFK",
                        column: x => x.AlbumFK,
                        principalTable: "Albuns",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AlbumFaixas_Faixas_FaixaFK",
                        column: x => x.FaixaFK,
                        principalTable: "Faixas",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Artista",
                columns: new[] { "ID", "FicheiroImg", "Nome", "Sexo" },
                values: new object[,]
                {
                    { 1, "Nirvana.jpg", "Nirvana", "M" },
                    { 2, "Foo Figthers.jpg", "Foo Figthers", "M" },
                    { 3, "Asap Rocky.jpg", "Asap Rocky", "M" },
                    { 4, "Juice wrld.jpg", "Juice wrld", "M" }
                });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "ad", "94f374f0-35dc-4a8c-af86-de113d3d06b1", "administrador", "administrador" },
                    { "c", "322f846e-33d9-4079-87ba-0dcd47409aa0", "utilizadore", "utilizadore" }
                });

            migrationBuilder.InsertData(
                table: "Faixas",
                columns: new[] { "ID", "FicheiroImg", "Genero", "Titulo" },
                values: new object[,]
                {
                    { 10, "goodbye & good riddance.jpg", "Hip hop", "Wasted" },
                    { 9, "Long. Live. ASAP.jpg", "Hip hop", "lVL" },
                    { 8, "Long. Live. ASAP.jpg", "Hip hop", "PMW" },
                    { 7, "Testing.jpg", "Hip hop", "Fukk Sleep" },
                    { 6, "The color and the shape.jpg", "Rock Alternativo", "Pretender" },
                    { 5, "In Your Honor.jpg", "Rock Alternativo", "Best of you" },
                    { 2, "Bleach.jpg", "Grunge", "About a girl" },
                    { 3, "Nevermind.jpg", "Grunge", "come as you are" },
                    { 1, "MTV Unplugged.jpg", "Grunge", "The man who sold the word" },
                    { 4, "The color and the shape.jpg", "Rock Alternativo", "Everlong" }
                });

            migrationBuilder.InsertData(
                table: "Funcionarios",
                columns: new[] { "ID", "Email", "Nome", "NumFuncionario", "Password", "Telefone", "TipoFuncionario", "UserId" },
                values: new object[] { 1, "gerente@ipt.pt", "Gerente Gerente", 666, null, "987456123", "administrador", "f554eee4-e19d-4830-a02c-aabe9f18e8a7" });

            migrationBuilder.InsertData(
                table: "IdentityUser",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[,]
                {
                    { "91b48022-fcca-4aed-8bee-63f2ff93a8c5", 0, "bd1c4aa5-aaed-45ff-a6e9-11e8c6888644", "utilizadore@ipt.pt", true, false, null, "UTILIZADORE@IPT.PT", "UTILIZADORE@IPT.PT", "AQAAAAEAACcQAAAAEOwjUR76Lx3fR0i9QH3Noni0nzQTLzJ9a2CM1v+IdBwB6ADWtKRgX4o4Sl8FyBIoqA==", null, false, "CYQGW2ATI3AOJUO66PHZWTHIPBZRU6NL", false, "utilizadore@ipt.pt" },
                    { "f554eee4-e19d-4830-a02c-aabe9f18e8a7", 0, "bd1c4aa5-aaed-45ff-a6e9-11e8c6888644", "gerente@ipt.pt", true, false, null, "GERENTE@IPT.PT", "GERENTE@IPT.PT", "AQAAAAEAACcQAAAAEOwjUR76Lx3fR0i9QH3Noni0nzQTLzJ9a2CM1v+IdBwB6ADWtKRgX4o4Sl8FyBIoqA==", null, false, "CYQGW2ATI3AOJUO66PHZWTHIPBZRU6NL", false, "gerente@ipt.pt" }
                });

            migrationBuilder.InsertData(
                table: "Utilizadores",
                columns: new[] { "ID", "CodigoPostal", "Email", "Morada", "Nome", "Telefone", "UserId" },
                values: new object[] { 1, "2000-070 Almeirim", "utilizadore@ipt.pt", "Rua São João da Ribeira, nº59", "Utilizadore Utilizadore", "987456123", "91b48022-fcca-4aed-8bee-63f2ff93a8c5" });

            migrationBuilder.InsertData(
                table: "Albuns",
                columns: new[] { "ID", "ArtistaFK", "Data", "FicheiroImg", "Genero", "Titulo" },
                values: new object[,]
                {
                    { 1, 1, new DateTime(2019, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), "MTV Unplugged.jpg", "Grunge", "MTV Unplugged" },
                    { 2, 1, new DateTime(2019, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), "Bleach.jpg", "Grunge", "Bleach" },
                    { 3, 1, new DateTime(2019, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), "Nevermind.jpg", "Grunge", "Nevermind" },
                    { 4, 2, new DateTime(2019, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), "The color and the shape.jpg", "Rock Alternativo ", "The color and the shape" },
                    { 5, 2, new DateTime(2019, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), "In Your Honor.jpg", "Rock Alternativo ", " In Your Honor" },
                    { 6, 2, new DateTime(2019, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), "Foo Fighters Echoes, Silence, Patience & Grace.jpg", "Rock Alternativo ", "Foo Fighters Echoes, Silence, Patience & Grace" },
                    { 7, 3, new DateTime(2019, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), "Testing.jpg", "Hip hop", "Testing" },
                    { 8, 3, new DateTime(2019, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), "Long. Live. ASAP.jpg", "Hip hop", "Long. Live. ASAP" },
                    { 9, 4, new DateTime(2019, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), "goodbye & good riddance.jpg", "Hip hop ", "goodbye & good riddance" }
                });

            migrationBuilder.InsertData(
                table: "AlbumFaixas",
                columns: new[] { "ID", "AlbumFK", "FaixaFK" },
                values: new object[,]
                {
                    { 1, 1, 1 },
                    { 11, 1, 9 },
                    { 2, 2, 2 },
                    { 3, 3, 3 },
                    { 4, 4, 4 },
                    { 5, 5, 5 },
                    { 6, 6, 6 },
                    { 7, 7, 7 },
                    { 10, 7, 9 },
                    { 8, 8, 8 },
                    { 9, 9, 10 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_AlbumFaixas_AlbumFK",
                table: "AlbumFaixas",
                column: "AlbumFK");

            migrationBuilder.CreateIndex(
                name: "IX_AlbumFaixas_FaixaFK",
                table: "AlbumFaixas",
                column: "FaixaFK");

            migrationBuilder.CreateIndex(
                name: "IX_Albuns_ArtistaFK",
                table: "Albuns",
                column: "ArtistaFK");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AlbumFaixas");

            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "Funcionarios");

            migrationBuilder.DropTable(
                name: "IdentityUser");

            migrationBuilder.DropTable(
                name: "Utilizadores");

            migrationBuilder.DropTable(
                name: "Albuns");

            migrationBuilder.DropTable(
                name: "Faixas");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "Artista");
        }
    }
}

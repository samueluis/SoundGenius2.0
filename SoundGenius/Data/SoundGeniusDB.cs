using System;
using System.Collections.Generic;
using System.Text;

using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SoundGenius.Models;

namespace SoundGenius.Data
{

    /// <summary>
    /// Esta classe estende o conjunto de dados de um utilizador, criado a quando da Identity
    /// É necessário, alterar a definição da BD, e redefinir a nossa aplicação para usar este novo utilizador
    /// Em todos os sítios onde se referenciar 'IdentityUser' deverá referenciar-se 'ApplicationUser'
    /// </summary>
    public class ApplicationUser : IdentityUser
    {

        /// <summary>
        /// nome da pessoa q se regista, e posteriormente, autentica
        /// </summary>
        public string Nome { get; set; }


        /// <summary>
        /// registo da hora+data da criação do registo
        /// </summary>
        public DateTime Timestamp { get; set; }
    }


    /// <summary>
    /// criação da BD do projeto.
    /// Neste caso concreto, estamos a usar os dados genéricos + os dados particulares da nossa aplicação
    /// </summary>
    public class SoundGeniusDB : IdentityDbContext<ApplicationUser>
    {


        /// <summary>
        /// Construtor da classe
        /// serve para ligar esta classe à BD
        /// </summary>
        /// <param name="options"></param>
        public SoundGeniusDB(DbContextOptions<SoundGeniusDB> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<IdentityRole>().HasData(
                new IdentityRole { Id = "ad", Name = "administrador", NormalizedName = "administrador" },
                new IdentityRole { Id = "c", Name = "utilizadore", NormalizedName = "utilizadore" }
                );
            modelBuilder.Entity<IdentityUser>().HasData(
                new IdentityUser { Id = "f554eee4-e19d-4830-a02c-aabe9f18e8a7", UserName = "gerente@ipt.pt", NormalizedUserName = "GERENTE@IPT.PT", Email = "gerente@ipt.pt", NormalizedEmail = "GERENTE@IPT.PT", EmailConfirmed = true, PasswordHash = "AQAAAAEAACcQAAAAEOwjUR76Lx3fR0i9QH3Noni0nzQTLzJ9a2CM1v+IdBwB6ADWtKRgX4o4Sl8FyBIoqA==", SecurityStamp = "CYQGW2ATI3AOJUO66PHZWTHIPBZRU6NL", ConcurrencyStamp = "bd1c4aa5-aaed-45ff-a6e9-11e8c6888644" },
                new IdentityUser { Id = "91b48022-fcca-4aed-8bee-63f2ff93a8c5", UserName = "utilizadore@ipt.pt", NormalizedUserName = "UTILIZADORE@IPT.PT", Email = "utilizadore@ipt.pt", NormalizedEmail = "UTILIZADORE@IPT.PT", EmailConfirmed = true, PasswordHash = "AQAAAAEAACcQAAAAEOwjUR76Lx3fR0i9QH3Noni0nzQTLzJ9a2CM1v+IdBwB6ADWtKRgX4o4Sl8FyBIoqA==", SecurityStamp = "CYQGW2ATI3AOJUO66PHZWTHIPBZRU6NL", ConcurrencyStamp = "bd1c4aa5-aaed-45ff-a6e9-11e8c6888644" }
                );
            //modelBuilder.Entity<IdentityUserRole<string>>().HasData(
            //    new IdentityUserRole<string> { UserId = "f554eee4-e19d-4830-a02c-aabe9f18e8a7", RoleId = "ad" },
            //    new IdentityUserRole<string> { UserId = "91b48022-fcca-4aed-8bee-63f2ff93a8c5", RoleId = "c" }
            //    );
            //modelBuilder.Entity<IdentityUserClaim<string>>().HasData(
            //    new IdentityUserClaim<string> { Id = 1, UserId = "f554eee4-e19d-4830-a02c-aabe9f18e8a7", ClaimType = "Nome", ClaimValue = "Gerente Gerente" },
            //    new IdentityUserClaim<string> { Id = 3, UserId = "91b48022-fcca-4aed-8bee-63f2ff93a8c5", ClaimType = "Nome", ClaimValue = "Utilizadore Utilizadore" }
            //    );
            modelBuilder.Entity<Funcionarios>().HasData(
                new Funcionarios { ID = 1, Email = "gerente@ipt.pt", Nome = "Gerente Gerente", NumFuncionario = 666, Password = null, Telefone = "987456123", TipoFuncionario = "administrador", UserId = "f554eee4-e19d-4830-a02c-aabe9f18e8a7" }
                );
            modelBuilder.Entity<Utilizadores>().HasData(
                    new Utilizadores { ID = 1, Email = "utilizadore@ipt.pt", Nome = "Utilizadore Utilizadore", CodigoPostal = "2000-070 Almeirim", Morada = "Rua São João da Ribeira, nº59", Telefone = "987456123", UserId = "91b48022-fcca-4aed-8bee-63f2ff93a8c5" }
                );


            // insert DB seed
            modelBuilder.Entity<Faixas>().HasData(
               new Faixas { ID = 1, Titulo = "The man who sold the word", Genero = "Grunge", FicheiroImg = "MTV Unplugged.jpg" },
               new Faixas { ID = 2, Titulo = "About a girl", Genero = "Grunge", FicheiroImg = "Bleach.jpg" },
               new Faixas { ID = 3, Titulo = "come as you are", Genero = "Grunge", FicheiroImg = "Nevermind.jpg" },
               new Faixas { ID = 4, Titulo = "Everlong", Genero = "Rock Alternativo", FicheiroImg = "The color and the shape.jpg" },
               new Faixas { ID = 5, Titulo = "Best of you", Genero = "Rock Alternativo", FicheiroImg = "In Your Honor.jpg" },
               new Faixas { ID = 6, Titulo = "Pretender", Genero = "Rock Alternativo", FicheiroImg = "The color and the shape.jpg" },
               new Faixas { ID = 7, Titulo = "Fukk Sleep", Genero = "Hip hop", FicheiroImg = "Testing.jpg" },
               new Faixas { ID = 8, Titulo = "PMW", Genero = "Hip hop", FicheiroImg = "Long. Live. ASAP.jpg" },
               new Faixas { ID = 9, Titulo = "lVL", Genero = "Hip hop", FicheiroImg = "Long. Live. ASAP.jpg" },
               new Faixas { ID = 10, Titulo = "Wasted", Genero = "Hip hop", FicheiroImg = "goodbye & good riddance.jpg" }
                );

            // insert DB seed
            modelBuilder.Entity<Artista>().HasData(
               new Artista { ID = 1, Nome = "Nirvana", Sexo = "M", FicheiroImg = "Nirvana.jpg" },
               new Artista { ID = 2, Nome = "Foo Figthers", Sexo = "M", FicheiroImg = "Foo Figthers.jpg" },
               new Artista { ID = 3, Nome = "Asap Rocky", Sexo = "M", FicheiroImg = "Asap Rocky.jpg" },
               new Artista { ID = 4, Nome = "Juice wrld", Sexo = "M", FicheiroImg = "Juice wrld.jpg" }
                );

            // insert DB seed
            modelBuilder.Entity<Albuns>().HasData(
               new Albuns { ID = 1, Titulo = "MTV Unplugged", Genero = "Grunge", FicheiroImg = "MTV Unplugged.jpg", Data = new DateTime(2019, 5, 20), ArtistaFK = 1 },
               new Albuns { ID = 2, Titulo = "Bleach", Genero = "Grunge", FicheiroImg = "Bleach.jpg", Data = new DateTime(2019, 5, 20), ArtistaFK = 1 },
               new Albuns { ID = 3, Titulo = "Nevermind", Genero = "Grunge", FicheiroImg = "Nevermind.jpg", Data = new DateTime(2019, 5, 20), ArtistaFK = 1 },
               new Albuns { ID = 4, Titulo = "The color and the shape", Genero = "Rock Alternativo ", FicheiroImg = "The color and the shape.jpg", Data = new DateTime(2019, 5, 20), ArtistaFK = 2 },
               new Albuns { ID = 5, Titulo = " In Your Honor", Genero = "Rock Alternativo ", FicheiroImg = "In Your Honor.jpg", Data = new DateTime(2019, 5, 20), ArtistaFK = 2 },
               new Albuns { ID = 6, Titulo = "Foo Fighters Echoes, Silence, Patience & Grace", Genero = "Rock Alternativo ", FicheiroImg = "Foo Fighters Echoes, Silence, Patience & Grace.jpg", Data = new DateTime(2019, 5, 20), ArtistaFK = 2 },
               new Albuns { ID = 7, Titulo = "Testing", Genero = "Hip hop", FicheiroImg = "Testing.jpg", Data = new DateTime(2019, 5, 20), ArtistaFK = 3 },
               new Albuns { ID = 8, Titulo = "Long. Live. ASAP", Genero = "Hip hop", FicheiroImg = "Long. Live. ASAP.jpg", Data = new DateTime(2019, 5, 20), ArtistaFK = 3 },
               new Albuns { ID = 9, Titulo = "goodbye & good riddance", Genero = "Hip hop ", FicheiroImg = "goodbye & good riddance.jpg", Data = new DateTime(2019, 5, 20), ArtistaFK = 4 }
                );


            // insert DB seed
            modelBuilder.Entity<AlbumFaixas>().HasData(
               new AlbumFaixas { ID = 1, AlbumFK = 1, FaixaFK = 1 },
               new AlbumFaixas { ID = 2, AlbumFK = 2, FaixaFK = 2 },
               new AlbumFaixas { ID = 3, AlbumFK = 3, FaixaFK = 3 },
               new AlbumFaixas { ID = 4, AlbumFK = 4, FaixaFK = 4 },
               new AlbumFaixas { ID = 5, AlbumFK = 5, FaixaFK = 5 },
               new AlbumFaixas { ID = 6, AlbumFK = 6, FaixaFK = 6 },
               new AlbumFaixas { ID = 7, AlbumFK = 7, FaixaFK = 7 },
               new AlbumFaixas { ID = 8, AlbumFK = 8, FaixaFK = 8 },
               new AlbumFaixas { ID = 9, AlbumFK = 9, FaixaFK = 10 },
               new AlbumFaixas { ID = 10, AlbumFK = 7, FaixaFK = 9 },
               new AlbumFaixas { ID = 11, AlbumFK = 1, FaixaFK = 9 }
                );
        }






        // adicionar as 'tabelas' à BD
        public virtual DbSet<AlbumFaixas> AlbumFaixas { get; set; }
        public virtual DbSet<Albuns> Albuns { get; set; }
        public virtual DbSet<Faixas> Faixas { get; set; }
        public virtual DbSet<Artista> Artista { get; set; }

        public virtual DbSet<Utilizadores> Utilizadores { get; set; }

        public virtual DbSet<Funcionarios> Funcionarios { get; set; }


    }
}

using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SoundGenius.Models
{
    public class Albuns
    {
        public Albuns()
        {
            AlbumFaixas = new HashSet<AlbumFaixas>();
        }

        [Key]
        public int ID { get; set; }

        public string Titulo { get; set; }

        public string Genero { get; set; }

        public string FicheiroImg { get; set; }

        // FK para a 'tabela' dos Faixas
        [ForeignKey("Artista")]
        public int ArtistaFK { get; set; }
        public virtual Artista Artista { get; set; }

        //------------------------------------------------------------------------------
        public virtual ICollection<AlbumFaixas> AlbumFaixas { get; set; }
    }
}

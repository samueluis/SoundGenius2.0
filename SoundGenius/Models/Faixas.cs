using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace SoundGenius.Models
{
    public class Faixas
    {
        public Faixas()
        {
            AlbumFaixas = new HashSet<AlbumFaixas>();
        }

        [Key]
        public int ID { get; set; }

        public string Titulo { get; set; }

        public string Genero { get; set; }

        public string FicheiroImg { get; set; }

        //---------------------------------------------------------------------------
        public virtual ICollection<AlbumFaixas> AlbumFaixas { get; set; }
        //adicinoar comentarios 

        /// <summary>
        /// este atributo irá receber o ID do utilizador que se autentica
        /// </summary>
        public string UserName { get; set; }
    }
}

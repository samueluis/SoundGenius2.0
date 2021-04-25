using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace SoundGenius.Models
{
    public class AlbumFaixas
    {
        [Key]
        public int ID { get; set; }


        [ForeignKey(nameof(Album))]
        public int AlbumFK { get; set; }
        public virtual Albuns Album { get; set; }

        [ForeignKey(nameof(Faixa))]
        public int FaixaFK { get; set; }
        public virtual Faixas Faixa { get; set; }


    }
}

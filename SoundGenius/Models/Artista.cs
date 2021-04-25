using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace SoundGenius.Models
{
    /// <summary>
    /// Representa os dados de um 'Artista'
    /// </summary>
    public class Artista
    {

        public Artista()
        {
            Albuns = new HashSet<Albuns>();
        }

        /// <summary>
        /// Identificador do Artista. Será PK na tabela da BD
        /// </summary>
        [Key]
        public int ID { get; set; }

        /// <summary>
        /// Nome do Artista
        /// </summary>
        [Required(ErrorMessage = "O Nome é de preenchimento obrigatório")]
        [StringLength(40, ErrorMessage = "O {0} não pode ter mais de {1} carateres.")]
        [RegularExpression("[A-ZÓÂÍ][a-zçáéíóúàèìòùãõäëïöüâêîôûñ]+(( | d[ao](s)? | e |-|'| d')[A-ZÓÂÍ][a-zçáéíóúàèìòùãõäëïöüâêîôûñ]+){1,3}",
                            ErrorMessage = "Deve escrever entre 2 e 4 nomes, começados por uma Maiúscula, seguidos de minúsculas.")]
        public string Nome { get; set; }

        /// <summary>
        /// Identifica o sexo do 'Artista'
        /// </summary>
        [RegularExpression("[FMfm]", ErrorMessage = "Deve escrever F ou M, no campo {0}")]
        [StringLength(1, MinimumLength = 1)]
        public string Sexo { get; set; }

        public string FicheiroImg { get; set; }


        /// <summary>
        /// lista de Animais de um determinado Artista
        /// </summary>
        public virtual ICollection<Albuns> Albuns { get; set; }
    }
}

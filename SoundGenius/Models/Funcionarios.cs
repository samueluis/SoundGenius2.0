using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using static SoundGenius.Areas.Identity.Pages.Account.RegisterModel;

namespace SoundGenius.Models
{
    public class Funcionarios
    {
        /// <summary>
        /// Representa os dados de um 'Funcionario'
        /// </summary>
        public Funcionarios()
        {
            
        }

        [Key]
        public int ID { get; set; }

        /// <summary>
        /// Nome do Funcionario
        /// </summary>
        [Required(ErrorMessage = "O {0} é de preenchimento obrigatório")]
        [StringLength(70, ErrorMessage = "Não pode ter maid do que {1} caráteres.")]
        [RegularExpression("[A-ZÓÂÍ][a-zçáéíóúàèìòùãõäëïöüâêîôûñ]+(( | d[ao](s)? | e |-|'| d')[A-ZÓÂÍ][a-zçáéíóúàèìòùãõäëïöüâêîôûñ]+){1,5}",
                         ErrorMessage = "Deve escrever 2 a 6 nomes, começando por Maiúsculas, seguido de  minúsculas.")]
        [Display(Name = "Nome")]
        public string Nome { get; set; }

        [DataType(DataType.Password)]
        public string Password { get; set; }

        /// <summary>
        /// Email Principal do Utilizador
        /// </summary>
        [Display(Name = "Email")]
        public string Email { get; set; }

        /// <summary>
        /// Contacto Telefonico de contacto com o Utilizador
        /// </summary>
        [Required(ErrorMessage = "O {0} é de preenchimento obrigatório")]
        [StringLength(9, MinimumLength = 9, ErrorMessage = "Deve escrever exatamente {1} algarismos no {0}.")]
        [RegularExpression("[239][0-9]{8}", ErrorMessage = "Deve escrever um nº, com 9 algarismos, começando por 2, 3 ou 9.")]
        [Display(Name = "Telefone")]
        public string Telefone { get; set; }

        /// <summary>
        /// Número do Funcionário
        /// </summary>
        [Display(Name = "Número de Funcionário")]
        public int NumFuncionario { get; set; }

        


        /// <summary>
        /// Referência ao Utilizador que se autentica
        /// </summary>
        public string UserId { get; set; }
    }
}

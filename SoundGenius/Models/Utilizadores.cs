using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace SoundGenius.Models
{
    public class Utilizadores
    {
        /// <summary>
        /// Representa os dados de um 'Utilizadore'
        /// </summary>
        public Utilizadores()
        {
            
        }

        [Key]
        public int ID { get; set; }

        /// <summary>
        /// Nome do Cliente
        /// </summary>
        [Required(ErrorMessage = "O {0} é de preenchimento obrigatório")]
        [StringLength(70, ErrorMessage = "Não pode ter maid do que {1} caráteres.")]
        [RegularExpression("[A-ZÓÂÍ][a-zçáéíóúàèìòùãõäëïöüâêîôûñ]+(( | d[ao](s)? | e |-|'| d')[A-ZÓÂÍ][a-zçáéíóúàèìòùãõäëïöüâêîôûñ]+){1,5}",
                          ErrorMessage = "Deve escrever 2 a 6 nomes, começando por Maiúsculas, seguido de  minúsculas.")]
        [Display(Name = "Nome")]
        public string Nome { get; set; }

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
        /// Morada do Utilizador para onde as encomendas serão enviadas
        /// </summary>
        [Required(ErrorMessage = "A {0} é de preenchimento obrigatório")]
        [Display(Name = "Morada")]
        public string Morada { get; set; }

        /// <summary>
        /// Código Postal do Utilizador
        /// </summary>
        [Required(ErrorMessage = "O {0} é de preenchimento obrigatório")]
        [RegularExpression("([0-9]{4}(-)[0-9]{3})(( | d[aeo](s)? | e |-|'| d')[A-ZÓÂÍ][a-zçáéíóúàèìòùãõäëïöüâêîôûñ]+){1,7}.*",
            ErrorMessage = "Deve escrever o código postal seguido da localidade ")]
        [Display(Name = "Código Postal")]
        public string CodigoPostal { get; set; }

        /// <summary>
        /// Referência ao Utilizador que se autentica
        /// </summary>
        public string UserId { get; set; }

        /// <summary>
        /// lista de Encomendas de um determinado cliente
        /// </summary>
       // public virtual ICollection<Encomendas> Orders { get; set; }
        /// <summary>
        /// lista de Itens que estao no ShoppingCart que pertence a um cliente
        /// </summary>
        //public virtual ICollection<ShoppingCart> Cart { get; set; }

    }
}
//using System;
//using System.Collections.Generic;
//using System.ComponentModel.DataAnnotations;
//using System.Linq;
//using System.Web;

//namespace WebApp.DTO
//{
//    public class AlunoDTO
//    {
       
//        public int id { get; set; }
//        [Required(ErrorMessage ="Campo nome é obrigatório")]
//        [StringLength(50, ErrorMessage ="Nome Min 2 Caracteres e Max 50", MinimumLength =2)]
//        public string nome { get; set; }
//        public string sobrenome { get; set; }
//        public string telefone { get; set; }

//        public string data { get; set; }
//        [Required(ErrorMessage = "Campo RA é obrigatório")]
//        public int? RA { get; set; }
//    }
//}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace iSec.Models
{
    [ModelMetadataType(typeof(TextTableMetaData))]
    public partial class TextTable
    { 
    
    }

    public class TextTableMetaData
    {
        public int Id { get; set; }
        [Required]
        [Display(Name = "Text Inputted")]
        public string EnteredText { get; set; }
        [Display(Name = "Encrypted Text")]
        public string EncryptedText { get; set; }
        [Display(Name = "Decrypted Text")]
        public string DecryptedText { get; set; }
    }
}

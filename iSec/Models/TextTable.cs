using System;
using System.Collections.Generic;

#nullable disable

namespace iSec.Models
{
    public partial class TextTable
    {
        public int Id { get; set; }
        public string EnteredText { get; set; }
        public string EncryptedText { get; set; }
        public string DecryptedText { get; set; }
    }
}

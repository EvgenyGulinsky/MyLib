using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyLib.Models.Context
{
    public class BookDisplay
    {
        public Book Book { get; set; }
        public bool InHave { get; set; }
        public bool Readed { get; set; }
        public LentBooks LentBook { get; set; }        
    }
}
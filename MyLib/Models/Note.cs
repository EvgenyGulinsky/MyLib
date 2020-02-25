using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace MyLib.Models
{
    public class Note
    {
        [Key]
        [ForeignKey("UserBook")]
        public int Id { get; set; }
        public UserBook UserBook { get; set; }

        public string UserNote { get; set; }
    }
}
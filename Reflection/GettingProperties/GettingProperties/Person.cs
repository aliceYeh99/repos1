using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GettingProperties
{
    [Table("PMS300.Person")]
    public class Person
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }

    }
}

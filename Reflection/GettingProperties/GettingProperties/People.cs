using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GettingProperties
{
    [Table("People")]
    public class People
    {
        private string _name;
        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }

        private int _age;

        [Key]
        public int Age
        {
            get { return _age; }
            set { _age = value; }
        }

        public People()
        {

        }

        public People(string name, int age)
        {
            this._name = name;
            this._age = age;
        }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFTestOneToMany.Models
{

    public  class OwnerDesc
    {
        [Key, ForeignKey("Owner")]
        public string OwnerId { get; set; }
        public Owner Owner { get; set; }
        public string DESC { get; set; }
    }
        //System.InvalidOperationException: 'The entity types 'Owner' and 'OwnerDesc'
        //cannot share table 'Owner' because they are not in the same type hierarchy
        //or do not have a valid one to one
        //foreign key relationship with matching primary keys between them.'    }
}

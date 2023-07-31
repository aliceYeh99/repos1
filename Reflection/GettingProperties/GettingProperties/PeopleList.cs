using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GettingProperties
{
    public class PeopleList : List<People>
    {
        public static void DoStuff()
        {
            PeopleList newList = new PeopleList();

            // Do some stuff

            newList.Add(new People("Tim", 35));
        }
    }
}

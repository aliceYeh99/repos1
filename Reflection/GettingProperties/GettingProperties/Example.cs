using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace GettingProperties
{
    internal class Example
    {
        void ReportValue(String propName, Object propValue)
        {
            Console.WriteLine($"{propName}={propValue}");
        }
        public void ReportTable()
        {
            foreach(Assembly assembly in AppDomain.CurrentDomain.GetAssemblies())
            {
                foreach (Type type in assembly.GetTypes().Where(type => type.GetCustomAttribute<TableAttribute>() != null))
                {


                    TableAttribute tableAttribute = type.GetCustomAttribute<TableAttribute>();
                    string tableName = tableAttribute.Name; //whatever the field the of the tablename is

                    //Console.WriteLine($"Table: {tableName} ");
                    TableInfo tbl = new TableInfo(tableName);

                    //foreach (var property in type.GetProperties())
                    //{
                    //    var columnAttribute = property.GetCustomAttribute<ColumnAttribute>();
                    //    var columnName = columnAttribute.Name; //whatever the field the of the columnname is
                    //    //Console.WriteLine($"Coumn: {columnName} ");
                    //    tbl.Keys.Add(property.Name);
                    //}

                    //不一定有 Column Attr ox
                    foreach (var property in type.GetProperties().Where(property => property.GetCustomAttribute<KeyAttribute>() != null))
                    {
                        var keyAttribute = property.GetCustomAttribute<KeyAttribute>();
                        var columnName = property.Name; //whatever the field the of the columnname is
                        //Console.WriteLine($"Coumn: {columnName} ");
                        tbl.Keys.Add(property.Name);
                    }
                    foreach (var prop in type.GetProperties())
                    {
                        //Console.WriteLine($"prop.Name: {prop.Name} ");
                        tbl.Columns.Add(prop.Name);
                    }
                    Console.WriteLine($" QueryString: {tbl.GetQueryString()} ");
                    Console.WriteLine($" QueryString: {tbl.GetFindString("1")} ");
                }
            }
        }
        public void ReadList<T>(List<T> list)
        {
            
            var props = typeof(T).GetProperties();
            var attrs = typeof(T).GetCustomAttributes();
            foreach (T item in list)
            {
                foreach (var prop in props)
                {
                    ReportValue(prop.Name, prop.GetValue(item));
                }

            }

        }

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GettingProperties
{
    public class TableInfo
    {
        public TableInfo(string tableName) { TableName = tableName; }

        public string TableName { get; set; }

        public List<String> Keys { get; set; } = new List<String>();

        public List<String> Columns { get; set; } = new List<String>();

        public String GetQueryString() { return $"SELECT {join(',', Columns)} FROM {TableName}"; }
        public String GetFindString(params object[] keyValues) { return $"SELECT {join(',', Columns)} FROM {TableName} WHERE {Keys[0]}=\"{keyValues[0]}\""; }

        private object join(char c, List<string> columns)
        {
            return String.Join(c, columns.ToArray()); //public virtual TEntity Find(params object[] keyValues)
        }
    }
}

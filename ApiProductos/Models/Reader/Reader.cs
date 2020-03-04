using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace ApiProductos.Models.Reader
{
    public static class Reader
    {
        //public static IEnumerable<IDataRecord> GetReader(IDataReader reader)
        //{
        //    while (reader.Read()) yield return reader;
        //}

        public static IEnumerable<IDataRecord> Enumerate(this IDataReader reader)
        {
            while (reader.Read())
            {
                yield return reader;
            }
        }
    }
}
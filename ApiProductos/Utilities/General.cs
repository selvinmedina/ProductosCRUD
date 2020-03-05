using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace ApiProductos.Utilities
{
    public class General
    {
        public static SqlConnection GetConnection()
        {
            return new SqlConnection(ConfigurationManager.ConnectionStrings["Productos"].ConnectionString);
        }

        public static string CrearNombreImagen(int name)
        {
            string _allowedChars = "abcdefghijkmnpqrstuvwxyzABCDEFGHJKLMNPQRSTUVWXYZ";
            Byte[] randomBytes = new Byte[name];
            char[] chars = new char[name];
            int allowedCharCount = _allowedChars.Length;

            for (int i = 0; i < name; i++)
            {
                Random randomObj = new Random();
                randomObj.NextBytes(randomBytes);
                chars[i] = _allowedChars[(int)randomBytes[i] % allowedCharCount];
            }

            return new string(chars);
        }
    }
}
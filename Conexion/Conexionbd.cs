using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace TiendaAPI.Conexion
{
    public class Conexionbd
    {
        private string connectionString = string.Empty;
        public Conexionbd()
        {
            var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json").Build();
            connectionString = builder.GetSection("ConnectionStrings:conexionMaestra").Value;
        }
        public string cadenaSQL()
        {
            return connectionString;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Parte_3.Models.Data
{
    public class Singleton
    {
        private readonly static Singleton _instance = new Singleton();
        public Parte_1.B<Movie> Movies { get; set; }

        public static Singleton Instance
        {
            get
            {
                return _instance;
            }
        }
    }
}

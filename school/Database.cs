using school.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SCHOOL_BUS
{
    public class Database
    {
        public static SbDbContext baza=null;


        public Database()
        { 
            
        }

        public static SbDbContext GetBaza()
        {
            
            if (baza == null)
            {
                baza = new SbDbContext();
             }
            return baza;
        }
    }
}

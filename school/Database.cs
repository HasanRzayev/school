using Sb.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SCHOOL_BUS
{
    public class Database
    {
        public static AppDB baza =null;


        public Database()
        { 
            
        }

        public static AppDB GetBaza()
        {
            
            if (baza == null)
            {
                baza = new AppDB();
             }
            return baza;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KitchenNotesDAL;
using System.Data.Linq;

namespace TestConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            newFam();
        }

        public static void newFam()
        {
            Family oFam = new Family();
            using (var dc = new DALDataContext())
            {
                oFam = dc.Families.FirstOrDefault(x => x.FamilyName == "Andy");

                Console.Write(string.Format("Family ID : {0}, Family Name {1}", oFam.FamilyId.ToString(), oFam.FamilyName));
                Console.ReadLine();
            }

        }
    }
}

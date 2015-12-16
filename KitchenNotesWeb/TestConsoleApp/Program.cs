using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KitchenNotesDAL;
using KitchenNotesBLL;
using System.Data.Linq;
using Helpers;

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
            string newHubName = Console.ReadLine();
            KitchenNotesHub.addHub(newHubName);
            Hub newHub = KitchenNotesHub.getHub(newHubName);
            //KitchenNotesUser.addNewUserToExistingHub("AndyMehaffy", newHub.HubName, SHA1.Encode("graham"), "Emma", newHub.HubId);

            
            Console.WriteLine("ID: " + newHub.HubId + " Name: " + newHub.HubName);
            Console.ReadKey();
        }
    }
}

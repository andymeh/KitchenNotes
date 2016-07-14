using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace KitchenNotesUnivers
{
    public sealed partial class LoginPage : Page
    {
        private static Frame frame = (Frame)Window.Current.Content;
        private static string returnedJson = "";
        public LoginPage()
        {
            this.InitializeComponent();
        }

        private async void button_Click(object sender, RoutedEventArgs e)
        {
            string hubRef = txtHubRefId.Text;
            string username = txtUsername.Text;
            string password = txtPassword.Password;
            
            using (HttpClient client = new HttpClient())
            {
                string reqAdd = String.Format("http://kitchennotesweb.azurewebsites.net/Home/AppLogin?hubRef={0}&username={1}&password={2}", hubRef, username, password);

                HttpResponseMessage response = await client.GetAsync(reqAdd);
                response.EnsureSuccessStatusCode();
                string responseBody = await response.Content.ReadAsStringAsync();
                returnedJson = responseBody;
            }
            returnedJson = returnedJson.Trim(new Char[] { ' ', '\\', '"' });
            var splitJson = returnedJson.Split(',');
            if(splitJson.Count() == 3)
            {
                var valid = splitJson.ElementAt(0);
                var HubId = splitJson.ElementAt(1);
                var HubName = splitJson.ElementAt(2);
                if (valid.Equals("true"))
                {
                    var hubInfo = new LoginInfo();
                    hubInfo.HubId = HubId;
                    hubInfo.username = username;
                    hubInfo.HubName = HubName;
                    navToHomePage(hubInfo);
                }
            }
            else
            {
                var dialog = new MessageDialog("Could not log in");
                dialog.Title = "Error";
                dialog.Commands.Add(new UICommand { Label = "Ok", Id = 0 });
                var res = await dialog.ShowAsync();

                //if ((int)res.Id == 0)
                //{ *** }
            }
            
        }

        private void navToHomePage(LoginInfo loginInfo)
        {
            frame.Navigate(typeof(HomePage), loginInfo);
        }
    }
}

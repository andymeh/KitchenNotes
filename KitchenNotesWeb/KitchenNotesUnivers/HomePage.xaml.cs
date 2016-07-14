using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Data.Json;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.System.Threading;
using Windows.UI.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace KitchenNotesUnivers
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class HomePage : Page
    {
        public string StringHubId { get; set; }
        public Guid HubId { get; set; }
        public string HubName { get; set; }

        ThreadPoolTimer _clockTimer = null;
        public HomePage()
        {
            this.InitializeComponent();
            txtHubName.Text = HubName + "  " + DateTime.Now.ToString("d MMM HH:mm");
            _clockTimer = ThreadPoolTimer.CreatePeriodicTimer(_clockTimer_Tick, TimeSpan.FromMilliseconds(1000));
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            var parameters = (LoginInfo)e.Parameter;
            StringHubId = parameters.HubId;
            HubName = parameters.HubName;
            fillNotes();
            fillEvents();
            fillTasks();
        }

        private void Sync_Tapped(object sender, TappedRoutedEventArgs e)
        {
            fillNotes();
            fillEvents();
            fillTasks();
        }
        private async void _clockTimer_Tick(ThreadPoolTimer timer)
        {
            await Windows.ApplicationModel.Core.CoreApplication.MainView.CoreWindow.Dispatcher.RunAsync(CoreDispatcherPriority.Normal,
            () =>
            {
                txtHubName.Text = HubName + "  " + DateTime.Now.ToString("d MMM HH:mm");
            }
            );
            
        }

        public async void fillEvents()
        {
            List<Events> lstEvents = new List<Events>();
            using (HttpClient client = new HttpClient())
            {
                string reqAdd = String.Format("http://kitchennotesweb.azurewebsites.net/Events/appGetEvents?requestHubId={0}", StringHubId);

                HttpResponseMessage response = await client.GetAsync(reqAdd);
                response.EnsureSuccessStatusCode();
                var jsonString = await response.Content.ReadAsStringAsync();
                JsonArray root = JsonValue.Parse(jsonString).GetArray();
                for (uint i = 0; i < root.Count; i++)
                {
                    Guid Id = new Guid(root.GetObjectAt(i).GetNamedString("id"));
                    string Name = root.GetObjectAt(i).GetNamedString("name");
                    string Description = root.GetObjectAt(i).GetNamedString("description");
                    string Start = root.GetObjectAt(i).GetNamedString("start");
                    string End = root.GetObjectAt(i).GetNamedString("end");
                    var newEvent = new Events
                    {
                        id = Id,
                        name = Name,
                        description = Description,
                        shortStart = Start,
                        shortEnd = End
                    };
                    lstEvents.Add(newEvent);
                }
            }
            listViewEvents.ItemsSource = lstEvents;
        }

        public async void fillNotes()
        {
            List<Notes> lstNotes = new List<Notes>();
            using (HttpClient client = new HttpClient())
            {
                string reqAdd = String.Format("http://kitchennotesweb.azurewebsites.net/Notes/appGetHubNotes?requestHubId={0}", StringHubId);

                HttpResponseMessage response = await client.GetAsync(reqAdd);
                response.EnsureSuccessStatusCode();
                var jsonString = await response.Content.ReadAsStringAsync();
                JsonArray root = JsonValue.Parse(jsonString).GetArray();
                for (uint i = 0; i < root.Count; i++)
                {
                    Guid NoteId = new Guid(root.GetObjectAt(i).GetNamedString("noteId"));
                    string Username = root.GetObjectAt(i).GetNamedString("username");
                    string Forename = root.GetObjectAt(i).GetNamedString("forename");
                    string TimeAgo = root.GetObjectAt(i).GetNamedString("timeAgo");
                    string NoteContent = root.GetObjectAt(i).GetNamedString("noteContent");
                    var note = new Notes
                    {
                        noteId = NoteId,
                        username = Username,
                        forename = Forename,
                        timeAgo = TimeAgo,
                        noteContent = NoteContent
                    };
                    lstNotes.Add(note);
                }
            }
            listViewNotes.ItemsSource = lstNotes;
        }

        public async void fillTasks()
        {
            List<Tasks> lstTasks = new List<Tasks>();
            using (HttpClient client = new HttpClient())
            {
                string reqAdd = String.Format("http://kitchennotesweb.azurewebsites.net/Tasks/appGetHubTasks?requestHubId={0}", StringHubId);

                HttpResponseMessage response = await client.GetAsync(reqAdd);
                response.EnsureSuccessStatusCode();
                var jsonString = await response.Content.ReadAsStringAsync();
                JsonArray root = JsonValue.Parse(jsonString).GetArray();
                for (uint i = 0; i < root.Count; i++)
                {
                    Guid TaskId = new Guid(root.GetObjectAt(i).GetNamedString("taskId"));
                    string Username = root.GetObjectAt(i).GetNamedString("username");
                    string Forename = root.GetObjectAt(i).GetNamedString("forename");
                    string TaskDetail = root.GetObjectAt(i).GetNamedString("taskDetail");
                    string TimeAgo = root.GetObjectAt(i).GetNamedString("timeAgo");
                    string AssignedTo = root.GetObjectAt(i).GetNamedString("assignedTo");
                    var task = new Tasks
                    {
                        taskId = TaskId,
                        username = Username,
                        forename = Forename,
                        taskDetail = TaskDetail,
                        timeAgo = TimeAgo,
                        assignedTo = AssignedTo
                    };
                    lstTasks.Add(task);
                }
            }
            listViewTasks.ItemsSource = lstTasks;
        }

        private void btnLogout_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(LoginPage));
        }
    }
}

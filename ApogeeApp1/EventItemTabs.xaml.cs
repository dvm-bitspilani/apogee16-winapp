using ApogeeApp1.Common;
using ApogeeApp1.Data;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Runtime.Serialization.Json;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Graphics.Display;
using Windows.Storage;
using Windows.UI.Popups;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Basic Page item template is documented at http://go.microsoft.com/fwlink/?LinkID=390556

namespace ApogeeApp1
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class EventItemTabs : Page
    {
        private NavigationHelper navigationHelper;
        private ObservableDictionary defaultViewModel = new ObservableDictionary();
        private SampleDataSubItem subitem;
        public EventItemTabs()
        {
            this.InitializeComponent();

            this.navigationHelper = new NavigationHelper(this);
            this.navigationHelper.LoadState += this.NavigationHelper_LoadState;
            this.navigationHelper.SaveState += this.NavigationHelper_SaveState;
        }

        /// <summary>
        /// Gets the <see cref="NavigationHelper"/> associated with this <see cref="Page"/>.
        /// </summary>
        public NavigationHelper NavigationHelper
        {
            get { return this.navigationHelper; }
        }

        /// <summary>
        /// Gets the view model for this <see cref="Page"/>.
        /// This can be changed to a strongly typed view model.
        /// </summary>
        public ObservableDictionary DefaultViewModel
        {
            get { return this.defaultViewModel; }
        }

        /// <summary>
        /// Populates the page with content passed during navigation.  Any saved state is also
        /// provided when recreating a page from a prior session.
        /// </summary>
        /// <param name="sender">
        /// The source of the event; typically <see cref="NavigationHelper"/>
        /// </param>
        /// <param name="e">Event data that provides both the navigation parameter passed to
        /// <see cref="Frame.Navigate(Type, Object)"/> when this page was initially requested and
        /// a dictionary of state preserved by this page during an earlier
        /// session.  The state will be null the first time a page is visited.</param>
        private async void NavigationHelper_LoadState(object sender, LoadStateEventArgs e)
        {
           subitem = await SampleDataSource.GetSubItemAsync((string)e.NavigationParameter);
            this.DefaultViewModel["SubItem"] = subitem;
            if (subitem.Overview == "")
            {
                Overview.Visibility = Visibility.Collapsed;
            }
            if (subitem.Rules == "")
            {
                Rules.Visibility = Visibility.Collapsed;
            }
            if (subitem.Eligibility == "")
            {
                Eligibility.Visibility = Visibility.Collapsed;
            }
            if (subitem.Guidelines == "")
            {
                Guidelines.Visibility = Visibility.Collapsed;
            }
            if (subitem.JudgingCriteria == "")
            {
                JudgingCriteria.Visibility = Visibility.Collapsed;
            }
            if (subitem.ProblemStatements == "")
            {
                ProblemStatements.Visibility = Visibility.Collapsed;
            }
            if (subitem.Resources == "")
            {
                Resource.Visibility = Visibility.Collapsed;
            }
            if (subitem.SampleQuestions == "")
            {
                SampleQ.Visibility = Visibility.Collapsed;
            }
            if (subitem.Specifications == "")
            {
                Specifications.Visibility = Visibility.Collapsed;
            }
            if (subitem.Materials == "")
            {
                Materials.Visibility = Visibility.Collapsed;
            }
            if (subitem.RegistrationDetails == "")
            {
                Registration.Visibility = Visibility.Collapsed;
            }
            if (subitem.FAQs == "")
            {
                FAQs.Visibility = Visibility.Collapsed;
            }
            if (subitem.Sponsors == "")
            {
                Sponsors.Visibility = Visibility.Collapsed;
            }
            if (subitem.Contacts == "")
            {
                Contacts.Visibility = Visibility.Collapsed;
            }
            await IsFavourite(subitem.UniqueId);
        }

        /// <summary>
        /// Preserves state associated with this page in case the application is suspended or the
        /// page is discarded from the navigation cache.  Values must conform to the serialization
        /// requirements of <see cref="SuspensionManager.SessionState"/>.
        /// </summary>
        /// <param name="sender">The source of the event; typically <see cref="NavigationHelper"/></param>
        /// <param name="e">Event data that provides an empty dictionary to be populated with
        /// serializable state.</param>
        private void NavigationHelper_SaveState(object sender, SaveStateEventArgs e)
        {
        }

        #region NavigationHelper registration

        /// <summary>
        /// The methods provided in this section are simply used to allow
        /// NavigationHelper to respond to the page's navigation methods.
        /// <para>
        /// Page specific logic should be placed in event handlers for the  
        /// <see cref="NavigationHelper.LoadState"/>
        /// and <see cref="NavigationHelper.SaveState"/>.
        /// The navigation parameter is available in the LoadState method 
        /// in addition to page state preserved during an earlier session.
        /// </para>
        /// </summary>
        /// <param name="e">Provides data for navigation methods and event
        /// handlers that cannot cancel the navigation request.</param>
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            this.navigationHelper.OnNavigatedTo(e);
        }

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            this.navigationHelper.OnNavigatedFrom(e);
        }

        #endregion

        private void Content_Tapped(object sender, TappedRoutedEventArgs e)
        {
            Button btn = sender as Button;
            string mappinid = (string)btn.Tag;
            List<string> item = new List<string>();
            item.Add(subitem.UniqueId);
            item.Add(mappinid);
            //if(mappinid=="Overview")
            Frame.Navigate(typeof(SubItemPage), item);

        }
        private async void MarkFav_Click(object sender, RoutedEventArgs e)
         {
             // AppBarButton btn = sender as AppBarButton;
             // string uid = (string)btn.Tag;
             // Frame.Navigate(typeof(Favourite),uid);
             await AddEntryIntoJsonAsync();
             MarkFav.Visibility = Visibility.Collapsed;
             MarkUnfav.Visibility = Visibility.Visible;
        
         }
         private async void MarkUnfav_Click(object sender, RoutedEventArgs e)
         {
             await DeleteEntryIntoJsonAsync();
             MarkFav.Visibility = Visibility.Visible;
             MarkUnfav.Visibility = Visibility.Collapsed;
         }
        private async Task AddEntryIntoJsonAsync()
        {
            string content = String.Empty;
            List<string> ListCars = new List<string>();
            StorageFolder local = ApplicationData.Current.LocalFolder;

            // Create a new file named DataFile.txt.
            var file = await local.CreateFileAsync("DataFile.json", CreationCollisionOption.OpenIfExists);
            // }
            if (local != null)
            {
                // var dataFolder = await local.GetFolderAsync("DataFolder");
                var myStream = await local.OpenStreamForReadAsync("DataFile.json");
                using (StreamReader reader = new StreamReader(myStream))
                {
                    content = await reader.ReadToEndAsync();
                }
                if (content != "")
                {
                    //Now add one more Entry.
                    ListCars = FavClass.ConvertToFavEvent(content);
                }
                ListCars.Add(subitem.UniqueId);
                /* CultureInfo provider = new CultureInfo("es-ES");
                 //if (item != null)
                  
                 var subitemDet = await SampleDataSource.GetGroupAsync("Schedule");
                 schedulednotif n = new schedulednotif();
                 int noOfItems = subitemDet.Items.Count;
                 string date = "25/10/2015";
                 for (int i = 0; i < noOfItems; i++)
                 {
                     int noOfsubitems = subitemDet.Items[i].SubItems.Count;
                     MessageDialog msgbox4 = new MessageDialog(noOfsubitems.ToString());
                     await msgbox4.ShowAsync();
                     if (i == 0)
                     {
                         date = "28/10/2015";
                     }
                     else if (i == 1)
                     {
                         date = "29/10/2015";
                     }
                     else if (i == 2)
                     {
                         date = "30/10/2015";
                     }
                     else if (i == 3)
                     {
                         date = "31/10/2015";
                     }
                     else if (i == 4)
                     {
                         date = "01/11/2015";
                     }
                     for (int j = 0; j < noOfsubitems; j++)
                     {
                         if (subitemDet.Items[i].SubItems[j].UniqueId.CompareTo(subitem.UniqueId) == 0)
                         {
                             string date1 = date + " " + subitemDet.Items[i].SubItems[j].ImagePath;
                             MessageDialog msgbox5 = new MessageDialog(date1);
                             await msgbox5.ShowAsync();
                             //DateTime dt = Convert.ToDateTime(date + subitemDet.Items[0].SubItems[0].ImagePath);
                             DateTime dt = DateTime.ParseExact(date1, "g", provider);
        
                             DateTime ddt = DateTime.Now;
                             var diffInSeconds = (dt - ddt).TotalSeconds;
                             MessageDialog msgbox6 = new MessageDialog(diffInSeconds.ToString());
                             await msgbox6.ShowAsync();
                             diffInSeconds = diffInSeconds - 900;
                             if ((diffInSeconds + 10) > 0)
                                 n.schedulenotif(diffInSeconds, subitemDet.Items[i].SubItems[j].Title);
                             MessageDialog msgbox7 = new MessageDialog("conv");
                             await msgbox7.ShowAsync();
                         }
                     }
                 }*/




                //else
                //  ListCars.Add(new FavClass() { UniqueId = subitem.UniqueId, Id = subitem.Id, Title = subitem.Title, Subtitle = subitem.Subtitle, ImagePath = subitem.ImagePath, Content = subitem.Content });
                try
                {
                    DataContractJsonSerializer serializer = new DataContractJsonSerializer(typeof(List<string>));
                    using (var stream = await local.OpenStreamForWriteAsync(
                                  "DataFile.json",
                                  CreationCollisionOption.ReplaceExisting))
                    {
                        serializer.WriteObject(stream, ListCars);
                    }
                }
                catch (Exception ex)
                {
                    throw;
                }

            }
        }
        private async Task IsFavourite(string id)
        {
            string content = String.Empty;
            List<string> ListCars = new List<string>();
            int flag = 0;
            StorageFolder local = ApplicationData.Current.LocalFolder;
            // Create a new file named DataFile.txt.
            var file = await local.CreateFileAsync("DataFile.json", CreationCollisionOption.OpenIfExists);

            if (local != null)
            {
                // var dataFolder = await local.GetFolderAsync("DataFolder");
                var myStream = await local.OpenStreamForReadAsync("DataFile.json");

                using (StreamReader reader = new StreamReader(myStream))
                {
                    content = await reader.ReadToEndAsync();
                }

                if (content != "")
                {
                    //Now add one more Entry.
                    ListCars = FavClass.ConvertToFavEvent(content);

                    foreach (var favEvent in ListCars)
                    {
                        if (String.Compare(favEvent, id) == 0)
                        {
                            flag = 1;
                            break;
                            //ListCars.Remove(new FavClass() { UniqueId = subitem.UniqueId, Id = subitem.Id, Title = subitem.Title, Subtitle = subitem.Subtitle, ImagePath = subitem.ImagePath, Content = subitem.Content });
                        }
                    }

                }
            }
            if (flag == 0)
            {
                MarkFav.Visibility = Visibility.Visible;
                MarkUnfav.Visibility = Visibility.Collapsed;
            }
            else
            {
                MarkFav.Visibility = Visibility.Collapsed;
                MarkUnfav.Visibility = Visibility.Visible;

            }

        }


        private async Task DeleteEntryIntoJsonAsync()
        {
            string content = String.Empty;
            List<string> ListCars = new List<string>();
            StorageFolder local = ApplicationData.Current.LocalFolder;
            // Create a new file named DataFile.txt.
            var file = await local.CreateFileAsync("DataFile.json", CreationCollisionOption.OpenIfExists);
           // CultureInfo provider = new CultureInfo("es-ES");
            if (local != null)
            {
                // var dataFolder = await local.GetFolderAsync("DataFolder");
                var myStream = await local.OpenStreamForReadAsync("DataFile.json");
                using (StreamReader reader = new StreamReader(myStream))
                {
                    content = await reader.ReadToEndAsync();
                }
                //MessageDialog msgbox3 = new MessageDialog(content);
                //await msgbox3.ShowAsync();
                if (content != "")
                {
                    //Now add one more Entry.
                    ListCars = FavClass.ConvertToFavEvent(content);

                    foreach (var favEvent in ListCars)
                    {
                        if (String.Compare(favEvent, subitem.UniqueId) == 0)
                        {
                            //MessageDialog msgbox4 = new MessageDialog(subitem.UniqueId);
                            //await msgbox4.ShowAsync();
                            //ListCars.Remove(favEvent);
                            int i = ListCars.FindIndex(f => f == subitem.UniqueId);
                            ListCars.RemoveAt(i);
                            break;
                            //ListCars.Remove(new FavClass() { UniqueId = subitem.UniqueId, Id = subitem.Id, Title = subitem.Title, Subtitle = subitem.Subtitle, ImagePath = subitem.ImagePath, Content = subitem.Content });
                        }
                    }
                    //ListCars.TrimExcess();
                    //var subitemDet = await SampleDataSource.GetGroupAsync("Schedule");
                    //schedulednotif n = new schedulednotif();
                    //n.schedulenotifrem(subitem.Title);
                    //int noOfItems = subitemDet.Items.Count;
                    //string date = "25/10/2015";
                    //for (int i = 0; i < noOfItems; i++)
                    //{
                    //    int noOfsubitems = subitemDet.Items[i].SubItems.Count;
                    //    MessageDialog msgbox4 = new MessageDialog(noOfsubitems.ToString());
                    //    await msgbox4.ShowAsync();
                    //    if (i == 0)
                    //    {
                    //        date = "26/10/2015";
                    //    }
                    //    else if (i == 1)
                    //    {
                    //        date = "26/10/2015";
                    //    }
                    //    else if (i == 2)
                    //    {
                    //        date = "30/10/2015";
                    //    }
                    //    else if (i == 3)
                    //    {
                    //        date = "31/10/2015";
                    //    }
                    //    else if (i == 4)
                    //    {
                    //        date = "01/11/2015";
                    //    }
                    //    for (int j = 0; j < noOfsubitems; j++)
                    //    {
                    //        if (subitemDet.Items[i].SubItems[j].UniqueId.CompareTo(subitem.UniqueId) == 0)
                    //        {
                    //            string date1 = date + " " + subitemDet.Items[i].SubItems[j].ImagePath;
                    //            MessageDialog msgbox5 = new MessageDialog(date1);
                    //            await msgbox5.ShowAsync();
                    //            //DateTime dt = Convert.ToDateTime(date + subitemDet.Items[0].SubItems[0].ImagePath);
                    //            DateTime dt = DateTime.ParseExact(date1, "g", provider);

                    //            DateTime ddt = DateTime.Now;
                    //            var diffInSeconds = (dt - ddt).TotalSeconds;
                    //            MessageDialog msgbox6 = new MessageDialog(diffInSeconds.ToString());
                    //            await msgbox6.ShowAsync();
                    //            diffInSeconds = diffInSeconds - 900;
                    //            if ((diffInSeconds + 10) > 0)

                    //            MessageDialog msgbox7 = new MessageDialog("conv");
                    //            await msgbox7.ShowAsync();
                    //        }
                    //    }
                    //}

                    //if (item != null)
                    //ListCars.Add(new FavClass() { UniqueId = item1.UniqueId, Id = item1.Id, Title = item1.Title, Subtitle = item1.Subtitle, ImagePath = item1.ImagePath, Content = item1.Content });
                    //else
                    //  ListCars.Add(new FavClass() { UniqueId = subitem.UniqueId, Id = subitem.Id, Title = subitem.Title, Subtitle = subitem.Subtitle, ImagePath = subitem.ImagePath, Content = subitem.Content });
                    try
                    {
                        DataContractJsonSerializer serializer = new DataContractJsonSerializer(typeof(List<string>));
                        using (var stream = await local.OpenStreamForWriteAsync(
                                      "DataFile.json",
                                      CreationCollisionOption.ReplaceExisting))
                        {
                            serializer.WriteObject(stream, ListCars);
                        }
                    }
                    catch (Exception ex)
                    {
                        throw;
                    }
                }

            }
        }

        
    }
}

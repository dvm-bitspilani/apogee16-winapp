using ApogeeApp1.Common;
using ApogeeApp1.Data;
//using HubApp4.FavData;
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
    public sealed partial class Favourite : Page
    {
        private NavigationHelper navigationHelper;
        private ObservableDictionary defaultViewModel = new ObservableDictionary();
        //private const string JSONFILENAME = "Fav.json";
        // private SampleDataItem item;
        //private SampleDataSubItem subitem;

        public Favourite()
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
        private void NavigationHelper_LoadState(object sender, LoadStateEventArgs e)
        {
            //item= await  SampleDataSource.GetItemAsync((string)e.NavigationParameter);
            // if(item==null)
            //{
            //subitem= await SampleDataSource.GetSubItemAsync((string)e.NavigationParameter);
            //}
            //await AddEntryIntoJsonAsync();


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
        private async Task deserializeJsonAsync()
        {
            string content = String.Empty;

            List<string> myCars;
            var jsonSerializer = new DataContractJsonSerializer(typeof(List<string>));
            StorageFolder local = ApplicationData.Current.LocalFolder;
            var file = await local.CreateFileAsync("DataFile.json",
          CreationCollisionOption.OpenIfExists);

            if (local != null)
            {

                //var dataFolder = await local.GetFolderAsync("DataFolder");

                var myStream = await local.OpenStreamForReadAsync("DataFile.json");

                myCars = (List<string>)jsonSerializer.ReadObject(myStream);
                if (myCars != null)
                {
                    List<SampleDataSubItem> evobj = new List<SampleDataSubItem>();
                    foreach (var favEvent in myCars)
                    {
                        var itemId = await SampleDataSource.GetSubItemAsync(favEvent);
                        evobj.Add(itemId);
                        //content += String.Format("ID: {0}, Make: {1}, Model: {2} ... ", favEvent.Id, favEvent.Title, favEvent.Model);
                    }

                    FavListView.ItemsSource = evobj;
                }
                /* if (myCars == null)
                 {
                     TextBlock tb = new TextBlock();
                     tb.Text = "Add favourite events";
                     tb.HorizontalAlignment = HorizontalAlignment.Center;
                     tb.VerticalAlignment = VerticalAlignment.Center;
                 }     */
            }
        }

        private async void ListView_Loaded(object sender, RoutedEventArgs e)
        {
            await deserializeJsonAsync();


        }

        private void ListView_ItemClick(object sender, ItemClickEventArgs e)
        {
            string subitemId = ((SampleDataSubItem)e.ClickedItem).UniqueId;
            // ListViewItem btn = sender as ListViewItem;
            // string subitemId = (string)btn.Tag;           
                Frame.Navigate(typeof(EventItemTabs), subitemId);         
        }
    }
}

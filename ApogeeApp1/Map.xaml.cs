using ApogeeApp1.Common;
using ApogeeApp1.Data1;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Input;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Devices.Geolocation;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage.Streams;
using Windows.Graphics.Display;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Maps;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;


// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkID=390556

namespace ApogeeApp1
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class Map : Page
    {
        MapIcon mapIcon = new MapIcon();
        public string eventVenue { get; set; }
        public string eventName { get; set; }

        private NavigationHelper navigationHelper;
        private ObservableDictionary defaultViewModel = new ObservableDictionary();
        public Map()
        {
            this.InitializeComponent();

            this.navigationHelper = new NavigationHelper(this);
            this.navigationHelper.LoadState += this.NavigationHelper_LoadState;
            this.navigationHelper.SaveState += this.NavigationHelper_SaveState;



        }
        public NavigationHelper NavigationHelper
        {
            get { return this.navigationHelper; }
        }
        public ObservableDictionary DefaultViewModel
        {
            get { return this.defaultViewModel; }
        }
        private async void NavigationHelper_LoadState(object sender, LoadStateEventArgs e)
        {
            var eventpin = await SampleDataSource1.GetSubItemAsync((string)e.NavigationParameter);
            if (eventpin != null)
            {
                eventName = eventpin.Title;

                eventVenue = eventpin.Venue;
            }
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

        /// <summary>
        /// Invoked when this page is about to be displayed in a Frame.
        /// </summary>
        /// <param name="e">Event data that describes how this page was reached.
        /// This parameter is typically used to configure the page.</param>

        protected async override void OnNavigatedTo(NavigationEventArgs e)
        {
            // TODO: Prepare page for display here.
            this.navigationHelper.OnNavigatedTo(e);
            // TODO: If your application contains multiple pages, ensure that you are
            // handling the hardware Back button by registering for the
            // Windows.Phone.UI.Input.HardwareButtons.BackPressed event.
            // If you are using the NavigationHelper provided by some templates,
            // this event is handled for you.
            //await Windows.UI.ViewManagement.StatusBar.GetForCurrentView().HideAsync();


            var locator = new Geolocator();
            var position = await locator.GetGeopositionAsync();
            myMap.Style = MapStyle.AerialWithRoads;

            MapIcon mapIcon = new MapIcon();
            mapIcon.Image = RandomAccessStreamReference.CreateFromUri(new Uri("ms-appx:///Assets/icons/mappin_new.png"));
            mapIcon.Location = new Geopoint(new BasicGeoposition() { Latitude = position.Coordinate.Point.Position.Latitude, Longitude = position.Coordinate.Point.Position.Longitude });
            mapIcon.NormalizedAnchorPoint = new Point(0.5, 1);
            myMap.MapElements.Add(mapIcon);
            var centerPoint = new Geopoint(new BasicGeoposition() { Latitude = 28.3641, Longitude = 075.5869 });
            if (eventVenue == null)
            {
                await myMap.TrySetViewAsync(centerPoint, 15, 0, 0, MapAnimationKind.Bow);
            }

            MapIcon mi1 = new MapIcon();
            mi1.Image = RandomAccessStreamReference.CreateFromUri(new Uri("ms-appx:///Assets/mapi6.png"));
            mi1.Location = new Geopoint(new BasicGeoposition() { Latitude = 28.3573, Longitude = 075.5882 });
            mi1.Title = "Saraswati Temple";
            myMap.MapElements.Add(mi1);
            MapIcon mi2 = new MapIcon();
            mi2.Image = RandomAccessStreamReference.CreateFromUri(new Uri("ms-appx:///Assets/mapi6.png"));
            mi2.Location = new Geopoint(new BasicGeoposition() { Latitude = 28.3635, Longitude = 075.5871 });
            mi2.Title = "Rotunda";
            myMap.MapElements.Add(mi2);
            MapIcon mi3 = new MapIcon();
            mi3.Image = RandomAccessStreamReference.CreateFromUri(new Uri("ms-appx:///Assets/mapi6.png"));
            mi3.Location = new Geopoint(new BasicGeoposition() { Latitude = 28.3639, Longitude = 075.5859 });
            mi3.Title = "FD-3";
            myMap.MapElements.Add(mi3);
            MapIcon mi4 = new MapIcon();
            mi4.Image = RandomAccessStreamReference.CreateFromUri(new Uri("ms-appx:///Assets/mapi6.png"));
            mi4.Location = new Geopoint(new BasicGeoposition() { Latitude = 28.3641, Longitude = 075.5881 });
            mi4.Title = "FD-2";
            myMap.MapElements.Add(mi4);
            MapIcon mi5 = new MapIcon();
            mi5.Image = RandomAccessStreamReference.CreateFromUri(new Uri("ms-appx:///Assets/mapi6.png"));
            mi5.Location = new Geopoint(new BasicGeoposition() { Latitude = 28.3608, Longitude = 075.5855 });
            mi5.Title = "SAC";
            myMap.MapElements.Add(mi5);
            MapIcon mi6 = new MapIcon();
            mi6.Image = RandomAccessStreamReference.CreateFromUri(new Uri("ms-appx:///Assets/mapi6.png"));
            mi6.Location = new Geopoint(new BasicGeoposition() { Latitude = 28.3644, Longitude = 075.5869 });
            mi6.Title = "Audi";
            myMap.MapElements.Add(mi6);
            MapIcon mi7 = new MapIcon();
            mi7.Image = RandomAccessStreamReference.CreateFromUri(new Uri("ms-appx:///Assets/mapi6.png"));
            mi7.Location = new Geopoint(new BasicGeoposition() { Latitude = 28.3597, Longitude = 075.5901 });
            mi7.Title = "Gym-G";
            myMap.MapElements.Add(mi7);
            MapIcon mi8 = new MapIcon();
            mi8.Image = RandomAccessStreamReference.CreateFromUri(new Uri("ms-appx:///Assets/mapi6.png"));
            mi8.Location = new Geopoint(new BasicGeoposition() { Latitude = 28.3604, Longitude = 075.5896 });
            mi8.Title = "ANC";
            myMap.MapElements.Add(mi8);
            MapIcon mi9 = new MapIcon();
            mi9.Image = RandomAccessStreamReference.CreateFromUri(new Uri("ms-appx:///Assets/mapi6.png"));
            mi9.Location = new Geopoint(new BasicGeoposition() { Latitude = 28.3653, Longitude = 075.5902 });
            mi9.Title = "LTC";
            myMap.MapElements.Add(mi9);
            MapIcon mi10 = new MapIcon();
            mi10.Image = RandomAccessStreamReference.CreateFromUri(new Uri("ms-appx:///Assets/mapi6.png"));
            mi10.Location = new Geopoint(new BasicGeoposition() { Latitude = 28.3655, Longitude = 075.5890 });
            mi10.Title = "Library";
            myMap.MapElements.Add(mi10);
            MapIcon mi11 = new MapIcon();
            mi11.Image = RandomAccessStreamReference.CreateFromUri(new Uri("ms-appx:///Assets/mapi6.png"));
            mi11.Location = new Geopoint(new BasicGeoposition() { Latitude = 28.3647, Longitude = 075.5869 });
            mi11.Title = "IC";
            myMap.MapElements.Add(mi11);
            MapIcon mi12 = new MapIcon();
            mi12.Image = RandomAccessStreamReference.CreateFromUri(new Uri("ms-appx:///Assets/mapi6.png"));
            mi12.Location = new Geopoint(new BasicGeoposition() { Latitude = 28.3644, Longitude = 075.5892 });
            mi12.Title = "FD-1";
            myMap.MapElements.Add(mi12);
            MapIcon mi13 = new MapIcon();
            mi13.Image = RandomAccessStreamReference.CreateFromUri(new Uri("ms-appx:///Assets/mapi6.png"));
            mi13.Location = new Geopoint(new BasicGeoposition() { Latitude = 28.3637, Longitude = 075.5895 });
            mi13.Title = "BITS Workshop";
            myMap.MapElements.Add(mi13);
            MapIcon mi14 = new MapIcon();
            mi14.Image = RandomAccessStreamReference.CreateFromUri(new Uri("ms-appx:///Assets/mapi6.png"));
            mi14.Location = new Geopoint(new BasicGeoposition() { Latitude = 28.3624, Longitude = 075.5894 });
            mi14.Title = "Vishwakarma Bhawan";
            myMap.MapElements.Add(mi14);
            MapIcon mi15 = new MapIcon();
            mi15.Image = RandomAccessStreamReference.CreateFromUri(new Uri("ms-appx:///Assets/mapi6.png"));
            mi15.Location = new Geopoint(new BasicGeoposition() { Latitude = 28.3626, Longitude = 075.5885 });
            mi15.Title = "Krishna Bhawan";
            myMap.MapElements.Add(mi15);
            MapIcon mi16 = new MapIcon();
            mi16.Image = RandomAccessStreamReference.CreateFromUri(new Uri("ms-appx:///Assets/mapi6.png"));
            mi16.Location = new Geopoint(new BasicGeoposition() { Latitude = 28.3613, Longitude = 075.5885 });
            mi16.Title = "Gandhi Bhawan";
            myMap.MapElements.Add(mi16);
            MapIcon mi17 = new MapIcon();
            mi17.Image = RandomAccessStreamReference.CreateFromUri(new Uri("ms-appx:///Assets/mapi6.png"));
            mi17.Location = new Geopoint(new BasicGeoposition() { Latitude = 28.3600, Longitude = 075.5889 });
            mi17.Title = "Shankar Bhawan";
            myMap.MapElements.Add(mi17);
            MapIcon mi18 = new MapIcon();
            mi18.Image = RandomAccessStreamReference.CreateFromUri(new Uri("ms-appx:///Assets/mapi6.png"));
            mi18.Location = new Geopoint(new BasicGeoposition() { Latitude = 28.3585, Longitude = 075.5891 });
            mi18.Title = "Vyas Bhawan";
            myMap.MapElements.Add(mi18);
            MapIcon mi19 = new MapIcon();
            mi19.Image = RandomAccessStreamReference.CreateFromUri(new Uri("ms-appx:///Assets/mapi6.png"));
            mi19.Location = new Geopoint(new BasicGeoposition() { Latitude = 28.3614, Longitude = 075.5895 });
            mi19.Title = "Bhagirath Bhawan";
            myMap.MapElements.Add(mi19);
            MapIcon mi20 = new MapIcon();
            mi20.Image = RandomAccessStreamReference.CreateFromUri(new Uri("ms-appx:///Assets/mapi6.png"));
            mi20.Location = new Geopoint(new BasicGeoposition() { Latitude = 28.3569, Longitude = 075.5863 });
            mi20.Title = "Meera Bhawan";
            myMap.MapElements.Add(mi20);
            MapIcon mi21 = new MapIcon();
            mi21.Image = RandomAccessStreamReference.CreateFromUri(new Uri("ms-appx:///Assets/mapi6.png"));
            mi21.Location = new Geopoint(new BasicGeoposition() { Latitude = 28.3630, Longitude = 075.5903 });
            mi21.Title = "Ranapratap Bhawan";
            myMap.MapElements.Add(mi21);
            MapIcon mi22 = new MapIcon();
            mi22.Image = RandomAccessStreamReference.CreateFromUri(new Uri("ms-appx:///Assets/mapi6.png"));
            mi22.Location = new Geopoint(new BasicGeoposition() { Latitude = 28.3660, Longitude = 075.5863 });
            mi22.Title = "SR Bhawan";
            myMap.MapElements.Add(mi22);
            MapIcon mi23 = new MapIcon();
            mi23.Image = RandomAccessStreamReference.CreateFromUri(new Uri("ms-appx:///Assets/mapi6.png"));
            mi23.Location = new Geopoint(new BasicGeoposition() { Latitude = 28.3623, Longitude = 075.5861 });
            mi23.Title = "Ram Bhawan";
            myMap.MapElements.Add(mi23);
            MapIcon mi24 = new MapIcon();
            mi24.Image = RandomAccessStreamReference.CreateFromUri(new Uri("ms-appx:///Assets/mapi6.png"));
            mi24.Location = new Geopoint(new BasicGeoposition() { Latitude = 28.3610, Longitude = 075.5864 });
            mi24.Title = "Buddh Bhawan";
            myMap.MapElements.Add(mi24);
            MapIcon mi25 = new MapIcon();
            mi25.Image = RandomAccessStreamReference.CreateFromUri(new Uri("ms-appx:///Assets/mapi6.png"));
            mi25.Location = new Geopoint(new BasicGeoposition() { Latitude = 28.3614, Longitude = 075.5907 });
            mi25.Title = "Ashok Bhawan";
            myMap.MapElements.Add(mi25);
            MapIcon mi26 = new MapIcon();
            mi26.Image = RandomAccessStreamReference.CreateFromUri(new Uri("ms-appx:///Assets/mapi6.png"));
            mi26.Location = new Geopoint(new BasicGeoposition() { Latitude = 28.3620, Longitude = 075.5852 });
            mi26.Title = "Malviya Bhawan";
            myMap.MapElements.Add(mi26);
            MapIcon mi27 = new MapIcon();
            mi27.Image = RandomAccessStreamReference.CreateFromUri(new Uri("ms-appx:///Assets/mapi6.png"));
            mi27.Location = new Geopoint(new BasicGeoposition() { Latitude = 28.3627, Longitude = 075.5912 });
            mi27.Title = "C.V. Raman Bhawan";
            myMap.MapElements.Add(mi27);
            MapIcon mi28 = new MapIcon();
            mi28.Image = RandomAccessStreamReference.CreateFromUri(new Uri("ms-appx:///Assets/mapi6.png"));
            mi28.Location = new Geopoint(new BasicGeoposition() { Latitude = 28.3607, Longitude = 075.5927 });
            mi28.Title = "V-Fast";
            myMap.MapElements.Add(mi28);
            MapIcon mi29 = new MapIcon();
            mi29.Image = RandomAccessStreamReference.CreateFromUri(new Uri("ms-appx:///Assets/mapi6.png"));
            mi29.Location = new Geopoint(new BasicGeoposition() { Latitude = 28.3631, Longitude = 075.5849 });
            mi29.Title = "Birla Museum";
            myMap.MapElements.Add(mi29);
            MapIcon mi30 = new MapIcon();
            mi30.Image = RandomAccessStreamReference.CreateFromUri(new Uri("ms-appx:///Assets/mapi6.png"));
            mi30.Location = new Geopoint(new BasicGeoposition() { Latitude = 28.3638, Longitude = 075.5850 });
            mi30.Title = "Sky Lawns";
            myMap.MapElements.Add(mi30);
            MapIcon mi31 = new MapIcon();
            mi31.Image = RandomAccessStreamReference.CreateFromUri(new Uri("ms-appx:///Assets/mapi6.png"));
            mi31.Location = new Geopoint(new BasicGeoposition() { Latitude = 28.3634, Longitude = 075.5840 });
            mi31.Title = "G.D.Birla Memorial";
            myMap.MapElements.Add(mi31);
            MapIcon mi32 = new MapIcon();
            mi32.Image = RandomAccessStreamReference.CreateFromUri(new Uri("ms-appx:///Assets/mapi6.png"));
            mi32.Location = new Geopoint(new BasicGeoposition() { Latitude = 28.3577, Longitude = 075.5900 });
            mi32.Title = "Medical Centre";
            myMap.MapElements.Add(mi32);
            MapIcon mi33 = new MapIcon();
            mi33.Image = RandomAccessStreamReference.CreateFromUri(new Uri("ms-appx:///Assets/mapi6.png"));
            mi33.Location = new Geopoint(new BasicGeoposition() { Latitude = 28.3572, Longitude = 075.5901 });
            mi33.Title = "Akshay Super Market";
            myMap.MapElements.Add(mi33);
            MapIcon mi34 = new MapIcon();
            mi34.Image = RandomAccessStreamReference.CreateFromUri(new Uri("ms-appx:///Assets/mapi6.png"));
            mi34.Location = new Geopoint(new BasicGeoposition() { Latitude = 28.3573, Longitude = 075.5909 });
            mi34.Title = "ICICI ATM";
            myMap.MapElements.Add(mi34);
            MapIcon mi35 = new MapIcon();
            mi35.Image = RandomAccessStreamReference.CreateFromUri(new Uri("ms-appx:///Assets/mapi6.png"));
            mi35.Location = new Geopoint(new BasicGeoposition() { Latitude = 28.3616, Longitude = 075.5859 });
            mi35.Title = "Axis ATM";
            myMap.MapElements.Add(mi35);
            MapIcon mi36 = new MapIcon();
            mi36.Image = RandomAccessStreamReference.CreateFromUri(new Uri("ms-appx:///Assets/mapi6.png"));
            mi36.Location = new Geopoint(new BasicGeoposition() { Latitude = 28.3611, Longitude = 075.5855 });
            mi36.Title = "Food King";
            myMap.MapElements.Add(mi36);
            MapIcon mi37 = new MapIcon();
            mi37.Image = RandomAccessStreamReference.CreateFromUri(new Uri("ms-appx:///Assets/mapi6.png"));
            mi37.Location = new Geopoint(new BasicGeoposition() { Latitude = 28.3635, Longitude = 075.5883 });
            mi37.Title = "M-lawns";
            myMap.MapElements.Add(mi37);
            MapIcon mi38 = new MapIcon();
            mi38.Image = RandomAccessStreamReference.CreateFromUri(new Uri("ms-appx:///Assets/mapi6.png"));
            mi38.Location = new Geopoint(new BasicGeoposition() { Latitude = 28.3633, Longitude = 075.5907 });
            mi38.Title = "UCO ATM";
            myMap.MapElements.Add(mi38);
            MapIcon mi39 = new MapIcon();
            mi39.Image = RandomAccessStreamReference.CreateFromUri(new Uri("ms-appx:///Assets/mapi6.png"));
            mi39.Location = new Geopoint(new BasicGeoposition() { Latitude = 28.3621, Longitude = 075.5876 });
            mi39.Title = "NAB Audi";
            myMap.MapElements.Add(mi39);
            MapIcon mi40 = new MapIcon();
            mi40.Image = RandomAccessStreamReference.CreateFromUri(new Uri("ms-appx:///Assets/mapi6.png"));
            mi40.Location = new Geopoint(new BasicGeoposition() { Latitude = 28.3564, Longitude = 075.5912 });
            mi40.Title = "C'Not";
            myMap.MapElements.Add(mi40);
            MapIcon mi41 = new MapIcon();
            mi41.Image = RandomAccessStreamReference.CreateFromUri(new Uri("ms-appx:///Assets/mapi7.png"));
            mi41.Location = new Geopoint(new BasicGeoposition() { Latitude = 28.3641, Longitude = 075.5869 });
            mi41.Title = "BITS Pilani";
            myMap.MapElements.Add(mi41);


            //event pin
            if (eventVenue != null)
            {
                //MapIcon eventicon = new MapIcon();

                if (String.Compare(eventVenue, mi1.Title) == 0)
                {

                    mi1.Image = RandomAccessStreamReference.CreateFromUri(new Uri("ms-appx:///Assets/mappin.png"));
                    //eventicon.Location = new Geopoint(new BasicGeoposition() { Latitude = 28.3580, Longitude = 075.5890 });
                    mi1.Title = eventName + ", " + eventVenue;
                    // myMap.MapElements.Add(eventicon);
                    await myMap.TrySetViewAsync(mi1.Location, 15, 0, 0, MapAnimationKind.Bow);

                }
                else if (String.Compare(eventVenue, mi2.Title) == 0)
                {
                    mi2.Image = RandomAccessStreamReference.CreateFromUri(new Uri("ms-appx:///Assets/mappin.png"));
                    mi2.Title = eventName + ", " + eventVenue;
                    await myMap.TrySetViewAsync(mi2.Location, 15, 0, 0, MapAnimationKind.Bow);
                }
                else if (String.Compare(eventVenue, mi3.Title) == 0)
                {
                    mi3.Image = RandomAccessStreamReference.CreateFromUri(new Uri("ms-appx:///Assets/mappin.png"));
                    mi3.Title = eventName + ", " + eventVenue;
                    await myMap.TrySetViewAsync(mi3.Location, 15, 0, 0, MapAnimationKind.Bow);
                }
                else if (String.Compare(eventVenue, mi4.Title) == 0)
                {
                    mi4.Image = RandomAccessStreamReference.CreateFromUri(new Uri("ms-appx:///Assets/mappin.png"));
                    mi4.Title = eventName + ", " + eventVenue;
                    await myMap.TrySetViewAsync(mi4.Location, 15, 0, 0, MapAnimationKind.Bow);
                }
                else if (String.Compare(eventVenue, mi5.Title) == 0)
                {
                    mi5.Image = RandomAccessStreamReference.CreateFromUri(new Uri("ms-appx:///Assets/mappin.png"));
                    mi5.Title = eventName + ", " + eventVenue;
                    await myMap.TrySetViewAsync(mi5.Location, 15, 0, 0, MapAnimationKind.Bow);
                }
                else if (String.Compare(eventVenue, mi6.Title) == 0)
                {
                    mi6.Image = RandomAccessStreamReference.CreateFromUri(new Uri("ms-appx:///Assets/mappin.png"));
                    mi6.Title = eventName + ", " + eventVenue;
                    await myMap.TrySetViewAsync(mi6.Location, 15, 0, 0, MapAnimationKind.Bow);
                }
                else if (String.Compare(eventVenue, mi7.Title) == 0)
                {
                    mi7.Image = RandomAccessStreamReference.CreateFromUri(new Uri("ms-appx:///Assets/mappin.png"));
                    mi7.Title = eventName + ", " + eventVenue;
                    await myMap.TrySetViewAsync(mi7.Location, 15, 0, 0, MapAnimationKind.Bow);
                }
                else if (String.Compare(eventVenue, mi8.Title) == 0)
                {
                    mi8.Image = RandomAccessStreamReference.CreateFromUri(new Uri("ms-appx:///Assets/mappin.png"));
                    mi8.Title = eventName + ", " + eventVenue;
                    await myMap.TrySetViewAsync(mi8.Location, 15, 0, 0, MapAnimationKind.Bow);
                }
                else if (String.Compare(eventVenue, mi9.Title) == 0)
                {
                    mi9.Image = RandomAccessStreamReference.CreateFromUri(new Uri("ms-appx:///Assets/mappin.png"));
                    mi9.Title = eventName + ", " + eventVenue;
                    await myMap.TrySetViewAsync(mi9.Location, 15, 0, 0, MapAnimationKind.Bow);
                }
                else if (String.Compare(eventVenue, mi10.Title) == 0)
                {
                    mi10.Image = RandomAccessStreamReference.CreateFromUri(new Uri("ms-appx:///Assets/mappin.png"));
                    mi10.Title = eventName + ", " + eventVenue;
                    await myMap.TrySetViewAsync(mi10.Location, 15, 0, 0, MapAnimationKind.Bow);
                }
                else if (String.Compare(eventVenue, mi11.Title) == 0)
                {
                    mi11.Image = RandomAccessStreamReference.CreateFromUri(new Uri("ms-appx:///Assets/mappin.png"));
                    mi11.Title = eventName + ", " + eventVenue;
                    await myMap.TrySetViewAsync(mi11.Location, 15, 0, 0, MapAnimationKind.Bow);
                }
                else if (String.Compare(eventVenue, mi12.Title) == 0)
                {
                    mi12.Image = RandomAccessStreamReference.CreateFromUri(new Uri("ms-appx:///Assets/mappin.png"));
                    mi12.Title = eventName + ", " + eventVenue;
                    await myMap.TrySetViewAsync(mi12.Location, 15, 0, 0, MapAnimationKind.Bow);
                }
                else if (String.Compare(eventVenue, mi13.Title) == 0)
                {
                    mi13.Image = RandomAccessStreamReference.CreateFromUri(new Uri("ms-appx:///Assets/mappin.png"));
                    mi13.Title = eventName + ", " + eventVenue;
                    await myMap.TrySetViewAsync(mi13.Location, 15, 0, 0, MapAnimationKind.Bow);
                }
                else if (String.Compare(eventVenue, mi14.Title) == 0)
                {
                    mi14.Image = RandomAccessStreamReference.CreateFromUri(new Uri("ms-appx:///Assets/mappin.png"));
                    mi14.Title = eventName + ", " + eventVenue;
                    await myMap.TrySetViewAsync(mi14.Location, 15, 0, 0, MapAnimationKind.Bow);
                }
                else if (String.Compare(eventVenue, mi15.Title) == 0)
                {
                    mi15.Image = RandomAccessStreamReference.CreateFromUri(new Uri("ms-appx:///Assets/mappin.png"));
                    mi15.Title = eventName + ", " + eventVenue;
                    await myMap.TrySetViewAsync(mi15.Location, 15, 0, 0, MapAnimationKind.Bow);
                }
                else if (String.Compare(eventVenue, mi16.Title) == 0)
                {
                    mi16.Image = RandomAccessStreamReference.CreateFromUri(new Uri("ms-appx:///Assets/mappin.png"));
                    mi16.Title = eventName + ", " + eventVenue;
                    await myMap.TrySetViewAsync(mi16.Location, 15, 0, 0, MapAnimationKind.Bow);
                }
                else if (String.Compare(eventVenue, mi17.Title) == 0)
                {
                    mi17.Image = RandomAccessStreamReference.CreateFromUri(new Uri("ms-appx:///Assets/mappin.png"));
                    mi17.Title = eventName + ", " + eventVenue;
                    await myMap.TrySetViewAsync(mi17.Location, 15, 0, 0, MapAnimationKind.Bow);
                }
                else if (String.Compare(eventVenue, mi18.Title) == 0)
                {
                    mi18.Image = RandomAccessStreamReference.CreateFromUri(new Uri("ms-appx:///Assets/mappin.png"));
                    mi18.Title = eventName + ", " + eventVenue;
                    await myMap.TrySetViewAsync(mi18.Location, 15, 0, 0, MapAnimationKind.Bow);
                }
                else if (String.Compare(eventVenue, mi19.Title) == 0)
                {
                    mi19.Image = RandomAccessStreamReference.CreateFromUri(new Uri("ms-appx:///Assets/mappin.png"));
                    mi19.Title = eventName + ", " + eventVenue;
                    await myMap.TrySetViewAsync(mi19.Location, 15, 0, 0, MapAnimationKind.Bow);
                }
                else if (String.Compare(eventVenue, mi20.Title) == 0)
                {
                    mi20.Image = RandomAccessStreamReference.CreateFromUri(new Uri("ms-appx:///Assets/mappin.png"));
                    mi20.Title = eventName + ", " + eventVenue;
                    await myMap.TrySetViewAsync(mi20.Location, 15, 0, 0, MapAnimationKind.Bow);
                }
                else if (String.Compare(eventVenue, mi21.Title) == 0)
                {
                    mi21.Image = RandomAccessStreamReference.CreateFromUri(new Uri("ms-appx:///Assets/mappin.png"));
                    mi21.Title = eventName + ", " + eventVenue;
                    await myMap.TrySetViewAsync(mi21.Location, 15, 0, 0, MapAnimationKind.Bow);
                }
                else if (String.Compare(eventVenue, mi22.Title) == 0)
                {
                    mi22.Image = RandomAccessStreamReference.CreateFromUri(new Uri("ms-appx:///Assets/mappin.png"));
                    mi22.Title = eventName + ", " + eventVenue;
                    await myMap.TrySetViewAsync(mi22.Location, 15, 0, 0, MapAnimationKind.Bow);
                }
                else if (String.Compare(eventVenue, mi23.Title) == 0)
                {
                    mi23.Image = RandomAccessStreamReference.CreateFromUri(new Uri("ms-appx:///Assets/mappin.png"));
                    mi23.Title = eventName + ", " + eventVenue;
                    await myMap.TrySetViewAsync(mi23.Location, 15, 0, 0, MapAnimationKind.Bow);
                }
                else if (String.Compare(eventVenue, mi24.Title) == 0)
                {
                    mi24.Image = RandomAccessStreamReference.CreateFromUri(new Uri("ms-appx:///Assets/mappin.png"));
                    mi24.Title = eventName + ", " + eventVenue;
                    await myMap.TrySetViewAsync(mi24.Location, 15, 0, 0, MapAnimationKind.Bow);
                }
                else if (String.Compare(eventVenue, mi25.Title) == 0)
                {
                    mi25.Image = RandomAccessStreamReference.CreateFromUri(new Uri("ms-appx:///Assets/mappin.png"));
                    mi25.Title = eventName + ", " + eventVenue;
                    await myMap.TrySetViewAsync(mi25.Location, 15, 0, 0, MapAnimationKind.Bow);
                }
                else if (String.Compare(eventVenue, mi26.Title) == 0)
                {
                    mi26.Image = RandomAccessStreamReference.CreateFromUri(new Uri("ms-appx:///Assets/mappin.png"));
                    mi26.Title = eventName + ", " + eventVenue;
                    await myMap.TrySetViewAsync(mi26.Location, 15, 0, 0, MapAnimationKind.Bow);
                }
                else if (String.Compare(eventVenue, mi27.Title) == 0)
                {
                    mi27.Image = RandomAccessStreamReference.CreateFromUri(new Uri("ms-appx:///Assets/mappin.png"));
                    mi27.Title = eventName + ", " + eventVenue;
                    await myMap.TrySetViewAsync(mi27.Location, 15, 0, 0, MapAnimationKind.Bow);
                }
                else if (String.Compare(eventVenue, mi28.Title) == 0)
                {
                    mi28.Image = RandomAccessStreamReference.CreateFromUri(new Uri("ms-appx:///Assets/mappin.png"));
                    mi28.Title = eventName + ", " + eventVenue;
                    await myMap.TrySetViewAsync(mi28.Location, 15, 0, 0, MapAnimationKind.Bow);
                }
                else if (String.Compare(eventVenue, mi29.Title) == 0)
                {
                    mi29.Image = RandomAccessStreamReference.CreateFromUri(new Uri("ms-appx:///Assets/mappin.png"));
                    mi29.Title = eventName + ", " + eventVenue;
                    await myMap.TrySetViewAsync(mi29.Location, 15, 0, 0, MapAnimationKind.Bow);
                }
                else if (String.Compare(eventVenue, mi30.Title) == 0)
                {
                    mi30.Image = RandomAccessStreamReference.CreateFromUri(new Uri("ms-appx:///Assets/mappin.png"));
                    mi30.Title = eventName + ", " + eventVenue;
                    await myMap.TrySetViewAsync(mi30.Location, 15, 0, 0, MapAnimationKind.Bow);
                }
                else if (String.Compare(eventVenue, mi31.Title) == 0)
                {
                    mi31.Image = RandomAccessStreamReference.CreateFromUri(new Uri("ms-appx:///Assets/mappin.png"));
                    mi31.Title = eventName + ", " + eventVenue;
                    await myMap.TrySetViewAsync(mi31.Location, 15, 0, 0, MapAnimationKind.Bow);
                }
                else if (String.Compare(eventVenue, mi32.Title) == 0)
                {
                    mi32.Image = RandomAccessStreamReference.CreateFromUri(new Uri("ms-appx:///Assets/mappin.png"));
                    mi32.Title = eventName + ", " + eventVenue;
                    await myMap.TrySetViewAsync(mi32.Location, 15, 0, 0, MapAnimationKind.Bow);
                }
                else if (String.Compare(eventVenue, mi33.Title) == 0)
                {
                    mi33.Image = RandomAccessStreamReference.CreateFromUri(new Uri("ms-appx:///Assets/mappin.png"));
                    mi33.Title = eventName + ", " + eventVenue;
                    await myMap.TrySetViewAsync(mi33.Location, 15, 0, 0, MapAnimationKind.Bow);
                }
                else if (String.Compare(eventVenue, mi34.Title) == 0)
                {
                    mi34.Image = RandomAccessStreamReference.CreateFromUri(new Uri("ms-appx:///Assets/mappin.png"));
                    mi34.Title = eventName + ", " + eventVenue;
                    await myMap.TrySetViewAsync(mi34.Location, 15, 0, 0, MapAnimationKind.Bow);
                }
                else if (String.Compare(eventVenue, mi35.Title) == 0)
                {
                    mi35.Image = RandomAccessStreamReference.CreateFromUri(new Uri("ms-appx:///Assets/mappin.png"));
                    mi35.Title = eventName + ", " + eventVenue;
                    await myMap.TrySetViewAsync(mi35.Location, 15, 0, 0, MapAnimationKind.Bow);
                }
                else if (String.Compare(eventVenue, mi36.Title) == 0)
                {
                    mi36.Image = RandomAccessStreamReference.CreateFromUri(new Uri("ms-appx:///Assets/mappin.png"));
                    mi36.Title = eventName + ", " + eventVenue;
                    await myMap.TrySetViewAsync(mi36.Location, 15, 0, 0, MapAnimationKind.Bow);
                }
                else if (String.Compare(eventVenue, mi37.Title) == 0)
                {
                    mi37.Image = RandomAccessStreamReference.CreateFromUri(new Uri("ms-appx:///Assets/mappin.png"));
                    mi37.Title = eventName + ", " + eventVenue;
                    await myMap.TrySetViewAsync(mi37.Location, 15, 0, 0, MapAnimationKind.Bow);
                }
                else if (String.Compare(eventVenue, mi38.Title) == 0)
                {
                    mi38.Image = RandomAccessStreamReference.CreateFromUri(new Uri("ms-appx:///Assets/mappin.png"));
                    mi38.Title = eventName + ", " + eventVenue;
                    await myMap.TrySetViewAsync(mi38.Location, 15, 0, 0, MapAnimationKind.Bow);
                }
                else if (String.Compare(eventVenue, mi39.Title) == 0)
                {
                    mi39.Image = RandomAccessStreamReference.CreateFromUri(new Uri("ms-appx:///Assets/mappin.png"));
                    mi39.Title = eventName + ", " + eventVenue;
                    await myMap.TrySetViewAsync(mi39.Location, 15, 0, 0, MapAnimationKind.Bow);
                }
                else if (String.Compare(eventVenue, mi40.Title) == 0)
                {
                    mi40.Image = RandomAccessStreamReference.CreateFromUri(new Uri("ms-appx:///Assets/mappin.png"));
                    mi40.Title = eventName + ", " + eventVenue;
                    await myMap.TrySetViewAsync(mi40.Location, 15, 0, 0, MapAnimationKind.Bow);
                }
                else if (String.Compare(eventVenue, mi41.Title) == 0)
                {
                    mi41.Image = RandomAccessStreamReference.CreateFromUri(new Uri("ms-appx:///Assets/mappin.png"));
                    mi41.Title = eventName + ", " + eventVenue;
                    await myMap.TrySetViewAsync(mi41.Location, 15, 0, 0, MapAnimationKind.Bow);
                }
                else if (String.Compare(eventVenue, "VK Lawns") == 0)
                {
                    mi14.Image = RandomAccessStreamReference.CreateFromUri(new Uri("ms-appx:///Assets/mappin.png"));
                    mi14.Title = eventName + ", " + eventVenue;
                    await myMap.TrySetViewAsync(mi14.Location, 15, 0, 0, MapAnimationKind.Bow);
                }
                else if (String.Compare(eventVenue, "Krishna Parking") == 0)
                {
                    mi15.Image = RandomAccessStreamReference.CreateFromUri(new Uri("ms-appx:///Assets/mappin.png"));
                    mi15.Title = eventName + ", " + eventVenue;
                    await myMap.TrySetViewAsync(mi15.Location, 15, 0, 0, MapAnimationKind.Bow);
                }
                else if (String.Compare(eventVenue, "FD-3 Terrace") == 0)
                {
                    mi3.Image = RandomAccessStreamReference.CreateFromUri(new Uri("ms-appx:///Assets/mappin.png"));
                    mi3.Title = eventName + ", " + eventVenue;
                    await myMap.TrySetViewAsync(mi3.Location, 15, 0, 0, MapAnimationKind.Bow);
                }
                else if (String.Compare(eventVenue, "M-Lawns1") == 0 || String.Compare(eventVenue, "M-Lawns2") == 0)
                {
                    mi36.Image = RandomAccessStreamReference.CreateFromUri(new Uri("ms-appx:///Assets/mappin.png"));
                    mi36.Title = eventName + ", " + eventVenue;
                    await myMap.TrySetViewAsync(mi36.Location, 15, 0, 0, MapAnimationKind.Bow);
                }
                else if (String.Compare(eventVenue, "SAC Hall") == 0 || String.Compare(eventVenue, "Badminton Court") == 0)
                {
                    mi5.Image = RandomAccessStreamReference.CreateFromUri(new Uri("ms-appx:///Assets/mappin.png"));
                    mi5.Title = eventName + ", " + eventVenue;
                    await myMap.TrySetViewAsync(mi5.Location, 15, 0, 0, MapAnimationKind.Bow);
                }
                else if (String.Compare(eventVenue, "Med-C grounds") == 0)
                {
                    mi32.Image = RandomAccessStreamReference.CreateFromUri(new Uri("ms-appx:///Assets/mappin.png"));
                    mi32.Title = eventName + ", " + eventVenue;
                    await myMap.TrySetViewAsync(mi32.Location, 15, 0, 0, MapAnimationKind.Bow);
                }
                else if (eventVenue.StartsWith("5") || String.Compare(eventVenue, "LTC QT") == 0)
                {
                    mi9.Image = RandomAccessStreamReference.CreateFromUri(new Uri("ms-appx:///Assets/mappin.png"));
                    mi9.Title = eventName + ", " + eventVenue;
                    await myMap.TrySetViewAsync(mi9.Location, 15, 0, 0, MapAnimationKind.Bow);
                }
                else if (eventVenue.StartsWith("3") || eventVenue == "FD-3 Terrace" || eventVenue == "Chem Lab(3143)")
                {
                    mi9.Image = RandomAccessStreamReference.CreateFromUri(new Uri("ms-appx:///Assets/mappin.png"));
                    mi9.Title = eventName + ", " + eventVenue;
                    await myMap.TrySetViewAsync(mi9.Location, 15, 0, 0, MapAnimationKind.Bow);
                }
                else if (eventVenue.StartsWith("6"))
                {
                    mi39.Image = RandomAccessStreamReference.CreateFromUri(new Uri("ms-appx:///Assets/mappin.png"));
                    mi39.Title = eventName + ", " + eventVenue;
                    await myMap.TrySetViewAsync(mi39.Location, 15, 0, 0, MapAnimationKind.Bow);
                }

                else if (eventVenue.StartsWith("2") || eventVenue == "FD-2 QT" || eventVenue == "FD-2 Water Cooler" || eventVenue == "O-Lab" || eventVenue == "DD Lab")
                {
                    mi4.Image = RandomAccessStreamReference.CreateFromUri(new Uri("ms-appx:///Assets/mappin.png"));
                    mi4.Title = eventName + ", " + eventVenue;
                    await myMap.TrySetViewAsync(mi4.Location, 15, 0, 0, MapAnimationKind.Bow);

                }




            }

        }
        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            this.navigationHelper.OnNavigatedFrom(e);
        }

        #endregion



        private void AppBarButton_Click(object sender, RoutedEventArgs e)
        {
            if (myMap.ZoomLevel <= 19)
                myMap.ZoomLevel += 1;

        }

        private void AppBarButton_Click_1(object sender, RoutedEventArgs e)
        {
            if (myMap.ZoomLevel > 1)
                myMap.ZoomLevel -= 1;
        }

        private async void AppBarButton_Click_2(object sender, RoutedEventArgs e)
        {

            var locator1 = new Geolocator();
            var position1 = await locator1.GetGeopositionAsync();
            myMap.Center = new Geopoint(new BasicGeoposition() { Latitude = position1.Coordinate.Point.Position.Latitude, Longitude = position1.Coordinate.Point.Position.Longitude });

            // MapIcon mapIcon1 = new MapIcon();
            //mapIcon1.Image = RandomAccessStreamReference.CreateFromUri(new Uri("ms-appx:///Assets/mapi4.png"));
            mapIcon.Location = new Geopoint(new BasicGeoposition() { Latitude = position1.Coordinate.Point.Position.Latitude, Longitude = position1.Coordinate.Point.Position.Longitude });
            //mapIcon1.NormalizedAnchorPoint = new Point(0.5, 1);
            // myMap.MapElements.Add(mapIcon1);


        }

        

        //private void myMap_MapTapped(MapControl sender, MapInputEventArgs args)
        //{

        //}

        /* private async void AppBarButton_Click_3(object sender, RoutedEventArgs e)
        {
            var locator1 = new Geolocator();
            var position1 = await locator1.GetGeopositionAsync();
           // myMap.Center = new Geopoint(new BasicGeoposition() { Latitude = position1.Coordinate.Point.Position.Latitude, Longitude = position1.Coordinate.Point.Position.Longitude });
            double lat = position1.Coordinate.Point.Position.Latitude;
            double log = position1.Coordinate.Point.Position.Longitude;
            if(lat>28.360159 && lat<28.361305 && log>75.584945 && log<75.585690)
            {

            }

        }
         */
    }
}

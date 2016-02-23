using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Devices.Geolocation;

namespace ApogeeApp1.MapPi
{
   public class MapPin
    {
       public MapPin(string pinTitle, Geopoint pinPoint)
       {
           this.PinTitle = pinTitle;
           this.PinPoint = pinPoint;
       }
       public string PinTitle {get; set;}
       public Geopoint PinPoint { get; set; }

       public ObservableCollection<MapPin> GetMapPins()
       {
           ObservableCollection<MapPin> mapPins = new ObservableCollection<MapPin>();
           mapPins.Add(new MapPin("Saraswati Temple", new Geopoint(new BasicGeoposition() { Latitude = 28.3573, Longitude = 075.5882 })));
           return mapPins;

       }
    }
     
}

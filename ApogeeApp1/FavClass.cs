using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading.Tasks;

namespace ApogeeApp1
{
    public class FavClass
    {
        public string UniqueId { get; set; }
        public string Id { get; set; }
        public string Title { get; set; }
        public string Subtitle { get; set; }
        public string ImagePath { get; set; }
        public string Content { get; set; }

        public static List<string> ConvertToFavEvent(string json)
        {
            List<string> fvc = new List<string>();
            try
            {
                DataContractJsonSerializer serializer = new DataContractJsonSerializer(typeof(List<string>));
                using (MemoryStream ms = new MemoryStream(Encoding.Unicode.GetBytes(json)))
                {
                    fvc = serializer.ReadObject(ms) as List<string>;
                }
            }
            catch (Exception ex)
            {
                throw;
            }
            return fvc;
        }
    }

}

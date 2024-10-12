using System.Text.Json.Serialization;

namespace csharpstarterapp.Models{
    public class Computer 
    {
        [JsonPropertyName("computer_id")]
        public int ComputerId {get; set;}
        [JsonPropertyName("motherboard")]
        public string Motherboard {get; set;}
        // a second method set value to null-set default value, instead of constructor
        //public string Motherboard {get; set;} = "";
        [JsonPropertyName("cpu_cores")]
        public int? CPUCores {get; set;}
        [JsonPropertyName("has_wifi")]
        public bool HasWifi {get; set;}
        [JsonPropertyName("has_lte")]
        public Boolean HasLTE {get; set;}
        [JsonPropertyName("release_date")]
        public DateTime? ReleaseDate {get; set;}
        [JsonPropertyName("price")]
        public decimal Price {get; set;}
        [JsonPropertyName("video_card")]
        public string VideoCard {get; set;}

        public Computer() {
            if (VideoCard == null) {
                VideoCard = "";
            }
            if (Motherboard == null) {
                Motherboard = "";
            }
            if (CPUCores == null) {
                CPUCores = 0;
            }
        }


        
    }
}
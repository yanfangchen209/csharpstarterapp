namespace csharpstarterapp.Models{
    public class Computer 
    {
        public int ComputerId {get; set;}
        public string Motherboard {get; set;}
        // a second method set value to null-set default value, instead of constructor
        //public string Motherboard {get; set;} = "";
        public int? CPUCores {get; set;}
        public bool HasWifi {get; set;}
        public Boolean HasLTE {get; set;}
        public DateTime? ReleaseDate {get; set;}
        public decimal Price {get; set;}
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
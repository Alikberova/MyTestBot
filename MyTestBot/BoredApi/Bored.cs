using System;
using System.Collections.Generic;
using System.Text;

namespace MyTestBot.BoredApi
{
    public class Bored
	{
        public string Activity { get; set; }

        public double? Accessibility { get; set; }
        
        public string Type { get; set; }

        public int Participants { get; set; }
        
        public double? Price { get; set; }
        
        public string Link { get; set; } //todo make nullable
        
        public string Key { get; set; } //todo or int
    }
}

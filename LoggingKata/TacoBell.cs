using System;
using System.Collections.Generic;
using System.Text;

namespace LoggingKata
{
    internal class TacoBell : ITrackable
    {
        //TacoBell class implements the Itrackable interface with Name & Location properties
        public string Name { get; set; }
        //Point is a struct that represents a GPS Location with Latitude and Longitude properties
        public Point Location { get; set; }

        //This Constructor can be used to create an instance of a TacoBell by passing values for each of the parameters
        //in the Constructor's signature
        public TacoBell(double latitude, double longitude, string name)
        {
            Location = new Point() { Latitude = latitude, Longitude = longitude };
            Name = name;
        }
    }
}

using System;
using System.Linq;
using System.IO;
using GeoCoordinatePortable;

namespace LoggingKata
{
    class Program
    {
        static readonly ILog logger = new TacoLogger();

        //csv file location - file is stored internally
        const string csvPath = "TacoBell-US-AL.csv";

        static void Main(string[] args)
        {
            //grab all lines of data from the csv file
            var lines = File.ReadAllLines(csvPath);
            #region log data
            if (lines.Length == 0) /*Log error if lines returns 0*/
                logger.LogError(csvPath);
            if (lines.Length == 1) /*Log a warning if lines returns 1*/
                logger.LogWarning(csvPath);


            logger.LogInfo($"FROM Lines: {lines[0]} TO {lines[236]}");
            #endregion
            //Instantiate the TacoParser class as parser
            var parser = new TacoParser();
            //Creates and array of TacoBell objects and uses Linq Select to parse each line in lines
            var locations = lines.Select(parser.Parse).ToArray();
            //Shells to be used later to hold the values of the two furthest apart TacoBell objects and distance
            ITrackable tacoBellA = null;
            ITrackable tacoBellB = null;
            double distance = 0;
            // foreach loop goes through each item in locations and assigns it to variable locA
            foreach (var locA in locations)
            {
                //corA serves as the GeoCoordinate that stores the longitude and latitude values of locA
                var corA = new GeoCoordinate();
                corA.Latitude = locA.Location.Latitude;
                corA.Longitude = locA.Location.Longitude;
                // for loop cycles each index of the locations list starting from the end and working backwards
                // and compares the distance between locA's coordinates and locB's coordinates
                // finally the distance is stored in the variable queryDistance
                for (int i = locations.Length - 1; i >= 0; i--)
                {
                    var locB = locations[i];
                    var corB = new GeoCoordinate();
                    corB.Latitude = locB.Location.Latitude;
                    corB.Longitude = locB.Location.Longitude;
                    //GetDistanceTo is a method of the GeoCoordinate class that is used to measure distance between two points in meters
                    var queryDistance = corA.GetDistanceTo(corB);
                    // queryDistance is compared to distance if queryDistance is greater than distance than each tacobell and distance between is updated
                    if (queryDistance > distance)
                    {
                        tacoBellA = locA;
                        tacoBellB = locB;
                        queryDistance = corA.GetDistanceTo(corB);
                        distance = queryDistance;
                    }
                }
            }
            Console.WriteLine();
            Console.WriteLine($"The two Taco Bells which are furthest away are:\n{tacoBellA.Name} and \n{tacoBellB.Name}. " +
                $"\nThe distance between them is {distance} meters");
        }
    }
}


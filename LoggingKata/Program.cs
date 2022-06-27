using System;
using System.Linq;
using System.IO;
using GeoCoordinatePortable;

namespace LoggingKata
{
    class Program
    {
        static readonly ILog logger = new TacoLogger();
        const string csvPath = "TacoBell-US-AL.csv";

        static void Main(string[] args)
        {

            logger.LogInfo("Log initialized");

            var lines = File.ReadAllLines(csvPath); /*<----------- Grab all lines from csvPath (list of Taco Bells in CSV)*/
            if (lines.Length == 0) /*Log error if lines returns 0*/
                logger.LogError(csvPath);
            if (lines.Length == 1) /*Log a warning if lines returns 1*/
                logger.LogWarning(csvPath);

            logger.LogInfo($"FROM Lines: {lines[0]} TO {lines[236]}");

            var parser = new TacoParser();
            // locations results in an ITrackable array of string arrays by taking lines and splitting each row into 3 parts of an array
            // and applies latitude to index 0 and longitude to index 1
            var locations = lines.Select(parser.Parse).ToArray();

            //Shells to be used later to hold the values of the two TacoBell objects which are furthest apart
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

                    var queryDistance = corA.GetDistanceTo(corB);

                    // queryDistance is compared to distance (which starts with a value of 0)
                    // if queryDistance is greater than distance both locA and locB are stored as tacoBellA and tacoBellB, respectively
                    // lastly distance is updated to equal queryDistance and the for loop repeats itself until all options are matched
                    // at which point the foreach loop repeats itself until all properties have been tried in locA
                    if (queryDistance > distance)
                    {
                        tacoBellA = locA;
                        tacoBellB = locB;
                        queryDistance = corA.GetDistanceTo(corB);
                        distance = queryDistance;

                    }
                }
            }
            Console.WriteLine($"The two Taco Bells which are furthest away are {tacoBellA.Name} and {tacoBellB.Name}. The distance between them is {distance}");
        }
    }
}


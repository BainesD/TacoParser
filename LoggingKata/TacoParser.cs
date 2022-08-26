namespace LoggingKata
{
    /// <summary>
    /// Parses a POI file to locate all the Taco Bells
    /// </summary>
    public class TacoParser
    {
        readonly ILog logger = new TacoLogger();

        public ITrackable Parse(string line)
        {

            //reads through csv and creates a string array with a new index at each comma
            var cells = line.Split(',');

            //checks to make sure the string array produced by a line has 3 values
            if (cells.Length < 3)
            {
                // Log that and return null
                logger.LogError(line);
                // Do not fail if one record parsing fails, return null
                return null;
            }

            // grab the latitude from the array at index 0
            double latitude = double.Parse(cells[0]);
            // grab the longitude from the array at index 1
            double longitude = double.Parse(cells[1]);
            // grab the name from the array at index 2
            string tacoBellName = cells[2];

            TacoBell tacoBell = new TacoBell(latitude, longitude, tacoBellName);
            return tacoBell;

        }
    }
}
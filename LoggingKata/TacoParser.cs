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
            logger.LogInfo("Begin parsing");

            // Take your line and use line.Split(',') to split it up into an array of strings, separated by the char ','
            var cells = line.Split(',');

            // If your array.Length is less than 3, something went wrong
            if (cells.Length < 3)
            {
                // Log that and return null
                logger.LogError(line);
                // Do not fail if one record parsing fails, return null
                return null;
            }

            // grab the latitude from your array at index 0
            double latitude = double.Parse(cells[0]);
            // grab the longitude from your array at index 1
            double longitude = double.Parse(cells[1]);
            // grab the name from your array at index 2
            string tacoBellName = cells[2];

            TacoBell tacoBell = new TacoBell() { Location = new Point() { Latitude = latitude, Longitude = longitude }, Name = tacoBellName };
            return tacoBell;

        }
    }
}
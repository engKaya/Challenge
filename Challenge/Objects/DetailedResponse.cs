using System.Collections.Generic;

namespace Challenge.Objects
{
    public class DetailedResponse
    {
        public DetailedResponse()
        {
            this.Players = new List<Players>();
            this.GameActions = new List<GameActions>();
        }
        public Players Winner { get; set; }
        public List<Players> Players { get; set; }
        public List<GameActions> GameActions { get; set; }
    }
}

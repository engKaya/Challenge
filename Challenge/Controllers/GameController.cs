using Challenge.BL;
using Challenge.Objects;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Challenge.Controllers
{
    [Route("Game/[Action]")]
    [ApiController]
    public class GameController : ControllerBase
    {
        [HttpGet]
        public int CreateGame(int PlayerCount, bool RandomName = false)
        {
            if ( PlayerCount <2)
                throw new ArgumentException("Player count must be at least 2");

            var detailedResponse = CircleGame.StarGame(PlayerCount, RandomName);
            return detailedResponse.Winner.QueueNumber;
        }

        [HttpGet]
        public DetailedResponse CreateGameWithDetailedResponse(int PlayerCount, bool RandomName = false)
        {
            if (PlayerCount < 2)
                throw new ArgumentException("Player count must be at least 2");

            var detailedResponse = CircleGame.StarGame(PlayerCount, RandomName);
            return detailedResponse;
        }

    }
}

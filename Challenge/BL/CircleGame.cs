using System.Collections.Generic;
using System.Linq;
using Challenge.Objects;

namespace Challenge.BL
{
    public static class CircleGame
    {
        public static DetailedResponse StarGame(int PlayerCount, bool randomName)
        {
            var detailedResponse = new DetailedResponse();
            var playerList = CircleGame.CreatePlayers(PlayerCount, randomName);
            CircleGame.StartEliminatePlayers(playerList, detailedResponse);
            var playerToLeft = playerList.Where(x => !x.BoEliminated).FirstOrDefault();
            detailedResponse.Winner = playerToLeft;
            detailedResponse.Players = playerList;
            return detailedResponse;
        } 
        public static List<Players> CreatePlayers(int PlayerCount, bool randomName)
        {
            var playerList = new List<Players>();
            for (int i = 0; i < PlayerCount; i++)
            {
                playerList.Add(new Players(i + 1, randomName));
            }

            return playerList;
        }

        public static List<Players> StartEliminatePlayers(List<Players> players, DetailedResponse detailedResp)
        {
            var index = 1;
            while (players.Where(x=>x.BoEliminated == false).ToList().Count > 1)
            {
                index = EliminatePlayerAndPassTheBall(players, index, detailedResp);
            }

            return players;
        }

        public static int EliminatePlayerAndPassTheBall(List<Players> players, int QueNumber, DetailedResponse detailedResponse)
        {
            var playerWithBall = players.Where(x => x.QueueNumber == QueNumber).LastOrDefault();
            var playerToEliminate = players.Where(x => !x.BoEliminated  && x.QueueNumber < playerWithBall.QueueNumber).LastOrDefault();
            if (playerToEliminate == null)
                playerToEliminate = players.Where(x => x.QueueNumber <= players.Count && !x.BoEliminated).LastOrDefault();

            playerToEliminate.EliminatePlayer();
            var playerToPass = players.Where(x => !x.BoEliminated && x.QueueNumber < playerToEliminate.QueueNumber).LastOrDefault();

            if (playerToPass == null)
                playerToPass = players.Where(x => x.QueueNumber <= players.Count && !x.BoEliminated).LastOrDefault();

            var action = new GameActions();
            action.StartedPlayer = playerWithBall.QueueNumber;
            action.PassedPlayer = playerToPass.QueueNumber;
            action.EliminatedPlayer = playerToEliminate.QueueNumber;
            detailedResponse.GameActions.Add(action);
            return playerToPass.QueueNumber;
        }
    }
}

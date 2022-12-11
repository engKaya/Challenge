using Newtonsoft.Json;
using System.IO;
using System.Net;
using System.Net.NetworkInformation;
using System.Text.Json.Serialization;

namespace Challenge.Objects
{
    public class Players
    {
        public string Name { get; set; }
        public int QueueNumber { get; }
        public bool BoEliminated { get; set; }
        public Players(int queueNumber, bool randomName)
        {
            QueueNumber = queueNumber;
            Name = randomName ? GetRandomName() : "Name " + queueNumber.ToString();
            BoEliminated = false;
        }

        public void EliminatePlayer()
        {
            this.BoEliminated = true;
        }

        private string GetRandomName()
        {
            try
            {
                var url = "https://randomuser.me/api/";
                var request = WebRequest.Create(url);
                request.Method = "GET";
                var webResponse = request.GetResponse();
                var webStream = webResponse.GetResponseStream();

                using var reader = new StreamReader(webStream);
                var data = reader.ReadToEnd();

                RandomPerson person = JsonConvert.DeserializeObject<RandomPerson>(data);
                return person.results[0].name.first + " " + person.results[0].name.last;

            }
            catch (System.Exception ex)
            {
                return "Name";
            }
        }
    }
}

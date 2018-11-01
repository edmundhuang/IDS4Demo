using IdentityModel.Client;
using System;
using System.Threading.Tasks;

namespace ConsoleClient
{
    class Program
    {
        public static void Main(string[] args)
        {
            MainAsync().GetAwaiter().GetResult();
            Console.ReadLine();
        }

        private static async Task MainAsync()
        {
            // discover endpoints from the metadata by calling Auth server hosted on 5000 port
            var discoveryClient = await DiscoveryClient.GetAsync("http://localhost:5000");
            if (discoveryClient.IsError)
            {
                Console.WriteLine(discoveryClient.Error);
                //Console.ReadLine();
                return;
            }

            // request the token from the Auth server
            var tokenClient = new TokenClient(discoveryClient.TokenEndpoint, "client", "secret");
            var response = await tokenClient.RequestClientCredentialsAsync("api1");

            if (response.IsError)
            {
                Console.WriteLine(response.Error);
                //Console.ReadLine();
                return;
            }

            Console.WriteLine(response.Json);
            //Console.ReadLine();

        }
    }
}

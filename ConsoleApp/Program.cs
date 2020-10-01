using System;
using System.Configuration;
using System.Net.Http;

namespace ConsoleApp
{
    class Program
    {
        static async System.Threading.Tasks.Task Main(string[] args)
        {
            try
            {
                string url = ConfigurationManager.AppSettings["endpoint"];

                using (HttpClient client = new HttpClient())
                {
                    HttpResponseMessage response = await client.GetAsync(url);
                    string content = await response.Content.ReadAsStringAsync();

                    Console.WriteLine(content);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Request error. Details:  " + e.Message);
            }
        }
    }
}

using System;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Spectre.Console;

namespace CurrentTimeExample
{
    class Program
    {
        static async Task Main(string[] args)
        {
            AnsiConsole.Write(new FigletText("Time Zone").LeftJustified().Color(Color.DarkOrange));

            try
            {
                // Create an instance of the HttpClient class
                var httpClient = new HttpClient();
            Loop:
                System.Console.Write("\nEnter Continent: ");
                var continent = Console.ReadLine();
                System.Console.Write("\nEnter Zone: ");
                var zone = Console.ReadLine();

                // Define the URLs of the time zone API for the desired countries
                var zoneUrl = $"https://timeapi.io/api/Time/current/zone?timeZone={continent}/{zone}";

                // Send a GET request to the time zone API for each country and retrieve the response
                var zoneResponse = await httpClient.GetAsync(zoneUrl);

                // Ensures the success response code from API
                zoneResponse.EnsureSuccessStatusCode();

                // Parse the response to retrieve the current time zone details of each country
                var zoneHour = JsonConvert.DeserializeObject<TimeZoneApiResponse>(await zoneResponse.Content.ReadAsStringAsync()).Hour;
                var zoneMinute = JsonConvert.DeserializeObject<TimeZoneApiResponse>(await zoneResponse.Content.ReadAsStringAsync()).Minute;
                var zoneSecond = JsonConvert.DeserializeObject<TimeZoneApiResponse>(await zoneResponse.Content.ReadAsStringAsync()).Seconds;
                var zoneDay = JsonConvert.DeserializeObject<TimeZoneApiResponse>(await zoneResponse.Content.ReadAsStringAsync()).Day;
                var zoneMonth = JsonConvert.DeserializeObject<TimeZoneApiResponse>(await zoneResponse.Content.ReadAsStringAsync()).Month;
                var zoneYear = JsonConvert.DeserializeObject<TimeZoneApiResponse>(await zoneResponse.Content.ReadAsStringAsync()).Year;
                var zoneDayOfWeek = JsonConvert.DeserializeObject<TimeZoneApiResponse>(await zoneResponse.Content.ReadAsStringAsync()).DayOfWeek;

                // Print the current time of each country
                System.Console.WriteLine("\n~~~~~~~~~~~~~~~~~~~~");
                System.Console.WriteLine($"Current Zone Details:");
                System.Console.WriteLine("~~~~~~~~~~~~~~~~~~~~~\n");
                Console.WriteLine($"Hour   : {zoneHour}\n");
                Console.WriteLine($"Minute : {zoneMinute}\n");
                Console.WriteLine($"Second : {zoneSecond}\n");
                Console.WriteLine($"Day    : {zoneDayOfWeek}\n");
                Console.WriteLine($"Date (DD/MM/YYYY): {zoneDay}/{zoneMonth}/{zoneYear}\n");


                Console.WriteLine("Do you Wish to Continue? Press Enter!");
                var getKeyBoardInput = Console.ReadKey();
                if (getKeyBoardInput.Key == ConsoleKey.Enter)
                {
                    goto Loop;
                }

                AnsiConsole.Write(new FigletText("Thank You!").LeftJustified().Color(Color.DarkOrange));
            }
            catch (Exception)
            {
                AnsiConsole.Write(new FigletText("Oops!!!").LeftJustified().Color(Color.DarkOrange));
                AnsiConsole.Write(new FigletText("Try Again Later...").LeftJustified().Color(Color.DarkOrange));
            }
        }
    }

}



// America/New_York
// Europe/London
// Asia/Kolkata
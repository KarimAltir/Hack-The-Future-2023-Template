using Common;
using System.Net.Http.Json;

internal class Program
{

    public static int BereikenDeWeekdag(MayanCalendarChallengeDto mayanCalendarChallengeDto)
    {
        // vind de eerste dag tussen de datums
        var firstDay = mayanCalendarChallengeDto.StartDate;
        while (firstDay.DayOfWeek != (DayOfWeek)Enum.Parse(typeof(DayOfWeek), mayanCalendarChallengeDto.Day))
        {
            firstDay = firstDay.AddDays(1);
        }


        // vind de laatste dag tussen de datums
        var lastDay = mayanCalendarChallengeDto.EndDate;
        while (lastDay.DayOfWeek != (DayOfWeek)Enum.Parse(typeof(DayOfWeek), mayanCalendarChallengeDto.Day))
        {
            lastDay = lastDay.AddDays(-1);
        }

        // bereken het verschil in dagen en deel door 7
        var difference = (lastDay - firstDay).TotalDays;
        var count = difference / 7 + 1; // tel de eerste dag mee

        return (int)count;
    }

    private static async Task Main(string[] args)
    {
        HackTheFutureClient client = new HackTheFutureClient();

        await client.Login("The Lawbenders", "CZ2zmwJ5Gv");

        var start = await client.GetAsync("/api/path/b/easy/start");

        var simpel = await client.GetFromJsonAsync<MayanCalendarChallengeDto>("/api/path/b/easy/puzzle");

        var puzzle = await client.PostAsJsonAsync("/api/path/b/easy/puzzle", BereikenDeWeekdag(simpel));

        //var result = await client.PostAsJsonAsync("/api/path/b/easy/sample", BereikenDeWeekdag(simpel));

        Console.WriteLine(await puzzle.Content.ReadAsStringAsync());



        //if (simpel.IsSuccessStatusCode)
        //{
        //    var start = await simpel.Content.ReadAsStringAsync();

        //    for (int i = 0; i < start.Length; i++)
        //    {
        //        var translated = Translate(start);
        //        Console.WriteLine($"The translated string is {translated}");
        //    }

        //}
        //else
        //{
        //    Console.WriteLine("Something went wrong while retrieving the animals");
        //}
    }

    //public static string Translate(string hieroglyphs)
    //{
    //    var result = "";
    //    foreach (var c in hieroglyphs)
    //    {
    //        if (HieroglyphAlphabet.Characters.ContainsKey(c))
    //        {
    //            result += HieroglyphAlphabet.Characters[c];
    //        }
    //        else
    //        {
    //            result += c; // als het karakter geen hiëroglief is, laat het dan onveranderd
    //        }
    //    }
    //    return result;
    //}
}
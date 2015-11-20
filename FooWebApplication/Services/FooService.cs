using Story.Core;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace FooWebApplication.Services
{
    public class FooService
    {
        private static readonly string[] Animals = { "Bear", "Lion", "Tiger", "Parrot", "Dog", "Cat", "Rabbit", "Dolphin", "Whale", "Giraffe", "Zebra" };
        private static readonly string[] Adjectives = { "Big", "Tired", "Fearsome", "Giant", "Small", "Smelly", "Cute", "Good", "Aging", "Fancy", "White" };

        private readonly Random _random = new Random();

        public async Task<string> GetRandomName()
        {
            Storytelling.Info("Getting random name");
            await Task.Delay(10);
            return GetRandomItem(Adjectives) + " " + GetRandomItem(Animals);
        }

        public async Task<string> GetRandomString()
        {
            Storytelling.Info("Getting random string");

            await Task.Delay(10);

            return Guid.NewGuid().ToString();
        }

        private string GetRandomItem(string[] items)
        {
            int index = _random.Next(items.Length);
            return items[index];
        }
    }
}

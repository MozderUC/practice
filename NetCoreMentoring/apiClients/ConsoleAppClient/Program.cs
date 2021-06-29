using System;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;

namespace ConsoleAppClient
{
    class Program
    {
        static readonly HttpClient _client = new HttpClient();

        static void Main(string[] args)
        {
            RunAsync().GetAwaiter().GetResult();
        }

        static async Task RunAsync()
        {
            _client.BaseAddress = new Uri("https://localhost:44380");

            // Read categories and write to file
            var categoriesResponse = await _client.GetAsync("/api/v1.0/products");
            var categories = await categoriesResponse.Content.ReadAsStreamAsync();

            var categoriesFileStream = File.Create(".\\categories.json");
            await categories.CopyToAsync(categoriesFileStream);
            categoriesFileStream.Close();

            // Read products and write to file
            var productsResponse = await _client.GetAsync("/api/v1.0/products");
            var products = await productsResponse.Content.ReadAsStreamAsync();

            var productsFileStream = File.Create(".\\products.json");
            await products.CopyToAsync(productsFileStream);
            productsFileStream.Close();
        }
    }
}

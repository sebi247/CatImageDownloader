using System;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;

namespace CatImageDownloader
{
    class Program
    {
        static async Task Main(string[] args)
        {
            string? outputPath = null;
            string? overlayText = null;

            // Parse command line arguments
            for (int i = 0; i < args.Length; i++)
            {
                if (args[i] == "-o" && i + 1 < args.Length)
                {
                    outputPath = args[i + 1];
                }
                if (args[i] == "-t" && i + 1 < args.Length)
                {
                    overlayText = args[i + 1];
                }
            }

            if (string.IsNullOrEmpty(outputPath))
            {
                Console.WriteLine("Output file path is required. Use -o <output_filepath>");
                return;
            }

            try
            {
                await DownloadCatImageAsync(outputPath, overlayText ?? string.Empty);
                Console.WriteLine($"Cat image saved to {outputPath}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }

        static async Task DownloadCatImageAsync(string outputPath, string text)
        {
            string url = "https://cataas.com/cat";
            if (!string.IsNullOrEmpty(text))
            {
                url += $"/says/{Uri.EscapeDataString(text)}";
            }

            using HttpClient client = new HttpClient();
            byte[] imageBytes = await client.GetByteArrayAsync(url);

            await File.WriteAllBytesAsync(outputPath, imageBytes);
        }
    }
}

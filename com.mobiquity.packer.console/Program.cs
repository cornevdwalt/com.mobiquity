using com.mobiquity.packer.api;
using Microsoft.Extensions.DependencyInjection;

int row = 0;
string results;


// Setup packer dependency injection
//
var serviceProvider = new ServiceCollection()
    .AddSingleton<IPacker, Packer>()
    .BuildServiceProvider();

var packer = serviceProvider.GetService<IPacker>();

do
{
    if (row == 0 || row >= 25) ResetConsole();

    string? input = Console.ReadLine();
    if (string.IsNullOrEmpty(input)) break;

    results = packer.Pack(input);                   // Pass input to the Packer to proces
    //results = MockPacker.Pack(input);             // Mock for testing directly (static)

    Console.WriteLine("---------------------------------------------------");
    Console.WriteLine(results);
    Console.WriteLine();

    row += 4;

} while (true);

return;

// Declare a ResetConsole local method
void ResetConsole()
{
    if (row > 0)
    {
        Console.WriteLine("Press any key to continue...");
        Console.ReadKey();
    }
    Console.Clear();
    Console.WriteLine($"{Environment.NewLine}Press <Enter> only to exit; otherwise, enter the path to the data file and press <Enter>:{Environment.NewLine}");
    row = 3;
}


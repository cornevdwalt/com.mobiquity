using com.mobiquity.packer.Packer;
using Microsoft.Extensions.DependencyInjection;

/// <summary>
/// TEST DATA FILE PATH : c:\temp\example_input.txt
/// </summary>

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


    try
    {
        results = packer.pack(input);                   // Pass input to the Packer to proces
                                                        //results = MockPacker.Pack(input);             // Mock for testing directly (static)

        Console.WriteLine("---------------------------------------------------");
        Console.WriteLine(results);
        Console.WriteLine();
    }
    catch (Exception ex)
    {
        Console.WriteLine("---------------------------------------------------");
        Console.WriteLine(ex.Message);                  // Show exception message to user
    }
    finally { }

 
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


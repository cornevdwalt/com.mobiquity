﻿using com.mobiquity.packer.api;

int row = 0;
string results;

do
{
    if (row == 0 || row >= 25)
        ResetConsole();

    string? input = Console.ReadLine();
    if (string.IsNullOrEmpty(input)) break;

    results = MockPacker.Pack(input);

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
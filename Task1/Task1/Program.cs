using Task1.ControlClasses;

namespace Task1;

public static class Program
{
    public static void Main()
    {
        try
        {
            CommandHandler.Start(@"S:\data");
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
        }
    }
}
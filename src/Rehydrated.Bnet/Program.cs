namespace Rehydrated.Bnet
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.Title = "Rehydrated.Bnet";
            BnetServerApp app = new();
            app.Run();
        }
    }
}

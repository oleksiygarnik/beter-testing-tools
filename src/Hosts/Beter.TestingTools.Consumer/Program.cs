using Beter.TestingTools.Hosting;

namespace Beter.TestingTools.Consumer;

public class Program
{
    public static void Main(string[] args)
    {
        HostStarter.Start<Startup>(args, "testing-tools", "consumer");
    }
}



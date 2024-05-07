using Beter.TestingTools.Hosting;

namespace Beter.TestingTools.Generator;

public class Program
{
    public static void Main(string[] args)
    {
        HostStarter.Start<Startup>(args, "testing-tools", "generator");
    }
}



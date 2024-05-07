using Beter.TestingTools.Generator;
using Beter.TestingTools.Hosting;

namespace Beter.B2B.Generator;

public class Program
{
    public static void Main(string[] args)
    {
        HostStarter.Start<Startup>(args, "testing-tools", "generator");
    }
}



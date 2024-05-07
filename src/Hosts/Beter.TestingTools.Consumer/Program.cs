using Beter.TestingTools.Consumer;
using Beter.TestingTools.Hosting;

namespace Beter.B2B.Consumer;

public class Program
{
    public static void Main(string[] args)
    {
        HostStarter.Start<Startup>(args, "testing-tools", "consumer");
    }
}



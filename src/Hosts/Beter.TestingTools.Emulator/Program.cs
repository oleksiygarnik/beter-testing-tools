using Beter.TestingTools.Emulator;
using Beter.TestingTools.Hosting;

namespace Beter.TestingTools.Emulator;

public class Program
{
    public static void Main(string[] args)
    {
        HostStarter.Start<Startup>(args, "testing-tools", "emulator");
    }
}



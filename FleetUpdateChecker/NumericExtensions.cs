using System.Diagnostics;

namespace FleetUpdateChecker;

public static class NumericExtensions
{
    public static double ToMillisecondDuration(this long startTime)
    {
        return (Stopwatch.GetTimestamp() - startTime) * 1000 / (double) Stopwatch.Frequency;
    }
}
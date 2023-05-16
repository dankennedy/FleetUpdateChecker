namespace FleetUpdateChecker;

public record BoundingBox(float minY, float minX, float maxY, float maxX);

public class LocationMethods
{
    private static readonly BoundingBox GreaterLondon = new(-0.489f, 51.28f, 0.236f, 51.686f);
    private static readonly Random rnd = new();
    
    public static (float lat, float lng) GetRandomLocation()
    {
        var lat = (float)(rnd.NextDouble() * 180 - 90) % (GreaterLondon.maxX - GreaterLondon.minX) + GreaterLondon.minX;
        var lng = (float)(rnd.NextDouble() * 360 - 180) % (GreaterLondon.maxY - GreaterLondon.minY) + GreaterLondon.minY;
        return (lat, lng);
    }
    
    
    public static double GetDistance(float longitude, float latitude, float otherLongitude, float otherLatitude)
    {
        var d1 = latitude * (Math.PI / 180.0);
        var num1 = longitude * (Math.PI / 180.0);
        var d2 = otherLatitude * (Math.PI / 180.0);
        var num2 = otherLongitude * (Math.PI / 180.0) - num1;
        var d3 = Math.Pow(Math.Sin((d2 - d1) / 2.0), 2.0) +
                 Math.Cos(d1) * Math.Cos(d2) * Math.Pow(Math.Sin(num2 / 2.0), 2.0);

        return 6376500.0 * (2.0 * Math.Atan2(Math.Sqrt(d3), Math.Sqrt(1.0 - d3)));
    }
}
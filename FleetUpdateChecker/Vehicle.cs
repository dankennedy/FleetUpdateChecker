namespace FleetUpdateChecker;

public class Vehicle
{
    public Vehicle(string callSign)
    {
        CallSign = callSign;
    }

    public Guid Id { get; set; } = Guid.NewGuid();
    public string CallSign { get; set; }
    public float? Lat { get; set; }
    public float? Lng { get; set; }
}
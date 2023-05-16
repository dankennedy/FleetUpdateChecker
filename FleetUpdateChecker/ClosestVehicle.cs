namespace FleetUpdateChecker;

public class ClosestVehicle
{
    public ClosestVehicle(Vehicle vehicle, double distance)
    {
        Vehicle = vehicle;
        Distance = distance;
    }

    public Vehicle Vehicle { get; }
    public double Distance { get; }
}
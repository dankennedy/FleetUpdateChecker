namespace FleetUpdateChecker;

public class Fleet
{
    public Fleet(string name, int size = 2_000)
    {
        Name = name;
        for (var i = 0; i < size; i++)
        {
            var vehicle = new Vehicle($"Vehicle {i}");
            var (lat, lng) = LocationMethods.GetRandomLocation();
            vehicle.Lat = lat;
            vehicle.Lng = lng;
            Vehicles.Add(vehicle);
        }
    }

    public Guid Id { get; set; } = Guid.NewGuid();
    public string Name { get; set; }
    public List<Vehicle> Vehicles { get; set; } = new();

    public virtual void UpdateVehicleLocation(Guid id, float lat, float lng)
    {
        Vehicle? vehicle = null;
        for (var i = 0; i < Vehicles.Count; i++)
        {
            var x = Vehicles[i];
            if (x.Id == id)
            {
                vehicle = x;
                break;
            }
        }

        if (vehicle == null)
            return;
        
        vehicle.Lat = lat;
        vehicle.Lng = lng;
    }

    public virtual ClosestVehicle? GetClosest(float lat, float lng)
    {
        ClosestVehicle? result = null;
        for (var i = 0; i < Vehicles.Count; i++)
        {
            var vehicle = Vehicles[i];
            if (vehicle.Lat == null || vehicle.Lng == null) continue;
            var distance = LocationMethods.GetDistance(lat, lng, vehicle.Lat.Value, vehicle.Lng.Value);
            if (result == null || distance < result.Distance)
                result = new ClosestVehicle(vehicle, distance);
        }

        return result;
    }
    
}
using KdTree.Math;
using KdTree;
namespace FleetUpdateChecker;

public class TreeFleet : Fleet
{
    private KdTree<float, Vehicle> _tree = new(2, new GeoMath());
    
    public TreeFleet(string name, int size = 2000) : base(name, size)
    {
        BuildTree();
    }

    public override void UpdateVehicleLocation(Guid id, float lat, float lng)
    {
        base.UpdateVehicleLocation(id, lat, lng);
        BuildTree();
    }

    public override ClosestVehicle? GetClosest(float lat, float lng)
    {
        var closest = _tree.GetNearestNeighbours(new[] { lat, lng }, 1);
        return closest.Length == 0
            ? null
            : new ClosestVehicle(closest[0].Value,
                LocationMethods.GetDistance(closest[0].Point[0], closest[0].Point[1], lat, lng));
    }

    private void BuildTree()
    {
        var tree = new KdTree<float, Vehicle>(2, new GeoMath());
        for (var i = 0; i < Vehicles.Count; i++)
        {
            var vehicle = Vehicles[i];
            if (vehicle.Lat == null || vehicle.Lng == null) continue;
            tree.Add(new[] { vehicle.Lat.Value, vehicle.Lng.Value }, vehicle);
        }

        _tree = tree;
    }
}
using System.Diagnostics;

namespace FleetUpdateChecker;

public class Program
{
    private static readonly Random rnd = new();
    private static readonly Dictionary<string, int> _updates = new();
    private static void Main()
    {
        Run();
    }

    public static void Run()
    {
        var start = Stopwatch.GetTimestamp();
        var f = new Fleet("Fleet 1");
        Console.WriteLine($"Initialised {f.Name}. {start.ToMillisecondDuration()}ms");

        //start = Stopwatch.GetTimestamp();
        //var tf = new TreeFleet("Fleet 2");
        //Console.WriteLine($"Initialised {tf.Name}. {start.ToMillisecondDuration()}ms");
        
        _updates.Add(f.Name, 0);
        //_updates.Add(tf.Name, 0);
        
        Task.Run(() => MoveVehicles(f));
        //Task.Run(() => MoveVehicles(tf));

        Task.Run(() => FindNearest(f));
        //Task.Run(() => FindNearest(tf));

        Console.ReadLine();
    }
    
    private static async Task MoveVehicles(Fleet fleet)
    {
        while (true)
        {
            var index = rnd.Next(0, fleet.Vehicles.Count);
            var vehicle = fleet.Vehicles[index];
            var (lat, lng) = LocationMethods.GetRandomLocation();
            var start = Stopwatch.GetTimestamp();
            fleet.UpdateVehicleLocation(vehicle.Id, lat, lng);
            _updates[fleet.Name]++;
            if (_updates[fleet.Name] % 1000 == 0)
                Console.WriteLine($"Updated location {fleet.Name}. {_updates[fleet.Name]} times. {start.ToMillisecondDuration()}ms");
            
            await Task.Delay(rnd.Next(1, 10));
        }
    }
    
    private static async Task FindNearest(Fleet fleet)
    {
        while (true)
        {
            var (lat, lng) = LocationMethods.GetRandomLocation();
            var start = Stopwatch.GetTimestamp();
            var closest = fleet.GetClosest(lat, lng);
            Console.WriteLine($"Closest from {fleet.Name}. {closest?.Vehicle.CallSign ?? "Not Found"}, {closest?.Distance.ToString("F3") ?? ""} metres. {start.ToMillisecondDuration()}ms");
            await Task.Delay(rnd.Next(1000, 5000));
        }
    }

}


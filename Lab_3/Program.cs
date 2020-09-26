using System;
using System.Collections.Generic;

namespace Lab_3
{
    class Program
    {
        static void Main()
        {
            var groundVehicles = new List<Vehicle>
            {
                new BactrianCamel(),
                new FastCamel(),
                new Centaur(),
                new SpeedBoots()
            };
            
            var airVehicles = new List<Vehicle>
            {
                new Carpet(),
                new Mortar(),
                new Broom()
            };
            
            var allVehicles = new List<Vehicle>();
            allVehicles.AddRange(groundVehicles);
            allVehicles.AddRange(airVehicles);

            // Creating race
            var airRace = new RaceEngine(new AirRace(6000));
            
            var listGroundRaces = new List<RaceEngine>
            {
                new RaceEngine(new GroundRace(10)),
                new RaceEngine(new GroundRace(100)),
                new RaceEngine(new GroundRace(500)),
                new RaceEngine(new GroundRace(700)),
                new RaceEngine(new GroundRace(1000)),
                new RaceEngine(new GroundRace(5000)),
                new RaceEngine(new GroundRace(10000)),
            };
            
            var generalRace = new RaceEngine(new GeneralRace(10000));
            
            // Registering vehicles
            // Example: trying to add wrong vehicle type to race:
            try
            {
                airRace.RegisterVehicle(groundVehicles[0]);
            }
            catch (ArgumentException e)
            {
                Console.WriteLine("---------------------------------------------------------------");
                Console.WriteLine(e.Message);
                Console.WriteLine("---------------------------------------------------------------");
            }
            
            // Adding vehicles
            foreach (var vehicle in airVehicles)
            {
                airRace.RegisterVehicle(vehicle);                    
            }
            
            
            foreach (var groundRace in listGroundRaces)
            {
                foreach (var vehicle in groundVehicles)
                {
                    groundRace.RegisterVehicle(vehicle);                    
                }
            }

            foreach (var vehicle in allVehicles)
            {
                generalRace.RegisterVehicle(vehicle);
            }
            
            // Getting results
            Console.WriteLine($"Winner of airRace: {airRace.Run()}");

                Console.WriteLine("---------------------------------------------------------------");
            
            for (int i = 0; i < listGroundRaces.Count; i++)
            {
                Console.WriteLine($"Winner of groundRace #{i}: {listGroundRaces[i].Run()}");
            }
            
            Console.WriteLine("---------------------------------------------------------------");
            
            Console.WriteLine($"Winner of generalRace: {generalRace.Run()}");
        }
    }
}
using System;
using System.Collections.Generic;

namespace Lab_3
{
    public class Program
    {
        public static void Main()
        {
            var groundVehicles = new List<GroundVehicle>
            {
                new BactrianCamel(),
                new FastCamel(),
                new Centaur(),
                new SpeedBoots()
            };
            
            var airVehicles = new List<AirVehicle>
            {
                new Carpet(),
                new Mortar(),
                new Broom()
            };
            
            var allVehicles = new List<Vehicle>();
            allVehicles.AddRange(groundVehicles);
            allVehicles.AddRange(airVehicles);

            // Creating race
            var airRace = new AirRace(6000);
            
            var listGroundRaces = new List<GroundRace>
            {
                new GroundRace(10),
                new GroundRace(100),
                new GroundRace(500),
                new GroundRace(700),
                new GroundRace(1000),
                new GroundRace(5000),
                new GroundRace(10000),
            };
            
            var generalRace = new GeneralRace(10000);
            
            // Registering vehicles
            
            // Example: trying to add wrong vehicle type to race:
            // This won't compile:
            // airRace.RegisterVehicle(groundVehicles[0]);

            
            // Adding vehicles
            foreach (var vehicle in airVehicles)
            {
                airRace.RegisterVehicle(vehicle);                    
            }
            
            
            foreach (var groundRace in listGroundRaces)
            {
                foreach (GroundVehicle vehicle in groundVehicles)
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
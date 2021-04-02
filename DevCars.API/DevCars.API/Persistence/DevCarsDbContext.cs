using DevCars.API.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DevCars.API.Persistence
{
    public class DevCarsDbContext
    {
        public DevCarsDbContext()
        {
            Cars = new List<Car>
            {
                new Car(1, "d56a456dsa654d", "Honda", "Civic", 2018, 80000, "Preto", new DateTime(2021, 1, 1)),
                new Car(2, "SDADGAJGHDGJAH", "Chevrolet", "Tracker", 2019, 105000, "Prata", new DateTime(2021, 1, 1)),
                new Car(3, "12by21buy1by1d", "Toyota", "Corolla", 2020, 105000, "Azul metálico", new DateTime(2021, 3, 15)),
            };

            Customers = new List<Customer>
            {
                new Customer(1, "Victor", "1234567", new DateTime(1993, 12, 15)),
                new Customer(2, "Luis", "678901", new DateTime(1989, 3, 6)),
                new Customer(3, "Camilla", "654344", new DateTime(1995, 1, 12)),
            };
        }
        public List<Car> Cars { get; set; }
        public List<Customer> Customers { get; set; }
    }
}

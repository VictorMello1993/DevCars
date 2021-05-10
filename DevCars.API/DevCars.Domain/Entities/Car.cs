using DevCars.Domain.Exceptions;
using System;

namespace DevCars.API.Entities
{
    public class Car
    {
        public Car(string vinCode,
                   string brand,
                   string model,
                   int year,
                   decimal price,
                   string color,
                   DateTime productionDate)
        {
            VinCode = vinCode;
            Brand = brand;
            Model = model;
            Year = year;
            Price = price;
            Color = color;
            ProductionDate = productionDate;

            Status = CarStatusEnum.Available;
            RegisteredAt = DateTime.Now;
        }

        protected Car() { }

        public int Id { get; private set; }
        public string VinCode { get; private set; }
        public string Brand { get; private set; }
        public string Model { get; private set; }
        public int Year { get; private set; }
        public decimal Price { get; private set; }
        public string Color { get; private set; }
        public DateTime ProductionDate { get; private set; }

        public CarStatusEnum Status { get; private set; }
        public DateTime RegisteredAt { get; private set; }

        public void Update(string color, decimal price)
        {
            Color = color;
            Price = price;
        }

        public void SetAsSuspended()
        {
            switch (Status)
            {
                case CarStatusEnum.Available:
                    Status = CarStatusEnum.Suspended;
                    break;
                case CarStatusEnum.Sold:
                    throw new OnlyAvailableCarsCanBeSuspendedException();                    
                case CarStatusEnum.Suspended:
                    throw new CarAlreadySuspendedException();                
            }            
        }

        public void SetAsSold()
        {
            switch (Status)
            {
                case CarStatusEnum.Available:
                    Status = CarStatusEnum.Sold;
                    break;
                case CarStatusEnum.Sold:
                    throw new CarAlreadySoldException();
                case CarStatusEnum.Suspended:
                    throw new OnlyAvailableCarsCanBeSoldException();                
            }
        }
    }
}

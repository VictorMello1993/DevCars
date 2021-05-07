using System;

namespace DevCars.API.ViewModels
{
    public class CarDetailsViewModel
    {
        public CarDetailsViewModel(int id, string brand, string model, string color, int year, decimal price, DateTime productionDate, string vinCode)
        {
            Id = id;
            Brand = brand;
            Model = model;
            Color = color;
            Year = year;
            Price = price;
            ProductionDate = productionDate;
            VinCode = vinCode;
        }

        public int Id { get; set; }
        public string Brand { get; set; }
        public string Model { get; set; }
        public string Color { get; set; }
        public int Year { get; set; }
        public decimal Price { get; set; }
        public DateTime ProductionDate { get; set; }

        //Chassi do carro
        public string VinCode { get; set; }
    }
}

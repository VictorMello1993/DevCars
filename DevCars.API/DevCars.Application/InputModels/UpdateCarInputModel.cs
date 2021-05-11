namespace DevCars.Application.InputModels
{
    public class UpdateCarInputModel
    {
        public UpdateCarInputModel(string color, decimal price)
        {
            Color = color;
            Price = price;
        }

        public string Color { get; set; }
        public decimal Price { get; set; }
    }
}

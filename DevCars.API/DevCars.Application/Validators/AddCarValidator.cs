using DevCars.Application.Commands.AddCar;
using FluentValidation;
using System.Text.RegularExpressions;

namespace DevCars.Application.Validators
{
    public class AddCarValidator : AbstractValidator<AddCarCommand>
    {
        public AddCarValidator()
        {
            RuleFor(c => c.Model).NotEmpty().WithMessage("É preciso informar o modelo do carro.")
                                 .NotNull().WithMessage("É preciso informar o modelo do carro.")
                                 .MaximumLength(20).WithMessage("O modelo não pode passar de 20 caracteres.");

            RuleFor(c => c.Brand).NotEmpty().WithMessage("É preciso informar a marca do carro.")
                                 .NotNull().WithMessage("É preciso informar a marca do carro.")
                                 .MaximumLength(15).WithMessage("A marca não pode passar de 15 caracteres.");

            RuleFor(c => c.Year).NotEmpty().WithMessage("É preciso informar o ano do carro.")
                                .NotNull().WithMessage("É preciso informar o ano do carro.")
                                .Must(value => value >= 0).WithMessage("Ano do carro não pode ser negativo.");

            RuleFor(c => c.Year.ToString()).Must(y => y.Length == 4).WithMessage("O ano deve conter exatamente 4 dígitos.");

            RuleFor(c => c.VinCode).NotEmpty().WithMessage("É preciso informar a placa do carro.")
                                   .NotNull().WithMessage("É preciso informar a placa do carro.")
                                   .Must(ValidateVinCode).WithMessage("Placa inválida.");

            RuleFor(c => c.ProductionDate).NotEmpty().WithMessage("É preciso informar a data de produção do carro.")
                                          .NotNull().WithMessage("É preciso informar a data de produção do carro.");

            RuleFor(c => c.Price).NotEmpty().WithMessage("É preciso informar o preço unitário do carro.")
                                 .NotNull().WithMessage("É preciso informar o preço unitário do carro.")
                                 .Must(value => value >= 0).WithMessage("O valor unitário não pode ser negativo.");

            RuleFor(c => c.Color).NotEmpty().WithMessage("É preciso informar a cor do carro.")
                                 .NotNull().WithMessage("É preciso informar a cor do carro")
                                 .MaximumLength(15).WithMessage("A cor não pode passar de 15 caracteres");
        }        

        private bool ValidateVinCode(string vincode)
        {
            if (!string.IsNullOrWhiteSpace(vincode))
            {
                var regex = new Regex(@"[A-Z]{3}\d{4}");
                return regex.IsMatch(vincode);
            }

            return true;
        }
    }
}

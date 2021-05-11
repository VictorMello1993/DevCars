using DevCars.Application.InputModels;
using FluentValidation;

namespace DevCars.Application.Validators
{
    public class UpdateCarValidator : AbstractValidator<UpdateCarInputModel>
    {
        public UpdateCarValidator()
        {
            RuleFor(c => c.Color)
                .NotEmpty().WithMessage("É preciso informar a cor do carro.")
                .NotNull().WithMessage("É preciso informar a cor do carro.")
                .MaximumLength(15).WithMessage("A cor não pode passar de 15 caracteres.");

            RuleFor(c => c.Price).Must(value => value > 0).WithMessage("O valor unitário não pode ser zero nem negativo.");            
        }
    }
}

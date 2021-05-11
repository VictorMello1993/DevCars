using DevCars.Application.Commands.AddOrder;
using FluentValidation;
using System.Text.RegularExpressions;

namespace DevCars.Application.Validators
{
    public class AddOrderValidator : AbstractValidator<AddOrderCommand>
    {
        public AddOrderValidator()
        {
            RuleFor(o => o.ExtraItems).Custom((list, context) =>
            {
                if (list != null && list.Count > 0)
                {
                    var index = 0;

                    foreach (var item in list)
                    {
                        if (item.Description.Length > 50)
                        {
                            context.AddFailure(string.Format("Erro item {0}: A descrição não pode passar de 50 caracteres.", index + 1));
                        }

                        if (item.Price == default(decimal))
                        {
                            context.AddFailure(string.Format("Erro item {0}: É obrigatório informar o preço do acessório.", index + 1));
                        }
                        else if (item.Price < 0)
                        {
                            context.AddFailure(string.Format("Erro item {0}: O preço do acessório não pode ser negativo.", index + 1));
                        }

                        index++;
                    }
                }
            });

            RuleFor(o => o.IdCar).Must(value => value > 0).WithMessage("O id do carro deve ser positivo!");
            RuleFor(o => o.IdCostumer).Must(value => value > 0).WithMessage("O id do cliente deve ser positivo!");
        }
    }
}

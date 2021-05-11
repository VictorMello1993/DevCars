using DevCars.Application.Commands.AddCostumer;
using FluentValidation;

namespace DevCars.Application.Validators
{
    public class AddCostumerValidator : AbstractValidator<AddCostumerCommand>
    {
        public AddCostumerValidator()
        {
            RuleFor(cs => cs.FullName).MaximumLength(40)
                                      .NotEmpty()
                                      .WithMessage("É preciso informar o nome completo.")
                                      .NotNull()
                                      .WithMessage("É preciso informar o nome completo.")
                                      .WithMessage("O nome completo não pode passar de 40 caracteres.");

            RuleFor(cs => cs.Document).NotEmpty().WithMessage("É preciso informar o documento do cliente.")
                                      .NotNull().WithMessage("É preciso informar o documento do cliente.")
                                      .Must(ValidateDocument).WithMessage("O documento precisa ter exatamente 12 caracteres");

        }

        private bool ValidateDocument(string document)
        {
            if (!string.IsNullOrWhiteSpace(document))
            {
                return document.Length == 12;
            }

            return true;
        }
    }
}

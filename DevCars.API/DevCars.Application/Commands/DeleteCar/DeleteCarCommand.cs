using MediatR;

namespace DevCars.Application.Commands.DeleteCar
{
    public class DeleteCarCommand : IRequest<Unit>
    {
        public DeleteCarCommand(int id)
        {
            this.id = id;
        }

        public int id { get; set; }
    }
}

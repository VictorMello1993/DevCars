using DevCars.API.Entities;
using DevCars.API.InputModels;
using DevCars.API.Persistence;
using DevCars.API.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DevCars.API.Controllers
{
    [Route("api/costumers")]
    public class CustomersController : ControllerBase
    {
        private readonly DevCarsDbContext _dbContext;

        public CustomersController(DevCarsDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        //Cadastrando clientes
        [HttpPost]
        public IActionResult Post([FromBody] AddCostumerInputModel model)
        {
            var costumer = new Customer(4, model.FullName, model.Document, model.BirthDate);

            _dbContext.Customers.Add(costumer);

            return NoContent();
        }
        
        //Cadastrando pedidos
        [HttpPost("{id}/orders")]
        public IActionResult PostOrder(int id, [FromBody] AddOrderInputModel model)
        {
            var car = _dbContext.Cars.SingleOrDefault(c => c.Id == model.IdCar);
            var extraItems = model.ExtraItems.Select(e => new ExtraOrderItem(e.Description, e.Price)).ToList();
            var customer = _dbContext.Customers.SingleOrDefault(c => c.Id == model.IdCostumer);

            var order = new Order(1, model.IdCar, model.IdCostumer, car.Price, extraItems);

            customer.Purchase(order);

            return CreatedAtAction(nameof(GetOrder), new { id = customer.Id, orderid = order.Id }, model);
        }
        
        //Mostrando detalhes do pedido
        [HttpGet("{id}/orders/{orderid}")]
        public IActionResult GetOrder(int id, int orderid)
        {
            var customer = _dbContext.Customers.SingleOrDefault(c => c.Id == id);            
            var order = customer.Orders.SingleOrDefault(o => o.Id == orderid);            

            if (customer == null && order == null)
            {
                return NotFound();
            }

            var extraItems = order.ExtraItems.Select(e => e.Description).ToList();

            var orderViewModel = new OrderDetailsViewModel(order.IdCar, order.IdCostumer, order.TotalCost, extraItems);

            return Ok(orderViewModel);
        }
    }
}

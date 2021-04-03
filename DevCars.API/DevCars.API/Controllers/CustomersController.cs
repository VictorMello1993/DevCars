using DevCars.API.Entities;
using DevCars.API.InputModels;
using DevCars.API.Persistence;
using DevCars.API.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DevCars.API.Controllers
{
    [Route("api/customers")]
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
            var costumer = new Customer(model.FullName, model.Document, model.BirthDate);

            _dbContext.Customers.Add(costumer);
            _dbContext.SaveChanges();

            return NoContent();
        }
        
        //Cadastrando pedidos
        [HttpPost("{id}/orders")]
        public IActionResult PostOrder(int id, [FromBody] AddOrderInputModel model)
        {
            var car = _dbContext.Cars.SingleOrDefault(c => c.Id == model.IdCar);
            var extraItems = model.ExtraItems.Select(e => new ExtraOrderItem(e.Description, e.Price)).ToList();
            var order = new Order(model.IdCar, model.IdCostumer, car.Price, extraItems);

            //var customer = _dbContext.Customers.SingleOrDefault(c => c.Id == model.IdCostumer);
            //customer.Purchase(order);

            _dbContext.Orders.Add(order);
            _dbContext.SaveChanges();

            return CreatedAtAction(nameof(GetOrder), new { id = order.IdCostumer, orderid = order.Id }, model);
        }
        
        //Mostrando detalhes do pedido
        [HttpGet("{id}/orders/{orderid}")]
        public IActionResult GetOrder(int id, int orderid)
        {            
            var order = _dbContext.Orders.Include(o => o.ExtraItems).Include(c => c.Customer).SingleOrDefault(o => o.Id == orderid);            

            if (order == null)
            {
                return NotFound();
            }

            var extraItems = order.ExtraItems.Select(e => e.Description).ToList();

            var orderViewModel = new OrderDetailsViewModel(order.IdCar, order.IdCostumer, order.TotalCost, extraItems);

            return Ok(orderViewModel);
        }
    }
}

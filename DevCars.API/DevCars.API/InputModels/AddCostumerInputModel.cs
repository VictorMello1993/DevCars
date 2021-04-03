using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DevCars.API.InputModels
{
    public class AddCostumerInputModel
    {
        public AddCostumerInputModel(string fullName, string document, DateTime birthDate)
        {
            FullName = fullName;
            Document = document;
            BirthDate = birthDate;
        }

        public string FullName { get; set; }
        public string Document { get; set; }
        public DateTime BirthDate { get; set; }        
    }
}

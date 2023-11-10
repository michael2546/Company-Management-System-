using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.DAL.Models
{
    public class Employee : ModelBase
    {
        public string Name { get; set; }

        public int Age { get; set; }


        //^         -> start
        //[0-9]     ->between 0 and 9  
        //{1,3}     -> 1 to 3 digits
        //-
        //[a-zA-Z] -> from a to z cabital or small
        //{5,10}   -> 5 to 10 digits
        //$        ->end   
        //@        -> skip characters 

        public string Adress { get; set; }


        public decimal Salary { get; set; }

        public bool IsActive { get; set; }

        public string Email { get; set; }


        public string PhoneNumber { get; set; }

        public DateTime HireDate { get; set; }

        public bool IsDeleted { get; set; }

        public DateTime CreationDate { get; set; }

        public string ImageName { get; set; }

        public int? DepartmentId { get; set; } //Forign Key Column

        //[InverseProperty(nameof(Models.Department.Employees))]
        //Navigational Proberty => One
        public Department Department { get; set; }  
    }
}

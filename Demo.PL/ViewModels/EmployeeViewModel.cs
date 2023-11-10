using Demo.DAL.Models;
using System.ComponentModel.DataAnnotations;
using System;
using Microsoft.AspNetCore.Http;

namespace Demo.PL.ViewModels
{
    public class EmployeeViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Name Is Required")]
        [MaxLength(50, ErrorMessage = "Max Length of Name is 50 chars")]
        [MinLength(5, ErrorMessage = "Min Length of Name is 5 chars")]
        public string Name { get; set; }

        [Range(22, 30)]
        public int Age { get; set; }


        //^         -> start
        //[0-9]     ->between 0 and 9  
        //{1,3}     -> 1 to 3 digits
        //-
        //[a-zA-Z] -> from a to z cabital or small
        //{5,10}   -> 5 to 10 digits
        //$        ->end   
        //@        -> skip characters 
        [RegularExpression(@"^[0-9]{1,3}-[a-zA-Z]{5,10}-[a-zA-Z]{4,10}-[a-zA-Z]{5,10}$",
             ErrorMessage = "Adress must like 123-Street-City-Country")]
        public string Adress { get; set; }

        [DataType(DataType.Currency)]
        public decimal Salary { get; set; }

        [Display(Name = "Is Active")]
        public bool IsActive { get; set; }

        [EmailAddress]
        public string Email { get; set; }

        [Phone]
        [Display(Name = "Phone Number")]
        public string PhoneNumber { get; set; }

        [Display(Name = "Hire Date")]
        public DateTime HireDate { get; set; }

        public IFormFile Image { get; set; }

        public string ImageName { get; set; }

        //public bool IsDeleted { get; set; }

        //public DateTime CreationDate { get; set; }

        public int? DepartmentId { get; set; } //Forign Key Column

        //[InverseProperty(nameof(Models.Department.Employees))]
        //Navigational Proberty => One
        public Department Department { get; set; }
    }
}

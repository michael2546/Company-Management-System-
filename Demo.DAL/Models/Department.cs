﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.DAL.Models
{
    public class Department : ModelBase
    {

        [Required(ErrorMessage ="Code Is Required!!")]
        public string Code { get; set; }

        [Required(ErrorMessage = "Name Is Required!!")]
        public string Name { get; set; }
        [Display(Name = "Date Of Creation")]
        public DateTime DateOfCreation { get; set; }

        
        //Navigational property => Many
        public ICollection<Employee> Employees { get; set; } = new HashSet<Employee>();
    }
}

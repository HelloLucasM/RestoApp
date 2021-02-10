using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace RestoApp.Models
{
    public class Employee
    {
    
        

        [Key]
        public int Employee_ID { get; set; }

        public string First_Name { get; set; }

        public string Last_Name { get; set; }

        public int Dni { get; set; }

        public int Area_ID { get; set; }




    }
}

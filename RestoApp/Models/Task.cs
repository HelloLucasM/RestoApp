using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace RestoApp.Models
{
    public class Task
    {
        [Key]
        public int Task_ID { get; set; }

        public int Area_ID { get; set; }

        public string Task_Description { get; set; }

        public int Employee_ID { get; set; }

    }
}

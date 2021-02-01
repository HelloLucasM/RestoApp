using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace RestoApp.Models
{
    public class Area
    {

        [Key]
        public int Area_ID { get; set; }

        public string Area_Name { get; set; }

        

    }
}

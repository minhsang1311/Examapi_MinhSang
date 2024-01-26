using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;

namespace exam_WebApi
{
   
        public class Order
        {
            public string itemCode { get; set; }

            public string ItemName { get; set; }

            public int ItemQty { get; set; }

            public string OrderAddress { get; set; }

            public string PhoneNumber { get; set; }
        }


        public class Order2
        {

            public string ItemName { get; set; }

            public int ItemQty { get; set; }

            public string OrderAddress { get; set; }

            public string PhoneNumber { get; set; }
        }
  
}


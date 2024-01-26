using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using examAzure.Entities;
using examAzure.Models;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace exam_WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderSController : ControllerBase
    {

        private readonly MyDbContext _context;

        public OrderSController(MyDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        [Route("order/get-all")]
        public ActionResult Get()
        {
            var order = _context.OrderTbl.ToList();

            if (order.Count == 0)
            {
                return BadRequest();
            }

            return Ok(new
            {
                status = 200,
                metadata = order
            });
        }


        [HttpPut]
        [Route("order/update")]
        public ActionResult updateOrder(modelOrder order_u)
        {
            var order = _context.OrderTbl.FirstOrDefault(o => o.itemCode == order_u.itemCode);

            if (order == null)
            {
                return NotFound("not found the data");
            }

            order.ItemName = order_u.ItemName;
            order.ItemQty = order_u.ItemQty;
            order.OrderAddress = order_u.OrderAddress;
            order.PhoneNumber = order_u.PhoneNumber;

            _context.SaveChanges();
            return Ok("update success");
        }

        [HttpPost]
        [Route("order/post")]
        public async Task<ActionResult> post(modelOrder2 order_u)
        {
            var orde_new = new OrderTbl()
            {
                itemCode = await RandomString(),
                ItemName = order_u.ItemName,
                ItemQty = order_u.ItemQty,
                OrderDelivery = (DateTime.Now).ToString(),
                OrderAddress = order_u.OrderAddress,
                PhoneNumber = order_u.PhoneNumber
            };

            _context.Add(orde_new);
            _context.SaveChanges();
            return Ok(new
            {
                status = 200,
                message = "add success",
                metadata = orde_new
            });
        }

        [HttpDelete]
        [Route("order/delete")]
        public ActionResult delete(string orderId)
        {
            var order = _context.OrderTbl.FirstOrDefault(o => o.itemCode == orderId);

            if (order == null)
            {
                return NotFound("fail pls try again");
            }
            _context.Remove(order);
            _context.SaveChanges();

            return Ok(new
            {
                status = 200,
                message = "delete success"
            });
        }



        private static async Task<string> RandomString(int length = 6)
        {
            Random random = new Random();
            string character = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            Task<string> task = Task.Run(() => new string(Enumerable.Repeat(character, length).Select(s => s[random.Next(s.Length)]).ToArray()));

            string result = await task;

            return result;
        }

    }
}


using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Product.Model;
using Product.Repository;

namespace Product.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        IProductRepo iproduct;
        public ProductController(IProductRepo _db)
        {
            iproduct = _db;
        }

        [HttpGet]
        public IActionResult Get()
        {
            if (iproduct.GetDetails() == null)
            {
                return BadRequest();
            }
            return Ok(iproduct.GetDetails());
        }
        
        /*
         getById || getByName
         */
        [HttpGet("{Var}")]
        public IActionResult GetbyNameOrId(string Var)
        {
            int id = -1;
            if (!int.TryParse(Var,out id))
            {
                if (iproduct.GetDetailbyName(Var) == null)
                {
                    return BadRequest();
                }
                return Ok(iproduct.GetDetailbyName(Var));
            }
            //if var is ID -- int
            if (iproduct.GetDetailbyId(id) == null)
            {
                return BadRequest();
            }
            return Ok(iproduct.GetDetailbyId(id));

        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] ProductItem obj)
        {
            if (iproduct.GetDetailbyId(id) == null)
            {
                return BadRequest();
            }
            if (iproduct.AddRating(id, obj.Rating))
            {
                return Ok();
            }
            return BadRequest();
        }

        [HttpPost]
        public IActionResult Post([FromBody] ProductItem obj)
        {
            if (iproduct.AddProduct(obj))
                return Ok();
            return BadRequest();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            if (iproduct.GetDetailbyId(id) == null)
            {
                return BadRequest();
            }
            if (iproduct.DeleteDetail(id))
            {
                return Ok();
            }
            return BadRequest();
        }
        //--------------------------------------------------
        /*
         below get by id is no longer need as GetbyNameOrId is implemented below checking var input
         */
        //[HttpGet("{id}")]
        //public IActionResult Get(int id)
        //{
        //    if (iproduct.GetDetailbyId(id) == null)
        //    {
        //        return BadRequest();
        //    }
        //    return Ok(iproduct.GetDetailbyId(id));
        //}
        //----------------------------------------------------
    }
}

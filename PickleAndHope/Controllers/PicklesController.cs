using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PickleAndHope.DataAccess;
using PickleAndHope.Models;

namespace PickleAndHope.Controllers
{
    [Route("api/pickles")]
    [ApiController]
    public class PicklesController : ControllerBase
    {
        PickleRepository _repository = new PickleRepository();

            //api/pickles/add - if I added ("add) below in post 
        [HttpPost]
        public IActionResult AddPickle(Pickle pickleToAdd)
        {
            var existingP = _repository.GetByType(pickleToAdd.Type);
            if (existingP == null)
            {
                _repository.Add(pickleToAdd);
                return Created("", pickleToAdd);
            }
            else
            {
                var updatedPickle = _repository.Update(pickleToAdd);
               // var updatedPickle = _repository.GetByType(pickleToAdd.Type);
                return Ok(updatedPickle);
                //existingP.NumberInStock += pickleToAdd.NumberInStock;
            }
            
        }

        [HttpGet]
        public IActionResult GetAllPickles()
        {
            var allPickles = _repository.GetAll();
            return Ok(allPickles);
        }

        // api/pickles/{id}
        [HttpGet("{id}")]
        public IActionResult GetPickleById(int id)
        {
           var pickle = _repository.GetById(id);
           if (pickle == null) return NotFound("No pickle with that id could be found.");
           return Ok(pickle);
        }
        
    }
}
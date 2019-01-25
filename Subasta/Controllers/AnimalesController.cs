using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Subasta.core.dtos;
using Subasta.core.interfaces;

namespace Subasta.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AnimalesController : ControllerBase
    {
        readonly IAnimalService animalService;

        public AnimalesController(IAnimalService animalService)
        {
            this.animalService = animalService;
        }

        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                var animales = animalService.GetAll();
                return Ok(animales);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        [HttpGet("{id}", Name = "Get")]
        public IActionResult Get(string id)
        {
            try
            {
                var animal = animalService.Find(id);
                return Ok(animal);
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpPost]
        public IActionResult Post(AnimalDto animal)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                animalService.Add(animal);
                return Ok(animal);
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, AnimalDto animal)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                var entity = animalService.Find(id);
                if (entity == null)
                {
                    return NotFound();
                }
                animalService.Edit(animal);
                return Ok(animal);
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                var entity = animalService.Find(id);
                if (entity == null)
                {
                    return NotFound();
                }
                animalService.Delete(entity);
                return Ok();
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}

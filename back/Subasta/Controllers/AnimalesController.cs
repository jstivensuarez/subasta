using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Subasta.core.dtos;
using Subasta.core.interfaces;

namespace Subasta.Controllers
{
    [EnableCors("MyPolicy")]
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
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


        [HttpGet()]
        [Route("[action]/{id}")]
        public IActionResult Get(int id)
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

        [HttpPost, DisableRequestSizeLimit]
        public IActionResult Post()
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                AnimalDto animal = new AnimalDto();
                if (Request.Form.Files.Count > 0)
                {
                    animal.Imagen = Request.Form.Files[0];
                }
                else
                {
                    animal.Video = Request.Form["video"];
                }

                animal.Descripcion = Request.Form["descripcion"];
                animal.Peso = Convert.ToDecimal(Request.Form["peso"]);
                animal.LoteId = Convert.ToInt32(Request.Form["loteId"]);
                animal.CategoriaId = Convert.ToInt32(Request.Form["categoriaId"]);
                animal.MunicipioId = Convert.ToInt32(Request.Form["municipioId"]);
                animal.RazaId = Convert.ToInt32(Request.Form["razaId"]);
                animal.SexoId = Convert.ToInt32(Request.Form["sexoId"]);

                animalService.Add(animal);
                return Ok(animal);
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpPut, DisableRequestSizeLimit]
        public IActionResult Put()
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                AnimalDto animal = new AnimalDto();
                if (Request.Form.Files.Count > 0)
                {
                    animal.Imagen = Request.Form.Files[0];
                }
                else
                {
                    animal.Video = Request.Form["video"];
                }

                animal.AnimalId = Convert.ToInt32(Request.Form["animalId"]);
                animal.Descripcion = Request.Form["descripcion"];
                animal.Foto = Request.Form["foto"];
                animal.Peso = Convert.ToDecimal(Request.Form["peso"]);
                animal.LoteId = Convert.ToInt32(Request.Form["loteId"]);
                animal.CategoriaId = Convert.ToInt32(Request.Form["categoriaId"]);
                animal.MunicipioId = Convert.ToInt32(Request.Form["municipioId"]);
                animal.RazaId = Convert.ToInt32(Request.Form["razaId"]);
                animal.SexoId = Convert.ToInt32(Request.Form["sexoId"]);
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

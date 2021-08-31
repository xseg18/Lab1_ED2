using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Text.Json;
using Parte_3.Models;
using Parte_3.Models.Data;

namespace Parte_3.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MovieController : Controller
    {
        [HttpGet("{traversal}")]
        public ActionResult Path([FromRoute] string traversal)
        {
            return NoContent();
        }

        [HttpPost]
        public ActionResult Degree(JsonElement source)
        {
            try
            {
                string prueba = source.GetProperty("Order").ToString();
                Singleton.Instance.Movies = new Parte_1.B<Movie>(Convert.ToInt32(prueba));
                return Ok();
            }
            catch (Exception)
            {
                return new StatusCodeResult(StatusCodes.Status400BadRequest);
            }
        }

        [HttpDelete]
        public ActionResult DeleteT()
        {
            return NoContent();
        }

        [Route("populate")]
        [HttpPost]
        public ActionResult Add(JsonElement source)
        {
            try
            {
                string prueba = source.ToString();
                List<Movie> movie = JsonSerializer.Deserialize<List<Movie>>(prueba);
                foreach (var item in movie)
                {
                    Singleton.Instance.Movies.Add(item);
                }
                return Ok();
            }
            catch (Exception)
            {
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }
        }

        [Route("populate")]
        [HttpDelete]
        public ActionResult Delete([FromRoute] string id)
        {
            return NoContent();
        }
    }
}

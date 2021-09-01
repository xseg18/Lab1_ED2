using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Text.Json;
using Parte_3.Models;
using Parte_3.Models.Data;
using System.IO;

namespace Parte_3.Controllers
{
    [Route("api/[controller]")]
    public class MovieController : Controller
    {
        [HttpGet("{traversal}")]
        public ActionResult Path([FromRoute] string traversal)
        {
            return NoContent();
        }

        [HttpPost]
        public ActionResult Degree([FromBody] JsonElement source)
        {
            try
            {
                string json = source.GetProperty("Order").ToString();
                Singleton.Instance.Movies = new Parte_1.B<Movie>(Convert.ToInt32(json));
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
            try
            {
                Singleton.Instance.Movies.Clear();
                return Ok();
            }
            catch (Exception)
            {
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }

        }

        [Route("populate")]
        [HttpPost]
        public ActionResult Add([FromBody] JsonElement source)
        {
            try
            {
                string json = source.ToString();
                List<Movie> movie = JsonSerializer.Deserialize<List<Movie>>(json);
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

        [Route("populate/[action]")]
        [HttpPost]
        public ActionResult UploadFile(IFormFile file)
        {
            try
            {
                using (StreamReader r = new StreamReader(file.OpenReadStream()))
                {
                    string json = r.ReadToEnd();
                    List<Movie> movie = JsonSerializer.Deserialize<List<Movie>>(json);
                    foreach (var item in movie)
                    {
                        Singleton.Instance.Movies.Add(item);
                    }
                }
                return Ok();
            }
            catch (Exception)
            {
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }
        }

        [Route("populate/{id}")]
        [HttpDelete]
        public ActionResult Delete([FromRoute] string id)
        {
            try
            {
                try
                {
                    //Singleton.Instance.Movies.Delete(id);
                    return Ok();
                }
                catch (Exception)
                {
                    return NotFound();
                }
            }
            catch (Exception)
            {

                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }
        }
    }
}

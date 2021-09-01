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
    [ApiController]
    [Route("api/[controller]")]
    public class MovieController : Controller
    {
        [HttpGet("{traversal}")]
        public ActionResult Path([FromRoute] string traversal)
        {
            try
            {
                switch (traversal.ToUpper())
                {
                    case "INORDER":
                        return Ok(JsonSerializer.Serialize(Singleton.Instance.Movies.InOrder()));
                    case "POSTORDER":
                        return Ok(JsonSerializer.Serialize(Singleton.Instance.Movies.PostOrder()));
                    case "PREORDER":
                        return Ok(JsonSerializer.Serialize(Singleton.Instance.Movies.PreOrder()));
                    default:
                        return new StatusCodeResult(StatusCodes.Status400BadRequest);
                }
            }
            catch (Exception)
            {
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpPost]
        public ActionResult Degree([FromBody] JsonElement source)
        {
            try
            {
                string json = source.GetProperty("Order").ToString();
                int degree = Convert.ToInt32(json);
                try
                {
                    Singleton.Instance.Movies = new Parte_1.B<Movie>(degree);
                    return Ok();
                }
                catch (Exception)
                {
                    return new StatusCodeResult(StatusCodes.Status500InternalServerError);
                }
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

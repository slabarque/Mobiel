using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Mobiel.DSL;

namespace Mobiel.Web.Controllers
{
  [Route("api/[controller]")]
  public class ShapesController : Controller
  {
    // GET api/values
    [HttpGet]
    public Parsing.Object2D Get()
    {
      Parsing.PartFactory factory = new Parsing.PartFactory();

      return factory.Create(factory.Config);
    }
  }
}

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
    public Gravity.Object2D Get()
    {
      Gravity.PartFactory factory = new Gravity.PartFactory();

      return factory.Create(factory.Config);
    }
  }
}

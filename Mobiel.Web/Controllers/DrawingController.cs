using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Mobiel.DSL;

namespace Mobiel.Web.Controllers
{
  [Route("api/[controller]")]
  public class DrawingController : Controller
  {
    // GET api/values
    [HttpPost]
    public Gravity.Object2D Post([FromBody]Parsing.Code code)
    {
      Gravity.PartFactory factory = new Gravity.PartFactory();

      return factory.Create(factory.Config, code);
    }
  }
}

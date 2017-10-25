using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;

namespace ApiRest_COCO_TRIP.Controllers
{
    public class M9_CategoriasController : ApiController
    {

      [ResponseType(typeof(string))]
      public IHttpActionResult Get()
      {
        return Ok("ok");
      }
    }
}

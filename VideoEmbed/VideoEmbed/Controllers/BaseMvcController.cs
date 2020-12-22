using System.Web.Mvc;

namespace VideoEmbed.Controllers
{
    public class BaseMvcController : Controller
    {
        public JsonResult JsonMax(object obj)
        {
            //add allow all cors
            Response.AppendHeader("Access-Control-Allow-Origin", "*");

            var json = Json(obj, JsonRequestBehavior.AllowGet);
            json.MaxJsonLength = int.MaxValue;
            return json;
        }
    }
}
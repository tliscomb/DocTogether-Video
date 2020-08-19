using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using VideoEmbed.VideoService;

namespace VideoEmbed.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            return View("index");
        }

        public JsonResult RoomInfo(string id)
        {
            //Get video conference info, this object will be used client side to connect to the meeting.
            var videoService = new VideoConferenceService();
            var meetingRoomData = videoService.GetConferenceInfo(id);

            //Set user info
            meetingRoomData.userId = Guid.NewGuid().ToString();
            meetingRoomData.userName = "Unknown";

            return Json(meetingRoomData, JsonRequestBehavior.AllowGet);
        }
    }
}
using System;
using System.Runtime.Remoting.Messaging;
using System.Web.Mvc;

namespace VideoEmbed.Controllers
{
    public class LoadController : BaseMvcController
    {
        public JsonResult Ping(Guid id)
        {
            var action = LoadTest.GetAction(id);

            return JsonMax(action);
        }

        public JsonResult AdminPing(AdminPingRequest request) 
        {
            if (NotOkay(request))
            {
                return NotOkayJson();
            }

            var result = LoadTest.GetStatus();

            return JsonMax(result);
        }


        public JsonResult AdminTestStatus(OpenRequest request)
        {
            if (NotOkay(request))
            {
                return NotOkayJson();
            }

            LoadTest.Status.Open = request.open;

            var result = LoadTest.GetStatus();

            return JsonMax(result);
        }

        public JsonResult AdminPeoplePerRoom(OpenRequest request)
        {
            if (NotOkay(request))
            {
                return NotOkayJson();
            }

            LoadTest.Status.PeoplePerRoom = request.peoplePerRoom;

            var result = LoadTest.GetStatus();

            return JsonMax(result);
        }

        public JsonResult AdminRoomCap(OpenRequest request)
        {
            if (NotOkay(request))
            {
                return NotOkayJson();
            }

            LoadTest.Status.RoomCap = request.roomCap;

            var result = LoadTest.GetStatus();

            return JsonMax(result);
        }

        public JsonResult AdminMessage(OpenRequest request)
        {
            if (NotOkay(request))
            {
                return NotOkayJson();
            }

            if(request.message != null)
            LoadTest.AddMessage(request.message);

            var result = LoadTest.GetStatus();

            return JsonMax(result);
        }

        private bool NotOkay(BaseRequestPass request)
        {
            return (request.p != "asdf:LKJ");
        }

        private JsonResult NotOkayJson()
        {
            return JsonMax(new
            {
                error=true,
                message="somethings not right."
            });
        }
    }

    public class OpenRequest : BaseRequestPass
    {
        public bool open { get; set; } = false;
        public int peoplePerRoom { get; set; } = 2;
        public int roomCap { get; set; } = 10;
        public string message { get; set; }
    }
    public class AdminPingRequest : BaseRequestPass
    {
        
    }

    public class BaseRequestPass
    {
        public string p { get; set; }
    }

    public class PingAction
    {
        public ActionType action { get; set; }
        public object meetingData { get; set; }
        public string message { get; set; }
    }

    public enum ActionType
    {
        join = 0, leave = 1, stop = 2, message = 3
    }
}
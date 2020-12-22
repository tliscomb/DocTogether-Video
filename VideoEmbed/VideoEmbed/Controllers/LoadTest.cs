using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;

namespace VideoEmbed.Controllers
{
    public class LoadTest
    {
        private static object _lockObject = new object();
        public static TestStatus Status = new TestStatus();
        public static Dictionary<Guid, string> _messages = new Dictionary<Guid, string>();
        public static Dictionary<Guid, HashSet<Guid>> _UserSawMessages = new Dictionary<Guid, HashSet<Guid>>();

        public static object GetStatus()
        {
            return new
            {
                testOpen = Status.Open,
                testersWaiting = 5,
                videosRunning = 3,
                roomCap = Status.RoomCap,
                peoplePerRoom = Status.PeoplePerRoom,
                messages = _messages.Count
            };
        }

        public static void AddMessage(string requestMessage)
        {
            lock (_lockObject)
            {
                _messages[Guid.NewGuid()] = requestMessage;
            }
        }
        public static string GetMessage(Guid userId)
        {
            lock (_lockObject)
            {
                string msg = null;
                var users = _UserSawMessages.ContainsKey(userId) ? _UserSawMessages[userId] : new HashSet<Guid>();
                foreach (var message in _messages)
                {
                    if (!users.Contains(message.Key))
                    {
                        users.Add(message.Key);
                        msg = message.Value;
                        break;
                    }
                }

                _UserSawMessages[userId] = users;
                return msg;
            }
        }

        public static PingAction GetAction(Guid id)
        {
            //look for messages...
            var msg = GetMessage(id);
            if (msg != null)
            {
                return new PingAction
                {
                    action = ActionType.message,
                    message = msg,
                };
            }

            if (!Status.Open)
            {
                return new PingAction
                {
                    action = ActionType.stop,
                    message = "Testing is closed. If this is a scheduled test time please wait a minute and try again.",
                };
            }

            var pingAction = new PingAction();
            pingAction.action = ActionType.message;
            pingAction.message = "We're working on it. Just hold your horses.";

            return pingAction;
        }
    }

    public class TestStatus
    {
        public bool Open { get; set; }
        public int PeoplePerRoom { get; set; } = 2;
        public int RoomCap { get; set; } = 10;
    }
}
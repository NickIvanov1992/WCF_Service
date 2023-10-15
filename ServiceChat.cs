using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace WCF_Service
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single)]
    public class ServiceChat : IServiceChat
    {
        List<ServerUser> users = new List<ServerUser>();
        int nextId = 1;
        public int Connect(string Name)
        {
            ServerUser user = new ServerUser()
            {
                Id = nextId,
                Name = Name,
                operationContext = OperationContext.Current
            };
            nextId++;
            SendMessage(user.Name + " подключился в чат",0);
            users.Add(user);
            return user.Id;
        }

        public void Disconnect(int id)
        {
            var user = users.FirstOrDefault(i => i.Id == id);
            if (users != null)
            {
                users.Remove(user);
                SendMessage(user.Name + " покинул чат",0);
            }
        }

        public void SendMessage(string msg, int id)
        {
            foreach(var item in users)
            {
                string answer = DateTime.Now.ToShortTimeString();

                var user = users.FirstOrDefault(i => i.Id == id);
                if (users != null)
                {
                    answer += ": " + user.Name + " ";
                }

                answer += msg;

                item.operationContext.GetCallbackChannel<IServerChatCallback>()
                    .MessageChatCallback(answer);
            }
        }
    }
}

﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace WCF_Service
{
    public interface IServerChatCallback
    {
        [OperationContract(IsOneWay = true)]
        void MessageChatCallback(string msg);
    }
}
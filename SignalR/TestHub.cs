﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;
using System.Threading.Tasks;

namespace SignalR
{
    public class TestHub : Hub
    {
        public void Hello()
        {
            Clients.All.hello();
        }
        public TestHub()
        {
            // Create a Long running task to do an infinite loop which will keep sending the server time
            // to the clients every 3 seconds.
            var taskTimer = Task.Factory.StartNew(async () =>
            {
                while (true)
                {
                    string timeNow = DateTime.Now.ToString();
                    //Sending the server time to all the connected clients on the client method SendServerTime()
                    Clients.All.SendServerTime(timeNow);
                    //Delaying by 3 seconds.
                    await Task.Delay(3000);
                }
            }, TaskCreationOptions.LongRunning
                );
        }
    }
}
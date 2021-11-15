using SignalR.Models;
using System;
using System.Collections.Generic;

namespace SignalR.DataStorage
{
    public static class DataManager
    {
        public static MessageDetails GetData()
        {
            return new MessageDetails()
            {
                id = "1",
                date = DateTime.Now,
                message = DateTime.Now.ToString("dd/MM/yyyy")

            };
        }
    }
}

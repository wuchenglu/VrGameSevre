﻿using System;
using NetFrame;
using Newtonsoft.Json;
using ControlConterServe.logic;

namespace ControlConterServe
{
    public class HandlerCenter : AbsHandlerCenter
    {
        private int index = 0;
        private HandlerInterface Accaount;
        private HandlerInterface PackageHandle;
        public HandlerCenter()
        {
            Accaount = new AccountHandler();
            PackageHandle = new PackageHandle();
        }
        public override void ClientConnect(UserToken token)
        {
            Console.WriteLine("有客户端连接了");
            index++;
            Console.WriteLine("当前连接人数:" + index);
        }

        public override void MessageReceive(UserToken token, String message)
        {
            if (string.IsNullOrWhiteSpace(message))
                return;
            string[] receive2 = message.Split('}');
            string receive = receive2[0] + "}";
            try
            {

            }
            catch (Exception w)
            {
                Console.WriteLine(w.Message);
                Console.WriteLine(receive);
            }
        }

        public override void ClientClose(UserToken token, string error)
        {
            Console.WriteLine("有客户端断开了");
            index--;
            Console.WriteLine("当前连接人数:" + index);
            Accaount.ClientClose(token, error);
            PackageHandle.ClientClose(token, error);
        }
    }
}
﻿using System;
using System.Net;
using System.Net.Sockets;
using System.Threading;

namespace NetFrame
{
    public class ServerStart
    {
        Socket server;//服务器socket监听对象
        int maxClient;//最大客户端连接数
        Semaphore acceptClients;
        UserTokenPool pool;

        public LengthEncode LE;
        public LengthDecode LD;
        public encode encode;
        public decode decode;

        /// <summary>
        /// 消息处理中心，由外部应用传入
        /// </summary>
        public AbsHandlerCenter center;
        public ServerStart(int max)
        {
            server=new Socket(AddressFamily.InterNetwork,SocketType.Stream,ProtocolType.Tcp);
            maxClient = max;
        }

        public void Start(int port)
        {
            pool=new UserTokenPool(maxClient);
            acceptClients=new Semaphore(maxClient,maxClient);
            for (int i = 0; i < maxClient; i++)
            {
                UserToken userToken=new UserToken();
                //初始化token信息               
                userToken.receiveSAEA.Completed += IO_Comleted;
                userToken.sendSAEA.Completed += IO_Comleted;
                userToken.LD = LD;
                userToken.LE = LE;
                userToken.encode = encode;
                userToken.decode = decode;
                userToken.sendProcess = ProcessSend;
                userToken.closeProcess = ClientClose;
                userToken.center = center;
                pool.Push(userToken);
            }

            //监听当前服务器网卡所有可用IP地址的port端口
            // 外网IP  内网IP192.168.x.x 本机IP一个127.0.0.1
            try
            {
                server.Bind(new IPEndPoint(IPAddress.Parse("172.17.41.121"), port));
                //置于监听状态
                server.Listen(10);
                StartAccept(null);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
        /// <summary>
        /// 开始客户端连接监听
        /// </summary>
        public void StartAccept(SocketAsyncEventArgs e)
        {
            //如果当前传入为空  说明调用新的客户端连接监听事件 否则的话 移除当前客户端连接
            if (e == null)
            {
                e = new SocketAsyncEventArgs();
                e.Completed += new EventHandler<SocketAsyncEventArgs>(Accept_Comleted);
            }
            else
            {
                e.AcceptSocket = null;
            }
            //信号量-1
            acceptClients.WaitOne();
            bool result = server.AcceptAsync(e);
            //判断异步事件是否挂起  没挂起说明立刻执行完成  直接处理事件 否则会在处理完成后触发Accept_Comleted事件
            if (!result)
            {
                ProcessAccept(e);
            }
        }

        public void ProcessAccept(SocketAsyncEventArgs e)
        {
            //从连接对象池取出连接对象 供新用户使用
            UserToken token = pool.Pop();
            token.conn = e.AcceptSocket;
            //TODO 通知应用层 有客户端连接
            center.ClientConnect(token);
            //开启消息到达监听
            StartReceive(token);
            //释放当前异步对象
            StartAccept(e);
        }

        public void Accept_Comleted(object sender, SocketAsyncEventArgs e)
        {
            ProcessAccept(e);
        }

        public void StartReceive(UserToken token)
        {
            try
            {
                //用户连接对象 开启异步数据接收
                bool result = token.conn.ReceiveAsync(token.receiveSAEA);
                //异步事件是否挂起
                if (!result)
                {
                    ProcessReceive(token.receiveSAEA);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        public void IO_Comleted(object sender, SocketAsyncEventArgs e)
        {
            if (e.LastOperation == SocketAsyncOperation.Receive)
            {
                ProcessReceive(e);
            }
            else
            {
                ProcessSend(e);
            }
        }

        public void ProcessReceive(SocketAsyncEventArgs e)
        {
            UserToken token = e.UserToken as UserToken;
            //判断网络消息接收是否成功
            if (token.receiveSAEA.BytesTransferred > 0 && token.receiveSAEA.SocketError == SocketError.Success)
            {
                byte[] message = new byte[token.receiveSAEA.BytesTransferred];
                //将网络消息拷贝到自定义数组
                Buffer.BlockCopy(token.receiveSAEA.Buffer, 0, message, 0, token.receiveSAEA.BytesTransferred);
                //处理接收到的消息
                token.receive(message);
                StartReceive(token);
            }
            else
            {
                if (token.receiveSAEA.SocketError != SocketError.Success)
                {
                    ClientClose(token, token.receiveSAEA.SocketError.ToString());
                }
                else
                {
                    ClientClose(token, "客户端主动断开连接");
                }
            }
        }
        public void ProcessSend(SocketAsyncEventArgs e)
        {
            UserToken token = e.UserToken as UserToken;
            if (e.SocketError != SocketError.Success)
            {
                ClientClose(token, e.SocketError.ToString());
            }
            else
            {
                //消息发送成功，回调成功
                token.writed();
            }
        }

        /// <summary>
        /// 客户端断开连接
        /// </summary>
        /// <param name="token"> 断开连接的用户对象</param>
        /// <param name="error">断开连接的错误编码</param>
        public void ClientClose(UserToken token, string error)
        {
            if (token.conn != null)
            {
                lock (token)
                {
                    //通知应用层面 客户端断开连接了
                    center.ClientClose(token, error);
                    token.Close();
                    //加回一个信号量，供其它用户使用
                    pool.Push(token);
                    acceptClients.Release();
                }
            }
        }
    }

}

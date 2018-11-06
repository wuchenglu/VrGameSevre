using GameServe.Test;
using NetFrame;
using System;
using System.Data;

namespace ControlConterServe
{
    class Program
    {
        //当前要加载的游戏路径
        private static string currentGameToLoadFilePath = "";
        private static string allGameResourse = "";
        static void Main(string[] args)
        {
            //初始话服务器资源配置
            ConfigXmlFileRead.ReadGameFilePath(currentGameToLoadFilePath, allGameResourse);
            ServerStart serverStart = new ServerStart(60);
            serverStart.center = new HandlerCenter();
            serverStart.Start(3389);
            Console.WriteLine("服务器已启动");
            MysqlConnection mysqlConnection = new MysqlConnection();
            Console.WriteLine(mysqlConnection.MysqlInfo());
            DataView dataView= mysqlConnection.MysqlDataAdapter("User");
            //Console.WriteLine( dataView.Table.;
            while (true)
            {
                Console.ReadLine();
            }
        }
    }
}

﻿using System;
using System.Configuration;
using MySql.Data.MySqlClient;

namespace GameServe.Test
{
    public class TestDatebase
    {
        public TestDatebase()
        {
            //
            // TODO: 在此处添加构造函数逻辑
            //
        }
        //public static void Main(String[] args)
        //{
        //    MySqlConnection mysql = getMySqlCon();
        //    //查询sql
        //    String sqlSearch = "select * from student";
        //    //插入sql
        //    String sqlInsert = "insert into student values (12,'张三',25,'大专')";
        //    //修改sql
        //    String sqlUpdate = "update student set name='李四' where id= 3";
        //    //删除sql
        //    String sqlDel = "delete from student where id = 12";
        //    //打印SQL语句
        //    Console.WriteLine(sqlDel);
        //    //四种语句对象
        //    //MySqlCommand mySqlCommand = getSqlCommand(sqlSearch, mysql);
        //    //MySqlCommand mySqlCommand = getSqlCommand(sqlInsert, mysql);
        //    //MySqlCommand mySqlCommand = getSqlCommand(sqlUpdate, mysql);
        //    MySqlCommand mySqlCommand = getSqlCommand(sqlDel, mysql);
        //    mysql.Open();
        //    //getResultset(mySqlCommand);
        //    //getInsert(mySqlCommand);
        //    //getUpdate(mySqlCommand);
        //    getDel(mySqlCommand);
        //    //记得关闭
        //    mysql.Close();
        //    String readLine = Console.ReadLine();
        //}
        /// <summary>
        /// 建立mysql数据库链接
        /// </summary>
        /// <returns></returns>
        public static MySqlConnection getMySqlCon()
        {
            String mysqlStr = "Database=test;Data Source=127.0.0.1;User Id=root;Password=root;pooling=false;CharSet=utf8;port=3306";
            // String mySqlCon = ConfigurationManager.ConnectionStrings["MySqlCon"].ConnectionString;
            MySqlConnection mysql = new MySqlConnection(mysqlStr);
            return mysql;
        }
        /// <summary>
        /// 建立执行命令语句对象
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="mysql"></param>
        /// <returns></returns>
        public static MySqlCommand getSqlCommand(String sql, MySqlConnection mysql)
        {
            MySqlCommand mySqlCommand = new MySqlCommand(sql, mysql);
            //  MySqlCommand mySqlCommand = new MySqlCommand(sql);
            // mySqlCommand.Connection = mysql;
            return mySqlCommand;
        }
        /// <summary>
        /// 查询并获得结果集并遍历
        /// </summary>
        /// <param name="mySqlCommand"></param>
        public static void getResultset(MySqlCommand mySqlCommand)
        {
            MySqlDataReader reader = mySqlCommand.ExecuteReader();
            try
            {
                while (reader.Read())
                {
                    if (reader.HasRows)
                    {
                        Console.WriteLine("编号:" + reader.GetInt32(0) + "|姓名:" + reader.GetString(1) + "|年龄:" + reader.GetInt32(2) + "|学历:" + reader.GetString(3));
                    }
                }
            }
            catch (Exception)
            {

                Console.WriteLine("查询失败了！");
            }
            finally
            {
                reader.Close();
            }
        }
        /// <summary>
        /// 添加数据
        /// </summary>
        /// <param name="mySqlCommand"></param>
        public static void getInsert(MySqlCommand mySqlCommand)
        {
            try
            {
                mySqlCommand.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                String message = ex.Message;
                Console.WriteLine("插入数据失败了！" + message);
            }

        }
        /// <summary>
        /// 修改数据
        /// </summary>
        /// <param name="mySqlCommand"></param>
        public static void getUpdate(MySqlCommand mySqlCommand)
        {
            try
            {
                mySqlCommand.ExecuteNonQuery();
            }
            catch (Exception ex)
            {

                String message = ex.Message;
                Console.WriteLine("修改数据失败了！" + message);
            }
        }
        /// <summary>
        /// 删除数据
        /// </summary>
        /// <param name="mySqlCommand"></param>
        public static void getDel(MySqlCommand mySqlCommand)
        {
            try
            {
                mySqlCommand.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                String message = ex.Message;
                Console.WriteLine("删除数据失败了！" + message);
            }
        }
    }
}

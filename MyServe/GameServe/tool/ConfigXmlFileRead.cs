using System;
using System.Xml;
using System.IO;
public class ConfigXmlFileRead
{
    /// <summary>
    /// 读取游戏项的配置，存到源数据实体中
    /// </summary>
    /// <param name="path1"></param>
    /// <param name="path2"></param>
    public static void ReadGameFilePath(string path1, string path2)
    {
        ReadCurreentAllGameInfo(ReadGameList(path1), path2);
    }
    /// <summary>
    /// 读取gameList
    /// </summary>
    /// <param name="filePath">xml文件路径</param>
    /// <returns>如果路径不存在返回null，出错返回null</returns>
    private static int[] ReadGameList(string filePath)
    {
        if (!File.Exists(filePath))
        {
            return null;
        }
        XmlDocument showGameInfoXml = new XmlDocument();
        try
        {
            showGameInfoXml.Load(filePath);
            XmlNodeList nodeList = showGameInfoXml.SelectSingleNode("root_NameList").ChildNodes;
            int count = nodeList.Count;
            int[] gameListId = new int[count];
            for (int i = 0; i < count; i++)
            {
                XmlElement node1 = nodeList[i] as XmlElement;
                int id = int.Parse(node1.GetAttribute("name"));
                gameListId[i] = id;
            }
            return gameListId;
        }
        catch (Exception e)
        {
            return null;
        }
    }
    private static bool ReadCurreentAllGameInfo(int[] gameListId, string path2)
    {
        if (gameListId == null || gameListId.Length <= 0)
        {
            FileDayLog.WriteDayLog("读取的上线游戏小于0不对，还有一个大厅需要加上");
            return false;
        }
        try
        {
            if (File.Exists(path2))
            {
                XmlDocument gameInfoXML = new XmlDocument();
                gameInfoXML.Load(path2);
                ConfigXmlFileEntity.Instance.Version = gameInfoXML.DocumentElement.SelectSingleNode("/content/version").InnerText;
                ConfigXmlFileEntity.Instance.ClientType = gameInfoXML.DocumentElement.SelectSingleNode("/content/clientType").InnerText;
                ConfigXmlFileEntity.Instance.GameNum = gameInfoXML.DocumentElement.SelectSingleNode("/content/gameNum").InnerText;
                ConfigXmlFileEntity.Instance.SwitchTime = gameInfoXML.DocumentElement.SelectSingleNode("/content/switchTime").InnerText;
                ConfigXmlFileEntity.Instance.DTIndex = gameInfoXML.DocumentElement.SelectSingleNode("/content/dTIndex").InnerText;
                int count = gameListId.Length;
                for (int i = 0; i < count; i++)
                {
                    GameItem body = new GameItem();
                    string gameID = gameListId[i].ToString();
                    body.GameName = gameInfoXML.DocumentElement.SelectSingleNode("/content/gameInfoList/game" + gameID + "/gameName").InnerText;
                    body.GameID = gameID;
                    body.GameVersion = gameInfoXML.DocumentElement.SelectSingleNode("/content/gameInfoList/game" + gameID + "/gameVersion").InnerText;
                    body.ProgressesName = gameInfoXML.DocumentElement.SelectSingleNode("/content/gameInfoList/game" + gameID + "/progressesName").InnerText;
                    body.CloseOption = gameInfoXML.DocumentElement.SelectSingleNode("/content/gameInfoList/game" + gameID + "/closeOption").InnerText;
                    body.FilePath = gameInfoXML.DocumentElement.SelectSingleNode("/content/gameInfoList/game" + gameID + "/filePath").InnerText;
                    body.CloseFilePath = gameInfoXML.DocumentElement.SelectSingleNode("/content/gameInfoList/game" + gameID + "/closeFilePath").InnerText;
                    ConfigXmlFileEntity.Instance.GameCache.Add(gameID, body);
                }
                return true;
            }
            else
            {
                FileDayLog.WriteDayLog("不存在:" + path2);
                return false;
            }
          
        }
        catch (Exception e)
        {
            FileDayLog.WriteDayLog("读取配置文件失败" + e.Message);
            return false;
        }

    }

}

/// <summary>/// 配置文件的实体类/// </summary>
using System.Collections.Generic;
public class ConfigXmlFileEntity : Singleton<ConfigXmlFileEntity>
{
    private string version;
    private string clientType;
    private string gameNum;
    private string switchTime;
    private string dTIndex;
    private Dictionary<string, GameItem> gameCache = new Dictionary<string, GameItem>();
    /// <summary>
    /// 软件版本
    /// </summary>
    public string Version
    {
        get
        {
            return version;
        }

        set
        {
            version = value;
        }
    }
    /// <summary>
    /// 根据Type来区分是哪一类功能的软件
    /// </summary>
    public string ClientType
    {
        get
        {
            return clientType;
        }

        set
        {
            clientType = value;
        }
    }
    /// <summary>
    /// 一共有几个游戏
    /// </summary>
    public string GameNum
    {
        get
        {
            return gameNum;
        }

        set
        {
            gameNum = value;
        }
    }
    /// <summary>
    /// 两个游戏EXE切换的时间
    /// </summary>
    public string SwitchTime
    {
        get
        {
            return switchTime;
        }

        set
        {
            switchTime = value;
        }
    }
    /// <summary>
    /// 大厅所在游戏项的位置
    /// </summary>
    public string DTIndex
    {
        get
        {
            return dTIndex;
        }
        set
        {
            dTIndex = value;
        }
    }
    /// <summary>
    /// 存储所有游戏项的容器
    /// </summary>
    public Dictionary<string, GameItem> GameCache
    {
        get
        {
            return gameCache;
        }
        set
        {
            gameCache = value;
        }
    }
}

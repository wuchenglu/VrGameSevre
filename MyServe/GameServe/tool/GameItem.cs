/// <summary>/// 游戏单个项/// </summary>
public class GameItem
{
    private string gameName;
    private string gameID;
    private string gameVersion;
    private string progressesName;
    private string closeOption;
    private string filePath;
    private string closeFilePath;
    /// <summary>
    /// 游戏名字
    /// </summary>
    public string GameName
    {
        get
        {
            return gameName;
        }

        set
        {
            gameName = value;
        }
    }
    /// <summary>
    /// 游戏Id
    /// </summary>
    public string GameID
    {
        get
        {
            return gameID;
        }

        set
        {
            gameID = value;
        }
    }
    /// <summary>
    /// 游戏版本
    /// </summary>
    public string GameVersion
    {
        get
        {
            return gameVersion;
        }

        set
        {
            gameVersion = value;
        }
    }
    /// <summary>
    /// 游戏进程名
    /// </summary>
    public string ProgressesName
    {
        get
        {
            return progressesName;
        }

        set
        {
            progressesName = value;
        }
    }
    /// <summary>
    /// 以何种关闭游戏
    /// </summary>
    public string CloseOption
    {
        get
        {
            return closeOption;
        }

        set
        {
            closeOption = value;
        }
    }
    /// <summary>
    /// 游戏启动路径
    /// </summary>
    public string FilePath
    {
        get
        {
            return filePath;
        }

        set
        {
            filePath = value;
        }
    }
    /// <summary>
    /// 游戏关闭路径
    /// </summary>
    public string CloseFilePath
    {
        get
        {
            return closeFilePath;
        }

        set
        {
            closeFilePath = value;
        }
    }
}

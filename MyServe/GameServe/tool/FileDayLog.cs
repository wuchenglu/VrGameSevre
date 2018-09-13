using System.IO;

public class FileDayLog
{

    private static string logFileHeelPath = "./DayLog/";
    /// <summary> ///写文件日志
    /// </summary>
    public static void WriteDayLog(string mes)
    {
        try
        {
            string path = CreateLogTxtPath();
            string currentTime = GetCurentTime();
            string revord = currentTime + ":" + mes;
            AdditionalData(path, revord);
        }
        catch
        {

        }
    }
    /// <summary>
    /// 在txt文件末尾追加一行数据
    /// </summary>
    private static void AdditionalData(string Path, string mes)
    {
        string path = Path;//文件存放路径，保证文件存在。
        if (!File.Exists(Path))
        {
            File.Create(Path).Close();
        }
        StreamWriter sw = new StreamWriter(path, true, System.Text.Encoding.UTF8);
        sw.WriteLine(mes);
        sw.Close();
    }
    private static string CreateLogTxtPath()
    {
        string time = GetLogDay();
        if (!Directory.Exists(logFileHeelPath))
        {
            Directory.CreateDirectory(logFileHeelPath);
        }
        return logFileHeelPath + time;
    }
    private static string GetLogDay()
    {
        int year = System.DateTime.Now.Year;
        int month = System.DateTime.Now.Month;
        int day = System.DateTime.Now.Day;
        string sYear = year < 10 ? "0" + year : year.ToString();
        string sMonth = month < 10 ? "0" + month : month.ToString();
        string sDay = day < 10 ? "0" + day : day.ToString();
        return sYear + "年" + sMonth + "月" + sDay + "日.txt";
    }
    /// <summary>/// 获取当前时间格式为2017-03-10 11:21:00这个方法为与服务器传输的时间的标准格式/// </summary>
    private static string GetCurentTime()
    {
        int year = System.DateTime.Now.Year;
        int month = System.DateTime.Now.Month;
        int day = System.DateTime.Now.Day;
        int hourr = System.DateTime.Now.Hour;
        int minn = System.DateTime.Now.Minute;
        int mss = System.DateTime.Now.Second;

        string sYear = year < 10 ? "0" + year : year.ToString();
        string sMonth = month < 10 ? "0" + month : month.ToString();
        string sDay = day < 10 ? "0" + day : day.ToString();
        string sHour = hourr < 10 ? "0" + hourr : hourr.ToString();
        string sMin = minn < 10 ? "0" + minn : minn.ToString();
        string sSec = mss < 10 ? "0" + mss : mss.ToString();
        return sYear + "-" + sMonth + "-" + sDay + " " + sHour + ":" + sMin + ":" + sSec;
    }
}

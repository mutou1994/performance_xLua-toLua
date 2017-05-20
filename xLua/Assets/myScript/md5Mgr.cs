using System.Collections;
using System.IO;
using System.Security.Cryptography;
public class md5Mgr
{

    /// <summary>
    /// 计算字节流的MD5值
    /// </summary>
    /// <param name="buffer"> 要计算的对象 </param>
    /// <param name="index"> 起始位置</param>
    /// <param name="count"> 计算长度</param>
    /// <returns></returns>
    public static string getMD5Buffer(byte[] buffer, int index, int count)
    {
        if (buffer == null) return "";
        string result = null;
        MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider();
        byte[] hashcode = md5.ComputeHash(buffer, index, count);
        result = System.BitConverter.ToString(hashcode);
        result = result.Replace("-", "");
        return result;
    }

    /// <summary>
    /// 计算一个文件的MD5值
    /// </summary>
    /// <param name="filePath"></param>
    /// <returns></returns>
    public static string getMD5Buffer(string filePath)
    {
        if (!File.Exists(filePath))
            return "";

        FileStream fileRead = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.Read);
        byte[] buffer = new byte[fileRead.Length];
        fileRead.Read(buffer, 0, (int)fileRead.Length);
        fileRead.Close();

        return getMD5Buffer(buffer, 0, buffer.Length);
    }

    /// <summary>
    /// 比较两个文件的md5值
    /// </summary>
    /// <param name="path1"></param>
    /// <param name="path2"></param>
    /// <returns></returns>
    public static bool CompareMD5(string path1, string path2)
    {
        return getMD5Buffer(path1) == getMD5Buffer(path2);
    }

    /// <summary>
    /// 比较字节流和文件的md5值
    /// </summary>
    /// <param name="buffer"></param>
    /// <param name="filePath"></param>
    /// <returns></returns>
    public static bool CompareMD5(byte[] buffer, string filePath)
    {
        return getMD5Buffer(buffer, 0, buffer.Length) == getMD5Buffer(filePath);
    }

    /// <summary>
    /// 比较字节流与指定md5值
    /// </summary>
    /// <param name="buffer"></param>
    /// <param name="md5"></param>
    /// <returns></returns>
    public static bool CheckMD5(byte[] buffer, string md5)
    {
        return getMD5Buffer(buffer, 0, buffer.Length) == md5;
    }

    /// <summary>
    /// 比较文件与指定md5值
    /// </summary>
    /// <param name="filePath"></param>
    /// <param name="md5"></param>
    /// <returns></returns>
    public static bool CheckMD5(string filePath, string md5)
    {
        return getMD5Buffer(filePath) == md5;
    }
}


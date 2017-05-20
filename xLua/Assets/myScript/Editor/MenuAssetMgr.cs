using UnityEngine;
using UnityEditor;
using System.Collections.Generic;
using System.IO;
using System.Text;

public class MenuAssetMgr : MonoBehaviour {

    [MenuItem("AssertMgr/Generate AssetList")]
    static void GenerateAssetList()
    {
        string streamPath = Application.streamingAssetsPath;
        string assetList = streamPath + "/assetlist.txt";
        if (File.Exists(assetList))
        {
            File.Delete(assetList);
        }

        DirectoryInfo dir = new DirectoryInfo(streamPath);
        List<string> filePaths = new List<string>();
        RecursionFilePath(dir, filePaths);

        Stream stream = File.Create(assetList);
        using (StreamWriter writer = new StreamWriter(stream, Encoding.Default))
        {
            string content = string.Empty;
            for (int i = 0, Imax = filePaths.Count; i < Imax; i++)
            {
                string path = filePaths[i];
                string md5 = md5Mgr.getMD5Buffer(path);
                path = path.Substring(streamPath.Length).Replace('\\', '/');

                if (i > 0)
                {
                    content += "\n";
                }

                content += path + "|" + md5;
            }
            writer.Write(content);
            writer.Flush();
            writer.Close();
            writer.Dispose();
        }
        AssetDatabase.Refresh();
        Debug.Log("Generate AssetList Finished");
    }

    static void RecursionFilePath(DirectoryInfo directoryInfo, List<string> paths)
    {
        foreach (FileSystemInfo fs in directoryInfo.GetFileSystemInfos())
        {
            FileInfo fi = fs as FileInfo;
            if (fi == null)
            {
                DirectoryInfo di = fs as DirectoryInfo;
                RecursionFilePath(di, paths);
            }
            else
            {
                string path = fi.FullName;
                if (path.IndexOf(".meta") >= 0) continue;
                if (paths == null)
                {
                    paths = new List<string>();
                }
                paths.Add(path);
            }
        }
    }

}

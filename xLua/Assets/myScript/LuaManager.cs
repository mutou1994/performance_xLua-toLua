using UnityEngine;
using System.Collections;
using System.IO;
using XLua;

public class LuaManager : MonoBehaviour {
    public static LuaManager Instance;

    public bool encryption = false;
    private LuaEnv lua;

    string envPath =
#if UNITY_EDITOR
            Application.streamingAssetsPath;
#else
        Application.persistentDataPath;
#endif

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        lua = new LuaEnv();
        InitThirdLibs();

        if (!encryption)
            InitPackagePath();
        else
            lua.AddLoader(Loader);
        
        //开启main.lua 入口
        StartUp();
    }

    void Update()
    {
        if(lua!=null)
            lua.Tick();
    }

    void OnDestroy()
    {
        Close();
    }
    #region 一些接口
    void StartUp()
    {
        DoFile("main");
    }


    public object[] DoFile(string filename)
    {
        return lua.DoString("require " + filename);
    }

    public object[] CallFunction(string funcName, params object[] args)
    {
        LuaFunction func = lua.Global.Get<LuaFunction>(funcName);
        return func.Call(args);
    }

    public void Get<T,K>(T key,out K val)
    {
         lua.Global.Get<T,K>(key,out val);
    }


    public void Close()
    {
        lua.Dispose();
        lua = null;
    }

    #endregion


    #region 环境设置
    void InitPackagePath()
    {
        lua.DoString("package.path=package.path..';"+envPath+"'");
    }

    //初始化一些第三方库
    void InitThirdLibs()
    {

    }

    byte[] Loader(ref string fileName)
    {
        //查找lua文件
        fileName = fileName.EndsWith(".lua") ? fileName.Substring(0, fileName.Length - 4) : fileName;
        fileName = fileName.Replace('.', '/');
        fileName = fileName + ".lua";
        DirectoryInfo dire = new DirectoryInfo(envPath);
        FileInfo fi = SeachFile(dire,fileName);
        if (fi == null)
            return null;

        fileName = fi.FullName;
        FileStream stream = fi.OpenRead();
        byte[] buffers = new byte[stream.Length];
        stream.Read(buffers, 0, buffers.Length);
        if (encryption)
        {
            //解密
        }
        return buffers;
    }

    FileInfo SeachFile(DirectoryInfo dire,string fileName)
    {
        FileInfo result=null;
        foreach (FileSystemInfo fs in dire.GetFileSystemInfos())
        {
            FileInfo fi = fs as FileInfo;
            if (fi == null)//directory
            {
                result= SeachFile(fs as DirectoryInfo, fileName);
            }
            else
            {
                string path = fi.FullName;
                if (path.IndexOf(fileName) >=0)
                {
                    result = fi;
                    break;
                }
            }
        }
        return result;
    }
    #endregion

}

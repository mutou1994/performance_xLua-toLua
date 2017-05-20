using UnityEngine;
using System.Collections;
using LuaInterface;
public class testCS : MonoBehaviour {

    LuaState lua;
    public TextAsset luaScript;
    LuaTable table;
    LuaFunction lua_callCSFunc;
    LuaFunction lua_callCSStaticFunc;
    LuaFunction lua_callLuaFunc;
    LuaFunction lua_setPosition;
    LuaFunction lua_readPosition;

    int time = 2000000;

	void Start () {

        lua = new LuaState();
        lua.Start();
        LuaBinder.Bind(lua);
        lua.DoString(luaScript.text);
        lua["this"] = this;
        lua_callCSFunc = lua.GetFunction("CallCSFuncTest");
        lua_callCSStaticFunc = lua.GetFunction("CallCSStaticFunc");
        lua_callLuaFunc = lua.GetFunction("CallLuaFuncTest");
        lua_setPosition = lua.GetFunction("SetPositionTest");
        lua_readPosition = lua.GetFunction("ReadPositionTest");
	}
	
    void OnGUI()
    {
        if(GUI.Button(new Rect(0,0,150,50),"CallCSFunc"))
        {
            lua_callCSFunc.Call();
        } 

        if(GUI.Button(new Rect(160,0,150,50),"CallCSStaticFunc"))
        {
            lua_callCSStaticFunc.Call();
        }

        if (GUI.Button(new Rect(320, 0, 150, 50), "CallLuaFunc"))
        {
            CallLuaFuncTest();
        }

        if(GUI.Button(new Rect(0,60,150,50),"SetPositionTest"))
        {
            lua_setPosition.Call();
        }

        if(GUI.Button(new Rect(160,60,150,50),"ReadPositionTest"))
        {
            lua_readPosition.Call();
        }
    }


    #region CSCallLuaTest
    void CallLuaFuncTest()
    {
        float t = Time.realtimeSinceStartup;
        for (int i=0;i<time;i++)
        {
            lua_callLuaFunc.Call();
        }
        Debug.Log("CallLuaFunc 200W Time Cost:" + (Time.realtimeSinceStartup - t) + "s");
    }

    #endregion

    #region LuaCallCS
    public void CallCSFuncTest()
    {
    }

    public static void CallCSStaticFuncTest()
    {

    }
    #endregion
}

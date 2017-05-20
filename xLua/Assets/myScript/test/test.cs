using UnityEngine;
using System.Collections;
using System;
using XLua;

[LuaCallCSharp]
public class test : MonoBehaviour {

    LuaEnv lua;
    public TextAsset luaScript;

    Action lua_callCSFunc;
    Action lua_callCSStaticFunc;
    Action lua_callLuaFunc;
    Action lua_setPosition;
    Action lua_readPosition;

    LuaFunction lua_callCSFunc_lf;
    LuaFunction lua_callCSStaticFunc_lf;
    LuaFunction lua_callLuaFunc_lf;
    LuaFunction lua_setPosition_lf;
    LuaFunction lua_readPosition_lf;

    int time = 2000000;
    void Start () {

        lua = new LuaEnv();
        lua.DoString(luaScript.text);

        lua.Global.Set("this", this);
        lua.Global.Get("CallCSFuncTest", out lua_callCSFunc);
        lua.Global.Get("CallCSStaticFunc", out lua_callCSStaticFunc);
        lua.Global.Get("CallLuaFuncTest", out lua_callLuaFunc);
        lua.Global.Get("SetPositionTest", out lua_setPosition);
        lua.Global.Get("ReadPositionTest", out lua_readPosition);

        lua.Global.Get("CallCSFuncTest", out lua_callCSFunc_lf);
        lua.Global.Get("CallCSStaticFunc", out lua_callCSStaticFunc_lf);
        lua.Global.Get("CallLuaFuncTest", out lua_callLuaFunc_lf);
        lua.Global.Get("SetPositionTest", out lua_setPosition_lf);
        lua.Global.Get("ReadPosition", out lua_readPosition_lf);
    }


    void OnGUI()
    {
        if (GUI.Button(new Rect(0, 0, 150, 50), "CallCSFunc"))
        {
            lua_callCSFunc();
        }

        if (GUI.Button(new Rect(160, 0, 150, 50), "CallCSStaticFunc"))
        {
            lua_callCSStaticFunc();
        }

        if (GUI.Button(new Rect(320, 0, 150, 50), "CallLuaFunc"))
        {
            CallLuaFuncTest();
        }

        if(GUI.Button(new Rect(0,120,150,50),"SetPositionTest"))
        {
            lua_setPosition();
        }

        if (GUI.Button(new Rect(320, 120, 150, 50), "ReadPositionTest"))
        {
            lua_readPosition();
        }

        if (GUI.Button(new Rect(0, 60, 150, 50), "CallLuaFunc_lf"))
        {
            lua_callCSFunc_lf.Call();
        }

        if (GUI.Button(new Rect(160, 60, 150, 50), "CallCSStaticFunc_lf"))
        {
            lua_callCSStaticFunc_lf.Call();
        }

        if (GUI.Button(new Rect(320, 60, 150, 50), "CallLuaFunc_lf"))
        {
            CallLuaFuncTest_lf();
        }

        if (GUI.Button(new Rect(0, 180, 150, 50), "ReadPositionTest_lf"))
        {
            lua_readPosition_lf.Call();
        }

        if(GUI.Button(new Rect(160,120,150,50),"SetPositionTest_lf"))
        {
            lua_setPosition_lf.Call();
        }

        

        


    }

    #region CSCallLua
    void CallLuaFuncTest_lf()
    {
        float t = Time.realtimeSinceStartup;
        for (int i = 0; i < time; i++)
        {
            lua_callLuaFunc_lf.Call();
        }
        Debug.Log("CallLuaFunc 200W Time Cost:" + (Time.realtimeSinceStartup - t) + "s");
    }

    void CallLuaFuncTest()
    {
        float t = Time.realtimeSinceStartup;
        for (int i = 0; i < time; i++)
        {
            lua_callLuaFunc();
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

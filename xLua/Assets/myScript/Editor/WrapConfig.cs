using UnityEngine;
using System.Collections.Generic;
using System;
using System.Linq;
using XLua;
using System.Reflection;

public static class WrapConfig  {

    [LuaCallCSharp]
    public static List<Type> List_LuaCallCS = new List<Type>()
    {
        typeof(WrapTest),
        typeof(Dictionary<string,int>),
    };

    [Hotfix]
    public static List<Type> List_Hotfix = new List<Type>()
    {
        typeof(WrapTest),
    };

    [Hotfix]
    public static List<Type>List_Hotfix_nameSpace
    {
        get
        {
            return (from type in Assembly.Load("Assembly-CSharp").GetTypes()
                    where type.Namespace== "NeedHotfix"
                    select type).ToList();
        }
    }
}

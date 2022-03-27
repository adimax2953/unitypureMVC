using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Threading;
using System.IO;

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

public class ConvertManager
{
    public static string createConvert(string numstring, string formate)
    {
        string overConvert = "";

        overConvert = Convert.ToString(Convert.ToInt32(numstring), Convert.ToInt32(formate));

        return overConvert;
    }

    public static string JsonToString(JArray jsonObject)
    {
        string overConvert = jsonObject.ToString();


        return overConvert;
    }

    public static JArray StringToJson(string stringObject)
    {
        JArray returnJob = JsonConvert.DeserializeObject<JArray>(stringObject);
        return returnJob;
    }

    public static string JsonToString(JObject jsonObject)
    {
        string overConvert = jsonObject.ToString();


        return overConvert;
    }

    public static JObject StringToJobj(string stringObject)
    {
        JObject returnJob = JsonConvert.DeserializeObject<JObject>(stringObject);
        return returnJob;
    }

    public static JArray SortJArray(string key, JArray Obj)
    {
        JArray SortJArray = new JArray(Obj.OrderByDescending(x => x[key]));
        return SortJArray;
    }
}

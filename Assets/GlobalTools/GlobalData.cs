using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using DataGroup.User;

public class GlobalData {

    public static string APIADDRESS = "http://127.0.0.1:9090/";
    public const string LOGIN_PATH = "api/v1/user/login";
    public const string REGISTER_PATH = "api/v2/user/register";
    public const string FORGAT_PASSWORD_PATH = "";
    public const string THIRD_PARTY_LOGIN_PATH = "api/v2/user/third_party_login";
    public const string POST_METHOD = "Post";
    public static UserData UserData;


    public static string PATH;
    public static byte[] PNG;
    public static List<string> fileList;
}

using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MappingManager : MonoBehaviour
{
    public static BasicResultData MappingBasicResultData(string json)
    {
        return JsonConvert.DeserializeObject<BasicResultData>(json);
    }
}
public class TokenData
{
    public string JwtToken { get; set; }
    public string ApiToken { get; set; }
    public string Token { get; set; }
}
public class BasicResultData
{
    public string Message { get; set; }
    public int Code { get; set; }
    public TokenData Result { get; set; }
}
using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RegistUserResponse
{
    //JsonProperty属性を付けてjsonキー名を変数に割り当てる
    [JsonProperty("user_id")]
    public int UserID { get; set; }
}

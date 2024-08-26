using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RegistUserRequest
{
    //JsonProperty属性を付けてjsonキー名を変数に割り当てる
    [JsonProperty("name")]
    public string Name{  get; set; }
}

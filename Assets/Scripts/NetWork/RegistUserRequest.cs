using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RegistUserRequest
{
    //JsonProperty������t����json�L�[����ϐ��Ɋ��蓖�Ă�
    [JsonProperty("name")]
    public string Name{  get; set; }
}

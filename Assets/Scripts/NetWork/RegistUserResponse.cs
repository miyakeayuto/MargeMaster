using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RegistUserResponse
{
    //JsonProperty������t����json�L�[����ϐ��Ɋ��蓖�Ă�
    [JsonProperty("user_id")]
    public int UserID { get; set; }
}

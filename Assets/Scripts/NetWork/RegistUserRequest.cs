using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RegistUserRequest
{
    //JsonProperty‘®«‚ğ•t‚¯‚ÄjsonƒL[–¼‚ğ•Ï”‚ÉŠ„‚è“–‚Ä‚é
    [JsonProperty("name")]
    public string Name{  get; set; }
}

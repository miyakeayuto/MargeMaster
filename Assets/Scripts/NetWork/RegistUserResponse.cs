using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RegistUserResponse
{
    //JsonProperty‘®«‚ğ•t‚¯‚ÄjsonƒL[–¼‚ğ•Ï”‚ÉŠ„‚è“–‚Ä‚é
    [JsonProperty("user_id")]
    public int UserID { get; set; }
}

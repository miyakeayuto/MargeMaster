using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Gamemanager : MonoBehaviour
{
    public int margeCounter;       //‰½‰ñ‡‘Ì‚µ‚È‚¢‚Æ‚¢‚¯‚È‚¢‚©

    void Awake()
    {
        //UIƒV[ƒ“‚ğ“Ç‚İ‚Ş
        SceneManager.LoadScene("UI",LoadSceneMode.Additive);
    }
}

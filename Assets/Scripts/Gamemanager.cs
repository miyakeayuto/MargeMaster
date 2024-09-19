using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Gamemanager : MonoBehaviour
{
    public int margeCounter;       //‰½‰ñ‡‘Ì‚µ‚È‚¢‚Æ‚¢‚¯‚È‚¢‚©

    // Start is called before the first frame update
    void Awake()
    {
        //UIƒV[ƒ“‚ğ“Ç‚İ‚Ş
        SceneManager.LoadScene("UI",LoadSceneMode.Additive);
    }

    // Update is called once per frame
    void Update()
    {

    }
}

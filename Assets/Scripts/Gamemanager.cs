using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Gamemanager : MonoBehaviour
{
    public int margeCounter;       //何回合体しないといけないか

    // Start is called before the first frame update
    void Awake()
    {
        //UIシーンを読み込む
        SceneManager.LoadScene("UI",LoadSceneMode.Additive);
    }

    // Update is called once per frame
    void Update()
    {

    }
}

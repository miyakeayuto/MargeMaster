using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Gamemanager : MonoBehaviour
{
    public int margeCounter;       //何回合体しないといけないか

    void Awake()
    {
        //UIシーンを読み込む
        SceneManager.LoadScene("UI",LoadSceneMode.Additive);
    }
}

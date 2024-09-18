using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Gamemanager : MonoBehaviour
{
    [SerializeField] float timer;                   //タイマー
    [SerializeField] Text timerText;                //タイマーテキスト
    [SerializeField] GameObject timeOverText;       //時間切れのテキスト
    [SerializeField] GameObject clearUI;            //クリア時に出すUI
    [SerializeField] GameObject posePanel;          //ポーズUI

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

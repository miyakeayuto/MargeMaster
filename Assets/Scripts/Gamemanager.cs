using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Gamemanager : MonoBehaviour
{
    [SerializeField] float timer;                   //タイマー
    [SerializeField] Text timerText;                //タイマーテキスト
    [SerializeField] GameObject timeOverText;             //時間切れのテキスト

    // Start is called before the first frame update
    void Start()
    {
        timeOverText.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        //ゲームタイマー更新
        timer -= Time.deltaTime;
        timerText.text = "" + (int)timer;

        //タイマーが0になったら
        if(timer < 0)
        {
            timer = 0;
            timerText.text = "0";
            timeOverText.SetActive (true);
            Invoke("Retry", 1.5f);
        }
    }

    //シーン遷移
    void Retry()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}

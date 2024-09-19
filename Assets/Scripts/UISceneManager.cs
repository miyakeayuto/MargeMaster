using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UISceneManager : MonoBehaviour
{
    public float timer;                             //タイマー
    [SerializeField] GameObject pause;
    [SerializeField] Text timerText;                //タイマーテキスト
    [SerializeField] GameObject clearUI;            //クリア時に出すUI
    [SerializeField] GameObject gameoverUI;         //ゲームオーバー時に出すUI
    [SerializeField] GameObject posePanel;          //ポーズUI

    void Awake()
    {//シーン遷移しても削除されない
        //DontDestroyOnLoad(this.gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {
        timer = 20;
        clearUI.SetActive(false);
        gameoverUI.SetActive(false);
        posePanel.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        //ゲームタイマー更新
        timer -= Time.deltaTime;
        timerText.text = "" + (int)timer;

        //タイマーが10秒切ったら
        if (timer <= 10)
        {
            //色を黄色にする
            timerText.color = Color.yellow;
        }
        //タイマーが5秒切ったら
        if (timer <= 5)
        {
            //色を赤にする
            timerText.color = Color.red;
        }
        //タイマーが0になったら
        if (timer < 0)
        {
            timer = 0;
            timerText.text = "0";
            GameOver();
        }
    }

    //シーン遷移
    void Retry()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    //次のステージ
    public void Next()
    {
        //現在のシーンのインデックス番号を取得
        int nowSceneIndexNumber = SceneManager.GetActiveScene().buildIndex;

        SceneManager.LoadScene(++nowSceneIndexNumber);
    }

    public void StageSelect()
    {
        SceneManager.LoadScene("StageSelect");
    }

    //クリア
    public void Clear()
    {
        pause.SetActive(false);
        clearUI.SetActive(true);

        //Updateに入らないようにする
        enabled = false;

        //Updateから抜ける
        return;
    }

    public void GameOver()
    {
        pause.SetActive(false);
        gameoverUI.SetActive(true);

        //Updateに入らないようにする
        enabled = false;

        //Updateから抜ける
        return;
    }

    //ポーズ画面
    public void GamePose()
    {
        posePanel.SetActive(true);

        //Updateに入らないようにする
        enabled = false;

        //Updateから抜ける
        return;
    }

    //ポーズ画面（リトライ関数）
    public void Restart()
    {
        posePanel.SetActive(false);
        Invoke("Retry", 0.5f);
    }

    //ポーズ画面（ステージを抜ける）
    public void StageExit()
    {
        posePanel.SetActive(false);
        SceneManager.LoadScene("StageSelect");
    }

    //ゲーム画面にもどる
    public void BackGame()
    {
        posePanel.SetActive(false);
        enabled = true;
    }

    //ポーズかどうか
    public bool IsPause()
    {
        //enabledを返してtrueだったらポーズ状態
        return !enabled;
    }
}
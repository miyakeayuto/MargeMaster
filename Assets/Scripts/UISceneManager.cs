using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UISceneManager : MonoBehaviour
{
    [SerializeField] float timer;                   //タイマー
    [SerializeField] Text timerText;                //タイマーテキスト
    [SerializeField] GameObject timeOverText;       //時間切れのテキスト
    [SerializeField] GameObject clearUI;            //クリア時に出すUI
    [SerializeField] GameObject posePanel;          //ポーズUI

    void Awake()
    {//シーン遷移しても削除されない
        DontDestroyOnLoad(this.gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {
        timeOverText.SetActive(false);
        clearUI.SetActive(false);
        posePanel.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        //ゲームタイマー更新
        timer -= Time.deltaTime;
        timerText.text = "" + (int)timer;

        //タイマーが0になったら
        if (timer < 0)
        {
            timer = 0;
            timerText.text = "0";
            timeOverText.SetActive(true);
            Invoke("Retry", 1.5f);
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
        clearUI.SetActive(true);

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
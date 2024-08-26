//===================================
//タイトル
//2024/7/19
//三宅歩人
//===================================
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TitleManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    private void Update()
    {
        
    }

    //ゲームスタート
    public void OnClickStart()
    {
        //ユーザー登録
        bool isSuccess = NetWorkManager.Instance.LoadUserData();
        if(!isSuccess)
        {
            //ユーザーデータが保存されていない場合は登録
            StartCoroutine(NetWorkManager.Instance.RegistUser(
                Guid.NewGuid().ToString(),          //名前                        ※Guid=>128bitの重複しないランダムな値。名前を入力させるならUIから取得。
                result =>               //登録終了後の処理
                {
                    //画面遷移
                    SceneManager.LoadScene("StageSelect");
                }));
        }
        else
        {
            //ユーザーデータが保存されている場合は登録せず次の画面へ
            //画面遷移
            SceneManager.LoadScene("StageSelect");
        }
    }

    //ゲーム終了
    public void OnClickExit()
    {
#if UNITY_EDITOR    //Unityエディタの場合
            UnityEditor.EditorApplication.isPlaying = false;
#else   //ビルドの場合
            Application.Quit();
#endif
    }

    //オプション
    public void OnClickOption()
    {

    }
}

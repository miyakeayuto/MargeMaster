//===================================
//タイトル
//2024/7/19
//三宅歩人
//===================================
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TitleManager : MonoBehaviour
{
    [SerializeField] GameObject CreateStagePanel;

    // Start is called before the first frame update
    void Start()
    {
        CreateStagePanel.SetActive(false);
    }

    // Update is called once per frame
    private void Update()
    {
        
    }

    IEnumerator cheackCatalog()
    {
        var checkHandle = Addressables.CheckForCatalogUpdates(false);
        yield return checkHandle;
        var updates = checkHandle.Result;
        Addressables.Release(checkHandle);
        if (updates.Count >= 1)
        {
            //更新がある場合はロード画面へ
            //画面遷移
            SceneManager.LoadScene("LoadScene");
        }
        else
        {
            //更新がない場合はステージ選択画面へ
            SceneManager.LoadScene("StageSelect");
        }
    }

    //ゲームスタート
    public void OnClickStart()
    {
        StartCoroutine(cheackCatalog());

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
    public void OnClickCreateStage()
    {
        CreateStagePanel.SetActive(true);
    }

    public void OnClickPlay()
    {
        //画面遷移
        SceneManager.LoadScene("CreateStageIndex");
    }

    public void OnClickEdit()
    {
        //画面遷移
        SceneManager.LoadScene("CreateStageEdit");
    }
}

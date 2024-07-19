//===================================
//タイトル
//2024/7/19
//三宅歩人
//===================================
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
        SceneManager.LoadScene("1-1");
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

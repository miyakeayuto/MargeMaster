//====================================
//ステージセレクト
//2024/7/24
//三宅歩人
//====================================
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StageSelectManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //1-1
    public void GoStarg1_1()
    {
        SceneManager.LoadScene("1-1");
    }

    //1-2
    public void GoStarg1_2()
    {
        SceneManager.LoadScene("1-2");
    }

    //1-3
    public void GoStarg1_3()
    {
        SceneManager.LoadScene("1-3");
    }

    //1-4
    public void GoStarg1_4()
    {
        SceneManager.LoadScene("1-4");
    }

    //1-5
    public void GoStarg1_5()
    {
        SceneManager.LoadScene("1-5");
    }

    //2-1
    public void GoStarg2_1()
    {
        SceneManager.LoadScene("2-1");
    }

    //2-2
    public void GoStarg2_2()
    {
        SceneManager.LoadScene("2-2");
    }
}

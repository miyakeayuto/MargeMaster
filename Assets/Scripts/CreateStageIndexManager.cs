using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class CreateStageIndexManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnClickBack()
    {
        //画面遷移
        SceneManager.LoadScene("Title");
    }

    public void OnClickNomalStage()
    {
        //画面遷移
        SceneManager.LoadScene("StageSelect");
    }
}

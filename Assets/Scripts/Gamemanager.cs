using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Gamemanager : MonoBehaviour
{
    public int margeCounter;       //���񍇑̂��Ȃ��Ƃ����Ȃ���

    // Start is called before the first frame update
    void Awake()
    {
        //UI�V�[����ǂݍ���
        SceneManager.LoadScene("UI",LoadSceneMode.Additive);
    }

    // Update is called once per frame
    void Update()
    {

    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Gamemanager : MonoBehaviour
{
    public int margeCounter;       //���񍇑̂��Ȃ��Ƃ����Ȃ���

    void Awake()
    {
        //UI�V�[����ǂݍ���
        SceneManager.LoadScene("UI",LoadSceneMode.Additive);
    }
}

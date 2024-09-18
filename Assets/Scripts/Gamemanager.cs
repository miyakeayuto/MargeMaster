using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Gamemanager : MonoBehaviour
{
    [SerializeField] float timer;                   //�^�C�}�[
    [SerializeField] Text timerText;                //�^�C�}�[�e�L�X�g
    [SerializeField] GameObject timeOverText;       //���Ԑ؂�̃e�L�X�g
    [SerializeField] GameObject clearUI;            //�N���A���ɏo��UI
    [SerializeField] GameObject posePanel;          //�|�[�YUI

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

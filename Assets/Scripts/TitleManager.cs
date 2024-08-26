//===================================
//�^�C�g��
//2024/7/19
//�O����l
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

    //�Q�[���X�^�[�g
    public void OnClickStart()
    {
        //���[�U�[�o�^
        bool isSuccess = NetWorkManager.Instance.LoadUserData();
        if(!isSuccess)
        {
            //���[�U�[�f�[�^���ۑ�����Ă��Ȃ��ꍇ�͓o�^
            StartCoroutine(NetWorkManager.Instance.RegistUser(
                Guid.NewGuid().ToString(),          //���O                        ��Guid=>128bit�̏d�����Ȃ������_���Ȓl�B���O����͂�����Ȃ�UI����擾�B
                result =>               //�o�^�I����̏���
                {
                    //��ʑJ��
                    SceneManager.LoadScene("StageSelect");
                }));
        }
        else
        {
            //���[�U�[�f�[�^���ۑ�����Ă���ꍇ�͓o�^�������̉�ʂ�
            //��ʑJ��
            SceneManager.LoadScene("StageSelect");
        }
    }

    //�Q�[���I��
    public void OnClickExit()
    {
#if UNITY_EDITOR    //Unity�G�f�B�^�̏ꍇ
            UnityEditor.EditorApplication.isPlaying = false;
#else   //�r���h�̏ꍇ
            Application.Quit();
#endif
    }

    //�I�v�V����
    public void OnClickOption()
    {

    }
}

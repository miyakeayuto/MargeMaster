//===================================
//�^�C�g��
//2024/7/19
//�O����l
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

    //�Q�[���X�^�[�g
    public void OnClickStart()
    {
        SceneManager.LoadScene("1-1");
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

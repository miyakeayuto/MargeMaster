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

    // Start is called before the first frame update
    void Start()
    {
        timeOverText.SetActive(false);
        clearUI.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        //�Q�[���^�C�}�[�X�V
        timer -= Time.deltaTime;
        timerText.text = "" + (int)timer;

        //�^�C�}�[��0�ɂȂ�����
        if(timer < 0)
        {
            timer = 0;
            timerText.text = "0";
            timeOverText.SetActive (true);
            Invoke("Retry", 1.5f);
        }
    }

    //�V�[���J��
    void Retry()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    //���̃X�e�[�W
    public void Next()
    {
        SceneManager.LoadScene("1-2");
    }

    //�N���A
    public void Clear()
    {
        clearUI.SetActive (true);
    }
}
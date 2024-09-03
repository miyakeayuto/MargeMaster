using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UISceneManager : MonoBehaviour
{
    [SerializeField] float timer;                   //�^�C�}�[
    [SerializeField] Text timerText;                //�^�C�}�[�e�L�X�g
    [SerializeField] GameObject timeOverText;       //���Ԑ؂�̃e�L�X�g
    [SerializeField] GameObject clearUI;            //�N���A���ɏo��UI
    [SerializeField] GameObject posePanel;          //�|�[�YUI

    void Awake()
    {//�V�[���J�ڂ��Ă��폜����Ȃ�
        DontDestroyOnLoad(this.gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {
        timeOverText.SetActive(false);
        clearUI.SetActive(false);
        posePanel.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        //�Q�[���^�C�}�[�X�V
        timer -= Time.deltaTime;
        timerText.text = "" + (int)timer;

        //�^�C�}�[��0�ɂȂ�����
        if (timer < 0)
        {
            timer = 0;
            timerText.text = "0";
            timeOverText.SetActive(true);
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
        //���݂̃V�[���̃C���f�b�N�X�ԍ����擾
        int nowSceneIndexNumber = SceneManager.GetActiveScene().buildIndex;

        SceneManager.LoadScene(++nowSceneIndexNumber);
    }

    public void StageSelect()
    {
        SceneManager.LoadScene("StageSelect");
    }

    //�N���A
    public void Clear()
    {
        clearUI.SetActive(true);

        //Update�ɓ���Ȃ��悤�ɂ���
        enabled = false;

        //Update���甲����
        return;
    }

    //�|�[�Y���
    public void GamePose()
    {
        posePanel.SetActive(true);

        //Update�ɓ���Ȃ��悤�ɂ���
        enabled = false;

        //Update���甲����
        return;
    }

    //�|�[�Y��ʁi���g���C�֐��j
    public void Restart()
    {
        posePanel.SetActive(false);
        Invoke("Retry", 0.5f);
    }

    //�|�[�Y��ʁi�X�e�[�W�𔲂���j
    public void StageExit()
    {
        posePanel.SetActive(false);
        SceneManager.LoadScene("StageSelect");
    }

    //�Q�[����ʂɂ��ǂ�
    public void BackGame()
    {
        posePanel.SetActive(false);
        enabled = true;
    }

    //�|�[�Y���ǂ���
    public bool IsPause()
    {
        //enabled��Ԃ���true��������|�[�Y���
        return !enabled;
    }
}
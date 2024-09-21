using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Networking;

public class NetWorkManager : MonoBehaviour
{
    const string API_BASE_URL = "http://localhost:8000/api/";
    private int userID = 0;                     //�����̃��[�U�[ID
    private string userName = "";               //�����̃��[�U�[��

    private static NetWorkManager instance;         //get�v���p�e�B���Ăяo�������񎞂ɃC���X�^���X��������static�ŕێ�
    public static NetWorkManager Instance
    {
        get
        {
            if (instance == null)
            {
                GameObject gameObj = new GameObject("NetWorkManager");
                instance = gameObj.AddComponent<NetWorkManager>();          //GameObject�𐶐�����NetWorkManager�R���|�[�l���g��ǉ�
                DontDestroyOnLoad(gameObj);                                 //�I�u�W�F�N�g���V�[���J�ڎ��ɍ폜���Ȃ��悤�ɂ���
            }
            return instance;
        }
    }

    //================================================================
    //�ʐM����������ۂ�NetWorkManager.Instance.�֐���();�ƌĂяo���B
    //================================================================

    //���[�U�[�o�^����
    public IEnumerator RegistUser(string name, Action<bool> result)             //Action<bool>�^=>bool�������ɂ����֐�������^�B�ʐM�������ɌĂяo���B
    {
        //�T�[�o�[�ɑ��M����I�u�W�F�N�g���쐬
        RegistUserRequest requestData = new RegistUserRequest();
        requestData.Name = name;
        //�T�[�o�[�ɑ��M����I�u�W�F�N�g��JSON�ɕϊ�
        string json = JsonConvert.SerializeObject(requestData);
        //���M                                 post�܂���get
        UnityWebRequest request = UnityWebRequest.Post(
            API_BASE_URL + "users/store", json, "application/json");
        yield return request.SendWebRequest();      //���ʂ���M����܂őҋ@
        bool isSuccess = false;
        if(request.result == UnityWebRequest.Result.Success
            && request.responseCode == 200)
        {//�ʐM�����������ꍇ�A�A���Ă���JSON���I�u�W�F�N�g�ɕϊ�
            string resultJson = request.downloadHandler.text;       //���X�|���X�{�f�B(json)�̕�������擾
            RegistUserResponse response =
                JsonConvert.DeserializeObject<RegistUserResponse>(resultJson);          //json���f�V���A���C�Y
            //�t�@�C���Ƀ��[�U�[ID��ۑ�
            this.userName = name;
            this.userID = response.UserID;
            SaveUserData();
            isSuccess = true;
        }
        result?.Invoke(isSuccess);          //�����ŌĂяo������result�������Ăяo��
    }

    //���[�U�[����ۑ�����
    private void SaveUserData()
    {
        SaveData saveData = new SaveData();
        saveData.Name = this.userName;
        saveData.UserID = this.userID;
        string json = JsonConvert.SerializeObject(saveData);
        var writer =
            new StreamWriter(Application.persistentDataPath + "/saveData.json");    //StreamWriter�N���X�Ńt�@�C����JSON��ۑ��B
        //Application.persistentDataPath�̓A�v���̕ۑ��t�@�C����u���ꏊ�BOS���ɕς��Ă����B
        writer.Write(json);
        writer.Flush();
        writer.Close();
    }

    //���[�U�[����ǂݍ���
    public bool LoadUserData()
    {
        if(!File.Exists(Application.persistentDataPath + "/saveData.json"))
        {
            return false;
        }
        var reader =
            new StreamReader(Application.persistentDataPath + "/saveData.json");
        string json = reader.ReadToEnd();
        reader.Close();
        SaveData saveData = JsonConvert.DeserializeObject<SaveData>(json);
        this.userID = saveData.UserID;      //���[�J���t�@�C�����烆�[�U�[���ƃ��[�U�[ID���擾
        this.userName = saveData.Name;
        return true;                        //�ǂݍ��񂾂��ǂ���
    }
}

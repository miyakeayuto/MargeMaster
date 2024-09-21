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
    private int userID = 0;                     //自分のユーザーID
    private string userName = "";               //自分のユーザー名

    private static NetWorkManager instance;         //getプロパティを呼び出した初回時にインスタンス生成してstaticで保持
    public static NetWorkManager Instance
    {
        get
        {
            if (instance == null)
            {
                GameObject gameObj = new GameObject("NetWorkManager");
                instance = gameObj.AddComponent<NetWorkManager>();          //GameObjectを生成してNetWorkManagerコンポーネントを追加
                DontDestroyOnLoad(gameObj);                                 //オブジェクトをシーン遷移時に削除しないようにする
            }
            return instance;
        }
    }

    //================================================================
    //通信処理をする際はNetWorkManager.Instance.関数名();と呼び出す。
    //================================================================

    //ユーザー登録処理
    public IEnumerator RegistUser(string name, Action<bool> result)             //Action<bool>型=>boolを引数にした関数を入れる型。通信完了時に呼び出す。
    {
        //サーバーに送信するオブジェクトを作成
        RegistUserRequest requestData = new RegistUserRequest();
        requestData.Name = name;
        //サーバーに送信するオブジェクトをJSONに変換
        string json = JsonConvert.SerializeObject(requestData);
        //送信                                 postまたはget
        UnityWebRequest request = UnityWebRequest.Post(
            API_BASE_URL + "users/store", json, "application/json");
        yield return request.SendWebRequest();      //結果を受信するまで待機
        bool isSuccess = false;
        if(request.result == UnityWebRequest.Result.Success
            && request.responseCode == 200)
        {//通信が成功した場合、帰ってきたJSONをオブジェクトに変換
            string resultJson = request.downloadHandler.text;       //レスポンスボディ(json)の文字列を取得
            RegistUserResponse response =
                JsonConvert.DeserializeObject<RegistUserResponse>(resultJson);          //jsonをデシリアライズ
            //ファイルにユーザーIDを保存
            this.userName = name;
            this.userID = response.UserID;
            SaveUserData();
            isSuccess = true;
        }
        result?.Invoke(isSuccess);          //ここで呼び出し元のresult処理を呼び出す
    }

    //ユーザー情報を保存する
    private void SaveUserData()
    {
        SaveData saveData = new SaveData();
        saveData.Name = this.userName;
        saveData.UserID = this.userID;
        string json = JsonConvert.SerializeObject(saveData);
        var writer =
            new StreamWriter(Application.persistentDataPath + "/saveData.json");    //StreamWriterクラスでファイルにJSONを保存。
        //Application.persistentDataPathはアプリの保存ファイルを置く場所。OS毎に変えてくれる。
        writer.Write(json);
        writer.Flush();
        writer.Close();
    }

    //ユーザー情報を読み込む
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
        this.userID = saveData.UserID;      //ローカルファイルからユーザー名とユーザーIDを取得
        this.userName = saveData.Name;
        return true;                        //読み込んだかどうか
    }
}

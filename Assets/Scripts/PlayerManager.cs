//=======================================
//プレイヤー処理
//2024/7/10
//三宅歩人
//=======================================
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.EventSystems;

public class PlayerManager : MonoBehaviour
{
    [SerializeField] PlayerManager playerPrefabs;           //プレイヤーのプレハブ
    [SerializeField] LayerMask blockLayer;
    [SerializeField] float speed = 3f;                      //移動速度
    [SerializeField] float playerSpeed;
    [SerializeField] float jumpPower = 150;                 //ジャンプ力
    [SerializeField] Text textTimer;                        //タイマーのテキスト
    [SerializeField] bool isDoMarge;                        //マージさせるかどうか
    Rigidbody2D rigidbody;
    public UISceneManager uiManager;
    public Gamemanager gamemanager;
    public bool isLeft;                                     //←ボタンを押したかどうか
    public bool isRight;                                    //→ボタンを押したかどうか
    public bool isJump;

    // Start is called before the first frame update
    void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        uiManager = GameObject.Find("UIManager").GetComponent<UISceneManager>();
        gamemanager = GameObject.Find("Gamemanager").GetComponent<Gamemanager>();
        isLeft = false;
        isRight = false;
    }

    // Update is called once per frame
    void Update()
    {
        //ポーズ中かどうか
        if (uiManager.IsPause())
        {
            //Updateから抜ける
            return;
        }

        //制限時間が0だったら
        if(uiManager.timer == 0)
        {
            //Updateに入らないようにする
            enabled = false;
            //Updateから抜ける
            return;
        }

        //========================
        //移動処理
        //========================

        // Aキーを押したら左方向へ進む
        if (Input.GetKey(KeyCode.A)) playerSpeed = -speed;
        // Dキーを押したら右方向へ進む
        else if (Input.GetKey(KeyCode.D)) playerSpeed = speed;
        // 何もおさなかったら止まる
        else playerSpeed = 0;

        if(isLeft)
        {
            playerSpeed = -speed;
        }else if(isRight)
        {
            playerSpeed = speed;
        }

        rigidbody.velocity = new Vector2(playerSpeed, rigidbody.velocity.y);
    }

    //スマホ操作
    //←ボタンを押したとき
    public void MoveLeft()
    {
        isLeft = true;
    }

    //→ボタンを押したとき
    public void MoveRight()
    {
        isRight = true;
    }

    //←ボタンを離した場合
    public void NotMoveLeft()
    {
        isLeft = false;
    }

    //→ボタンを離した場合
    public void NotMoveRight()
    {
        isRight = false;
    }

    //ジャンプボタン押した場合
    public void MoveJump()
    {
        if(isJump)
        {
            Jump();
        }
    }

    /// <summary>
    /// ジャンプ
    /// </summary>
    private void Jump()
    {
        //プレイヤーを上方向に力を加える
        rigidbody.AddForce(Vector2.up * jumpPower);
        transform.localPosition += new Vector3(0, 0.1f, 0);         //（※）ジャンプした時に終点の高さ分を足す
        isJump = false;
    }


    //当たり合判定が発生したら呼ばれる
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "player_nomal" || collision.gameObject.tag == "player_red" || collision.gameObject.tag == "player_blue")
        {
            if (isDoMarge)
            {
                //合体させる
                Merge(this, collision.gameObject.GetComponent<PlayerManager>());
                if (gamemanager.margeCounter == 0)
                {
                    //クリアUI表示関数呼び出し
                    uiManager.Clear();
                }
            }
        }

        if(collision.gameObject.tag == "ground")
        {
            isJump = true;
        }

        //棘に当たった時
        if(collision.gameObject.tag == "Spike")
        {
            //player_redタグ以外のオブジェクトの場合
            if (gameObject.tag != "player_red")
            {
                //ゲームオーバー
                uiManager.GameOver();
                //Updateに入らないようにする
                enabled = false;
                //Updateから抜ける
                return;
            }
        }
    }

    //自機を合体させる
    public void Merge(PlayerManager playerA, PlayerManager playerB)
    {
        //2点間の中心
        Vector2 lerpPosition =
            Vector2.Lerp(playerA.transform.position, playerB.transform.position, 0.5f);

        PlayerManager player;

        //合体した自機（タグ）がデフォ以外だった場合
        if((playerA.tag == "player_red" && playerB.tag == "player_blue") || (playerA.tag == "player_blue" && playerB.tag == "player_red"))
        {
            //新しい自機を生成
            player = Instantiate(playerPrefabs, lerpPosition, Quaternion.identity);

            //タグとレイヤーを変更させて能力を継承
            player.tag = "player_red";
            player.blockLayer = 7;
        }
        else if(playerA.tag == "player_red" || playerB.tag == "player_red")
        {
            //新しい自機を生成
            player = Instantiate(playerPrefabs, lerpPosition, Quaternion.identity);
            player.tag = "player_red";
        }
        else if(playerA.tag == "player_blue" || playerB.tag == "player_blue")
        {
            //新しい自機を生成
            player = Instantiate(playerPrefabs, lerpPosition, Quaternion.identity);
            player.blockLayer = 7;
        }
        else
        {//デフォ同士の合体
            //新しい自機を生成
            player = Instantiate(playerPrefabs, lerpPosition, Quaternion.identity);
        }

        //シーンから削除
        Destroy(playerA.gameObject);
        Destroy(playerB.gameObject);


        //=============================
        //生成された自機をボタンに登録
        //=============================

        //左
        //押してる間のボタン
        EventTrigger.Entry entry = new EventTrigger.Entry();
        entry.eventID = EventTriggerType.PointerDown;   //PointerClickの部分は追加したいEventによって変更する
        entry.callback.AddListener((x) => player.MoveLeft());  //ラムダ式の右側は追加するメソッド
        //move_leftを探してEventTriggerに登録
        GameObject.Find("move_left").GetComponent<EventTrigger>().triggers.Add(entry);

        //ボタンを離したとき
        entry = new EventTrigger.Entry();
        entry.eventID = EventTriggerType.PointerUp;
        entry.callback.AddListener((x) => player.NotMoveLeft());
        GameObject.Find("move_left").GetComponent<EventTrigger>().triggers.Add(entry);


        //右
        //押してる間のボタン
        entry = new EventTrigger.Entry();
        entry.eventID = EventTriggerType.PointerDown;
        entry.callback.AddListener((x) => player.MoveRight());
        //move_rightを探してEventTriggerに登録
        GameObject.Find("move_right").GetComponent<EventTrigger>().triggers.Add(entry);

        //ボタンを離したとき
        entry = new EventTrigger.Entry();
        entry.eventID = EventTriggerType.PointerUp;
        entry.callback.AddListener((x) => player.NotMoveRight());
        GameObject.Find("move_right").GetComponent<EventTrigger>().triggers.Add(entry);


        //ジャンプ
        entry = new EventTrigger.Entry();
        entry.eventID = EventTriggerType.PointerClick;
        entry.callback.AddListener((x) => player.MoveJump());
        //move_jumpを探してEventTriggerに登録
        GameObject.Find("move_jump").GetComponent<EventTrigger>().triggers.Add(entry);

        //合体カウンターを減らす
        gamemanager.margeCounter--;
    }
}
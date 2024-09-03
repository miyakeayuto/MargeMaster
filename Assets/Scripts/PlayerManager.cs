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
    public bool isLeft;                                     //←ボタンを押したかどうか
    public bool isRight;                                    //→ボタンを押したかどうか

    // Start is called before the first frame update
    void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        uiManager = GameObject.Find("UIManager").GetComponent<UISceneManager>();
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
        if(textTimer.text == "0")
        {
            //Updateに入らないようにする
            enabled = false;
            //Updateから抜ける
            return;
        }

        //==========
        //移動処理
        //==========

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

    public void MoveJump()
    {
        Jump();
    }

    /// <summary>
    /// ジャンプ
    /// </summary>
    private void Jump()
    {
        //プレイヤーを上方向に力を加える
        rigidbody.AddForce(Vector2.up * jumpPower);
        transform.localPosition += new Vector3(0, 0.1f, 0);         //（※）ジャンプした時に終点の高さ分を足す
    }


    //当たり合判定が発生したら呼ばれる
    private void OnCollisionEnter2D(Collision2D collision)
    {
        PlayerManager player = collision.gameObject.GetComponent<PlayerManager>();

        //バブルじゃなければ抜ける
        if (!player) return;

        if(isDoMarge)
        {
            //クリアUI表示関数呼び出し
            uiManager.Clear();
            //合体させる
            Merge(this, player);
        }
    }

    //自機を合体させる
    public void Merge(PlayerManager playerA, PlayerManager playerB)
    {
        //2点間の中心
        Vector2 lerpPosition =
            Vector2.Lerp(playerA.transform.position, playerB.transform.position, 0.5f);

        //新しい自機を生成
        Instantiate(playerPrefabs,lerpPosition,Quaternion.identity);

        //シーンから削除
        Destroy(playerA.gameObject);
        Destroy(playerB.gameObject);
    }
}
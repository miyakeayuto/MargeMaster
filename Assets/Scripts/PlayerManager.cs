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
    bool isMarge;                                           //合体したかどうか
    Rigidbody2D rigidbody;
    public Gamemanager gamemanager;

    // Start is called before the first frame update
    void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if(textTimer.text == "0")
        {
            //Updateに入らないようにする
            enabled = false;
            //Updateから抜ける
            return;
        }
        //移動処理

        // Aキーを押したら左方向へ進む
        if (Input.GetKey(KeyCode.A)) playerSpeed = -speed;
        // Dキーを押したら右方向へ進む
        else if (Input.GetKey(KeyCode.D)) playerSpeed = speed;
        // 何もおさなかったら止まる
        else playerSpeed = 0;

        rigidbody.velocity = new Vector2(playerSpeed, rigidbody.velocity.y);


        //ジャンプ処理
        if (Input.GetKeyDown("space"))
        {//スペースキーが押されたら
            Jump();
        }

        //クリア関数呼び出し
        if (isMarge)
        {//合体フラグが建ったら
            gamemanager.Clear();
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
    }


    //当たり合判定が発生したら呼ばれる
    private void OnCollisionEnter2D(Collision2D collision)
    {
        PlayerManager player = collision.gameObject.GetComponent<PlayerManager>();

        //バブルじゃなければ抜ける
        if (!player) return;

        if(isDoMarge)
        {
            //合体フラグを建たせる
            isMarge = true;
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
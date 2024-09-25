//=======================================
//�v���C���[����
//2024/7/10
//�O����l
//=======================================
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.EventSystems;

public class PlayerManager : MonoBehaviour
{
    [SerializeField] PlayerManager playerPrefabs;           //�v���C���[�̃v���n�u
    [SerializeField] LayerMask blockLayer;
    [SerializeField] float speed = 3f;                      //�ړ����x
    [SerializeField] float playerSpeed;
    [SerializeField] float jumpPower = 150;                 //�W�����v��
    [SerializeField] Text textTimer;                        //�^�C�}�[�̃e�L�X�g
    [SerializeField] bool isDoMarge;                        //�}�[�W�����邩�ǂ���
    Rigidbody2D rigidbody;
    public UISceneManager uiManager;
    public Gamemanager gamemanager;
    public bool isLeft;                                     //���{�^�������������ǂ���
    public bool isRight;                                    //���{�^�������������ǂ���
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
        //�|�[�Y�����ǂ���
        if (uiManager.IsPause())
        {
            //Update���甲����
            return;
        }

        //�������Ԃ�0��������
        if(uiManager.timer == 0)
        {
            //Update�ɓ���Ȃ��悤�ɂ���
            enabled = false;
            //Update���甲����
            return;
        }

        //========================
        //�ړ�����
        //========================

        // A�L�[���������獶�����֐i��
        if (Input.GetKey(KeyCode.A)) playerSpeed = -speed;
        // D�L�[����������E�����֐i��
        else if (Input.GetKey(KeyCode.D)) playerSpeed = speed;
        // ���������Ȃ�������~�܂�
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

    //�X�}�z����
    //���{�^�����������Ƃ�
    public void MoveLeft()
    {
        isLeft = true;
    }

    //���{�^�����������Ƃ�
    public void MoveRight()
    {
        isRight = true;
    }

    //���{�^���𗣂����ꍇ
    public void NotMoveLeft()
    {
        isLeft = false;
    }

    //���{�^���𗣂����ꍇ
    public void NotMoveRight()
    {
        isRight = false;
    }

    //�W�����v�{�^���������ꍇ
    public void MoveJump()
    {
        if(isJump)
        {
            Jump();
        }
    }

    /// <summary>
    /// �W�����v
    /// </summary>
    private void Jump()
    {
        //�v���C���[��������ɗ͂�������
        rigidbody.AddForce(Vector2.up * jumpPower);
        transform.localPosition += new Vector3(0, 0.1f, 0);         //�i���j�W�����v�������ɏI�_�̍������𑫂�
        isJump = false;
    }


    //�����荇���肪����������Ă΂��
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "player_nomal" || collision.gameObject.tag == "player_red" || collision.gameObject.tag == "player_blue")
        {
            if (isDoMarge)
            {
                //���̂�����
                Merge(this, collision.gameObject.GetComponent<PlayerManager>());
                if (gamemanager.margeCounter == 0)
                {
                    //�N���AUI�\���֐��Ăяo��
                    uiManager.Clear();
                }
            }
        }

        if(collision.gameObject.tag == "ground")
        {
            isJump = true;
        }

        //���ɓ���������
        if(collision.gameObject.tag == "Spike")
        {
            //player_red�^�O�ȊO�̃I�u�W�F�N�g�̏ꍇ
            if (gameObject.tag != "player_red")
            {
                //�Q�[���I�[�o�[
                uiManager.GameOver();
                //Update�ɓ���Ȃ��悤�ɂ���
                enabled = false;
                //Update���甲����
                return;
            }
        }
    }

    //���@�����̂�����
    public void Merge(PlayerManager playerA, PlayerManager playerB)
    {
        //2�_�Ԃ̒��S
        Vector2 lerpPosition =
            Vector2.Lerp(playerA.transform.position, playerB.transform.position, 0.5f);

        PlayerManager player;

        //���̂������@�i�^�O�j���f�t�H�ȊO�������ꍇ
        if((playerA.tag == "player_red" && playerB.tag == "player_blue") || (playerA.tag == "player_blue" && playerB.tag == "player_red"))
        {
            //�V�������@�𐶐�
            player = Instantiate(playerPrefabs, lerpPosition, Quaternion.identity);

            //�^�O�ƃ��C���[��ύX�����Ĕ\�͂��p��
            player.tag = "player_red";
            player.blockLayer = 7;
        }
        else if(playerA.tag == "player_red" || playerB.tag == "player_red")
        {
            //�V�������@�𐶐�
            player = Instantiate(playerPrefabs, lerpPosition, Quaternion.identity);
            player.tag = "player_red";
        }
        else if(playerA.tag == "player_blue" || playerB.tag == "player_blue")
        {
            //�V�������@�𐶐�
            player = Instantiate(playerPrefabs, lerpPosition, Quaternion.identity);
            player.blockLayer = 7;
        }
        else
        {//�f�t�H���m�̍���
            //�V�������@�𐶐�
            player = Instantiate(playerPrefabs, lerpPosition, Quaternion.identity);
        }

        //�V�[������폜
        Destroy(playerA.gameObject);
        Destroy(playerB.gameObject);


        //=============================
        //�������ꂽ���@���{�^���ɓo�^
        //=============================

        //��
        //�����Ă�Ԃ̃{�^��
        EventTrigger.Entry entry = new EventTrigger.Entry();
        entry.eventID = EventTriggerType.PointerDown;   //PointerClick�̕����͒ǉ�������Event�ɂ���ĕύX����
        entry.callback.AddListener((x) => player.MoveLeft());  //�����_���̉E���͒ǉ����郁�\�b�h
        //move_left��T����EventTrigger�ɓo�^
        GameObject.Find("move_left").GetComponent<EventTrigger>().triggers.Add(entry);

        //�{�^���𗣂����Ƃ�
        entry = new EventTrigger.Entry();
        entry.eventID = EventTriggerType.PointerUp;
        entry.callback.AddListener((x) => player.NotMoveLeft());
        GameObject.Find("move_left").GetComponent<EventTrigger>().triggers.Add(entry);


        //�E
        //�����Ă�Ԃ̃{�^��
        entry = new EventTrigger.Entry();
        entry.eventID = EventTriggerType.PointerDown;
        entry.callback.AddListener((x) => player.MoveRight());
        //move_right��T����EventTrigger�ɓo�^
        GameObject.Find("move_right").GetComponent<EventTrigger>().triggers.Add(entry);

        //�{�^���𗣂����Ƃ�
        entry = new EventTrigger.Entry();
        entry.eventID = EventTriggerType.PointerUp;
        entry.callback.AddListener((x) => player.NotMoveRight());
        GameObject.Find("move_right").GetComponent<EventTrigger>().triggers.Add(entry);


        //�W�����v
        entry = new EventTrigger.Entry();
        entry.eventID = EventTriggerType.PointerClick;
        entry.callback.AddListener((x) => player.MoveJump());
        //move_jump��T����EventTrigger�ɓo�^
        GameObject.Find("move_jump").GetComponent<EventTrigger>().triggers.Add(entry);

        //���̃J�E���^�[�����炷
        gamemanager.margeCounter--;
    }
}
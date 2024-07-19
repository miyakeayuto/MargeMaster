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

public class PlayerManager : MonoBehaviour
{
    [SerializeField] PlayerManager playerPrefabs;           //�v���C���[�̃v���n�u
    [SerializeField] LayerMask blockLayer;
    [SerializeField] float speed = 3f;                      //�ړ����x
    [SerializeField] float playerSpeed;
    [SerializeField] float jumpPower = 150;                 //�W�����v��
    [SerializeField] Text textTimer;                        //�^�C�}�[�̃e�L�X�g
    [SerializeField] bool isDoMarge;                        //�}�[�W�����邩�ǂ���
    bool isMarge;                                           //���̂������ǂ���
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
            //Update�ɓ���Ȃ��悤�ɂ���
            enabled = false;
            //Update���甲����
            return;
        }
        //�ړ�����

        // A�L�[���������獶�����֐i��
        if (Input.GetKey(KeyCode.A)) playerSpeed = -speed;
        // D�L�[����������E�����֐i��
        else if (Input.GetKey(KeyCode.D)) playerSpeed = speed;
        // ���������Ȃ�������~�܂�
        else playerSpeed = 0;

        rigidbody.velocity = new Vector2(playerSpeed, rigidbody.velocity.y);


        //�W�����v����
        if (Input.GetKeyDown("space"))
        {//�X�y�[�X�L�[�������ꂽ��
            Jump();
        }

        //�N���A�֐��Ăяo��
        if (isMarge)
        {//���̃t���O����������
            gamemanager.Clear();
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
    }


    //�����荇���肪����������Ă΂��
    private void OnCollisionEnter2D(Collision2D collision)
    {
        PlayerManager player = collision.gameObject.GetComponent<PlayerManager>();

        //�o�u������Ȃ���Δ�����
        if (!player) return;

        if(isDoMarge)
        {
            //���̃t���O����������
            isMarge = true;
            //���̂�����
            Merge(this, player);
        }
    }

    //���@�����̂�����
    public void Merge(PlayerManager playerA, PlayerManager playerB)
    {
        //2�_�Ԃ̒��S
        Vector2 lerpPosition =
            Vector2.Lerp(playerA.transform.position, playerB.transform.position, 0.5f);

        //�V�������@�𐶐�
        Instantiate(playerPrefabs,lerpPosition,Quaternion.identity);

        //�V�[������폜
        Destroy(playerA.gameObject);
        Destroy(playerB.gameObject);
    }

}
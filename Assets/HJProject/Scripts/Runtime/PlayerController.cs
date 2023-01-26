using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private const float PLAYER_STEP_ON_Y_ANGLE_MIN = 0.7f; //!< 45�� ����

    public AudioClip deathSound = default;
    public float jumpForce = default;

    private int jumpCount = default;
    private bool isGrounded = false;
    private bool isDead = false;

    #region Player's Component
    private Rigidbody2D playerRigid = default;
    private Animator playerAni = default;
    private AudioSource playerAudio = default;
    #endregion      // Player's Component


    // Start is called before the first frame update
    void Start()
    {
        // Set Player's Component
        playerRigid = gameObject.GetComponentMust<Rigidbody2D>();
        playerAni = gameObject.GetComponentMust<Animator>();
        playerAudio = gameObject.GetComponentMust<AudioSource>();

        //GioleFunc.Assert(playerRigid != null || playerRigid != default);
        //GioleFunc.Assert(playerAni != null || playerAni != default);
        //GioleFunc.Assert(playerAudio != null || playerAudio != default);

        //Debug.Assert(playerRigid != null || playerRigid != default);
        //Debug.Assert(playerAni != null || playerAni != default);
        //Debug.Assert(playerAudio != null || playerAudio != default);
    }

    // Update is called once per frame
    void Update()
    {
        if (isDead)
        {
            
            return;
        }


        // { �÷��̾� ���� ���� ����
        if (Input.GetMouseButtonDown(0) && jumpCount < 2)
        {
            //! ���� Ƚ�� ����
            jumpCount++;
            //! ���� ������ �ӵ��� ���������� (0,0)�� ����
            playerRigid.velocity = Vector2.zero;
            //! �÷��̾�� �������� �� �߰�
            playerRigid.AddForce(new Vector2(0, jumpForce));

            playerAudio.Play();
        }
        else if (Input.GetMouseButtonUp(0) && 0 < playerRigid.velocity.y)
        {
            //! ���콺 ���� ��ư���� ���� ���� ���� && �ӵ��� y ���� ������ (����  �����)
            //! ���� �ӵ��� �������� ����


            playerRigid.velocity = playerRigid.velocity * 0.5f;
        }
        // } �÷��̾� ���� ���� ����

        //! �������� �ƴ� �� �׶��忡�� ����ϴ� ����
        playerAni.SetBool("Grounded", isGrounded);
    }

    private void Die()
    {
        playerAni.SetTrigger("Die");

        playerAudio.clip = deathSound;
        playerAudio.Play();

        playerRigid.velocity = Vector2.zero;

        isDead = true;
    }       // Die()

    //! Ʈ���� �浹 ���� ó���� ����  �Լ�
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag.Equals("DeadZone") && isDead == false)
        {
            
            Die();
        }

    }

    //! �ٴڿ� ��Ҵ��� üũ�ϴ� �Լ�
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (PLAYER_STEP_ON_Y_ANGLE_MIN < collision.contacts[0].normal.y)
        {
            isGrounded = true;
            jumpCount = 0;
        }       // if: 45�� ���� �ϸ��� ���� ���� ���
    }

    //! �ٴڿ��� ������� üũ�ϴ� �Լ�
    private void OnCollisionExit2D(Collision2D collision)
    {
        isGrounded = false;
    }

}

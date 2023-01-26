using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private const float PLAYER_STEP_ON_Y_ANGLE_MIN = 0.7f; //!< 45도 각도

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


        // { 플레이어 점프 관련 로직
        if (Input.GetMouseButtonDown(0) && jumpCount < 2)
        {
            //! 점프 횟수 증가
            jumpCount++;
            //! 점프 직전에 속도를 순간적으로 (0,0)로 변경
            playerRigid.velocity = Vector2.zero;
            //! 플레이어에게 위쪽으로 힘 추가
            playerRigid.AddForce(new Vector2(0, jumpForce));

            playerAudio.Play();
        }
        else if (Input.GetMouseButtonUp(0) && 0 < playerRigid.velocity.y)
        {
            //! 마우스 왼쪽 버튼에서 손을 떼는 순간 && 속도의 y 값이 양수라면 (위로  상승중)
            //! 현재 속도를 절반으로 변경


            playerRigid.velocity = playerRigid.velocity * 0.5f;
        }
        // } 플레이어 점프 관련 로직

        //! 점프중이 아닐 때 그라운드에서 사용하는 로직
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

    //! 트리거 충돌 감지 처리를 위한  함수
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag.Equals("DeadZone") && isDead == false)
        {
            
            Die();
        }

    }

    //! 바닥에 닿았는지 체크하는 함수
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (PLAYER_STEP_ON_Y_ANGLE_MIN < collision.contacts[0].normal.y)
        {
            isGrounded = true;
            jumpCount = 0;
        }       // if: 45도 보다 완만한 땅을 밟은 경우
    }

    //! 바닥에서 벗어났는지 체크하는 함수
    private void OnCollisionExit2D(Collision2D collision)
    {
        isGrounded = false;
    }

}

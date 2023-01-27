using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance = default;

    private const string UI_OBJS = "UIObjs";
    private const string SCORE_TEXT_OBJ = "ScoreText";
    private const string GAME_OVER_TEXT = "GameOver";




    public bool isGameOver = false;
    public GameObject scoreTxtObj = default;
    public GameObject gameOverUi = default;

    private int score = default;

    private void Awake()
    {
        if (instance == null || instance == default)
        {
            instance = this;
            
            // Init
            isGameOver = false;
            GameObject uiObjs_ = GioleFunc.GetRootObj(UI_OBJS);
            scoreTxtObj = uiObjs_.FindChildObj(SCORE_TEXT_OBJ);
            gameOverUi = uiObjs_.FindChildObj(GAME_OVER_TEXT);

            score = 0;

        }       // if: 게임 매니저가 존재하지 않는 경우 변수에 할당 및 초기화
        else
        {

            GioleFunc.LogWarning("[System] GameManager : Duplicated object warning");
            Destroy(gameObject);
        }
    }       // Awake()

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (isGameOver == true && Input.GetMouseButtonDown(0))
        {
            GioleFunc.LoadScene(GioleFunc.GetActiveScene().name);
        }
    }

    //! 점수를 증가시키는 메서드
    public void AddScore(int newScore)
    {
        if (isGameOver == true) { return; }

        // 게임이 진행중인 경우
        score += newScore;
        scoreTxtObj.SetTmpText($"score : {score}");
    }       // AddScore()

    //! 플레이어 사망 시 게임오버를 출력하는 메서드
    public void OnPlayerDead()
    {
        isGameOver = true;
        gameOverUi.SetActive(true);
    }

}

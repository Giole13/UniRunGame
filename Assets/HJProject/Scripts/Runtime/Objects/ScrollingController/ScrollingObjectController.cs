using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class ScrollingObjectController : MonoBehaviour
{
    public string prefabName = default;
    public int scrollingObjCount = default;

    public float scrollingSpeed = default;

    protected float prefabYPos = default;
    protected GameObject objPrefab = default;
    protected Vector2 objPrefabSize = default;
    protected List<GameObject> scrollingPool = default;

    

    // Start is called before the first frame update
    public virtual void Start()
    {
        objPrefab = gameObject.FindChildObj(prefabName);
        objPrefabSize = objPrefab.GetRectSizeDelta();
        scrollingPool = new List<GameObject>();

        prefabYPos = objPrefab.transform.localPosition.y;

        GioleFunc.Assert(objPrefab != null || objPrefab != default);

        // { 스크롤링 풀을 생성해서 주어진 수만큼 초기화
        GameObject tempObj = default;
        if (scrollingPool.Count <= 0)
        {
            for (int i = 0; i < scrollingObjCount; i++)
            {
                tempObj = Instantiate(objPrefab,
                    objPrefab.transform.position,
                    objPrefab.transform.rotation, transform);

                scrollingPool.Add(tempObj);
                tempObj = default;
            }       // loop: 스크롤링 오브젝트를 주어진 수만큼 초기화 하는 루프
        }       // if: scrolling Pool 을 초기화 한다.

        objPrefab.SetActive(false);
        // } 스크롤링 풀을 생성해서 주어진 수만큼 초기화

        InitObjsPosition();
        // } 생성한 오브젝트의 위치를 설정한다.


    }       // Start()

    // Update is called once per frame
    public virtual void Update()
    {
        if (scrollingPool == default && scrollingPool.Count <= 0)
        {
            return;
        }       // if: 스크롤링 할 오브젝트가 존재하지 않는 경우

        //! 게임오버가 켜져 있지 않으면 배경은 움직인다.
        if (GameManager.instance.isGameOver == false)
        {
            // 스크립트 할 오브젝트가 존재하는 경우
            for (int i = 0; i < scrollingObjCount; ++i)
            {
                scrollingPool[i].AddLocalPos(scrollingSpeed * Time.deltaTime * (-1), 0f, 0f);
            }       // loop: 배경이 왼쪽으로 움직이도록 하는 루프
            RepositionFirstObj();
        }
    }       // Update()


    //! 생성한 오브젝트의 위치를 설정한다.
    protected virtual void InitObjsPosition()
    {
        /* Do Something */

    }       // InitObjsPosition()


    //! 스크롤링 풀의 첫번째 오브젝트를 마지막으로 리포지셔닝 하는 함수
    protected virtual void RepositionFirstObj()
    {
        /* Do something */

    }       // RepositionFirstObj()
}

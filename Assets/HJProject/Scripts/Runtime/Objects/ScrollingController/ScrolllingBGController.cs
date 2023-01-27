using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrolllingBGController : ScrollingObjectController
{
    public override void Start()
    {
        base.Start();
    }

    public override void Update()
    {
        base.Update();
    }

    protected override void InitObjsPosition()
    {
        float horizonPos =
            objPrefabSize.x * (scrollingObjCount - 1) * (-1) * 0.5f;
        for (int i = 0; i < scrollingObjCount; i++)
        {
            scrollingPool[i].SetLocalPos(horizonPos, 0f, 0f);
            horizonPos = horizonPos + objPrefabSize.x;
        }       // loop: 생성한 오브젝트를 가로로 왼쪽부터 차례대로 정렬하는 루프
    }

    protected override void RepositionFirstObj()
    {
        float lastScrObjCurrentXPos = scrollingPool[scrollingPool.Count - 1].transform.localPosition.x;
        if (lastScrObjCurrentXPos < objPrefabSize.x * 0.5f)
        {
            float lastScrObjInitXpos =
                Mathf.Floor(scrollingObjCount * 0.5f) *
                objPrefabSize.x + (objPrefabSize.x * 0.5f) + (-1f);
            scrollingPool[0].SetLocalPos(lastScrObjInitXpos, 0f, 0f);
            scrollingPool.Add(scrollingPool[0]);
            scrollingPool.RemoveAt(0);
        }       // if: 스크롤링 오브젝트의 마지막 오브젝트가 화면 상의 절반정도 Draw 되는 때
    }
}

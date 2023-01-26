using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AutoSizeBoxCollider2D : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Vector2 objSize_ = default;


        BoxCollider2D boxCollider = 
            gameObject.GetComponent<BoxCollider2D>();
        RectTransform parnetrectTransform_ = transform.parent.
            gameObject.GetComponent<RectTransform>();
        RectTransform rectTransform_ = 
            gameObject.GetComponent<RectTransform>();

        objSize_.x = rectTransform_.sizeDelta.x;
        objSize_.y = rectTransform_.sizeDelta.y;

        boxCollider.size = objSize_;

        //GioleFunc.Log($"Rect size : {rectTransform_.sizeDelta.x}" +
        //    $", {rectTransform_.sizeDelta.y}");


    }


}

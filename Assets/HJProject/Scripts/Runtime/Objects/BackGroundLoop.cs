using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackGroundLoop : GioleComponent
{
    private float width = default;


    public override void Awake()
    {
        base.Awake();

        width = gameObject.GetRectSizeDelta().x;
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.localPosition.x <= -width)
        {
            Reposition();
        }
    }


    private void Reposition()
    {
        Vector3 offset = new Vector3(width * 2f, 0f, 0f);
        transform.localPosition = transform.localPosition + offset;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpownBlock : GimmickBase
{
    private void Start()
    {
        gameObject.SetActive(false);
    }

    public override void GimmicOn()
    {
        base.GimmicOn();
        gameObject.SetActive(true);
    }
    public override void GimmicEnd()
    {
        base.GimmicEnd();
        gameObject.SetActive(false);
    }
}

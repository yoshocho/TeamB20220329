using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpownBlock : GimmickBase
{
    private void Start()
    {
        gameObject.SetActive(false);
    }

    public override void Gimmic(bool flag)
    {
        if (flag)
        {
            gameObject.SetActive(true);
            return;
        }
        gameObject.SetActive(false);
    }
}

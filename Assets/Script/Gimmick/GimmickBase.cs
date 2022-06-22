using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GimmickBase : MonoBehaviour
{
    /// <summary>
    /// ギミックを起動させる関数
    /// </summary>
    public virtual void GimmicOn()
    {
        Debug.Log("GimmicOn");
        
    }

    /// <summary>
    /// ギミックを終了させる関数
    /// </summary>
    public virtual void GimmicEnd()
    {
        Debug.Log("GimmicEnd");
    }

    public virtual void Gimmic(bool flag)
    {
        
    }

   
}

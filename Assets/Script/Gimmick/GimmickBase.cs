using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GimmickBase : MonoBehaviour
{
    /// <summary>
    /// �M�~�b�N���N��������֐�
    /// </summary>
    public virtual void GimmicOn()
    {
        Debug.Log("GimmicOn");
        
    }

    /// <summary>
    /// �M�~�b�N���I��������֐�
    /// </summary>
    public virtual void GimmicEnd()
    {
        Debug.Log("GimmicEnd");
    }

    public virtual void Gimmic(bool flag)
    {
        
    }

   
}

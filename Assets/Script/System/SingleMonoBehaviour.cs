using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingleMonoBehaviour<TOwer> : MonoBehaviour where TOwer : SingleMonoBehaviour<TOwer>
{
    private static TOwer s_instance = null;

    public static TOwer Instance 
    {
        get 
        {
            if (s_instance is null)
            {
                Init();
            }
            return s_instance;
        }
    }

    private void Awake()
    {
        CheakInstance();
    }

    bool CheakInstance()
    {
        if (s_instance is null)
        {
            s_instance = this as TOwer;
            OnAwake();
            return true;

        }
        else if (s_instance == this)
        {
            return true;
        }

        Destroy(this);
        return false;
    }

    protected virtual void OnAwake() { }

    static void Init()
    {
        if(s_instance is null)
        {
            var prev = FindObjectOfType(typeof(TOwer));
            if (prev)
            {
                Debug.Log(string.Format("ÉqÉGÉâÉãÉLÅ[è„ÇÃ{0}Çïœä∑",nameof(TOwer)));
                s_instance = prev as TOwer;
            }
            else
            {
                Debug.LogWarning(string.Format("Hierarchyè„Ç…{0}Ç™å©Ç¬Ç©ÇÁÇ»Ç©Ç¡ÇΩÇÃÇ≈ê∂ê¨ÇµÇ‹Ç∑", typeof(TOwer).Name));
                var go = new GameObject(typeof(TOwer).Name);
                s_instance = go.AddComponent<TOwer>();
                s_instance.ForcedRunSet();
            }
        }
    }

    protected virtual void ForcedRunSet() { }
    
    private void OnDestroy()
    {
        if (s_instance == this)
        {
            OnRelease();
            s_instance = null;
        }
    }

    protected virtual void OnRelease() { }
}

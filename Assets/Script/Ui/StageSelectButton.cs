using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

[System.Serializable]
public class StageData 
{
    [SerializeField,Header("ステージの画像")]
    public Sprite StageImage;
    [SerializeField,Header("ステージのScene名")]
    public string SceneName = "";
}
public class StageSelectButton : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, ISelectHandler, ISubmitHandler,IPointerExitHandler
{
    [SerializeField]
    StageData _stageData = new StageData();
    [SerializeField]
    Animator _anim;
    private void Start()
    {
        if(!_anim) _anim = GetComponent<Animator>();
    }
    public void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log("OnClick");
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        Debug.Log("OnEnter");
        _anim.Play("Select");
    }

    public void OnSelect(BaseEventData eventData)
    {
        Debug.Log("Select");
        StageView.Instance.SetImage(_stageData);
    }

    public void OnSubmit(BaseEventData eventData)
    {
        Debug.Log("Submit");
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        Debug.Log("Exit");
        _anim.Play("Close");
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

[System.Serializable]
public class StageData 
{
    [SerializeField,Header("�X�e�[�W�̉摜")]
    public Sprite StageImage;
    [SerializeField,Header("�X�e�[�W��Scene��")]
    public string SceneName = "";
}
public class TitleButton : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, ISelectHandler, ISubmitHandler
{
    [SerializeField]
    StageData _stageData = new StageData();

    public void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log("OnClick");
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        Debug.Log("OnEnter");
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
}

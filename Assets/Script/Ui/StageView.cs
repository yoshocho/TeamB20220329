using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using GameUtility;

public class StageView : MonoBehaviour
{
    public static StageView Instance { get; private set; } = null;

    [SerializeField]
    Image _viewImage;

    public StageData StageData { get; private set; }
    private void Start()
    {
        _viewImage = GetComponent<Image>();
        Instance = this;
    }
    public void SetImage(StageData data)
    {
        StageData = data;
        _viewImage.sprite = StageData.StageImage;
    }
    
    public void ChangeSelectScene() 
    {
        SceneChanger.FadeScene(StageData.SceneName);
    }
}

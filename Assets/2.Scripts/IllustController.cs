using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IllustController : MonoBehaviour
{
    private string IllustPath = "Illustration/";
    private Image _illustImg;

    private void Start()
    {
        _illustImg = GetComponent<Image>();
        Manager.Instance.Data.v_data.Illust = this;
    }

    public void SetImage(int m_selectId)
    {
        this._illustImg.sprite 
            = Resources.Load<Sprite>($"{IllustPath}{Manager.Instance.Data.GetCharacterData(m_selectId).Name}");
    }

}

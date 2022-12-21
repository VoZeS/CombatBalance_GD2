using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ActionButton : MonoBehaviour, IPointerClickHandler
{
    private Color _color;
    private Image _image;
    private TextMeshProUGUI _text;

    private ActionButtonController _colorButtonController;
    FightCommandTypes _type;

    public void OnPointerClick(PointerEventData eventData)
    {
        _colorButtonController.OnButtonPressed(_type);
       
    }
    void Awake()
    {
        _image = GetComponent<Image>();
        _text = GetComponentInChildren<TextMeshProUGUI>();
    }
    void Start()
    {
        
    }

    public void Init(FightCommandTypes type, ActionButtonController colorButtonController)
    {
        // _color = color;
        // _image.color = _color;
        _type = type;
         _colorButtonController = colorButtonController;
        _text.text = type.ToString(); ;
    }

  
}

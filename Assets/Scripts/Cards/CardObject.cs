using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using TMPro;
using DG.Tweening;
public class CardObject : MonoBehaviour
{
    [SerializeField] private SpriteRenderer _Icon;
    [SerializeField] private SpriteRenderer _Background;
    [SerializeField] private TextMeshPro _strengthText;

    public CardData _data { get; private set; }
    private int strength;
    private bool _selected;

    private Color cacheColor;

    [HideInInspector] public UnityEvent<CardObject> OnCardSelected = new UnityEvent<CardObject>();
    [HideInInspector] public UnityEvent<CardObject> OnCardHighlighted = new UnityEvent<CardObject>();

    public void SetupCard(CardEntry data)
    {
        _data = data._data;
        strength = data._strength;
        _Icon.sprite = _data._Icon;
        _strengthText.text = strength.ToString();
        cacheColor = _Background.color;
    }

    //Very old methods from Unity. Usually there are better ways of doing this but for sake of time this will be used
    private void OnMouseDown() 
    {
        PickCard();
    }
    private void OnMouseEnter()
    {
        if (!_selected)
            HighlightCard();
    }
    private void OnMouseExit()
    {
        if(!_selected)
            UnhighlightCard();
    }
    public void HighlightCard()
    {
        transform.GetChild(0).DOLocalMoveY(0.3f, 0.15f);
        transform.GetChild(0).DOScale(1.2f, 0.15f);
        OnCardHighlighted.Invoke(this);
    }
    public void UnhighlightCard()
    {
        transform.GetChild(0).DOLocalMoveY(0f, 0.15f);
        transform.GetChild(0).DOScale(1, 0.15f);
        _Background.color = cacheColor;
        //turn off all animations
    }

    public void PickCard()
    {
        //Lock choice until 3 seconds countdown
        transform.DOPunchScale(Vector3.one * 0.15f, 0.1f);
        OnCardSelected.Invoke(this);
        _selected = true;
        _Background.color = Color.white;
    }

    public void UnselectCard()
    {
        _selected = false;
        UnhighlightCard();
    }
}

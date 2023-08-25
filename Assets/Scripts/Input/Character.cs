using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;
public class Character : MonoBehaviour
{
    public int amountOfWins { get; private set; }

    [SerializeField] private List<CardEntry> _currentHand = new List<CardEntry>();
    [SerializeField] protected SpriteRenderer _handObject;
    [SerializeField] protected TextMeshPro _winCount;
    [SerializeField] protected ParticleSystem _celebration;

    protected CardEntry _selectedCard;
    public virtual void GiveCard(CardEntry entry)
    {
        _winCount.text = "WINS: " + 0;
        _currentHand.Add(entry);
    }
    public virtual void ClearCards()
    {
        _currentHand.Clear();
    }
    public virtual CardEntry GetCardSelected()
    {
        _selectedCard = _currentHand[Random.Range(0, _currentHand.Count)];
        return _selectedCard;
    }

    public void AddWin()
    {
        amountOfWins++;
        _winCount.enabled = true;
        _winCount.text = "WINS: " + amountOfWins.ToString();

    }
    public void WinCelebration()
    {
        _celebration.Play();
    }
    public void ResetGame()
    {
        amountOfWins = 0;
        ClearCards();
        //_winCount.text = "WINS: " + amountOfWins.ToString();
        
    }
    public virtual void ShowHand()
    {
        _handObject.sprite = _selectedCard._data._Hand;
        _handObject.transform.localScale = Vector3.zero;
        _handObject.transform.DOScale(Vector3.one, 0.3f);
    }

    public virtual void HideHand()
    {
        _handObject.transform.DOScale(Vector3.zero, 0.2f);
    }
}

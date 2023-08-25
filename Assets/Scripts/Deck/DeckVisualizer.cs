using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class DeckVisualizer : MonoBehaviour
{
    [SerializeField] private List<CardObject> _instantiatedCards = new List<CardObject>();

    [SerializeField] private CardObject _cardPrefab;
    [SerializeField] private float _spacing;

    public List<CardObject> MyCards => _instantiatedCards;
    public CardObject SpawnCard(CardEntry data)
    {
        CardObject clone = Instantiate(_cardPrefab, transform, false);
        clone.SetupCard(data);
        clone.gameObject.name = data._data._Name;
        clone.transform.localPosition = Vector3.left * 5f;
        _instantiatedCards.Add(clone);
        //Spawn card in spawn point
        //move to center
        RearrangeCards();
        return clone;
        
    }
    public void ClearCards()
    {
        foreach(CardObject card in MyCards)
        {
            Vector2 targetPosition = Vector3.right * 5f;
            card.transform.DOLocalMove(targetPosition, 0.5f).OnComplete(() => Destroy(card.gameObject));
        }

        _instantiatedCards.Clear();
    }

    public void RearrangeCards()
    {
        foreach (CardObject card in _instantiatedCards)
        {
            card.transform.DOKill();
        }

        //set distance between cards
        float totalDistance = _spacing * _instantiatedCards.Count;
        float firstPosX = -totalDistance / 2;
        firstPosX += _spacing / 2f;

        Vector3 pos = new Vector3(firstPosX, 0, 0);

        foreach (CardObject card in _instantiatedCards)
        {
            Vector2 targetPosition = Vector3.zero + pos;
            card.transform.DOLocalMove(targetPosition, 0.5f);
            //card.transform.localPosition = Vector3.zero + pos;
            pos.x += _spacing;
        }

    }

    private void OnValidate()
    {
        RearrangeCards();
    }

}

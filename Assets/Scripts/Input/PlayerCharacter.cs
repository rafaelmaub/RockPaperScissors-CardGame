using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCharacter : Character
{
    [SerializeField] private DeckVisualizer _hand;
    [SerializeField] private CardObject _currentCardToPlay;

    public override void GiveCard(CardEntry entry)
    {
        base.GiveCard(entry);
        CardObject cardObj = _hand.SpawnCard(entry);
        cardObj.OnCardSelected.AddListener(SelectNewCard);
    }
    public override void ClearCards()
    {
        _hand.ClearCards();
        base.ClearCards();
    }
    void SelectNewCard(CardObject card)
    {
        if(_currentCardToPlay && _currentCardToPlay != card)
        {
            _currentCardToPlay.UnselectCard();
        }

        _currentCardToPlay = card;
    }

    public override CardEntry GetCardSelected()
    {
        if (!_currentCardToPlay) return base.GetCardSelected();

        _selectedCard = new CardEntry(_currentCardToPlay._data, 0);
        return _selectedCard;
    }



}

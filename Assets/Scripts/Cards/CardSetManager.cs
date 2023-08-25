using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CardSetManager : MonoBehaviour
{
    #region Singleton
    private static CardSetManager _instance;
    public static CardSetManager Instance => _instance;
    private void Awake()
    {
        if (Instance == null)
        {
            _instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    } 
    #endregion

    [SerializeField] private GameRules _currentRules;
    [SerializeField] private Character _player1;
    [SerializeField] private Character _player2;
    [SerializeField] private int _currentRound;

    private float _currentRoundTimer;
    private bool _ongoingRound;

    public CardData[] CardsDatabase => _currentRules._cardSet._Cards;

    [HideInInspector] public UnityEvent<float, bool> OnTimerChanged = new UnityEvent<float, bool>();
    [HideInInspector] public UnityEvent<bool> OnGameStarted = new UnityEvent<bool>();
    private void Update()
    {
        if(_ongoingRound)
        {
            _currentRoundTimer -= Time.deltaTime;
            if(_currentRoundTimer <= 0)
            {
                ResolveRound();
                OnTimerChanged.Invoke(0, false);
            }
            else
            {
                OnTimerChanged.Invoke(_currentRoundTimer, true);
            }
        }
    }

    public void StartMatch()
    {
        //set rules
        //distribute cards
        _currentRound = 0;
        DistributeCards();
        StartRound();
        OnGameStarted.Invoke(true);
    }

    void EndMatch(Character winner)
    {
        OnGameStarted.Invoke(false);
        _currentRound = 0;

        winner.WinCelebration();

        _player1.ResetGame();
        _player2.ResetGame();

        Debug.Log("Final Winner is: " + winner.gameObject.name);
    }
    void StartRound()
    {
        Debug.Log("Starting Round");

        _player1.HideHand();
        _player2.HideHand();

        _currentRoundTimer = _currentRules._roundDuration;
        _ongoingRound = true;
    }

    void ResolveRound()
    {
        _currentRoundTimer = 0;
        _ongoingRound = false;

        CardEntry _player1Play = _player1.GetCardSelected();
        CardEntry _player2Play = _player2.GetCardSelected();

        _player1.ShowHand();
        _player2.ShowHand();
        Character winner = null;

        if(_player1Play._data == _player2Play._data)
        {
            //for simplicity: strength won't be used

            //if(_player2Play._strength != _player1Play._strength)
            //{
            //    winner = _player1Play._strength > _player2Play._strength ? _player1 : _player2;
            //}
        }
        else
        {
            if (_player1Play._data.GetsBeatenBy(_player2Play._data)) winner = _player2;
            else if (_player2Play._data.GetsBeatenBy(_player1Play._data)) winner = _player1;
        }

        Debug.Log("AI Play was: " + _player2Play._data._Name);

        bool winnerAlready = false;
        if (winner)
        {
            _currentRound++;
            winner.AddWin();
            Debug.Log("Round Winner: " + winner.gameObject.name);
            winnerAlready = (float)winner.amountOfWins > (float)_currentRules._bestOf / 2f;
        }
        else
        {
            //tie
            Debug.Log("TIE");
        }

        
        if(_currentRound >= _currentRules._bestOf || winnerAlready)
        {
            EndMatch(winner);
        }
        else
        {
            Invoke("StartRound", 2f);
        }

    }

    public void DistributeCards()
    {
        int max = _currentRules._RandomCards ? _currentRules._maxCardsInHand : CardsDatabase.Length;

        for (int i = 0; i < max; i++)
        {
            CardEntry _entry = new CardEntry();
            _entry._strength = Random.Range(0, 12);

            if (_currentRules._RandomCards)
            {
                _entry._data = CardsDatabase[Random.Range(0, CardsDatabase.Length)];
            }
            else
            {
                _entry._data = CardsDatabase[i];
            }

            _player1.GiveCard(_entry);
            _player2.GiveCard(_entry);
        }
    }

    

}

[System.Serializable]
public struct CardEntry
{
    public CardData _data;
    public int _strength;

    public CardEntry(CardData data, int strength)
    {
        _data = data;
        _strength = strength;
    }

}

[System.Serializable]
public class GameRules
{
    public bool _RandomCards;
    public int _maxCardsInHand;
    public SetData _cardSet;
    public int _bestOf;
    public float _roundDuration;
    //difficulty settings
}

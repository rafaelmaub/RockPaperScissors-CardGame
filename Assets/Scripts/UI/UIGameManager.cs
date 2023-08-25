using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//My plan for this UI was to be able to control the game rules too
//But I don't wanna go too far beyond the 4 hours so I'll keep it simple
public class UIGameManager : MonoBehaviour
{
    [SerializeField] private GameObject _startButton;

    private void Start()
    {
        if(CardSetManager.Instance)
        {
            CardSetManager.Instance.OnGameStarted.AddListener(ShowAndHide);
        }
    }

    void ShowAndHide(bool gameGoing)
    {
        _startButton.SetActive(!gameGoing);
    }


}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;
public class UITimer : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _textTimer;

    private int _currentSeconds;

    private void Start()
    {
        if(CardSetManager.Instance)
        {
            CardSetManager.Instance.OnTimerChanged.AddListener(UpdateTimer);
        }
    }

    public void UpdateTimer(float second, bool show)
    {
        _textTimer.enabled = show;
        _textTimer.text = second.ToString("F2");
    }
}

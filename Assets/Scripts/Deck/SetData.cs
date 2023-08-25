using UnityEngine;


[CreateAssetMenu(menuName = "Game Structure/New Set")]
public class SetData : ScriptableObject
{
    public string _Name;
    public string _Description;

    public CardData[] _Cards;
}

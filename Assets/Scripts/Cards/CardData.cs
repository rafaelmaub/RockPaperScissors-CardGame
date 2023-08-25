using UnityEngine;

[CreateAssetMenu(menuName = "Game Structure/New Card")]
public class CardData : ScriptableObject
{
    public string _Name;
    public string _Description;
    public Sprite _Icon;
    public Sprite _Hand;

    public CardData[] _Weakness;


    public bool GetsBeatenBy(CardData comparison)
    {
        foreach(CardData d in _Weakness)
        {
            if(d == comparison)
            {
                return true;
            }
        }
        return false;
    }
}

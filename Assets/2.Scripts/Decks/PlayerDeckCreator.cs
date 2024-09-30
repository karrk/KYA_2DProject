using UnityEngine;

public class PlayerDeckCreator : MonoBehaviour
{
    public Deck GetDeck(int m_deckId)
    {
        return CreateDeck(m_deckId);
    }

    private Deck CreateDeck(int m_deckId)
    {
        Deck newDeck = Manager.Instance.Pool.GetObject(E_PoolType.Deck).MyObj.GetComponent<Deck>();
        newDeck.SetDeckData(m_deckId);

        return newDeck;
    }
}

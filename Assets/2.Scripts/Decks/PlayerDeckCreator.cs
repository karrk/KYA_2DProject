using UnityEngine;

public class PlayerDeckCreator : MonoBehaviour
{
    public PlayerDeck GetPlayerDeck(int m_deckId)
    {
        return CreatePlayerDeck(m_deckId);
    }

    private PlayerDeck CreatePlayerDeck(int m_deckId)
    {
        PlayerDeck newDeck = Manager.Instance.Pool.GetObject(E_PoolType.Deck).MyObj.GetComponent<PlayerDeck>();
        newDeck.SetDeckID(m_deckId);

        return newDeck;
    }
}

using UnityEngine;

public class DeckJudge
{
    public static bool UseDeck(Character m_user,int m_deckId, Character m_target = null)
    {
        if (m_user is PlayerCharacter player)
        {
            PlayerDeckStruct playerDeckData = Manager.Instance.Data.GetPlayerDeckData(m_deckId);
            return UseDeckToMonster(player, playerDeckData, m_target as Monster);
        }
        //else if(m_user is Monster mob)
        //{
        //    MobDeckStruct mobDeckData = Manager.Instance.Data.GetMobDeckData(m_deckId);
        //    return UseDeckToPlayer(mob, mobDeckData, m_target as PlayerCharacter);
        //}

        return false;
    }

    private static bool UseDeckToMonster
        (PlayerCharacter m_user, PlayerDeckStruct m_deckData, Monster m_target)
    {
        E_DeckUseType useType = m_deckData.UseType;

        switch (useType)
        {
            case E_DeckUseType.NonTarget:

                ApplyDeckValues(m_user, m_deckData, m_target);
                break;

            case E_DeckUseType.Targetting:

                if (m_target == null)
                    return false;

                ApplyDeckValues(m_user, m_deckData, m_target);
                break;

            case E_DeckUseType.Self:

                ApplyDeckValues(m_user, m_deckData, m_target);
                break;

            default:
                return false;

        }
        return true;
    }

    private static void ApplyDeckValues(PlayerCharacter m_user, PlayerDeckStruct m_deckData, Monster m_target)
    {
        ApplyHP(m_deckData.Atk * -1, m_target); // ����
        
        m_user.AddDef(m_deckData.Def); // ���
        // �߰� �� �̱�
        m_user.AddAlphaAtk(m_deckData.AlphaAtk); // �߰��Ŀ�
        ApplyHP(m_deckData.SelfAtk * -1, m_user); // �ڰ� ����
        // �� ���� ����
        ApplyHP(m_deckData.Heal, m_user); // ��
    }

    private static void ApplyHP(int m_value, PlayerCharacter m_target)
    {
        m_target.AddHP(m_value);
    }

    private static void ApplyHP(int m_value, Monster m_target = null)
    {
        if (m_target != null)
            m_target.AddHP(m_value);
        else
        {
            Monster[] mobs = Manager.Instance.Data.v_data.MonstersDatas;

            for (int i = 1; i < mobs.Length; i++)
            {
                mobs[i].AddHP(m_value);
            }
        }
    }

    //private static bool UseDeckToPlayer
    //    (Monster m_user,MobDeckStruct m_deckData, PlayerCharacter m_target)
    //{
    //    E_DeckType baseType = m_deckData.Type;
    //    E_DeckUseType useType = m_deckData.UseType;
    //}



}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class DataManager : IListener
{
    private const string Address = "https://docs.google.com/spreadsheets/d/1fnA7AIF2ew1lcKoNMuGfEJ2O2yr75v_Uhf6SPx3OjqM";
    private const string TableRange = "A2";
    private const string MainSheetID = "0";

    // ������ ������ �����ο�
    public var_Data v_data;

    // ������� ���� �����͸� ���߽�Ŵ
    // ������ ������ �����ϰ� ����, ���� ����� public ���� �����Ǿ�������
    // �ش� �����ʹ� �����͸Ŵ����� ���� �ҷ��ü� �ִ�.
    private st_Data s_data;

    public int InitPoolCount => s_data.PoolInitCount;
    public Vector2 PlayerSpawnPos => s_data.Positions.PlayerSpawnPos;
    public Vector2 PlayerReadyPos => s_data.Positions.PlayerReadyPos;
    public Vector2 MobSpawnPos => s_data.Positions.MobSpawnPos;
    public Vector2 MobReadyPos => s_data.Positions.MobReadyPos;
    public Vector2 DeckGravePos => s_data.Positions.DeckGravePos;
    public Vector2 DeckHandPos => s_data.Positions.DeckHandPos;
    public Vector2 DeckWaitPos => s_data.Positions.DeckWaitPos;

    public int DeckInitSortValue = 10;
    public uint DeckMaskLayerNumber = 1<<9;
    public float DeckMoveAnimTime => 0.5f;
    public float DeckWidth => 1.8f;
    public float DeckPullCoolTime => 0.2f;


    public DataManager()
    {
        v_data = new var_Data();
        s_data = new st_Data();
    }

    #region CSV ����

    private string GetAddreass(string m_range, int m_sheetId)
    {
        return $"{Address}/export?format=csv&range={m_range}&gid={m_sheetId}";
    }

    private string GetAddreass(string m_range, string m_sheetId)
    {
        return GetAddreass(m_range, int.Parse(m_sheetId));
    }

    public IEnumerator LoadData()
    {
        CSVParser parser = new CSVParser();
        UnityWebRequest request;
        string[] dataIDs;
        string tempRange;

        yield return TableRequest(out request); // �������̺��� ������ ��ȯ����
        Debug.Log("�������̺� �ε���..");

        tempRange = request.downloadHandler.text;
        yield return TableRequest(out request, MainSheetID, tempRange);
        dataIDs = request.downloadHandler.text.Split(',');

        for (int i = 0; i < dataIDs.Length; i++)
        {
            yield return TableRequest(out request, dataIDs[i]);
            
            tempRange = request.downloadHandler.text;

            yield return TableRequest(out request, dataIDs[i], tempRange);

            yield return parser.CSVParse((E_CSVTableType)i, request.downloadHandler.text);

            Debug.Log($"{(E_CSVTableType)i} ���̺� �ε� �Ϸ�..");
        }
    }

    public void RegistInitCharacter(PlayerCharacter m_character)
    {
        v_data.CurrentCharacter = m_character;
        v_data.PlayerData.TotalDecks = new List<int>();
        CopyDecksData();
        v_data.PlayerData.RoundGold = 0;

        void CopyDecksData()
        {
            List<int> characterInitDecks = s_data.Characters[v_data.PlayerData.SelectedCharacterID].StartDecks;

            for (int i = 0; i < characterInitDecks.Count; i++)
            {
                v_data.PlayerData.TotalDecks.Add(characterInitDecks[i]);
            }
        }
    }

    private void SetMapCount(int m_count)
    {
        this.s_data.MapDataList = new MapStruct[m_count];
    }

    private void SetMapSize(int m_stageNumber,int m_y,int m_x)
    {
        this.s_data.MapDataList[m_stageNumber] = new MapStruct();
        this.s_data.MapDataList[m_stageNumber].RoomTypes = new E_RoomType[m_y, m_x];
        this.s_data.MapDataList[m_stageNumber].NextPaths = new string[m_y, m_x];
    }

    private void SetMapData(int m_stageNumber,int m_y, int m_x, E_RoomType m_type, string m_nextPath)
    {
        this.s_data.MapDataList[m_stageNumber].RoomTypes[m_y, m_x] = m_type;
        this.s_data.MapDataList[m_stageNumber].NextPaths[m_y, m_x] = m_nextPath;
    }

    /// <summary>
    /// ��Ʈ ���̵� ���� ������ ���°�� �������̺� �ε�����
    /// </summary>
    private UnityWebRequestAsyncOperation TableRequest(out UnityWebRequest m_request)
    {
        m_request = UnityWebRequest.Get(GetAddreass(TableRange, MainSheetID));
        return m_request.SendWebRequest();
    }

    private UnityWebRequestAsyncOperation TableRequest(out UnityWebRequest m_request, string m_sheetID)
    {
        m_request = UnityWebRequest.Get(GetAddreass(TableRange, m_sheetID));
        return m_request.SendWebRequest();
    }

    private UnityWebRequestAsyncOperation TableRequest(out UnityWebRequest m_request, string m_sheetID, string m_range)
    {
        m_request = UnityWebRequest.Get(GetAddreass(m_range, m_sheetID));
        return m_request.SendWebRequest();
    }

    private class CSVParser
    {
        public string[] PartitionRow(string m_csvTable)
        {
            return m_csvTable.Split("\n");
        }

        public string[] PartitionCol(string m_data)
        {
            return m_data.Split(',');
        }

        public IEnumerator CSVParse(E_CSVTableType m_requestType,string m_tableData)
        {
            switch (m_requestType)
            {
                case E_CSVTableType.Character:
                    CharacterDataParse(m_tableData);
                    break;
                case E_CSVTableType.PlayerDeck:
                    PlayerDeckDataParse(m_tableData);
                    break;
                case E_CSVTableType.Map:
                    //1287715732;E2:J6
                    //1287715732; ?:?
                    UnityWebRequest request;

                    string[] stageData = PartitionRow(m_tableData);
                    Manager.Instance.Data.SetMapCount(stageData.Length+1);

                    for (int i = 0; i < stageData.Length; i++)
                    {
                        string[] datas = stageData[i].Split(';');

                        yield return Manager.Instance.Data.TableRequest(out request, datas[0], datas[1]);

                        MapDataParse(request.downloadHandler.text,i+1);
                    }

                    break;
                case E_CSVTableType.Mob:
                    MobDataParse(m_tableData);
                    break;
                case E_CSVTableType.MobDeck:
                    MobDeckDataParse(m_tableData);
                    break;
            }
        }

        private void MapDataParse(string m_mapData,int m_stageNumber)
        {
            string[] items = PartitionRow(m_mapData);
            string[] datas = PartitionCol(items[0]); // B 000 , 0 , N 010
            string tempData;

            Manager.Instance.Data.SetMapSize(m_stageNumber, items.Length, datas.Length);

            for (int i = 0; i < items.Length; i++)
            {
                datas = PartitionCol(items[i]); // depth datas

                for (int j = 0; j < datas.Length; j++)
                {
                    tempData = datas[j];

                    if (tempData[0] == '0')
                        continue;

                    Manager.Instance.Data.SetMapData(m_stageNumber, i, j,
                        GetRoomType(tempData[0]), GetRoomNextPath(tempData));
                }
            }

            E_RoomType GetRoomType(char m_firstChar)
            {
                switch (m_firstChar)
                {
                    case 'B':
                        return E_RoomType.B;

                    case 'N':
                        return E_RoomType.N;

                    case 'E':
                        return E_RoomType.E;

                    case 'R':
                        return E_RoomType.R;

                    case 'U':
                        return E_RoomType.U;

                    case 'S':
                        return E_RoomType.S;
                        
                    case 'C':
                        return E_RoomType.C;
                        
                    default:
                        throw new ArgumentException("�� Ÿ������ �Ľ̿��� : " +
                            "CSV �� ���ǵ� ��Ÿ�԰� Ȯ�ο��");
                }
            }

            string GetRoomNextPath(string m_data)
            {
                return m_data.Substring(1);
            }
        }

        private void CharacterDataParse(string m_charData)
        {
            string[] items = PartitionRow(m_charData);

            for (int i = 0; i < items.Length; i++) // �ϳ��� ĳ������
            {
                PlayerCharacterStruct newChar = new PlayerCharacterStruct();
                string[] datas = PartitionCol(items[i]); // ĳ���Ͱ� ���� ������

                newChar.ID = int.Parse(datas[(int)E_CharacterStats.ID]);
                newChar.Name = datas[(int)E_CharacterStats.Name];
                newChar.HP = int.Parse(datas[(int)E_CharacterStats.HP]);
                newChar.StartDecks = new System.Collections.Generic.List<int>();
                string[] decks = datas[(int)E_CharacterStats.StartDeck].Split(";");

                for (int j = 0; j < decks.Length; j++)
                {
                    newChar.StartDecks.Add(int.Parse(decks[j]));
                }

                newChar.PullDeckCount = int.Parse(datas[(int)E_CharacterStats.PullDeckCount]);
                newChar.AbilityPoint = int.Parse(datas[(int)E_CharacterStats.AbilityPoint]);

                Manager.Instance.Data.s_data.AddCharacter((E_Character)i, newChar);
            }
        }

        private void MobDataParse(string m_mobData)
        {
            string[] items = PartitionRow(m_mobData);

            for (int i = 0; i < items.Length; i++)
            {
                MonsterStruct newMob = new MonsterStruct();
                string[] datas = PartitionCol(items[i]);

                newMob.ID = int.Parse(datas[(int)E_MonsterInfo.ID]);
                newMob.Grade = (E_MonsterGrade)int.Parse(datas[(int)E_MonsterInfo.Grade].Split('.')[0]);
                newMob.Name = datas[(int)E_MonsterInfo.Name];
                newMob.Level = int.Parse(datas[(int)E_MonsterInfo.Level]);
                newMob.HP = int.Parse(datas[(int)E_MonsterInfo.HP]);
                newMob.StartDecks = new System.Collections.Generic.List<int>();
                string[] decks = datas[(int)E_MonsterInfo.StartDeck].Split(';');

                for (int j = 0; j < decks.Length; j++)
                {
                    newMob.StartDecks.Add(int.Parse(decks[j]));
                }

                newMob.AbilityPoint = int.Parse(datas[(int)E_MonsterInfo.AbilityPoint]);

                Manager.Instance.Data.s_data.AddMob(newMob);
            }
        }

        private void PlayerDeckDataParse(string m_deckData)
        {
            string[] items = PartitionRow(m_deckData);

            for (int i = 0; i < items.Length; i++)
            {
                PlayerDeckStruct newDeck = new PlayerDeckStruct();
                string[] datas = PartitionCol(items[i]);

                newDeck.ID = int.Parse(datas[(int)E_PlayerDeckInfo.ID]);

                newDeck.Type = (E_DeckType)int.Parse(datas[(int)E_PlayerDeckInfo.Type][0].ToString());
                newDeck.Grade = (E_DeckGrade)int.Parse(datas[(int)E_PlayerDeckInfo.Grade][0].ToString());
                newDeck.UseType = (E_DeckUseType)int.Parse(datas[(int)E_PlayerDeckInfo.UseType][0].ToString());

                newDeck.Name = datas[(int)E_PlayerDeckInfo.Name];

                newDeck.Cost = int.Parse(datas[(int)E_PlayerDeckInfo.Cost]);
                newDeck.Atk = int.Parse(datas[(int)E_PlayerDeckInfo.Atk]);
                newDeck.Def = int.Parse(datas[(int)E_PlayerDeckInfo.Def]);
                newDeck.AddDeckCount = int.Parse(datas[(int)E_PlayerDeckInfo.AddDeckCount]);
                newDeck.AlphaAtk = int.Parse(datas[(int)E_PlayerDeckInfo.Alpha]);
                newDeck.SelfAtk = int.Parse(datas[(int)E_PlayerDeckInfo.SelfAtk]);

                newDeck.SaveDef = datas[(int)E_PlayerDeckInfo.SaveDef] == "1" ? true : false;
                newDeck.Disappear = datas[(int)E_PlayerDeckInfo.Disappear] == "1" ? true : false;

                newDeck.Heal = int.Parse(datas[(int)E_PlayerDeckInfo.Heal]);
                newDeck.Description = datas[(int)E_PlayerDeckInfo.Description];

                Manager.Instance.Data.s_data.AddDeckInfo(newDeck);
            }
        }

        private void MobDeckDataParse(string m_deckData)
        {
            string[] items = PartitionRow(m_deckData);

            for (int i = 0; i < items.Length; i++)
            {
                MobDeckStruct newDeck = new MobDeckStruct();
                string[] datas = PartitionCol(items[i]);

                newDeck.ID = int.Parse(datas[(int)E_MobDeckInfo.ID]);
                newDeck.Type = (E_DeckType)int.Parse(datas[(int)E_MobDeckInfo.Type][0].ToString());
                newDeck.Name = datas[(int)E_MobDeckInfo.Name];
                newDeck.Cost = int.Parse(datas[(int)E_MobDeckInfo.Cost]);
                newDeck.UseType = (E_DeckUseType)int.Parse(datas[(int)E_MobDeckInfo.UseType][0].ToString());
                newDeck.Atk = int.Parse(datas[(int)E_MobDeckInfo.Atk]);
                newDeck.Def = int.Parse(datas[(int)E_MobDeckInfo.Def]);
                newDeck.NextTurnPlusPower = int.Parse(datas[(int)E_MobDeckInfo.NextTurnPlusPower]);
                newDeck.DecreasePowerPercent = float.Parse(datas[(int)E_MobDeckInfo.DecreasePowerPercent]);

                string[] nTurnDatas = datas[(int)E_MobDeckInfo.N_TurnAction].Split(';');
                newDeck.N_turnAction = new int[nTurnDatas.Length];
                
                for (int j = 0; j < nTurnDatas.Length; j++)
                { newDeck.N_turnAction[j] = int.Parse(nTurnDatas[j]); }

                newDeck.SteelGold = int.Parse(datas[(int)E_MobDeckInfo.SteelGold]);
                newDeck.Runaway = datas[(int)E_MobDeckInfo.Runaway] == "1" ? true : false;
                newDeck.Disappear = datas[(int)E_MobDeckInfo.Disappear] == "1" ? true : false;
                newDeck.Heal = int.Parse(datas[(int)E_MobDeckInfo.Heal]);

                Manager.Instance.Data.s_data.AddDeckInfo(newDeck);
            }
        }
    }
    #endregion

    public PlayerCharacterStruct GetCharacterData(int m_charId)
    {
        return s_data.Characters[m_charId];
    }

    public MonsterStruct GetMobData(int m_mobId)
    {
        return s_data.Monsters[m_mobId];
    }

    public PlayerDeckStruct GetPlayerDeckData(int m_deckId)
    {
        return s_data.PlayerDecks[m_deckId];
    }

    public MobDeckStruct GetMobDeckData(int m_deckId)
    {
        return s_data.MobDecks[m_deckId];
    }

    public Sprite GetDeckImage(int m_deckId)
    {
        return Resources.Load<Sprite>($"DeckImages/{m_deckId}");
    }

    public void OnEvent(E_Events m_eventType, System.ComponentModel.Component m_order, object m_param)
    {
        if(m_eventType == E_Events.ChangedBattleScene)
        {
            if (CheckSpawnPostionValue())
                GetPostionDatas();
        }
    }

    private bool CheckSpawnPostionValue()
    {
        return s_data.Positions.PlayerSpawnPos == Vector2.zero;
    }

    private void GetPostionDatas()
    {
        Transform poser = GameObject.FindGameObjectWithTag("Poser").transform;

        s_data.Positions.PlayerSpawnPos = poser.GetChild(0).position;
        s_data.Positions.PlayerReadyPos = poser.GetChild(1).position;
        s_data.Positions.MobSpawnPos = poser.GetChild(2).position;
        s_data.Positions.MobReadyPos = poser.GetChild(3).position;
        s_data.Positions.DeckGravePos = poser.GetChild(4).position;
        s_data.Positions.DeckHandPos = poser.GetChild(5).position;
        s_data.Positions.DeckWaitPos = poser.GetChild(6).position;
    }
}






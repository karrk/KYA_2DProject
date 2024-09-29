using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

public class DataManager : IListener
{
    private const string Address = "https://docs.google.com/spreadsheets/d/1fnA7AIF2ew1lcKoNMuGfEJ2O2yr75v_Uhf6SPx3OjqM";
    private const string TableRange = "A2";
    private const string MainSheetID = "0";

    // 데이터 변경이 자유로움
    public var_Data v_data;

    // 변경되지 않을 데이터를 집중시킴
    // 데이터 변경을 엄격하게 통제, 내부 멤버는 public 으로 설정되어있으나 
    // 해당 데이터는 데이터매니저를 통해 불러올수 있다.
    private st_Data s_data;

    public int InitPoolCount => s_data.PoolInitCount;
    public Vector2 PlayerSpawnPos => s_data.Positions.PlayerSpawnPos;
    public Vector2 PlayerReadyPos => s_data.Positions.PlayerReadyPos;
    public Vector2 MobSpawnPos => s_data.Positions.MobSpawnPos;
    public Vector2 MobReadyPos => s_data.Positions.MobReadyPos;

    public DataManager()
    {
        v_data = new var_Data();
        s_data = new st_Data();
    }

    #region CSV 관련

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

        yield return TableRequest(out request); // 메인테이블의 범위를 반환받음
        Debug.Log("메인테이블 로드중..");

        tempRange = request.downloadHandler.text;
        yield return TableRequest(out request, MainSheetID, tempRange);
        dataIDs = request.downloadHandler.text.Split(',');

        for (int i = 0; i < dataIDs.Length; i++)
        {
            yield return TableRequest(out request, dataIDs[i]);
            
            tempRange = request.downloadHandler.text;

            yield return TableRequest(out request, dataIDs[i], tempRange);

            yield return parser.CSVParse((E_CSVTableType)i, request.downloadHandler.text);

            Debug.Log($"{(E_CSVTableType)i} 테이블 로드 완료..");
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
    /// 시트 아이디가 별도 지정이 없는경우 메인테이블 로드진행
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
                    DeckDataParse(m_tableData);
                    break;
                case E_CSVTableType.Map:
                    //1287715732;E2:J6
                    //1287715732; ?:?
                    UnityWebRequest request;

                    string[] stageData = PartitionRow(m_tableData);
                    Manager.Instance.Data.SetMapCount(stageData.Length+1);

                    for (int i = 0; i < stageData.Length; i++)
                    {
                        //string[] datas = PartitionCol(stageData[i]); // sheetid + range
                        string[] datas = stageData[i].Split(';');

                        yield return Manager.Instance.Data.TableRequest(out request, datas[0], datas[1]);

                        MapDataParse(request.downloadHandler.text,i+1);
                    }

                    //yield return Manager.Instance.Data.TableRequest(out request, data[0], data[1]);

                    //MapDataParse(request.downloadHandler.text);

                    break;
                case E_CSVTableType.Mob:
                    break;
                case E_CSVTableType.MobDeck:
                    break;
                case E_CSVTableType.Size:
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
                        throw new ArgumentException("룸 타입정보 파싱에러 : " +
                            "CSV 에 정의된 룸타입값 확인요망");
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

            for (int i = 0; i < items.Length; i++) // 하나의 캐릭터의
            {
                CharacterStruct newChar = new CharacterStruct();
                string[] datas = PartitionCol(items[i]); // 캐릭터가 갖는 정보들

                newChar.ID = int.Parse(datas[(int)E_CharacterStats.ID]);
                newChar.Name = datas[(int)E_CharacterStats.Name];
                newChar.HP = int.Parse(datas[(int)E_CharacterStats.HP]);
                newChar.StartDecks = new System.Collections.Generic.List<int>();
                string[] decks = datas[(int)E_CharacterStats.StartDeck].Split(";");

                for (int j = 0; j < decks.Length; j++)
                {
                    newChar.StartDecks.Add(int.Parse(decks[j]));
                }

                Manager.Instance.Data.s_data.AddCharacter((E_Character)i, newChar);
            }
        }

        private void DeckDataParse(string m_deckData)
        {
            string[] items = PartitionRow(m_deckData);

            for (int i = 0; i < items.Length; i++)
            {
                DeckStruct newDeck = new DeckStruct();
                string[] datas = PartitionCol(items[i]);

                newDeck.ID = int.Parse(datas[(int)E_DeckInfo.ID]);

                newDeck.Type = (E_DeckType)int.Parse(datas[(int)E_DeckInfo.Type][0].ToString());
                newDeck.Grade = (E_DeckGrade)int.Parse(datas[(int)E_DeckInfo.Grade][0].ToString());

                newDeck.Name = datas[(int)E_DeckInfo.Name];

                newDeck.Cost = int.Parse(datas[(int)E_DeckInfo.Cost]);
                newDeck.Atk = int.Parse(datas[(int)E_DeckInfo.Atk]);
                newDeck.Def = int.Parse(datas[(int)E_DeckInfo.Def]);
                newDeck.AddDeckCount = int.Parse(datas[(int)E_DeckInfo.AddDeckCount]);
                newDeck.Alpha = int.Parse(datas[(int)E_DeckInfo.Alpha]);
                newDeck.SelfAtk = int.Parse(datas[(int)E_DeckInfo.SelfAtk]);

                newDeck.SaveDef = datas[(int)E_DeckInfo.SaveDef] == "1" ? true : false;
                newDeck.Disappear = datas[(int)E_DeckInfo.Disappear] == "1" ? true : false;

                Manager.Instance.Data.s_data.AddDeckInfo(newDeck);
            }
        }
    }
    #endregion

    public DeckStruct GetDeckData(int m_deckId)
    {
        return s_data.Decks[m_deckId];
    }

    public Sprite GetDeckImage(int m_deckId)
    {
        return Resources.Load<Sprite>($"DeckImages/{m_deckId}");
    }

    public void OnEvent(E_Events m_eventType, System.ComponentModel.Component m_order, object m_param)
    {
        if(m_eventType == E_Events.ChangedBattle)
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
    }
}






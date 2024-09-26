using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

public class DataManager
{
    private const string Address = "https://docs.google.com/spreadsheets/d/1fnA7AIF2ew1lcKoNMuGfEJ2O2yr75v_Uhf6SPx3OjqM";
    private const string TableRange = "B2:E2";
    private const int SheetID = 0;

    public var_Data v_data;
    private st_Data s_data;
    private CSVParser _parser;

    public DataManager()
    {
        v_data = new var_Data();
        s_data = new st_Data();
        _parser = new CSVParser();
    }

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
        UnityWebRequest main = UnityWebRequest.Get(GetAddreass(TableRange,SheetID));
        yield return main.SendWebRequest();

        Debug.Log("메인테이블로드 완료");

        // 메인테이블데이터
        string[] infos = UnlockTable(main.downloadHandler.text);

        UnityWebRequest temp;

        // 캐릭터테이블 데이터 로드
        temp = UnityWebRequest.Get(
            GetAddreass(infos[(int)E_CSVTableType.Character], infos[(int)E_CSVTableType.Character + 1]));
        yield return temp.SendWebRequest();

        _parser.CharacterDataParse(temp.downloadHandler.text);
        
        
        Debug.Log("캐릭터 데이터 로드완료");

        // 덱 테이블 데이터 로드
        temp = UnityWebRequest.Get(
            GetAddreass(infos[(int)E_CSVTableType.Deck], infos[(int)E_CSVTableType.Deck + 1]));
        yield return temp.SendWebRequest();

        _parser.DeckDataParse(temp.downloadHandler.text);

        Debug.Log("덱 데이터 로드완료");

    }

    private string[] UnlockTable(string m_data)
    {
        return _parser.PartitionCol(m_data);
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

        public void CharacterDataParse(string m_charData)
        {
            string[] items = PartitionRow(m_charData);

            for (int i = 0; i < items.Length; i++) // 하나의 캐릭터의
            {
                CharacterStruct newChar = new CharacterStruct();
                string[] datas = PartitionCol(items[i]); // 캐릭터가 갖는 정보들

                newChar.Name = datas[(int)E_CharacterStats.Name];

                newChar.HP = int.Parse(datas[(int)E_CharacterStats.HP]);

                newChar.StartDecks = new System.Collections.Generic.List<int>();
                string[] decks = datas[(int)E_CharacterStats.StartDeck].Split(";");

                for (int j = 0; j < decks.Length; j++)
                {
                    newChar.StartDecks.Add(int.Parse(decks[j]));
                }

                Manager.Data.s_data.AddCharacter((E_Character)i, newChar);
            }
        }

        public void DeckDataParse(string m_deckData)
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

                Manager.Data.s_data.AddDeckInfo(newDeck);
            }
        }
    }
}






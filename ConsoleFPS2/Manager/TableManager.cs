using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
class TableManager
{
    #region SingleTon

    public static TableManager Instance = null;

    static public void CreateSingleTon()
    {
        Debug.Assert(Instance == null);
        Instance = new TableManager();
    }
    static public TableManager Get() { Debug.Assert(Instance != null); return Instance; }
    static public void DeletInstance()
    {
        Debug.Assert(Instance != null);
        Instance = null;
    }
    private TableManager() { Init(); }

    #endregion
    public Dictionary<eDifficulty, DropEventTable> m_DropEventTable = new Dictionary<eDifficulty, DropEventTable>();
    public Dictionary<eDifficulty, TileDataTable> m_tileDataTable = new Dictionary<eDifficulty, TileDataTable>();
    public Dictionary<eDifficulty, ArrowEventTable> m_ArrowEventTable = new Dictionary<eDifficulty, ArrowEventTable>();
    public void Init()
    {
        //tile data table
        m_tileDataTable.Add(eDifficulty.Level1, new TileDataTable(6, 6, 1.2f, 1, 0, 10, 30, 60,16));
        m_tileDataTable.Add(eDifficulty.Level2, new TileDataTable(4, 6, 0.8f, 2, 5, 15, 40, 70,14));
        m_tileDataTable.Add(eDifficulty.Level3, new TileDataTable(3, 5, 0.4f, 3, 5, 20, 55, 80,12));

        //Event Arrow

        m_ArrowEventTable.Add(eDifficulty.Level2, new ArrowEventTable(2, 0.1f,3));
        m_ArrowEventTable.Add(eDifficulty.Level3, new ArrowEventTable(1, 0.05f,4));

        //Drop 
        m_DropEventTable.Add(eDifficulty.Level2, new DropEventTable(1.0f));
        m_DropEventTable.Add(eDifficulty.Level3, new DropEventTable(0.5f));

    }

    public bool GetTileDataTable(eDifficulty a_key , out TileDataTable a_tileTable)
    {
        return m_tileDataTable.TryGetValue(a_key, out a_tileTable);
    }

    public bool GetArrowDataTable(eDifficulty a_key , out ArrowEventTable a_arrTable)
    {
        return m_ArrowEventTable.TryGetValue(a_key, out a_arrTable);
    }

    public bool GetDropDataTable(eDifficulty a_key, out DropEventTable a_arrTable)
    {
        return m_DropEventTable.TryGetValue(a_key, out a_arrTable);
    }
}

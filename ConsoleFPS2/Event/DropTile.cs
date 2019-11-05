using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;


class DropEvent : IEventExecute
{
    eDifficulty m_nowLv;
    DropEventTable m_EventTable;
    TileManager m_refTileManager;

    private float m_fTimeCheck;

    public DropEvent(eDifficulty a_lv, TileManager a_TM)
    {
        m_fTimeCheck = 0;
        m_nowLv = a_lv;
        m_refTileManager = a_TM;
        Debug.Assert(TableManager.Get().GetDropDataTable(m_nowLv, out m_EventTable));
    }
    public void Init()
    {
    }

    public void Render()
    {
    }

    public void Update(float a_Delta, bool a_bIsEnd)
    {
        m_fTimeCheck += a_Delta;

        if(m_fTimeCheck > m_EventTable.DropSpeed  && a_bIsEnd==false)
        {
            m_refTileManager.GoDownTile();
            m_fTimeCheck = 0;
        }
    }
}

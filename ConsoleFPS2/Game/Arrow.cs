using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

class Arrow : GameObj
{
    public bool m_bIsLive;
    public Arrow()
    {

    }
    public override void Init(Vec2 a_vPos, Vec2 a_vDir)
    {
        m_vcPos = a_vPos;
        m_vcDir = a_vDir;
        m_bIsLive = true;
        if (a_vDir.x == 1)
            m_cImage = '→';
        else
            m_cImage = '←';
    }

    public override void Update(float a_Delta, Player a_player)
    {
        Interaction(a_player);
        m_bIsLive = isLive();


        //플레이어가 점프로 올라가면 화살들도 내려 올 수 있게
        if (a_player.m_bIsScrollDown)
        {
            ClearRender();
            m_vcPos.y += 1;
        }
    }

    //날라가는 속도 조절을 해야해서 분리해놓음 
    public void MoveArrow()
    {
        ClearRender();
        m_vcPos += m_vcDir;
    }

    public override void ClearRender()
    {
        Console.SetCursorPosition((int)m_vcPos.x, (int)m_vcPos.y);
        Console.Write(" ");
        Console.SetCursorPosition((int)m_vcPos.x+1, (int)m_vcPos.y);
        Console.Write(" ");

    }
    public override void Render()
    {
        if (m_bIsLive == false) return;
        base.Render();
    }


    //양쪽 벽에 도달하면 비활성화 
    public bool isLive()
    {
        if ((int)m_vcPos.x < (int)eValueNum.ScreenLeft + 3 ||
           (int)m_vcPos.x > (int)eValueNum.ScreenRight - 3 ||
           (int)m_vcPos.y > (int)eValueNum.TileEndlLine-1)
        {
            ClearRender();
            return false;
        }

        return true;
    }
    
    //플레이어와 충돌하면 게임 끝 
    public override bool Interaction(Player p)
    {
        if (m_vcPos != p.m_vcPos && m_vcPos.x + 1 != p.m_vcPos.x) return false;
        p.m_bIsDie = true;
        return true;
    }
}

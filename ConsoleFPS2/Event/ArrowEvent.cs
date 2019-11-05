using System;
using System.Collections.Generic;
using System.Diagnostics;


class ArrowEvent : IEventExecute
{


    public eDifficulty m_nowLevel;

    private List<Arrow> m_arrArrow;
    private bool m_bIsEventEnd; //이벤트 지속페이즈인지 준비페이즈인지 확인용
    private Random rand;
    private ArrowEventTable m_eventTable;
    private Player m_refPlayer = null;
    private float m_fTimeCheckMove;
    private float m_fTimeCheckShot;

    public ArrowEvent(eDifficulty a_Lv, Player a_player)
    {
        m_arrArrow = new List<Arrow>();

        m_fTimeCheckMove = 0;//일정 시간마다 움직이도록 delta 값 +=할 맴버변수
        m_fTimeCheckShot = 0;//  비슷한 의미의 화살 생성 주기 조절 맴버변수
        m_nowLevel = a_Lv;
        m_refPlayer = a_player;

        //난이도에 따른 변화를 주기위해 테이블 업데이트 
        Debug.Assert(TableManager.Get().GetArrowDataTable(m_nowLevel, out m_eventTable));
    }

    public void Init()
    {
    }

    public void Render()
    {
        foreach (var arr in m_arrArrow)
            arr.Render();
        
    }
    public void Update(float a_Delta)
    {

    }
    public void Update(float a_Delta, bool a_end)
    {
        m_fTimeCheckMove += a_Delta;
        m_fTimeCheckShot += a_Delta;


        m_bIsEventEnd = a_end; //EventManager의 evnetRest을 인자로 받아서 이벤트를 멈춤 



        foreach (var arr in m_arrArrow)
            arr.Update(a_Delta,m_refPlayer);

        //일정시간마다 화살 이동 
        if (m_fTimeCheckMove > m_eventTable.m_fMoveTime)
        {
            for (int i = 0; i < m_arrArrow.Count; i++)
            {
                if (m_arrArrow[i].m_bIsLive)
                    m_arrArrow[i].MoveArrow();
            }
            m_fTimeCheckMove = 0;
        }
        //일정시간마다 생성
        if (m_fTimeCheckShot > m_eventTable.m_fShotTiem && m_bIsEventEnd == false)
        {
            Create();
            m_fTimeCheckShot = 0;
        }

    }

    public void Create()
    {
        bool isNotLive = true;
        bool isCreateLeftSide = true;

    
        rand = new Random(Environment.TickCount);

        //현재 레벨에 따른 생성갯수 
        for (int j = 0; j < m_eventTable.m_fMakeCount; j++)
        {
            isNotLive = false;

            //사용 끝난걸 재사용
            for (int i = 0; i < m_arrArrow.Count; i++)
            {
                if (m_arrArrow[i].m_bIsLive == false)
                {
                    isNotLive = true;
                    CreateModule( ref isCreateLeftSide, i);
                }
            }
            //재활용 할게 없으면 생성 
           if (isNotLive == false)
            {
                m_arrArrow.Add(new Arrow());
                CreateModule(ref isCreateLeftSide, m_arrArrow.Count-1);
            }

        }
    
    }


    //코드 중복되서 분리해놓음 
    void CreateModule( ref bool a_isCreateLeftSide ,int a_index)
    {
        int ArrowPosY;

        //생성 위치 중복 안되게 
        while (true)
        {
            ArrowPosY = rand.Next(0, 24);
            if (!m_arrArrow.Exists(e => e.m_bIsLive && (int)e.m_vcPos.y == ArrowPosY ))
                break;
        }
        // 양쪽다 나올 수 있게 bool값 전환으로 
        if (a_isCreateLeftSide)
            m_arrArrow[a_index].Init(new Vec2((int)eValueNum.ScreenLeft+3, ArrowPosY), new Vec2(1, 0));
        else
            m_arrArrow[a_index].Init(new Vec2((int)eValueNum.ScreenRight-3, ArrowPosY), new Vec2(-1, 0));

        a_isCreateLeftSide = !a_isCreateLeftSide;
    }


}

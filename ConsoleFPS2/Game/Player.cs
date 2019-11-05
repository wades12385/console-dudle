using System;
using System.Windows.Input;

class Player : GameObj
{


    //타일메니저 쉘카피용 
    TileManager m_refTileManger = null;

    public bool m_bIsScrollDown; //player가 일정 높이 이상 점프 했는지 확인용
    public bool m_bIsDie;// 죽었는지 확인 

    public int m_nJumpTilePow;//점프 타일 밟고 있을때 더해질 점프력 

    public int m_ItemJumpCount;//점프 아이템 갯수
    public int m_ItemSlowCount;//낙하감속 아이템 갯수 


    private int m_nJumpPow; // 기본 점프력  

    //아래에서 서술
    private float m_fJumpingPos;
    private float m_fJumpPrevPos;

    //아이템 효과 수치
    private float m_nSlowBuffVal;
    private int m_nJumpBuffVal;

    //낙하상태 점프상태 확인용
    private bool m_bIsFall;
    private bool m_bIsJump;

    //아이템 지속시간
    private float m_JumpBuffLiveTime; 
    private float m_SlowBuffLiveTime;

    public Player()
    {
        Init(new Vec2((int)eValueNum.StartPlayerSpone, 20));
    }

    public override void Init(Vec2 a_vPos)
    {
        m_vcPos = a_vPos;
        m_cImage = 'A';
		m_bIsDie = false;

        //-----
        m_fSpeed = 30.0f;
        m_nJumpBuffVal = 0;
        m_nSlowBuffVal = 0;
        m_bIsScrollDown = false;
        m_bIsFall = false;
        m_bIsJump = false;
        m_fJumpPrevPos = 0.0f;//
        m_nJumpPow = 6;
        m_fJumpingPos = 0.0f;
		m_nJumpTilePow = 1;
		m_ItemJumpCount = 3;
		m_ItemSlowCount = 3;
	}

    public override void Update(float a_Delta)
    {
        ClearRender();

        //if로 아이템 사용상태인지 아닌지 확인하는 것보다 쓸때만 지속시간 양수값으로 설정 해놓는게 좋다고 생각함 
        m_JumpBuffLiveTime -= a_Delta;
        m_SlowBuffLiveTime -= a_Delta;
        //------------------------------


        Input();


     
        if (m_bIsJump==false)
           m_bIsFall = !Interaction();

        Jump(a_Delta);
        Fall(a_Delta);

        m_vcPos += (m_vcDir * m_fSpeed * a_Delta);
		if (m_vcPos.y > (int)eValueNum.TileEndlLine) m_bIsDie = true;
		if (m_vcPos.x > (int)eValueNum.ScreenRight) m_vcPos.x = (int)eValueNum.ScreenLeft + 2;
		if (m_vcPos.x < (int)eValueNum.ScreenLeft+2) m_vcPos.x = (int)eValueNum.ScreenRight;

        //아이템 지속시간 0으로 떨어지면 아이템 적용 수치 0으로 
        if (m_JumpBuffLiveTime <= 0)  m_nJumpBuffVal = 0;
        if (m_SlowBuffLiveTime <= 0)  m_nSlowBuffVal = 0;
	}

	void Input()
	{
		m_vcDir.x = 0;

		if (InputKey.Get().keyPress(Key.A))
			m_vcDir.x = -1;

		if (InputKey.Get().keyPress(Key.D))
			m_vcDir.x = 1;

		if (InputKey.Get().KeyDown(Key.Space) &&
			m_bIsJump == false && m_bIsFall == false)
			JumpReady();


        //아이템 사용시 지속시간 초기화 갯수 감소 아이템 적용 수치 초기화 
		if (InputKey.Get().KeyDown(Key.J) && m_ItemJumpCount > 0 && m_JumpBuffLiveTime <= 0)
		{
            m_JumpBuffLiveTime = 5.0f;
			m_ItemJumpCount--;
            m_nJumpBuffVal = 3;
        }
		if (InputKey.Get().KeyDown(Key.K) && m_ItemSlowCount > 0 && m_SlowBuffLiveTime <= 0)
		{
            m_SlowBuffLiveTime = 3.5f;
			m_ItemSlowCount--;
            m_nSlowBuffVal = 0.2f;
        }

    }

	void Jump(float a_Delta)
    {
        if (!m_bIsJump)
            return;
        //일정 높이에 도달하면 플레이어는 더 이상 올라가지 않지만
        //계속 점프상태처럼 보이기 위해 타일들이 내려와야함 
        //멈춰 있지만 점프 상태는 유지해야하기에 더미용 y값을 만듬 
        //m_fJumpPrevPos 가 점프하기 이전 위치 y값이고 m_fJumpingPos 가 최종 점프 높이까지 올라왔는지 확인용 y 값

        if (m_fJumpingPos <= m_fJumpPrevPos - ((m_nJumpPow * m_nJumpTilePow) + m_nJumpBuffVal))
        {
            m_bIsJump = false;
            m_bIsScrollDown = false;
            return;
        }

        m_fJumpingPos -= 1; // 점프 높이 체크용


        if (m_vcPos.y <= (int)eValueNum.TileScrollDownLine)
            m_bIsScrollDown = true;
        else
             m_vcPos.y -= 1;
    }

    void Fall(float a_Delta)
    {
        //점프상태가 아니며 타일과 충돌 상태가 아닐경우에만 낙하 하도록 
      
        if (m_bIsJump || !m_bIsFall) return;
        //슬로우아이템 사용시 낙하속도 감소 
        m_vcPos.y += 0.5f - m_nSlowBuffVal;
    }
    void JumpReady()
    {
        //점프 전 필요한 맴버변수들 초기화 

        m_fJumpPrevPos = m_vcPos.y;
        m_fJumpingPos = m_vcPos.y;
        m_bIsJump = true;
        m_bIsScrollDown = false;
    }



  
    public override bool Interaction()
    {
        if (!m_bIsJump)//점프 상태가 아닐때 타일들 충돌체크 
        {
            for (int i = 0; i < m_refTileManger.m_arrTile.Count; i++)
            {
                if (m_refTileManger.m_arrTile[i].Interaction(this))
                {
                    return true;
                }
            }
        }
        return false;
    }
    public void ReceiveData(TileManager a_manger)
    {
        m_refTileManger = a_manger;
    }

}

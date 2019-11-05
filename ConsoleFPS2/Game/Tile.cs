using System;



class Tile : GameObj
{
	public eTileType m_TileType;
	public bool m_bIsUse;
	public float m_fTimeCheck;
	bool m_bIsReturn;
	public Tile()
	{
	}

	public override void Init(Vec2 a_vPos, int a_nSize,eTileType eTile)
	{

        //타일 좌표랑 길이 종류를 인자로 받아옴 
		m_vcPos = a_vPos;
		m_size = a_nSize;
		m_TileType = eTile;
		switch (m_TileType)
		{
			case eTileType.None:
				m_cImage = '=';
				break;
			case eTileType.Week:
				m_cImage = '-';
				break;
			case eTileType.move:
				m_cImage = '~';
				break;
			case eTileType.Jump:
				m_cImage = '*';
				break;
			case eTileType.Item:
				m_cImage = '=';
				break;
			default:
				break;
		}

        //움직이는 발판의 진행 방향 
		m_vcDir = new Vec2(0,1);
		m_bIsReturn = false; // 진행 방향 반전 용 
		m_bIsUse = false; //밟으면 사라지는 발판 확인용 
		m_fTimeCheck = 0;
		Random rand = new Random();

		if(rand.Next(1,3) == 1)
            m_bIsReturn = !m_bIsReturn;

	}

	public override void Update(float a_fDelta, float a_moveTileSec)
	{
		Move(a_fDelta, a_moveTileSec);
    }

	public override bool Interaction(Player p)
	{
        //발판을 밟았는지 확인
		if ((int)p.m_vcPos.y == (int)(m_vcPos.y - 1) &&
			((int)m_vcPos.x <= (int)p.m_vcPos.x && 
			(int)p.m_vcPos.x <= (int)m_vcPos.x + m_size))
        {
			if (m_TileType == eTileType.Week) m_bIsUse = true;//밟으면 사라지는 밟판이면 밟았는지 확인 

			if (m_TileType == eTileType.Jump)
                p.m_nJumpTilePow = 2; //점프 발판이면 플레이어 점프력 두배 

			else p.m_nJumpTilePow = 1;//아니라면 원래대로 1

            return true;
        }
		return false;
	}

	void Move(float a_Delta, float a_moveTileSec)
	{
		if (m_TileType != eTileType.move) return;
        //무브타일 이동 모듈

		m_fTimeCheck += a_Delta;

        //사이드 벽에서 진행방향 반전 
		if (m_vcPos.x < (int)eValueNum.ScreenLeft+ (int)eValueNum.ScreenFontWidth)
			m_bIsReturn = false;
		else if (m_vcPos.x + m_size > (int)eValueNum.ScreenRight- 1)
			m_bIsReturn = true;

        //일정 시간마다 타일 이동 
		if (m_fTimeCheck > a_moveTileSec)
		{
			ClearRender();
			if (m_bIsReturn)
				m_vcPos.x -= 1;
			else
				m_vcPos.x += 1;

			m_fTimeCheck = 0;
		}

	}

	public override void ClearRender()
	{
		if (Util.ConsoleBoundaryCheck(m_vcPos) == false) { return; }
		for (int i = 0; i < m_size; i++)
		{
	    	Console.SetCursorPosition((int)m_vcPos.x+i, (int)m_vcPos.y);
			Console.Write(' ');
		}
	}
	public override void Render()
	{
		if (Util.ConsoleBoundaryCheck(m_vcPos) == false) { return; }

		Console.SetCursorPosition((int)m_vcPos.x  , (int)m_vcPos.y);
		for (int i = 0; i < m_size; i++)
			Console.Write(m_cImage);
	}
}

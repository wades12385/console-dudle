using System;


class Item : GameObj
{


	public eItem m_ItemState;
	Tile m_refTile;
	public bool m_bIsLive;
	Random rand;
	int m_nUpdatePos;

	public Item()
	{
		Init();
	}

	public override void Init()
	{
		m_refTile = null;
		m_bIsLive = false;
		m_ItemState = eItem.None;
		m_vcPos = new Vec2(0, 0);
		m_nUpdatePos = 0;
	}

	public void SetItem(Tile T)
	{
		rand = new Random(Environment.TickCount);
        //타일 위에 올려져 놔야 하고 x값 위치는 랜덤하게 
        //종류는 enum값 랜덤하게 뽑아서 
		m_ItemState = RandomEnum.Of<eItem>();
		m_refTile = T;
        //특수문자 크기가 2정도 되니 문자가 타일 오른쪽 가장자리에 안튀어나오게 1 뺌  
		m_nUpdatePos = rand.Next(0, T.m_size-1);
        //아이템위치 타일 사이즈 안쪽으로 생성되게  x 랜덤으로  y는 -1;
		m_vcPos = new Vec2(T.m_vcPos.x+m_nUpdatePos, T.m_vcPos.y-1);
		m_bIsLive = true;
		switch (m_ItemState)
		{
			case eItem.JumpUp:
				m_cImage = 'J';
				break;
			case eItem.SlowFall:
				m_cImage = 'S';
				break;
			default:
				break;
		}
	}

	public override void Update(float f_Delta, Player a_player)
	{
		if (m_bIsLive == false) return;
		ClearRender();
        Interaction(a_player);

        //아래로 아웃 되면 초기화 
        if (m_vcPos.y+2 > (int)eValueNum.TileEndlLine)
		{
			Init();
			return;
		}
        //아이템 위치는 아이템 타일 위로 갱신 
		m_vcPos.y = m_refTile.m_vcPos.y - 1;
	}

	public override void ClearRender()
	{
		base.ClearRender();
	}

	public override void Render()
	{
		if (m_bIsLive == false) return;
		base.Render();
	}

	public override bool Interaction(Player p)
	{
		if(m_bIsLive == false) return false;

		if (m_vcPos != p.m_vcPos) return false; 

        //아이템 갯수가 3개 이면 먹어도 안늘어남 
		switch (m_ItemState)
		{
			case eItem.JumpUp:
			if( p.m_ItemJumpCount < 3 ) p.m_ItemJumpCount++;
				break;
			case eItem.SlowFall:
                if (p.m_ItemSlowCount < 3) p.m_ItemSlowCount++;
				break;
			default:
				break;
		}
		m_bIsLive = false;
		return true;
	}



}

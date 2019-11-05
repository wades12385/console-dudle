using System;
using System.Collections.Generic;
using System.Diagnostics;

class TileManager : IExecute
{

    #region SingleTon
    static TileManager Instance = null;

    static public void CreatSingleTon()
    {
        Debug.Assert(Instance == null);
        Instance = new TileManager();
        Instance.Init();
    }

    static public TileManager Get()
    {
        Debug.Assert(Instance != null);
        return Instance;
    }

    static public void DeletInstance()
    {
        Debug.Assert(Instance != null);
        Instance = null;
    }
    private TileManager() { }
    #endregion
    
    //플레이어 데이터가 필요해서 쉘카피용 맴버변수임
    Player m_refPlayerData;

    TileDataTable m_tileDataTable;

    public int m_nScore;
    //접근자 수정 -stage log()가 물고있음 
    public  List<Tile> m_arrTile;
    public Item m_Item;


    
    Random rand;



    public eDifficulty m_nowLevel;


    public void Init()
    {
        m_arrTile = new List<Tile>();
        m_Item = new Item();
     

        //레벨 마다 발판생성에 영향을 주기 위해 테이블값 받아옴
        Debug.Assert(TableManager.Get().GetTileDataTable(m_nowLevel, out m_tileDataTable));

        m_nScore = 0;
        m_nowLevel = eDifficulty.Level1;


        //맨 처음 바닥 타일 세팅 
        m_arrTile.Add(new Tile());
        m_arrTile[0].Init(new Vec2((int)eValueNum.TileSponeXMin,22), 32, eTileType.None);


        //랜덤 값 분포가 이상한거 같아서 
        //Environment.TickCount(프로그램이 실행 이후 ms 반환 프로퍼티)를 
        //시드로 난수 쓸때마다 넣음 플라시보 효과인지 괜찮게 나옴 
        rand = new Random(Environment.TickCount);


        //초기 배치될 타일 세팅 
        for (int i = 1; i < m_tileDataTable.TileCount; i++)
        {
            m_arrTile.Add(new Tile());
            m_arrTile[i].Init(new Vec2(rand.Next((int)eValueNum.TileSponeXMin, (int)eValueNum.TileSponeXMax),-8 + (i * 2)),
                rand.Next(m_tileDataTable.SizeMin, m_tileDataTable.SizeMax), eTileType.None);
        }


    }
    public void Update(float a_fDelta)
    {
        rand = new Random(Environment.TickCount);

        TileScrollDown();
        Create();

        foreach (var val in m_arrTile)
            val.Update(a_fDelta, m_tileDataTable.TileMoveSecond);


            m_Item.Update(a_fDelta, m_refPlayerData);
    }



    //발판이 화면 밑으로 나가거나 사라지면  재사용 하는 메서드
    public void Create()
    {


        //아래 조건 만족시 해당 타일이 CreateTile로 들어감 
        for (int i = 0; i < m_arrTile.Count; i++)
        {
            //화면 밑으로 내려간 경우
            if (m_arrTile[i].m_vcPos.y > (int)eValueNum.TileEndlLine)
            {
                m_nScore++;
                CreateTile(m_arrTile[i]);
            }
            //밟으면 사라지는 발판 재사용 
            else if (m_arrTile[i].m_bIsUse) //닿으면 사라지는 발판 리셋
            {
                m_arrTile[i].ClearRender();
                CreateTile(m_arrTile[i]);
            }

        }

    }

    //재 생성 해야하는 발판의 종류를 설정하는 메서드 
    //일반 , 밟으면 사라짐 , 점프두배 , 좌우로 움직이는 발판 , 아이템이 올려져있는 발판
    //5종류의 발판을 현재 레벨에 맞게 설정된 확률 값에 의해 정해짐 
    //랜덤 이벤트 중에 발판을 싹다 랜덤하게 바꾸는거 생각해서 따로 분리 시켜 만들었는데 까먹고 안 만듬
    void CreateTile(Tile T)
    {
        eTileType Tiletype = eTileType.None;
        

        switch (m_nowLevel)
        {
            
            case eDifficulty.Level1:
                T.Init(ReSetTile(T), rand.Next(m_tileDataTable.SizeMin, m_tileDataTable.SizeMax), Tiletype);
                break;

            case eDifficulty.Level2:
            case eDifficulty.Level3:

                int ntemp = rand.Next(0, 101);//0~100 까지 난수 생성 

                if (ntemp < m_tileDataTable.WeekChancePer) // 밟으면 사라지는 발판
                    Tiletype = eTileType.Week;

                else if (ntemp < m_tileDataTable.ItemChancePer)//아이템이 올려져 있는 발판 
                {

                    //아이템은 화면에 하나씩만 나오게 할거라 이미 게임 화면에 활성화 되어
                    //있으면 일반 발판으로 교체
                    //비활성화된 아이템이 있으면 재사용 없으면 일반 발판
                    if (m_Item.m_bIsLive == false)
                          Tiletype = eTileType.Item;
                }

                else  if (ntemp < m_tileDataTable.JumpChancePer)// 점프력이 증가하는 발판 
                    Tiletype = eTileType.Jump;

                else if (ntemp < m_tileDataTable.MoveChancePer)// 좌우로 움직이는 발판 
                    Tiletype = eTileType.move;

                //발판 종류가 정해지고 발판의 길이도 랜덤값으로 정해짐 생성 위치는 ReSetTile함수로
                T.Init(ReSetTile(T), rand.Next(m_tileDataTable.SizeMin, m_tileDataTable.SizeMax), Tiletype);
                
                //아이템으로 결정됬으면 아이템 세팅메서드 실행 
                if (Tiletype == eTileType.Item)
                    m_Item.SetItem(T);
                break;
            default:
                break;
        }
    }


    //이 메서드 특징이 발판을 랜덤 위치에 생성을 하는데 
    //플레이어가 뛰어 갈 수 있는 범위 내에서 생성 하도록 조절을 해야함
    //그리고 타일간의 높이 간격 랜덤해야 하며 레벨이 올라가면 더 멀어지는 느낌이 나도록 

    private Vec2 ReSetTile(Tile T)
    {
        //타일 검색범위 간격 
        int nCheckRange = m_tileDataTable.SearchRange;

        //초기 범위 Y : -1~1  
        int SearchLengthMin = (int)eValueNum.SerchTileRangeMin;
        int SearchLengthMax = (int)eValueNum.SerchTileRangeMax;

        //검색 범위에 타일 없는지 체크 
        bool bCheckEmpty = true;

        while (true)
        {
            bCheckEmpty = true;

            //갯수 얼마 안되니 전체 타일 검색 
            for (int i = 0; i < m_arrTile.Count; i++)
            {

                //타겟 범위 내에 타일이 있으면 범위 재설정 
                if (m_arrTile[i].m_vcPos.y >= SearchLengthMin &&
                    m_arrTile[i].m_vcPos.y <= SearchLengthMax)
                {
                    bCheckEmpty = false;

                    //범위 내에 존재한 타일을 기준 윗칸부터 CheckRange 만큼 위로 검색범위 설정
                    SearchLengthMax = (int)m_arrTile[i].m_vcPos.y - 1;
                    SearchLengthMin = SearchLengthMax - nCheckRange;
                    break;
                }
            }

            //검색범위 안에 타일 없으면 반복문 탈출 
            if (bCheckEmpty)
                break;
        }

        //x값은 랜덤으로 y값은 검색범위 내로 랜덤 위치에 생성 
        return new Vec2(rand.Next((int)eValueNum.TileSponeXMin, (int)eValueNum.TileSponeXMax),
             rand.Next(SearchLengthMin, SearchLengthMax));
    }



    private void TileScrollDown()
    {
        //플레이어가 일정 위치 도달하면 타일들이 내려오는 메서드
        if (m_refPlayerData.m_bIsScrollDown)
            GoDownTile();
    }

    //타일 아래로 움직이는 메서드
   public void GoDownTile()
    {
        for (int i = 0; i < m_arrTile.Count; i++)
        {
            if (m_arrTile[i].m_vcPos.y >= (int)eValueNum.ScreenTop)
                m_arrTile[i].ClearRender();

                m_arrTile[i].m_vcPos.y += 1;
        }
    }


    //플레이어 데이터 받아오는 메서드 
    public void ReceiveData(Player p)
	{
		m_refPlayerData = p;
	}


    //일정 점수 넘어가면 stage 맴버메서드에서 호출되어 테이블 데이터 값들 변경 됨 
	public void ChangeLevel(eDifficulty a_elevel)
    {
        m_nowLevel = a_elevel;

        int deleteIndex = (int)eValueNum.ScreenTop;        // 검색용  범위 값 
        int temp = m_tileDataTable.TileCount;

        Debug.Assert(TableManager.Get().GetTileDataTable(m_nowLevel, out m_tileDataTable));

        //난이도 변경으로 타일 총 갯수가 줄어듬 그냥 list의 맨뒤에값들 제거하면 
        //올라가는 도중에 발판 사라짐 게임 화면 밖의 발판만 줄어든 갯수만큼 제거 해야함 


        for (int i = 0; i < temp - m_tileDataTable.TileCount; i++)//이전 레벨 타일 갯수최대 - 현 레벨 타일 갯수최대 만큼 루프 돌려야함
        {
            // deleteIndex 보다 작은 값 즉 위에 있으면 해당 인덱스값을 반환함 만약 없으면 -1
            int Index = m_arrTile.FindIndex(r => r.m_vcPos.y < deleteIndex);

            //없을 때마다 검색용 인덱스를 아래로 한칸씩 내리고 찾으면 해당 값 삭제 
            if (Index == -1)
            {
                deleteIndex++;
                i--;
                continue;
            }
            m_arrTile.RemoveAt(Index);
        }
    }
    public void Render()
	{
        foreach (var val in m_arrTile)
			val.Render();
            m_Item.Render();
	}

}


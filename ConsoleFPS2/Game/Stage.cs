using System;

class Stage : IExecute
{
	//Field
	public TileManager m_TManager;
	public Player m_Player;
    public UiRender m_uiRender;

     EventManager m_EventManager;
    private int m_nDifficulty;
	public Stage()
	{
		Init();
	}

	//End_Field


	public void Init()
	{
        //싱글톤 생성 게임씬에서만 사용 하는 것들이라 이 씬이 넘어가면 삭제 처리합니다.
        TableManager.CreateSingleTon();
        TileManager.CreatSingleTon();
        EventManager.CreateSingleTon();


        m_TManager = TileManager.Get();
        m_EventManager = EventManager.Get();

        m_Player = new Player();
        m_uiRender = new UiRender();

        //얕은복사로 데이터를 접근하기 편하게 복사 떠서 줍니다.
        m_uiRender.ReciveData(m_Player, m_EventManager);
		m_TManager.ReceiveData(m_Player);

		m_Player.ReceiveData(m_TManager);
        m_EventManager.ReceiveData(m_TManager, m_Player);
        m_nDifficulty = 0;
    }

	public void Update(float f_Delta)
	{

    //이벤트먼저 처리 후 타일 메니저 처리해야함 
		m_Player.Update(f_Delta);
		m_TManager.Update(f_Delta);
        m_EventManager.Update(f_Delta);

        //일정 점수 이상 되면 게임 난이도 상승 
        DifficultyCheck();

        //점수판이 바로바로 갱신되도록 점수 값을 넘겨줌 
        m_uiRender.Update(m_TManager.m_nScore);
    }


    void DifficultyCheck()
    {
        m_nDifficulty = m_TManager.m_nScore;

        //1레벨 때 2레벨 조건 값 넘어가면 2레벨 그리고 타일 메니저와 이벤트 메니저한테 
        //바뀐 레벨 enum값 적용합니다. 
        if (m_nDifficulty > (int)eDifficulty.Level2 && m_TManager.m_nowLevel == eDifficulty.Level1 )
        {
            m_TManager.ChangeLevel(eDifficulty.Level2);
            m_EventManager.ChangeLevel(eDifficulty.Level2);
        }
        else if (m_nDifficulty > (int)eDifficulty.Level3 && m_TManager.m_nowLevel == eDifficulty.Level2)
        {
            m_TManager.ChangeLevel(eDifficulty.Level3);
            m_EventManager.ChangeLevel(eDifficulty.Level3);
        }
    }
	public void Render()
	{
        m_uiRender.Render();
		m_TManager.Render();
        m_EventManager.Render();
        m_Player.Render();
	}

}

using System;

class GameScene : IExecute
{
	Stage m_stage;


	public GameScene()
	{
        Console.SetWindowSize(90, 32);
        Init();
	}

	public void Init()
	{
		m_stage = new Stage();
	}

	public void Update(float a_fDelta)
	{

		m_stage.Update(a_fDelta);

        //플레이어가 죽으면 생성됬던 게임씬에 필요한 싱글톤, 인스턴스 다 지웁니다.
        if (m_stage.m_Player.m_bIsDie)
		{
			TileManager.DeletInstance();
            TableManager.DeletInstance();
            EventManager.DeletInstance();
            m_stage.m_Player = null;
            m_stage.m_uiRender = null;
            this.TotestChange(eScene.Record,m_stage.m_TManager.m_nScore);
		}
    }

	public void Render()
	{
		
		m_stage.Render();
	}

}

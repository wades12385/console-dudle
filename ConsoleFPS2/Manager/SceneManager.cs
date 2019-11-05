using System.Diagnostics;
using System;

class SceneManager
{
    #region SINGLETON

    static SceneManager instance = null;

    static public void CraeteInstance()
    {
        Debug.Assert(instance == null);
        instance = new SceneManager();
        instance.Init();
    }
    static public SceneManager Get() { Debug.Assert(instance != null); return instance; }

    private SceneManager() { }

    #endregion SINGLETON

    //부모타입으로 객체를 만들고 씬 바꿀때 마다 자식들을 인스턴스 하기 위한 인터페이스 객체

    public static IExecute m_pNowScene = null;

    void Init()
    {
        m_pNowScene.TotestChange(eScene.Title);
    }

    public void Update(float a_fDelta)
    {
        m_pNowScene.Update(a_fDelta);
    }

    public void Render()
    {
        m_pNowScene.Render();
    }
}

//씬 바꾸기용 확장메서드
public static class SceneEx
{
    public static void TotestChange(this IExecute s, eScene es, int score = 0)
    {
        SceneManager.m_pNowScene = null;

        switch (es)
        {
            case eScene.Title:
                SceneManager.m_pNowScene = new TitleScene(); break;
            case eScene.InGame:
                SceneManager.m_pNowScene = new GameScene(); break;
            case eScene.Record:
                SceneManager.m_pNowScene = new RecordScene(score); break;
            default: break;
        }
        Console.Clear();
    }

}


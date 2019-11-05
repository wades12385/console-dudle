using System;
using System.Diagnostics;

class EventManager : IExecute
{
    #region SingleTon
    static public EventManager Instance = null;

    static public void CreateSingleTon()
    {
        Debug.Assert(Instance == null);

        Instance = new EventManager();
    }

    static public EventManager Get() { Debug.Assert(Instance != null); return Instance; }

    static public void DeletInstance()
    {
        Debug.Assert(Instance != null);
        Instance = null;
    }

    private EventManager()
    {
        Init();
    }
    #endregion


    //필요한 데이터 접근하기 쉽게 쉘카피 객체

    TileManager m_refTileManager = null;
    Player m_refPlayer = null;


    //이벤트 실행단계
    //랜덤으로 결정된 이벤트가 실행되는 단계
    //이벤트가 끝나면 다음 이벤트 준비 단계 2개의 페이즈가 반복됨 

    public eEvent m_EventType; //랜덤 이벤트 종류
    public eDifficulty m_nowLevel; //현재 레벨 

    public bool m_eventRest; //준비 단계인가 판별용 

    float m_fDurationTime; //이벤트 지속시간
    float m_fRestTime;   // 지속시간 동안 생성된 오브젝트들이 정리되는 시간 


    //Scene 전환 메서드와 비슷한 방식으로 인터페이스 객체에다 이벤트 클래스들을 인스턴스 해서 실행
    //나중에 이벤트 추가 작업이 간단해짐 

    public  IEventExecute m_nowEvent = null;


    public void Init()
    {
        m_nowLevel = eDifficulty.Level1;
        m_EventType = eEvent.None;

        m_eventRest = false;
        m_fDurationTime = 0;
        m_fRestTime = 0;
    }
    public void Render()
    {
        //이벤트페이즈 , 준비페이즈가 아니면 랜더 스킵
        if (m_fDurationTime <= 0 && m_fRestTime <=0) return;
            m_nowEvent.Render();
    }
    public void Update(float a_Delta)
    {
        if (m_nowLevel == eDifficulty.Level1) return;

        //난이도 2부터 이벤트 발생 
        if (m_fRestTime <= 0 && m_fDurationTime <= 0)
            NewEvent();
        else if ( m_fDurationTime > 0)//지속시간이 남아 있는 동안 해당 이벤트 update 실행 
            EventExecute(a_Delta);
        else  if (m_eventRest) // 준비단계 일경우 일단 이벤트는 update돌아감 이유는 아래에 후술 
        {
            EventExecute(a_Delta);
            m_fRestTime -= a_Delta;
        }
    }

    public void NewEvent()
    {
        m_EventType = RandomEnum.Of<eEvent>();//랜덤한 enum값 반환 

       //랜덤으로 이벤트가 인스턴스되고 실행하게 됨  
        switch (m_EventType)
        {
            case eEvent.None:
                break;
            case eEvent.Arrow:
                m_nowEvent = new ArrowEvent(m_nowLevel, m_refPlayer);
                break;
            case eEvent.Drop:
                 m_nowEvent = new DropEvent(m_nowLevel, m_refTileManager);
                break;
            default:
                break;
        }
        //지속시간 설정 및 준비페이즈 false;
        m_fDurationTime = (float)eEventTime.Duration;
        m_eventRest = false;
    }

    //준비단계를 만든 이유가 만약 방해물이 총알이나 화살같은게 날라오는 이벤트 일경우
    //지속시간이 끝나면 날라가는 도중에 사라지게 됨 
    //지속시간이 끝나면 화살들의 생성만 멈추고 생성된건 마저 날라가서 없어질 때까지 마무리가 되도록 하기 위해 
    //이벤트 단계를 2단계로 나눔
    public void EventExecute(float a_Delta)
    {
        


        //eventrest가 참이면 오브젝트를 생성하는 이벤트들은 
        //생성을 멈추고 rest타임 동안 생성된 오브젝트들이 사라지도록 기다림 
        m_nowEvent.Update(a_Delta, m_eventRest);
        m_fDurationTime -= a_Delta;

        //이벤트 지속시간이 끝나고 준비단계로 전환 
        if (m_fDurationTime < 0 && m_eventRest ==false)
        {
            m_fRestTime = (float)eEventTime.RestTime;
            m_eventRest = true;
        }
    }
    public void ReceiveData(TileManager a_T, Player a_P)
    {
        m_refTileManager = a_T;
        m_refPlayer = a_P;
    }
    public void ChangeLevel(eDifficulty a_elevel)
    {
        m_nowLevel = a_elevel;
    }

}

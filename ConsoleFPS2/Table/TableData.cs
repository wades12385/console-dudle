struct ArrowEventTable
{
    //쏘는 주기 
    public float m_fShotTiem;
    //화살 이동 주기 
    public float m_fMoveTime;
    //생성 갯수 
    public float m_fMakeCount;

    public ArrowEventTable(float m_fShotTiem, float m_fMoveTime, float m_fMakeCount)
    {
        this.m_fShotTiem = m_fShotTiem;
        this.m_fMoveTime = m_fMoveTime;
        this.m_fMakeCount = m_fMakeCount;
    }
}

struct TileDataTable
{

    //양옆 길이 
    public int SizeMin;
    public int SizeMax;
    public int TileCount;
    //무브 타일 속도
    public float TileMoveSecond;

    //타일 생성 간격
    public int SearchRange;


    //타일 타입 생성 확률
    public int WeekChancePer;
    public int ItemChancePer;
    public int JumpChancePer;
    public int MoveChancePer;

    public TileDataTable(int sizeMin, int sizeMax, float tileMoveSecond, 
        int searchRange, int weekChancePer, int itemChancePer, int jumpChancePer, int moveChancePer, int tileCount)
    {
        SizeMin = sizeMin;
        SizeMax = sizeMax;
        TileCount = tileCount;
        TileMoveSecond = tileMoveSecond;
        SearchRange = searchRange;
        WeekChancePer = weekChancePer;
        ItemChancePer = itemChancePer;
        JumpChancePer = jumpChancePer;
        MoveChancePer = moveChancePer;
    }
}



struct DropEventTable
{
    public float DropSpeed;

    public DropEventTable(float dropSpeed)
    {
        DropSpeed = dropSpeed;
    }
}
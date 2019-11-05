using System;
using System.Diagnostics;

struct Vec2
{
	public float x;
	public float y;

	public Vec2(float _x, float _y)
	{
		x = _x; y = _y;
	}

	public static Vec2 operator +(Vec2 a, Vec2 b)
	{
		return new Vec2(a.x + b.x, a.y + b.y);
	}

	public static Vec2 operator *(Vec2 a, float b)
	{
		return new Vec2(a.x * b, a.y * b);
	}
	public static bool operator ==(Vec2 a, Vec2 b)
	{
		return (int)a.x == (int)b.x && (int)a.y == (int)b.y;
	}
	public static bool operator !=(Vec2 a, Vec2 b)
	{
		return !(a == b);
	}
}


public static class RandomEnum
{
	private static Random rand = new Random(Environment.TickCount);

	public static T Of<T>()
	{
        Debug.Assert(typeof(T).IsEnum);
		Array enumValues = Enum.GetValues(typeof(T));
		return (T)enumValues.GetValue(rand.Next(1,enumValues.Length));
	}
}



static class Util
{
	static public bool ConsoleBoundaryCheck(Vec2 v)
	{
		return !(
			(v.x < 0 || v.x > Console.WindowWidth) ||
			(v.y < 0 || v.y > Console.WindowHeight));
	}
}



public enum eScene //씬 종류
{
	Title,
	InGame,
	Record,
}
public enum eItem //아이템 종류
{
    None,
	JumpUp,
	SlowFall
}

public enum eTileType //발판 종류
{
	None,
	Week,
	Jump,
	move,
	Item,
}

enum eEvent //이벤트종류
{
    None,
    Arrow,
    Drop,
}

enum eDifficulty //난이도 조건점수
{
    Level1,
    Level2 = 30,
    Level3 = 50,
}

enum eEventTime//이벤트 시간 
{
    Duration = 10,
    RestTime = 7,

}


//ui관련 상수값
enum eUiValue
{
    UiScroeStartPosX= 88,
    UiScroeStartPosY = 10,
    UiPanelX = 43,
    UiPanelY = 1,

    EventUix = 44,
    EventUiy = 20,

    EventTextUix = 44,
    EventTextUiy = 23,

   RecordUisizeX = 80,
  RecordUisizeY = 30,

    BestRecordInitPosX = 3,
    BestRecordInitPosY = 1,

    UserInitPosY = 20,

}

//최대값
enum eMaxValue
{
    ItemMax =3,
    KeyMax = 173,
}

//상수값 정의
public enum eValueNum
{
	ScreenLeft =2,
	ScreenRight = 38,
    ScreenTop = 0,
	ScreenFontWidth = 5,

    TileScrollDownLine = 15,
	TileEndlLine =25,

    TileSponeXMin = 5,
    TileSponeXMax = 30,

    StartPlayerSpone = 8,

    A = 44,
    FromStrToImage = 96,
    FromKeyToImage = 43,
    Z = 69,
    Blank =0,
    BackSpace =2,

    SerchTileRangeMin = -1,
    SerchTileRangeMax = 1,
}
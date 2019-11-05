using System;

class Timer
{


    public static Timer Instance = null;
    const float nFpsSet = 40;
    public const long tickPerSececond = 10000000;

	DateTime lastTime;
    public float runningTime;
    public long TargetTicks;  //1
	
	static public void CreateSingleTon()
    {
        if (Instance == null)
        {
            Instance = new Timer();
            Instance.Init();
        }
    }

    static public Timer Get()
    {
        return Instance;
    }

    public void Init()
    {
      //  timeElapsed = 0;
        lastTime = DateTime.Now;
        runningTime = 0;
        TargetTicks = (long)GetTargetFPSTicks();
    }

    public float Delta { get { return runningTime / (float)tickPerSececond; } }

	public void Update()
    {
        runningTime += (DateTime.Now - lastTime).Ticks;
        lastTime = DateTime.Now;

    }

    static float GetTargetFPSTicks()
    {
        return tickPerSececond / nFpsSet;
    }


}

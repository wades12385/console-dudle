using System;
using System.Collections.Generic;
using System.Windows.Input;
using System.Linq;
class Program
{

	[STAThread]
	static void Main(string[] args)
    {
        Timer.CreateSingleTon();
		Timer sTime = Timer.Get();
		InputKey.CreateSingleTon();
        SceneManager.CraeteInstance();
        SceneManager sceneMng = SceneManager.Get();
        
		Console.CursorVisible = false;


        while (true)
        {
            sTime.Update();

            if (sTime.runningTime >= sTime.TargetTicks)
            {
                sceneMng.Update(sTime.Delta);

                sceneMng.Render();

                sTime.runningTime = 0;





            }

        }


      

    }
}


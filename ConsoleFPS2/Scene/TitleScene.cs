using System;
using System.Windows.Input;

class TitleScene : IExecute
{
	int m_nSelecPos;

	public TitleScene()
	{
        Console.SetWindowSize(30, 8);
        Init();
	}

	public void Init()
	{
		m_nSelecPos = 0;
	}

	public void Render()
	{
        Console.SetCursorPosition(0,0);
		if (m_nSelecPos == 0) Console.Write("->");
		Console.Write("  Play Game   ");
        Console.SetCursorPosition(0, 1);
        if (m_nSelecPos == 1) Console.Write("->");
		Console.Write("  Exit   ");
        Console.SetCursorPosition(0, 4);
        Console.Write("Press Enter to Select Menu");

    }

    public void Update(float a_fDelta)
	{
        //입력받으면서 m_nSelecPos로 메뉴선택 
        if (InputKey.Get().KeyDown(Key.W) && m_nSelecPos>0)
			m_nSelecPos--;
		if (InputKey.Get().KeyDown(Key.S) && m_nSelecPos < 1)
			m_nSelecPos++;
		if (InputKey.Get().KeyDown(Key.Enter))
			SelecNextScene();
    }

    public void SelecNextScene()
	{
		if (m_nSelecPos == 0)
			this.TotestChange(eScene.InGame);
		else
			Environment.Exit(0);
	}
}

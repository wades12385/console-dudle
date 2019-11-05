using System.Collections.Generic;
using System.Windows.Input;
using System.Diagnostics;
using System;

class InputKey
{
    #region SingleTon

    static InputKey instance = null;


    static public void CreateSingleTon()
    {
        Debug.Assert(instance == null);
        instance = new InputKey();
      
    }

    static public InputKey Get()
    {
        Debug.Assert(instance != null);
        return instance;
    }
    InputKey() { Init(); }
    #endregion


    //bool 배열로 Key down과 keyup에서 필요한 조건문 체크용 
    bool[] m_bIsDown =new bool[(int)eMaxValue.KeyMax];
    bool[] m_bIsUp = new bool[(int)eMaxValue.KeyMax];

    public void Init()
    {
        for(int i = 0; i< (int)eMaxValue.KeyMax; i++)
        {
            m_bIsDown[i] = false;
            m_bIsUp[i] = false;
        }   
    }

    public bool KeyDown(Key key)
    {
        if (Keyboard.GetKeyStates(key).HasFlag(KeyStates.Down))
        {
            if (m_bIsDown[(int)key] == false)
            {
                m_bIsDown[(int)key] = true;
                return true;
            }
        }
        else
        {
            m_bIsDown[(int)key] = false;
        }
        return false;

    }

    public bool KeyUp(Key key)
    {
        if (Keyboard.GetKeyStates(key).HasFlag(KeyStates.Down))
        {
            m_bIsUp[(int)key] = true;
        }
        else
        {
            if (m_bIsUp[(int)key] == true)
            {
                m_bIsUp[(int)key] = false;
                return true;
            }
        }
        return false;
    }

    //누른키를 찾아서 그 키의 값을 int값으로 반환 
    public int FromAnyKeyToInt()
    {
        Array arr = Enum.GetValues(typeof(Key));

        foreach (var v in arr)
        {
            if (((Key)v) != Key.None)
            {
                if (KeyDown((Key)v))
                    return (int)(v);
            }
        }
         return (int)eMaxValue.KeyMax;
    }
    
    public bool keyPress(Key key)
    {
            if (Keyboard.IsKeyUp(key)) return false;
        return true;
    }
}

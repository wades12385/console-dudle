using System;
using System.Windows.Input;
class UiRender : IExecute
{
    string[,] m_Number = new string[10, 7]
    {
        {"■■■■",
         "■    ■",
         "■    ■",
         "■    ■",
         "■    ■",
         "■    ■",
         "■■■■",
        },
        {
         "    ■  ",
         "  ■■  ",
         "■  ■  ",
         "    ■  ",
         "    ■  ",
         "    ■  ",
         "■■■■",

        },
        {
         "■■■■",
         "      ■",
         "      ■",
         "■■■■",
         "■      ",
         "■      ",
         "■■■■",
        },
         {
         "■■■■",
         "      ■",
         "      ■",
         "■■■■",
         "      ■",
         "      ■",
         "■■■■",
        },
         {
         "■    ■",
         "■    ■",
         "■    ■",
         "■■■■",
         "      ■",
         "      ■",
         "      ■",
        },
    {
         "■■■■",
         "■      ",
         "■      ",
         "■■■■",
         "      ■",
         "      ■",
         "■■■■",
        },
    {
         "■■■■",
         "■      ",
         "■      ",
         "■■■■",
         "■    ■",
         "■    ■",
         "■■■■",
        },
           {
         "■■■■",
         "■    ■",
         "■    ■",
         "      ■",
         "      ■",
         "      ■",
         "      ■",
        },
          {
         "■■■■",
         "■    ■",
         "■    ■",
         "■■■■",
         "■    ■",
         "■    ■",
         "■■■■",
        },
          {
         "■■■■",
         "■    ■",
         "■    ■",
         "■■■■",
         "      ■",
         "      ■",
         "■■■■",
        }
    };
    // m_arrAlphabet 이전에 만들었는데 사이즈가 다른 버전이라 냅둡니다.
    string[,] m_Score = new string[5, 7]
    {
         {
            "  ■■■",
            "■      ",
            "■      ",
            "  ■■  ",
            "      ■",
            "      ■",
            "■■■  ",
        },
         {
            "  ■■  ",
            "■    ■",
            "■      ",
            "■      ",
            "■      ",
            "■    ■",
            "  ■■  ",
        },{
            "  ■■  ",
            "■    ■",
            "■    ■",
            "■    ■",
            "■    ■",
            "■    ■",
            "  ■■  ",
        },{
            "  ■■  ",
            "■    ■",
            "■    ■",
            "■■■  ",
            "■    ■",
            "■    ■",
            "■    ■",
        },
         {
            "■■■■",
            "■      ",
            "■      ",
            "■■■■",
            "■      ",
            "■      ",
            "■■■■",
        },
    };
    // 0 = Blank 1 = A ....26 = Z  
    string[,] m_arrAlphabet = new string[27, 7]
    {
           {
            "          ",
            "          ",
            "          ",
            "          ",
            "          ",
            "          ",
            "          ",
              },
          {
            "  ■■■    ",
            "■      ■ ",
            "■      ■",
            "■■■■■",
            "■      ■",
            "■      ■",
            "■      ■",
        },
         {
            "■■■■  ",
            "■      ■",
            "■      ■",
            "■■■■■",
            "■      ■",
            "■      ■",
            "■■■■  ",
        },
          {
            "  ■■■  ",
            "■      ■",
            "■        ",
            "■        ",
            "■        ",
            "■      ■",
            "  ■■■  ",
        },

        {
            "■■■■  ",
            "■      ■",
            "■      ■",
            "■      ■",
            "■      ■",
            "■      ■",
            "■■■■  ",
        },
                 {
            "■■■■■",
            "■        ",
            "■        ",
            "■■■■■",
            "■        ",
            "■        ",
            "■■■■■",
        },
         {
            "■■■■■",
            "■        ",
            "■        ",
            "■■■■■",
            "■        ",
            "■        ",
            "■        ",
          },
         {
            "  ■■■  ",
            "■      ■",
            "■        ",
            "■        ",
            "■    ■■",
            "■      ■",
            "  ■■■  ",
        },
         {
            "■      ■",
            "■      ■",
            "■      ■",
            "■■■■■",
            "■      ■",
            "■      ■",
            "■      ■",
        },{
            "■■■■■",
            "    ■    ",
            "    ■    ",
            "    ■    ",
            "    ■    ",
            "    ■    ",
            "■■■■■",
        },
         {
            "  ■■■  ",
            "      ■  ",
            "      ■  ",
            "      ■  ",
            "      ■  ",
            "■    ■  ",
            "  ■■■  ",
        },
         {
            "■        ",
            "■    ■  ",
            "■  ■    ",
            "■■      ",
            "■■      ",
            "■  ■    ",
            "■    ■  ",
        },
         {
            "■        ",
            "■        ",
            "■        ",
            "■        ",
            "■        ",
            "■        ",
            "■■■■■",
          },
         {
            "■      ■",
            "■■  ■■",
            "■  ■  ■",
            "■  ■  ■",
            "■      ■",
            "■      ■",
            "■      ■",
        },
         {
            "■■    ■",
            "■■    ■",
            "■  ■  ■",
            "■  ■  ■",
            "■    ■■",
            "■    ■■",
            "■      ■",
        },
         {
            "  ■■■  ",
            "■      ■",
            "■      ■",
            "■      ■",
            "■      ■",
            "■      ■",
            "  ■■■  ",
        }, 
         {
            "  ■■■  ",
            "■      ■",
            "■      ■",
            "■■■■■",
            "■        ",
            "■        ",
            "■        ",
        },
         {
            "  ■■■  ",
            "■      ■",
            "■      ■",
            "■  ■  ■",
            "■  ■  ■",
            "■    ■■",
            "  ■■■■",
        },
         {
            "  ■■■  ",
            "■      ■",
            "■      ■",
            "■■■■  ",
            "■      ■",
            "■      ■",
            "■      ■",
        },
         {
            "  ■■■■",
            "■        ",
            "■        ",
            "  ■■■  ",
            "        ■",
            "        ■",
            "■■■■  ",
        },
         {
            "■■■■■",
            "    ■    ",
            "    ■    ",
            "    ■    ",
            "    ■    ",
            "    ■    ",
            "    ■    ",
        }, 
         {
           "■      ■",
           "■      ■",
           "■      ■",
           "■      ■",
           "■      ■",
           "■      ■",
           "  ■■■  ",
        },
         {
            "■      ■",
            "■      ■",
            "■      ■",
            "■      ■",
            "■■  ■■",
            "  ■  ■  ",
            "    ■    ",
        },{
             "■      ■",
             "■      ■",
             "■      ■",
             "■      ■",
             "■  ■  ■",
             "■■  ■■",
             "■      ■",
        },
         {
            "■      ■",
            "  ■  ■  ",
            "    ■    ",
            "    ■    ",
            "    ■    ",
            "  ■  ■  ",
            "■      ■",
        },
         {
             "■      ■",
             "  ■  ■  ",
             "  ■  ■  ",
             "    ■    ",
             "    ■    ",
             "    ■    ",
             "    ■    ",
        },
          {
           "■■■■■",
           "        ■",
           "      ■  ",
           "    ■    ",
           "  ■      ",
           "■        ",
           "■■■■■",
          },
    };

    //이미지 문자 출력시 필요한 간격들 
    int m_NumberInterval;
    int m_AlphabetInterval;

    //int -> string 받을 맴버변수 
    string m_scoreStrData;


    Player m_refPlayer;
    EventManager m_refEventMg;

    public UiRender()
    {
        m_AlphabetInterval = 11;
        m_NumberInterval = 9;
        m_scoreStrData = string.Empty;
    }

    public void Init()
    {



    }

    public void Render()
    {
        UiItem();
        UiPanel();
        ScoreRender(m_scoreStrData, (int)eUiValue.UiScroeStartPosX, (int)eUiValue.UiScroeStartPosY);
        EvnetUiRender();
        UiClear();
    }

    public void Update(float a_score)
    {
        m_scoreStrData = ((int)a_score).ToString();
    }

    //int값 점수를 string케스팅 하고 숫자 하나씩 저장해놓은 배열값대로 출력 
    public void ScoreRender(string a_sScore, int a_UiPosX , int a_UiPosy)
    {
        int UiScorePosX = a_UiPosX - m_NumberInterval;

        for (int i = a_sScore.Length - 1; i >= 0; i--)
        {
            int num = int.Parse(a_sScore[i].ToString());
            for (int j = 0; j < m_Number.GetLength(1); j++)
            {
                Console.SetCursorPosition(UiScorePosX, a_UiPosy + j);
                Console.Write(m_Number[num, j]);
            }
            UiScorePosX -= m_NumberInterval;
        }
        Console.SetCursorPosition(0, 0);

    }


    //인게임 라인 그리기 용 
    void UiPanel()
    {
        #region GameLine
        for (int i = 0; i <= 25; i++)
        {
            Console.SetCursorPosition((int)eValueNum.ScreenLeft, i);
            Console.Write('■');
            Console.SetCursorPosition((int)eValueNum.ScreenRight, i);
            Console.Write('■');
        }
        Console.SetCursorPosition((int)eValueNum.ScreenLeft, 26);
        Console.Write("■■■■■■■■■■■■■■■■■■■");
        #endregion


        //대문짝만한 score 단어 그리기용 
        int UiScorePosX = (int)eUiValue.UiPanelX;

        for (int i = 0; i < m_Score.GetLength(0); i++)
        {
            for (int j = 0; j < m_Score.GetLength(1); j++)
            {
                Console.SetCursorPosition(UiScorePosX, (int)eUiValue.UiPanelY + j);
                Console.Write(m_Score[i, j]);
            }
            UiScorePosX += m_NumberInterval;
        }
    }


    //아이템 ui 
    void UiItem()
    {
        #region ItemUi
        Console.SetCursorPosition((int)eValueNum.ScreenLeft + 2, 28);
        Console.Write("Jump : ");
        for (int i = 0; i < (int)eMaxValue.ItemMax; i++)
        {
            if (i < m_refPlayer.m_ItemJumpCount)
                Console.Write("[J] ");
            else
                Console.Write("    ");
        }
        Console.SetCursorPosition((int)eValueNum.ScreenLeft + 2, 29);
        Console.Write("Slow : ");
        for (int i = 0; i < (int)eMaxValue.ItemMax; i++)
        {
            if (i < m_refPlayer.m_ItemSlowCount)
                Console.Write("[S] ");
            else
                Console.Write("    ");

        }
        #endregion
    }

    //이벤트 확인 ui 
    public void EvnetUiRender()
    {
        if (m_refEventMg.m_EventType == eEvent.None) return;

        if (m_refEventMg.m_nowEvent is ArrowEvent)
        {
            Console.SetCursorPosition((int)eUiValue.EventUix, (int)eUiValue.EventUiy);
            Console.Write("이벤트 발생 : 화살조심");
            Console.SetCursorPosition((int)eUiValue.EventTextUix, (int)eUiValue.EventTextUiy);
            Console.Write("양쪽벽에서 화살이 날라옴");

        }

        if (m_refEventMg.m_nowEvent is DropEvent)
        {
            Console.SetCursorPosition((int)eUiValue.EventUix, (int)eUiValue.EventUiy);
            Console.Write("이벤트 발생 : 타일이 내려감");
            Console.SetCursorPosition((int)eUiValue.EventTextUix, (int)eUiValue.EventTextUiy);
            Console.Write(" 발판이  점점 내려감");
        }

    }



    //점수 기록용 메서드 ShowScoreBord 에서 입력된 a_Initail 값에 따라 알파벳 이미지가 그려진다.
    public void ScorePanal(int[] a_BestScoreName, int a_BestScore, int a_Playerscore, int[] a_Initail, bool a_isNewBestRecord)
    {

        InitialRender(a_BestScoreName, (int)eUiValue.BestRecordInitPosX, (int)eUiValue.BestRecordInitPosY);

        ScoreRender(a_BestScore.ToString(), 75, 10);

        ScoreBordLine(a_BestScore.ToString().Length);

        ScoreRender(a_Playerscore.ToString(), 75, 21);

        InitialRender(a_Initail, (int)eUiValue.BestRecordInitPosX, (int)eUiValue.UserInitPosY);

        ScoreBordConment(a_isNewBestRecord);
    }

    void InitialRender(int[] a_sInitial, int a_nStartPosX, int a_nStartPosY)
    {

        int UiPosX = a_nStartPosX;
        for (int i = 0; i < a_sInitial.Length; i++)
        {
            for (int j = 0; j < m_arrAlphabet.GetLength(1); j++)
            {
                Console.SetCursorPosition(UiPosX, a_nStartPosY + j);
                Console.Write(m_arrAlphabet[a_sInitial[i], j]);
            }

            UiPosX += m_AlphabetInterval;
        }

    }
    public void ReciveData(Player p, EventManager e)
    {
        m_refEventMg = e;
        m_refPlayer = p;
    }
    void UiClear()
    {
        if (m_refEventMg.m_eventRest == false) return;
        Console.SetCursorPosition((int)eUiValue.EventUix, (int)eUiValue.EventUiy);
        Console.Write("                                ");
        Console.SetCursorPosition((int)eUiValue.EventTextUix, (int)eUiValue.EventTextUiy);
        Console.Write("                                ");
    }

    //매직넘버 고치다가 포기

    void ScoreBordLine(int Maxline)//선그리는 용
    {
        Console.SetCursorPosition(0, 9);
        for(int i=0; i<36-((m_NumberInterval*Maxline)/2);i++)
        {
            Console.Write("▶");
        }
        for (int i = 0; i <9; i++)
        {
            Console.SetCursorPosition(70- (m_NumberInterval * Maxline), 10+i);
            Console.WriteLine("▼");
        }
        Console.SetCursorPosition(70 - (m_NumberInterval * Maxline), 19);
        for (int i = 0; i < (Console.WindowWidth- (70 - (m_NumberInterval * Maxline)))/2; i++)
        {
            Console.Write("▶");
        }
    }

    void ScoreBordConment(bool a_bIsNewRecord) //점수판랜더 할때 같이 나올 텍스트 
    {
        Console.SetCursorPosition(50, 2);
        Console.WriteLine("<<BEST RECORD>>");
        Console.SetCursorPosition(40,4);
        Console.WriteLine("<<<< 이 사람의 최고 기록 ↓↓↓");

        Console.SetCursorPosition(3, 11);
        Console.WriteLine("     <<YOUR SOCRE>>");

        if (a_bIsNewRecord)
        {
            Console.SetCursorPosition(3, 13);
            Console.WriteLine("기록 갱신 축하!!! 이니셜을 남기세요 ");
            Console.SetCursorPosition(3, 14);
            Console.WriteLine("키보드로 바로 입력되며 ");
            Console.SetCursorPosition(3, 15);
            Console.WriteLine("벡스페이스도 적용됩니다.");
        }
            Console.SetCursorPosition(3, 16);
            Console.WriteLine("Space로 넘어갑니다.");

    }

    public void GameOverRender()// 죽으면 나오는거 
    {
        Console.SetWindowSize(100, (int)eUiValue.RecordUisizeY);

        int[] Text = {7,1,13,5,15,22,5,18};//game over 

        InitialRender(Text, 5, 5);

        Console.SetCursorPosition(0, 28);
        Console.Write("Press any key to Enter...");
        //엔터키나 누르면 메서드 나감
        while (InputKey.Get().KeyDown(Key.Enter)==false) ;
    }
}

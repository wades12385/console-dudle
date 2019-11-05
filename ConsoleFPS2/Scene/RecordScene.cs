using System;
using System.Windows.Input;
using System.IO;
using System.Collections.Generic;

class RecordScene : IExecute
{

    private int m_nowScore;
    private int m_RankScore;
    private string m_sRankerName;
    int[] m_UserNameToInt;
    int[] m_RankerNameToInt;
    StreamWriter m_fileWrite;
    StreamReader m_fileRead;


    bool m_bIsNewRecord;
    int m_nNameIndex; //ShowScoreBord 매서들에서 리스트 인덱스용
    UiRender m_uiRender;

    public RecordScene(int a_score)
    {
        m_uiRender = new UiRender();


        //생성자에서 받은 점수 쓰기 
        m_nowScore = a_score;

        //0은 공백문자를 출력할거라 미리 0으로 초기화 해놓음;
        m_UserNameToInt = new int[] { 0, 0, 0 };
        m_RankerNameToInt = new int[3];
        Console.Clear();
        m_nNameIndex = 0;
        Init();
    }

    public void Init()
    {

        m_uiRender.GameOverRender();

        FileCheck();

        m_bIsNewRecord = m_nowScore > m_RankScore ;


        //기록 갱신 실폐시 입력 대신 출력될 이니셜 
        if (m_bIsNewRecord == false)
        {
            m_UserNameToInt[0] = 25;
            m_UserNameToInt[1] = 15;
            m_UserNameToInt[2] = 21;
            m_nNameIndex = 3; 
        }
        Console.SetWindowSize((int)eUiValue.RecordUisizeX, (int)eUiValue.RecordUisizeY);
    }


    public void Update(float a_Delta)
    {
        ShowScoreBord();

       // 이니셜 3칸 입력이 안되어 있으면 안넘어감 
        if (InputKey.Get().keyPress(Key.Space) && m_nNameIndex ==3)
        {
            WriteNewRecord(m_nowScore);
            this.TotestChange(eScene.Title);
        }
    }

    void WriteNewRecord(int a_Score)
    {
        if (m_bIsNewRecord == false) return;
        //파일 읽기 쓰기용으로 
        m_fileWrite = new StreamWriter("../../Record.txt");

        for (int i = 0; i < m_UserNameToInt.Length; i++)
            m_UserNameToInt[i] += 96;

        //m_UserNameToInt int배열을 char 배열로 변환 
        char[] temp = Array.ConvertAll<int, char>(m_UserNameToInt, 
           new Converter<int, char>((c) => Convert.ToChar(c)));

        //char[] -> string
        string name = new string(temp);

        //입력한 내용 텍스트에 쓰기  

         m_fileWrite.WriteLine(name+","+a_Score);

         //파일 닫기
        m_fileWrite.Close();
    }

    void ShowScoreBord()
    {
        //기존 기록 넘으면 자신의 이니셜 기록할 수 있고 안되면 보는는것만 
        //space로 넘어감 

        int keyData = InputKey.Get().FromAnyKeyToInt();

        if (m_bIsNewRecord)
        {
            //A~Z 를 눌렀을 때만 
            if ((int)Key.A <= keyData && keyData <= (int)Key.Z
                && m_nNameIndex < m_UserNameToInt.Length)
            {
                //44~ 69 값을 1~26으로 
                m_UserNameToInt[m_nNameIndex] = keyData - (int)eValueNum.FromKeyToImage;
                m_nNameIndex++;
            }
            //백스페이스
            if ((int)Key.Back == keyData && 0 < m_nNameIndex)
            {
                m_nNameIndex--;
                m_UserNameToInt[m_nNameIndex] = (int)eValueNum.Blank;
            }
        }


        m_uiRender.ScorePanal(m_RankerNameToInt, m_RankScore, m_nowScore, m_UserNameToInt, m_bIsNewRecord);

    }
    public void Render()
    {

    }

    //텍스트 파일 열어서 필요한 데이터 받음 
    void FileCheck()
    {
		//파일이 있는지 체크 없으면 생성 
		if (File.Exists("../../Record.txt") == false)
		{
			m_fileWrite = File.CreateText("../../Record.txt");
			m_fileWrite.Close();
		}
        //파일 읽음 
        m_fileRead = new StreamReader("../../Record.txt");

        string temp = m_fileRead.ReadLine();

        //파일에 데이터가 없을 경우 
        if (string.IsNullOrEmpty(temp))
        {
            m_RankScore = 0;
            m_sRankerName = "";
        }
        else
        {
			
            string[] str = temp.Split(',');
            m_RankScore = int.Parse(str[1]);
            m_sRankerName = str[0];

            for (int i = 0; i < str[0].Length; i++)
                m_RankerNameToInt[i]= str[0][i]-(int)eValueNum.FromStrToImage;
        }
        m_fileRead.Close();

    }
}

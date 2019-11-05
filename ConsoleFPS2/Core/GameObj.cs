using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


abstract class GameObj 
{
	public Vec2 m_vcPos;
	public Vec2 m_vcDir;
	public int m_size;
	public char m_cImage;
	public float m_fSpeed;

	virtual public void Update(float a_Delta) { }
    virtual public void Update(float a_Delta,Player a_player) { }

    virtual public void Update(float a_fDelta,float a_moveTileSec) { }

    virtual public void ClearRender()
	{
		if (Util.ConsoleBoundaryCheck(m_vcPos) == false) { return; }
		Console.SetCursorPosition((int)m_vcPos.x, (int)m_vcPos.y);
		Console.Write(' ');
	}
	virtual public void Render()
	{
		if (Util.ConsoleBoundaryCheck(m_vcPos) == false) { return; }

		Console.SetCursorPosition((int)m_vcPos.x, (int)m_vcPos.y);
		Console.Write(m_cImage);
	}
    virtual public void Init() { }
    virtual public void Init(Vec2 a_vPos) { }
    virtual public void Init(Vec2 a_vPos, Vec2 a_vDir) { }
	virtual public void Init(Vec2 a_vPos, int a_nSize, eTileType eTile) { }
	virtual public bool Interaction(Player p) { return false; } 
	virtual public bool Interaction() { return false; } 
}

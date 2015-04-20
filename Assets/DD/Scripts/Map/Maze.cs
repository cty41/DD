using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Maze : MonoBehaviour
{
// Properties:
	// enum:
	public enum CorriderDir
	{
		CD_Row,
		CD_Col
	}
	// struct:
	public struct MiniMapInfo
	{
		public Map.MapType type;
		public int row;
		public int col;
		public CorriderDir dir;
	};
	// public:
	public bool[,] R, D, v;
	public int row, col;
	public IList<MiniMapInfo> miniMapInfos;
// Functions:
	// dfs search
	void dfs(int r, int c)
	{
		int d = Random.Range(0, 3);
		int dd = Random.Range(0, 1);
		dd = dd == 1 ? 1 : 3;
		v[r,c] = true;
		for (int i = 0; i < 4; i++)
		{
			int [] debugTest0 = {-1,0,1,0};
			int [] debugTest1 = {0,-1,0,1};
			int rr = r + debugTest0[d];
			int cc = c + debugTest1[d];
			
			if ((uint)rr < row && (uint)cc < col && ! v[rr,cc])
			{
				int nextC = d == 1 ? 1 : 0;
				int nextR = d == 0 ? 1 : 0;
				if (d % 2 == 1)
					R[r,c - nextC] = true;
				else
					D[r - nextR,c] = true;
				dfs(rr, cc);
			}
			d = (d + dd) % 4;
		}
	}
	// Init properties
	public void Init()
	{
        row = 5;
        col = 5;

		R = new bool[row, col];
        D = new bool[row, col];
        v = new bool[row, col];

        dfs(row-1, col-1);
		
		//InitMiniMapInfos();
        InitUIElements();
	}
	// create ui IElements
	void CreateUIElement(Map.MapType type, int x, int y)
	{
		string path = "UI/room_panel";
		
		switch (type)
		{
			case Map.MapType.MT_Corrider:
				path = "UI/corrider_panel";
			break;
			
			case Map.MapType.MT_Room:
				path = "UI/room_panel";
			break;
		}
		// TODO:hard code for test
		x -= 500;
		GameObject panelMap = GameObject.Find("panel_map");
		
		if (panelMap != null)
		{
			GameObject obj = Instantiate(Resources.Load(path, typeof(GameObject))) as GameObject;
			obj.transform.SetParent(panelMap.transform, false);
			//obj.transform.localScale = new Vector3(.1f, .1f, .1f);
			obj.transform.position = new Vector3(x, y, obj.transform.position.z);
		}
	}
	// Init mini map info
	void InitMiniMapInfos()
	{
		MiniMapInfo temp;
		miniMapInfos = new List<MiniMapInfo>();
		
		for (int i = 0; i < row; i++)
		{
			/*temp = new MiniMapInfo();
			temp.row = i;
			temp.col = 0;
			temp.type = Map.MapType.MT_Room;
			miniMapInfos.Add(temp);*/
			
			for (int j = 0; j < col-1; j++)
			{
				temp = new MiniMapInfo();
				temp.row = i;
				temp.col = j;
				temp.dir = CorriderDir.CD_Row;
				if (D[i, j])
				{
					temp.type = Map.MapType.MT_Corrider;
					miniMapInfos.Add(temp);
				}
				else
				{
					temp.type = Map.MapType.MT_None;
					miniMapInfos.Add(temp);
				}
				
				temp = new MiniMapInfo();
				temp.row = i;
				temp.col = j;
				temp.dir = CorriderDir.CD_Col;
				if (R[i, j])
				{
					temp.type = Map.MapType.MT_Corrider;
					miniMapInfos.Add(temp);
				}
				else
				{
					temp.type = Map.MapType.MT_None;
					miniMapInfos.Add(temp);
				}
			}
		}
	}

    void InitUIElements()
    {
        int width = -80;
        int offsetX = -30;

        for (int i = 0; i < row; i++)
        {
            for (int j = 0; j < col; j++)
            {
                CreateUIElement(Map.MapType.MT_Room, j * width, i * width);
                if (D[i, j])
                {
                    // means collider
                    CreateUIElement(Map.MapType.MT_Corrider, j * width, i * width + offsetX);
                }
                else
                {
                    // means null
                }

                if (R[i, j])
                {
                    // means collider
                    CreateUIElement(Map.MapType.MT_Corrider, j * width + offsetX, i * width);
                }
                else
                {
                    // means null
                }
            }
        }
        
    }
}
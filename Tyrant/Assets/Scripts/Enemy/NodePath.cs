using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NodePath : GameObjectsLocator.IGameObjectRegister
{
	public class Node
	{
		public List<Node> neighbors = new List<Node>();
		public Node parent;
		public float c = 0;
		public float r = 0;
	
		public float g = 0.0f;
		public float h = 0.0f;
		public bool opened = false;
		public bool closed = false;
	}

	private List<Node> mNodes;
	private List<Node> mGrid = new List<Node>();
	int mColumn;
	int mRow;

    public Node FindNode(int x, int y)
    {
        var Nodes = GameObjectsLocator.Instance.Get<Block>();
        
            foreach (var node in Nodes)
            {
                for (int position = 0; position < node.worldPosition.Count; ++position)
                {
                    if (node.worldPosition[position].x == x && node.worldPosition[position].y == y)
                    {
                        return mNodes[position];
                    }
                }
            }
		return mNodes[0];
	}

    public Node GetNode(int x, int y)
	{
		if (x < 0 || y < 0 || y >= mColumn || x >= mRow)
		{
			return null;
		}
        else
        {
			return mNodes[x + y * mRow];
		}
	}

    //void getGrid()
    //{
    //	var Nodes = GameObjectsLocator.Instance.Get<Block>();
    //	{
    //		foreach (var node in Nodes)
    //		{
    //			foreach (var position in node.gridPosition)
    //			{
    //				Node temp = new Node();
    //				temp.r = (int)position.x;
    //				temp.c = (int)position.y;
    //				mNodes.Add(temp);
    //			}
    //		}
    //	}
    //}

    void getNodes()
    {
		mNodes = new List<Node>();
		var Nodes = GameObjectsLocator.Instance.Get<Block>();
        {
            foreach (var node in Nodes)
            {
                foreach (var position in node.worldPosition)
                {
                    Node temp = new Node();
                    temp.r = (int)position.x;
                    temp.c = (int)position.y;
                    mNodes.Add(temp);
                }
            }
        }
    }

    public void init(int rows, int columns)
    {
		mColumn = columns;
		mRow = rows;
		getNodes();
		//mNodes.AddRange(new Node[rows * columns]);
		for (int y = 0; y < columns; ++y)
		{
			for (int x = 0; x < rows; ++x)
			{
				//8 direction
				for (int d = 0; d <= 7; ++d)
				{
					int row = x;
					int column = y;
					//NORTH
					if (d == 0)
					{
						column = y + 1;
					}//SOUTH
					if (d == 1)
					{
						column = y - 1;
					}//EAST
					if (d == 2)
					{
						row = x - 1;
					}//WEST
					if (d == 3)
					{
						row = x + 1;
					}
					//if (d == 4)
					//{
					//	column = y + 1;
					//	row = x - 1;
					//}
					//if (d == 5)
					//{
					//	column = y + 1;
					//	row = x + 1;
					//}
					//if (d == 6)
					//{
					//	column = y - 1;
					//	row = x - 1;
					//}
					//if (d == 7)
					//{
					//	column = y - 1;
					//	row = x + 1;
					//}
					if (mNodes[x + y * mRow].neighbors.Count < 4)
					{
						mNodes[x + y * mRow].neighbors.Add(GetNode(row, column));
					}
				}
			}
		}
		RegisterToLocator();
	}

	public void ResetPath()
	{
		foreach (var node in mNodes)
		{
			node.parent = null;
			node.opened = false;
			node.closed = false;
			node.g = 0.0f;
			node.h = 0.0f;
		}
	}

    public void RegisterToLocator()
    {
		GameObjectsLocator.Instance.Register<NodePath>(this);
    }

    public void UnRegisterToLocator()
    {
        throw new System.NotImplementedException();
    }
}

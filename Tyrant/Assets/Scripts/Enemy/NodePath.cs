using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NodePath : GameObjectsLocator.IGameObjectRegister
{
	public class Node
	{
		public List<Node> neighbors = new List<Node>();
		public Node parent;
		public int c = 0;
		public int r = 0;
	
		public float g = 0.0f;
		public float h = 0.0f;
		public bool opened = false;
		public bool closed = false;
	}

	private List<Node> mNodes = new List<Node>();
	private List<Node> mNeighbors = new List<Node>();
	int mColumn;
	int mRow;

	public Node GetNode(int x, int y)
    {
		if (x < 0 || y < 0)
		{
			var Nodes = GameObjectsLocator.Instance.Get<Block>();
			{
				foreach (var node in Nodes)
				{
					for (int position = 0; position < node.WorldPosition.Count; ++position)
					{
						if (node.WorldPosition[position].x == x && node.WorldPosition[position].y == y)
						{
							return mNodes[position];
						}
					}
				}
			}
		}
		return mNodes[x + y + mColumn];
	}

	void getNodes()
    {
		var Nodes = GameObjectsLocator.Instance.Get<Block>();
        {
			foreach (var node in Nodes)
            {
				foreach (var position in node.WorldPosition)
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
		for (int x = 0; x < rows; ++x)
		{
			for (int y = 0; y < columns; ++y)
			{
				var node = GetNode(x, y);
				
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
						row = x - 1;
					}
					if (d == 4)
					{
						column = y + 1;
						row = x - 1;
					}
					if (d == 5)
					{
						column = y + 1;
						row = x + 1;
					}
					if (d == 6)
					{
						column = y - 1;
						row = x - 1;
					}
					if (d == 7)
					{
						column = y - 1;
						row = x + 1;
					}
					node.neighbors.Add(GetNode(x, y));
					node.c = y;
					node.r = x;
				}

			}
		}
		RegisterToLocator();
	}

	public void ResetPath()
	{
		foreach (var node in mNodes)
		{
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

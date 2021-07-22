using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NodePath
{
	public class Node
	{
		public List<Node> neighbors;
		public Node parent;
		public int c;
		public int r;
		public float g;
		public float h;
		public bool opened;
		public bool closed;
	}

	private List<Node> mNodes = new List<Node>();
	private List<Node> mNeighbors = new List<Node>();
	int mColumn;
	int mRow;

	public Node GetNode(int x, int y)
    {
        return mNodes[x + y * mColumn];
    }

    public void init(int rows, int columns)
    {
		mColumn = columns;
		mRow = rows;
		mNodes.AddRange(new Node[mRow * mColumn]);
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

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pathfinding : MonoBehaviour
{
    private List<NodePath.Node> openList;
    private List<NodePath.Node> closeList;
    NodePath nodePath;
    bool found = false;

    public List<NodePath.Node> OpenList { get => openList; }
    public List<NodePath.Node> CloseList { get => closeList; }

    //heuristic
    float GetHCost(int fromX, int fromY, int toX, int toY)
    {
        float dx = Mathf.Abs(fromX - toX);
        float dy = Mathf.Abs(fromY - toY);
        return dx + dy;
    }

    float GetGCost()
    {
        return 1;
    }

    bool isBlock(int row, int column)
    {
        var blocks = GameObjectsLocator.Instance.Get<Block>();
        foreach (var objects in blocks)
        {
            foreach (var block in objects.blockObject)
            {
                if (row == block.x && column == block.y)
                {
                    return true;
                }
            }
        }
        return false;
    }

    public bool Search(Vector2 startposition, Vector2 endposition)
    {
        nodePath.ResetPath();
        var node = nodePath.GetNode((int)startposition.x, (int)startposition.y);
        node.opened = true;

        var end = nodePath.GetNode((int)endposition.x, (int)endposition.y);

        while (!found && OpenList.Count != 0)
        {
            var current = OpenList[0];
            OpenList.RemoveAt(0);

            if (current == end)
            {
                found = true;
            }
            else
            {
                //eight direction
                for (int i = 0; i < 8; i++)
                {
                    if (current.neighbors[i] != null)
                    {
                        if (isBlock(current.neighbors[i].r, current.neighbors[i].c))
                        {
                            continue;
                        }

                        current.neighbors[i].h = GetHCost(current.neighbors[i].r, current.neighbors[i].c, end.r, end.c);
                        var G = current.neighbors[i].g + GetGCost();
                        float F = G + current.neighbors[i].h;

                        if (!current.neighbors[i].opened)
                        {
                            if (OpenList.Count != 0)
                            {
                                for (int op = 0; op < OpenList.Count; op++)
                                {
                                    if (F < OpenList[op].g + OpenList[op].h)
                                    {
                                        current.neighbors[i].opened = true;
                                        current.neighbors[i].parent = current;
                                        current.neighbors[i].g = G;
                                        OpenList.Insert(op, current.neighbors[i]);
                                        break;
                                    }
                                }
                                current.neighbors[i].opened = true;
                                current.neighbors[i].parent = current;
                                current.neighbors[i].g = G;
                                OpenList.Add(current.neighbors[i]);
                            }
                            else
                            {
                                current.neighbors[i].opened = true;
                                current.neighbors[i].parent = current;
                                current.neighbors[i].g = G;
                                OpenList.Add(current.neighbors[i]);
                            }
                        }
                        else if (!current.neighbors[i].closed)
                        {
                            if (F < current.neighbors[i].g+ current.neighbors[i].h)
                            {
                                current.neighbors[i].parent = current;
                                current.neighbors[i].g = G;
                                for (int op = 0; op < OpenList.Count; op++)
                                {
                                    if (OpenList[op] == current.neighbors[i])
                                    {
                                        OpenList.Remove(OpenList[op]);
                                        OpenList.Insert(0, current.neighbors[i]);
                                        break;
                                    }
                                }
                            }
                        }
                    }
                }
            }
            CloseList.Add(current);
            current.closed = true;
        }
        return found;
    }
}

using System.Collections.Generic;
using UnityEngine;

public class Pathfinding
{
    private List<NodePath.Node> openList;
    private List<NodePath.Node> closeList;
    NodePath nodePath;
    bool found = false;
    bool less = false;
    bool isCloseWall = false;

    NodePath.Node lastend;
    public List<NodePath.Node> OpenList { get => openList; }
    public List<NodePath.Node> CloseList { get => closeList; }

    //heuristic
    float GetHCost(float fromX, float fromY, float toX, float toY)
    {
        float dx = Mathf.Abs(fromX - toX);
        float dy = Mathf.Abs(fromY - toY);
        return dx + dy;
    }

    float GetGCost()
    {
        return 10;
    }

    bool isBlock(float row, float column)
    {
        var blocks = GameObjectsLocator.Instance.Get<Block>();
        foreach (var objects in blocks)
        {
            foreach (var block in objects.BlockObject)
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
        float F = 0.0f;
        found = false;
        openList = new List<NodePath.Node>();
        closeList = new List<NodePath.Node>();
        nodePath = new NodePath();
        nodePath = GameObjectsLocator.Instance.Get<NodePath>()[0];
        closeList.Clear();

        nodePath.ResetPath();
        //if (isBlock(Mathf.FloorToInt(startposition.x), Mathf.FloorToInt(startposition.y)))
        //{
        //    if (startposition.x > (int)startposition.x)
        //    {
        //        startposition.x += 1;
        //    }
        //    if (startposition.y > (int)startposition.y)
        //    {
        //        startposition.y += 1;
        //    }
        //}

        var node = nodePath.FindNode(Mathf.FloorToInt(startposition.x), Mathf.FloorToInt(startposition.y));

        openList.Add(node);
        node.opened = true;

        var end = nodePath.FindNode(Mathf.FloorToInt(endposition.x), Mathf.FloorToInt(endposition.y));

        //if (isBlock(end.r, end.c))
        //{
        //    end = lastend;
        //}
        //else
        //{
        //    lastend = end;
        //}

        while (!found && OpenList.Count != 0)
        {
            var current = OpenList[0];
            OpenList.RemoveAt(0);
            if (end != null)
            {
                if (current.c == end.c && current.r == end.r)
                {
                    found = true;
                    isCloseWall = false;
                }
                else
                {
                    //eight direction
                    for (int i = 0; i < current.neighbors.Count; i++)
                    {
                        if (current.neighbors[i] != null)
                        {
                            var G = current.neighbors[i].g;
                          
                            if (isBlock(current.neighbors[i].r, current.neighbors[i].c))
                            {
                                current.neighbors[i].h = GetHCost(current.neighbors[i].r, current.neighbors[i].c, end.r, end.c);
                                G += GetGCost();
                                F = G + current.neighbors[i].h;
                                isCloseWall = true;
                            }
                            else
                            {
                                if(isCloseWall && i>=4)
                                {
                                    continue;
                                }
                                current.neighbors[i].h = GetHCost(current.neighbors[i].r, current.neighbors[i].c, end.r, end.c);
                                F = G + current.neighbors[i].h;
                            }

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
                                            less = true;
                                            break;
                                        }
                                    }
                                    if (less == false)
                                    {
                                        current.neighbors[i].opened = true;
                                        current.neighbors[i].parent = current;
                                        current.neighbors[i].g = G;
                                        OpenList.Add(current.neighbors[i]);
                                    }
                                    less = false;
                                }
                                else
                                {
                                    current.neighbors[i].opened = true;
                                    current.neighbors[i].parent = current;
                                    current.neighbors[i].g = G;
                                    OpenList.Add(current.neighbors[i]);
                                }
                            }
                            if (!current.neighbors[i].closed)
                            {
                                if (F < current.neighbors[i].g + current.neighbors[i].h)
                                {
                                    current.neighbors[i].g = G;
                                    current.neighbors[i].parent = current;
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
        }
        return found;
    }
}
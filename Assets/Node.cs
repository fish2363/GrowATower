using UnityEngine;

public class Node
{
    public int x, y;
    public bool isBuildable;
    public GameObject towerOnTop;

    public Node(int x, int y, bool buildable)
    {
        this.x = x;
        this.y = y;
        this.isBuildable = buildable;
    }
}

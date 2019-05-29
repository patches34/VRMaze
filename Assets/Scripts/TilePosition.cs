using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TilePosition
{
    int x, z;

    public int X
    {
        get
        {
            return x;
        }
    }

    public int Z
    {
        get
        {
            return z;
        }
    }

    public TilePosition() : this(0, 0) { }
    public TilePosition(int x, int z)
    {
        this.x = x;
        this.z = z;
    }
}

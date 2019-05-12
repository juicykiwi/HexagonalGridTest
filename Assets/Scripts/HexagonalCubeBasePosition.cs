using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HexagonalCubeBasePosition
{
    public int PosX { get; private set; }
    public int PosY { get; private set; }
    public int PosZ { get; private set; }

    public void SetPositionByOffsetPosition( int x, int y )
    {
        PosX = x;

        //var x = hex.col - (hex.row - (hex.row & 1)) / 2

        if ( x >= 0 )
        {
            PosY = y - ((x + 1) / 2);
        }
        else
        {
            PosY = y - ((x - 1) / 2);
        }

        PosZ = 0 - (x + y);
    }
}

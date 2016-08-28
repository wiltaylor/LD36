using UnityEngine;
using ModestTree.Util;

public static class CordUtil
{
    public static Vector3 TileToWorld(int x, int y)
    {
        var rx = x/100f*32f + 0.16f;
        var ry = y/100f*32f + 0.16f;

        return new Vector3(rx, ry);
    }

    public static Tuple<int, int> WorldToTile(Vector3 position)
    {
        var x = (int)((position.x / 32f) * 100f);
        var y = (int)((position.y / 32f) * 100f);

        return new Tuple<int, int>(x, y);
    }
}

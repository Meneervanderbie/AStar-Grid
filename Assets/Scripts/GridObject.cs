using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridObject : MonoBehaviour {

    public int x_pos;
    public int z_pos;
    public GridTile tilePosition;

    public bool obstruction = true;

    public virtual void initialize(int x, int z, GridTile pos) {
        x_pos = x;
        z_pos = z;
        tilePosition = pos;
    }

    public GridTile returnTilePosition() {
        return tilePosition;
    }
	
}

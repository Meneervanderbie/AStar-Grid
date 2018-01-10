using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : GridObject {

    public GridManager gm;

    public override void initialize(int x, int z, GridTile pos) {
        base.initialize(x, z, pos);
        obstruction = false;
    }

    public void startFindPath(Target target) {
        Pathfinding pathfinder = new Pathfinding();
        pathfinder.gridManager = gm;
        List<GridTile> path = pathfinder.startSearch(tilePosition, target.returnTilePosition());
    }

}

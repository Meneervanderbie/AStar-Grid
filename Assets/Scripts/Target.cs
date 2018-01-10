using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : GridObject {

    public override void initialize(int x, int z, GridTile pos) {
        base.initialize(x, z, pos);
        obstruction = false;
    }

}

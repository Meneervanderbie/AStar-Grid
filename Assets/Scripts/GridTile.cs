using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridTile : MonoBehaviour {

    public int x_pos;
    public int z_pos;

    public int tile_cost;
    public int max_tile_cost;

    public float distanceFromStart;
    public float distanceToEnd;
    public float totalDistance;

    public GridTile previousTile;

    public GridObject objectOnTile;

    public void setTileCost(int max) {
        max_tile_cost = max;
        tile_cost = Random.Range(0, max);
        Color tile_color = new Color(0, ((float)tile_cost / max), 0);
        gameObject.GetComponent<Renderer>().material.color = tile_color;
    }

    public void placeObject(GridObject obj) {
        GameObject newObj = Instantiate(obj.gameObject, transform.position, obj.gameObject.transform.rotation, transform);
        objectOnTile = newObj.GetComponent<GridObject>();
        objectOnTile.initialize(x_pos, z_pos, this);
        objectOnTile.transform.position += new Vector3(0, 0.5f, 0);
        tile_cost = 1000000;
    }

    public bool tileIsFree() {
        return objectOnTile == null;
    }

    // Should include Tile cost!
    public void calculateDistances(float startDist, GridTile target, GridTile previous) {
        distanceFromStart = startDist;
        distanceToEnd = Vector3.Distance(transform.position, target.gameObject.transform.position);
        totalDistance = distanceFromStart + distanceToEnd;
        previousTile = previous;
    }

    public GridObject getObject() {
        return objectOnTile;
    }
}

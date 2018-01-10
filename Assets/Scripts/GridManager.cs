using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour { 
    public GridTile[,] grid;
    public int x_size;
    public int z_size;

    public GridTile tilePrefab;
    public int tileVariance;

    public GridTree treePrefab;
    public int numberOfTrees;

    public Unit unitPrefab;
    public Target targetPrefab;

    public Unit pathfindingUnit;
    public Target pathfindingTarget;

	void Start () {
        grid = new GridTile[x_size,z_size];
        for(int i = 0; i < x_size; i++) {
            for(int j = 0; j < z_size; j++) {
                GridTile newTile = Instantiate(tilePrefab, tilePrefab.transform.position + new Vector3(i, 0, j), tilePrefab.transform.rotation, transform);
                newTile.x_pos = i;
                newTile.z_pos = j;
                newTile.setTileCost(tileVariance);
                grid[i, j] = newTile;
            }
        }
        placeTrees();
        pathfindingUnit = placeUnit();
        pathfindingUnit.gm = this;
        pathfindingTarget = placeTarget();
        pathfindingUnit.startFindPath(pathfindingTarget);
    }

    public void placeTrees() {
        for (int i = 0; i < numberOfTrees; i++) {
            int randomX = Random.Range(0, x_size);
            int randomZ = Random.Range(0, z_size);
            if (grid[randomX, randomZ].tileIsFree()) {
                // If the tile is occupied, the tree is skipped instead. 
                grid[randomX, randomZ].placeObject(treePrefab);
            }
        }
    }

    public Unit placeUnit() {
        // tries to place the unit 100 times, if unsuccessful all times, it quits the game. 
        bool placeSuccess = false;
        for (int i = 0; i < 100; i++) {
            int randomX = Random.Range(0, x_size);
            int randomZ = Random.Range(0, z_size);
            if (grid[randomX, randomZ].tileIsFree()) {
                grid[randomX, randomZ].placeObject(unitPrefab);
                placeSuccess = true;
                return (Unit)grid[randomX, randomZ].getObject();
            }
        }
        if (!placeSuccess) {
            Debug.LogError("Could not find a spot to place the unit");
        }
        return null;
    }

     public Target placeTarget() {
        // tries to place the target 100 times, if unsuccessful all times, it quits the game. 
        bool placeSuccess = false;
        for (int i = 0; i < 100; i++) {
            int randomX = Random.Range(0, x_size);
            int randomZ = Random.Range(0, z_size);
            if (grid[randomX, randomZ].tileIsFree()) {
                grid[randomX, randomZ].placeObject(targetPrefab);
                placeSuccess = true;
                return (Target)grid[randomX, randomZ].getObject();
            }
        }
        if (!placeSuccess) {
            Debug.LogError("Could not find a spot to place the target");
        }
        return null;
    }
	
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pathfinding {

    public GridManager gridManager;
    public List<GridTile> todo;
    public List<GridTile> done;

    public Pathfinding() {
        todo = new List<GridTile>();
        done = new List<GridTile>();
    }

    public List<GridTile> startSearch(GridTile startPos, GridTile endPos) {
        Debug.Log(startPos.x_pos + " " + startPos.z_pos);
        Debug.Log(endPos.x_pos + " " + endPos.z_pos);
        startPos.calculateDistances(0, endPos, null);
        todo.Add(startPos);
        List<GridTile> neighbors;

        while (todo.Count != 0) {
            GridTile current = findLowestCost();
            neighbors = new List<GridTile>();

            todo.Remove(current);
            done.Add(current);

            for (int i = current.x_pos-1; i <= current.x_pos+1; i++) {
                for(int j = current.z_pos-1; j <= current.z_pos+1; j++) {
                    if (i > 0 && j > 0 && i < gridManager.x_size && j < gridManager.z_size) {
                        if (gridManager.grid[i, j] != null) {
                            neighbors.Add(gridManager.grid[i, j]);
                        }
                    }
                }
            }
            foreach(GridTile tile in neighbors) {
                float distanceFromStart = current.distanceFromStart +
                        Vector3.Distance(current.gameObject.transform.position,
                        endPos.gameObject.transform.position);
                float totalDistance = distanceFromStart + 
                    Vector3.Distance(startPos.gameObject.transform.position, endPos.gameObject.transform.position);
                
                if (tile == endPos) {
                    Debug.Log("Path Found!");
                    tile.calculateDistances(distanceFromStart, endPos, current);
                    return returnPath(startPos, endPos);
                }
                if (isInDone(tile)){
                    continue;
                }
                if (!isInToDo(tile)) {
                    todo.Add(tile);
                }
                //Debug.Log("Calculating distances...");
                tile.calculateDistances(distanceFromStart, endPos, current);
            }
        }
        Debug.LogError("Target not reachable!");
        return null;
    }

    public GridTile findLowestCost() {
        GridTile toReturn = todo[0];
        foreach (GridTile tile in todo) {
            if(tile.totalDistance <= toReturn.totalDistance) {
                toReturn = tile;
            }
        }
        return toReturn;
    }

    public bool isInToDo(GridTile searchTile) {
        foreach(GridTile tile in todo) {
            if(tile == searchTile) {
                return true;
            }
        }
        return false;
    }

    // Beter zou zijn om isInDone een variant te maken van IsInToDo omdat beide functie ongeveer hetzelfde doen. 
    public bool isInDone(GridTile searchTile) {
        foreach (GridTile tile in done) {
            if (tile == searchTile) {
                return true;
            }
        }

        return false;
    }

    public List<GridTile> returnPath(GridTile start, GridTile target) {
        List<GridTile> toReverse = new List<GridTile>();
        GridTile currentTile = target;
        Debug.Log("backtracking...");
        while (currentTile != start) {
            Debug.Log(currentTile.x_pos + " " + currentTile.z_pos);
            toReverse.Add(currentTile);
            Debug.Log(currentTile.previousTile);
            currentTile = currentTile.previousTile;
        }

        List<GridTile> toReturn = new List<GridTile>();

        Debug.Log("Reversetracking...");
        while (toReverse.Count > 0) {
            toReturn.Add(toReverse[toReverse.Count - 1]);
            toReverse.RemoveAt(toReverse.Count - 1);
        }
        Debug.Log("Returning path!");
        return toReturn;
    }
}

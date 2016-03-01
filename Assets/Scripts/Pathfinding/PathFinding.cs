using UnityEngine;
using System.Collections.Generic;
using System;

public class PathFinding : MonoBehaviour {

    public static PathFinding instance;
    Grid pathGrid;
    Action gridInvalidatedCallback;

    List<Node<Tile>> closedSet;
    List<Node<Tile>> openSet;
    Dictionary<Node<Tile>, float> gScore;
    Dictionary<Node<Tile>, float> fScore;
    Dictionary<Node<Tile>, Node<Tile>> camefrom;


    // Use this for initialization
    void Start () {
        instance = this;
	}
	
	// Update is called once per frame
	void Update () {
	}

    public void InvalidateGrid() {
        pathGrid = null;
        if (gridInvalidatedCallback != null)
            gridInvalidatedCallback();
    }

    public void RegisterGridInvalidatedCallback(Action cb) {
        gridInvalidatedCallback += cb;
    }

    public void UnregisterGridInvalidatedCallback(Action cb){
        gridInvalidatedCallback -= cb;
    }

    public List<Tile> FindPath(Tile startT, Tile endT) {
        if (pathGrid == null)
            pathGrid = new Grid(WorldController.world);

        Node<Tile> start = pathGrid.nodes[startT];
        Node<Tile> end = pathGrid.nodes[endT];
        closedSet = new List<Node<Tile>>();
        openSet = new List<Node<Tile>>();
        openSet.Add(start);
        camefrom = new Dictionary<Node<Tile>, Node<Tile>>();
        gScore = new Dictionary<Node<Tile>, float>();
        gScore.Add(start, 0);
        fScore = new Dictionary<Node<Tile>, float>();
        fScore.Add(start, heuristicCostEstimate(start, end));
        while (openSet.Count > 0) {
            Node<Tile> current = getLowestfScore();
            if (current == end)
                return reconstructPath(current);
            openSet.Remove(current);
            closedSet.Add(current);
            foreach (Edge<Tile> edge in current.edges) {
                if (closedSet.Contains(edge.node))
                    continue;
                float tentativeGScore = gScore[current] + Mathf.Sqrt((Mathf.Pow(current.data.x - edge.node.data.x, 2) + Mathf.Pow(current.data.y - edge.node.data.y, 2)));
                if (!openSet.Contains(edge.node))
                {
                    openSet.Add(edge.node);
                }
                else if (tentativeGScore >= gScore[edge.node])
                    continue;
                camefrom[edge.node] = current;
                gScore[edge.node] = tentativeGScore;
                fScore[edge.node] = gScore[edge.node] + heuristicCostEstimate(edge.node, end);
            }
        }
        return null;

    }

    List<Tile> reconstructPath(Node<Tile> current) {
        Node<Tile> currentNode = current;
        List<Tile> fullPath = new List<Tile>();
        while (camefrom.ContainsKey(currentNode)) {
            currentNode = camefrom[currentNode];
            fullPath.Add(currentNode.data);
        }
        fullPath.Reverse();
        return fullPath;
    }

    Node<Tile> getLowestfScore() {
        Node<Tile> lowestFScoreNode = null;
        float lowestFScore = Mathf.Infinity;
        foreach (Node<Tile> node in openSet) {
            if (lowestFScoreNode == null || fScore[node] < lowestFScore) {
                lowestFScoreNode = node;
                lowestFScore = fScore[node];
            }
        }
        return lowestFScoreNode;
    }

    float heuristicCostEstimate(Node<Tile> src, Node<Tile> dest){
        return Mathf.Sqrt((Mathf.Pow(src.data.x - dest.data.x, 2) + Mathf.Pow(src.data.y - dest.data.y, 2)));
    }
}

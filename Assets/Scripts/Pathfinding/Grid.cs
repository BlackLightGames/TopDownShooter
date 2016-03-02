using UnityEngine;
using System.Collections.Generic;

public class Grid {

    public Dictionary<Tile, Node<Tile>> nodes;

    public Grid(World world) {
        nodes = new Dictionary<Tile, Node<Tile>>();
        for (int x = 0; x < world.width; x++){
            for (int y = 0; y < world.height; y++){
                Tile t = world.getTileAt(x, y);

                if (WorldController.isWalkable(t)) {
                    Node<Tile> node = new Node<Tile>();
                    node.data = t;
                    nodes.Add(t, node);
                }
            }
        }

        foreach (Tile t in nodes.Keys) {
            Tile[] neighbors = t.getNeighbors(true, false);
            foreach (Tile neighbor in neighbors) {
                if (neighbor != null && WorldController.isWalkable(neighbor) && nodes.ContainsKey(neighbor)) {
                    Edge<Tile> edge = new Edge<Tile>();
                    edge.cost = 1;
                    edge.node = nodes[neighbor];
                    nodes[t].edges.Add(edge);
                }
            }
        }
    }

}

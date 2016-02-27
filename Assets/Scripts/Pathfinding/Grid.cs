using UnityEngine;
using System.Collections.Generic;

public class Grid {

    Dictionary<Tile, Node<Tile>> nodes;

    public Grid(World world) {
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

        }
    }

}

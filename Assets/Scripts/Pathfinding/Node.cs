using UnityEngine;
using System.Collections.Generic;

public class Node<T> {

    public T data;

    public List<Edge<T>> edges;

    public Node() {
        edges = new List<Edge<T>>();
    }
	
}

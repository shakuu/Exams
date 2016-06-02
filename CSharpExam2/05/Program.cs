using System;
using System.Collections.Generic;
using System.z

/// <summary>Represents a directed unweighted graph structure
/// </summary>
public class Graph
{
    // Contains the child nodes for each vertex of the graph
    // assuming that the vertices are numbered 0 ... Size-1
    private List<int>[] childNodes;

    /// <summary>Constructs an empty graph of given size</summary>
    /// <param name="size">number of vertices</param>
    public Graph(int size)
    {
        this.childNodes = new List<int>[size];
        for (int i = 0; i < size; i++)
        {
            // Assing an empty list of adjacents for each vertex
            this.childNodes[i] = new List<int>();
        }
    }

    /// <summary>Constructs a graph by given list of
    /// child nodes (successors) for each vertex</summary>
    /// <param name="childNodes">children for each node</param>
    public Graph(List<int>[] childNodes)
    {
        this.childNodes = childNodes;
    }

    /// <summary>
    /// Returns the size of the graph (number of vertices)
    /// </summary>
    public int Size
    {
        get { return this.childNodes.Length; }
    }

    /// <summary>Adds new edge from u to v</summary>
    /// <param name="u">the starting vertex</param>
    /// <param name="v">the ending vertex</param>
    public void AddEdge(int u, int v)
    {
        childNodes[u].Add(v);
    }

    /// <summary>Removes the edge from u to v if such exists
    /// </summary>
    /// <param name="u">the starting vertex</param>
    /// <param name="v">the ending vertex</param>
    public void RemoveEdge(int u, int v)
    {
        childNodes[u].Remove(v);
    }

    /// <summary>
    /// Checks whether there is an edge between vertex u and v
    /// </summary>
    /// <param name="u">the starting vertex</param>
    /// <param name="v">the ending vertex</param>
    /// <returns>true if there is an edge between
    /// vertex u and vertex v</returns>
    public bool HasEdge(int u, int v)
    {
        bool hasEdge = childNodes[u].Contains(v);
        return hasEdge;
    }

    /// <summary>Returns the successors of a given vertex
    /// </summary>
    /// <param name="v">the vertex</param>
    /// <returns>list of all successors of vertex v</returns>
    public IList<int> GetSuccessors(int v)
    {
        return childNodes[v];
    }

}

class GraphComponents
{
    static Graph graph = new Graph();

    static bool[] visited = new bool[graph.Size];
    private static bool[,] flags;
    private static bool[,] input;

    static void TraverseDFS(int v)
    {
        if (!visited[v])
        {
            Console.Write(v + " ");
            visited[v] = true;
            foreach (int child in graph.GetSuccessors(v))
            {
                TraverseDFS(child);
            }
        }
    }

    static void Input()
    {
        var lines = int.Parse(Console.ReadLine());

        input = new bool[lines, lines];
        flags = new bool[lines, lines];

        for (int i = 0; i < lines; i++)
        {
            var line = Console.ReadLine().Trim()
                .Split(' ')
                .Select(int.Parse)
                .ToArray();

            input[line[0], line[1]] = true;
            input[line[1], line[0]] = true;
        }
    }

    static void Main()
    {
        Console.WriteLine("Connected graph components: ");
        for (int v = 0; v < graph.Size; v++)
        {
            if (!visited[v])
            {
                TraverseDFS(v);
                Console.WriteLine();
            }
        }
    }
}
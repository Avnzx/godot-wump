using Godot;
using System;
using System.Collections.Generic;
using System.Linq;


class Alg {

    public bool[] IsFlippedBFS(Godot.Collections.Array adjArray, int start=0) {
        int _numverts = adjArray.Count; 
        return IsFlippedBFS(_numverts, adjArray, start);
    }

    public bool[] IsFlippedBFS(int numverts, Godot.Collections.Array adjArray, int start=0) {
        // Mark all the vertices as not
        // visited(By default set as false)
        bool[] visited = new bool[numverts];
        bool[] flipped = new bool[numverts];
        
        for(int i = 0; i < numverts; i++) {
            visited[i] = false;
            flipped[i] = false;
        }


        // Create a queue for BFS
        LinkedList<int> queue = new LinkedList<int>();
        
        // Mark the current node as
        // visited and enqueue it
        visited[start] = true;
        queue.AddLast(start);        
    
        while(queue.Any())
        {
            
            // Dequeue a vertex from queue
            // and print it
            start = queue.First();
            //GD.PrintS(start + " " + flipped[start]);
            queue.RemoveFirst();
            
            // Get all adjacent vertices of the
            // dequeued vertex start. If a adjacent
            // has not been visited, then mark it
            // visited and enqueue it
            Godot.Collections.Array list = (Godot.Collections.Array) adjArray[start];
    
            foreach (int val in list)            
            {
                if (!visited[val])
                {
                    visited[val] = true;
                    flipped[val] = !flipped[start];
                    queue.AddLast(val);
                }
            }
        }

        return flipped;

    }


}
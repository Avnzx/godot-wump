using Godot;
using System;

public class GameState : Node {
    public Boolean debugMode { get; set; }

    public int CurrentPlayerRoom { get; set; }
    public int CurrentMonsterRoom { get; set; }


    public Godot.Collections.Array? adjacency { get; private set; }

    public GameState(){
        // TODO: change
        debugMode = true;

        CurrentPlayerRoom = 0;

    }

    public override void _Ready() {
        IcoSphereGeom adj = new IcoSphereGeom();
        adjacency = adj.CreateAdjacencyGraph();
    }




}
using Godot;
using System;

public class GameState : Node {
    public Boolean debugMode { get; set; }

    public int CurrentPlayerRoom { get; set; }
    public int CurrentMonsterRoom { get; set; }


    public Godot.Collections.Array? adjacency { get; set; }

    public GameState(){
        adjacency = new Godot.Collections.Array();
        adjacency.Resize(20);

        // TODO: change
        debugMode = true;

    }




}
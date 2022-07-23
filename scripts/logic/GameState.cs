using Godot;
using System;

public class GameState : Node {
    public Boolean debugMode { get; set; }

    public int CurrentPlayerRoom { get; set; }
    public int CurrentMonsterRoom { get; set; }

    public Godot.Collections.Array? pitRooms;
    public Godot.Collections.Array? batRooms;


    public Godot.Collections.Array? adjacency { get; private set; }

    public GameState(){
        // TODO: change
        debugMode = true;

        CurrentPlayerRoom = 0;
        CurrentMonsterRoom = 1;

    }

    public override void _Ready() {
        IcoSphereGeom adj = new IcoSphereGeom();
        adjacency = adj.CreateAdjacencyGraph();

        AddChild(new audiomanager());
        AddChild(new NewState());
        AddChild(new Death());
        

        this.AddUserSignal("GameStateChanged");
        this.AddUserSignal("CheckGameState");
    }




    public void HandleStateChanged() {
        EmitSignal("GameStateChanged");
        HandleCheckGameState();
    }

    public void HandleCheckGameState() {
        EmitSignal("CheckGameState");
        // handle if you should die / anything
    }

    [Signal]
    public delegate void StateChanged();
    [Signal]
    public delegate void CheckGameState();




}
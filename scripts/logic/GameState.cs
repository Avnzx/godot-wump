using Godot;
using System;

public class GameState : Node {
    public SceneManager.EndReason? endReason;
    public Boolean debugMode { get; set; }

    public int CurrentPlayerRoom { get; set; }
    public int CurrentMonsterRoom { get; set; }

    public int NumArrows = 5;

    public Godot.Collections.Array<int>? pitRooms = new Godot.Collections.Array<int>();
    public Godot.Collections.Array<int>? batRooms = new Godot.Collections.Array<int>();


    public Godot.Collections.Array? adjacency { get; private set; }

    public GameState(){
        // TODO: change
        debugMode = false;

        CurrentPlayerRoom = 0;
        // FIXME: debugging, remove
        // CurrentMonsterRoom = 7;
        // batRooms.Add(1);
        // pitRooms.Add(4);
    }

    public override void _Ready() {
        // Create a new icosphere geometry so we can get the adjacency graph
        // As many things need to utilise the adjacency graph
        IcoSphereGeom adj = new IcoSphereGeom();
        adjacency = adj.CreateAdjacencyGraph();

        AddChild(new audiomanager());
        AddChild(new Death());
        
        // Add these signals that are mediated by this object
        // Other objects can register themselves with these signals 
        // And they will recieve notifications for the events
        this.AddUserSignal("GameStateChanged");
        this.AddUserSignal("CheckGameState");

        this.EmitSignal("CheckGameState");
    }




    public void HandleStateChanged() {
        // Handle incoming signal and then send out our own signal
        EmitSignal("GameStateChanged");
        HandleCheckGameState();
    }

    public void HandleCheckGameState() {
        EmitSignal("CheckGameState");
        if (NumArrows < 1) {
            // Check the game state, if there are <1 arrow then go to the 
            // end of the game because we die
            GetNode<SceneManager>("/root/SceneManager").GotoEndScene(SceneManager.EndReason.Arrows);
        }
        // handle if you should die / anything
    }

    [Signal]
    public delegate void StateChanged();
    [Signal]
    public delegate void CheckGameState();




}
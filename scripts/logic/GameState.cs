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
        debugMode = true;

        CurrentPlayerRoom = 0;
        // FIXME: debugging, remove
        CurrentMonsterRoom = 7;
        batRooms.Add(1);
        pitRooms.Add(4);
    }

    public override void _Ready() {
        IcoSphereGeom adj = new IcoSphereGeom();
        adjacency = adj.CreateAdjacencyGraph();

        AddChild(new audiomanager());
        // AddChild(new NewState());
        AddChild(new Death());
        

        this.AddUserSignal("GameStateChanged");
        this.AddUserSignal("CheckGameState");

        this.EmitSignal("CheckGameState");
    }




    public void HandleStateChanged() {
        EmitSignal("GameStateChanged");
        HandleCheckGameState();
    }

    public void HandleCheckGameState() {
        EmitSignal("CheckGameState");
        if (NumArrows < 1) {
            GetNode<SceneManager>("/root/SceneManager").GotoEndScene(SceneManager.EndReason.Arrows);
        }
        // handle if you should die / anything
    }

    [Signal]
    public delegate void StateChanged();
    [Signal]
    public delegate void CheckGameState();




}
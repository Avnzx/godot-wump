using Godot;
using System;

class TrapTeleporter : Spatial {

    private int SelectRoom() {
        GameState _gamestate = GetNode<GameState>("/root/GameState");

        RandomNumberGenerator rng = new RandomNumberGenerator();
        rng.Randomize();
        int rand = rng.RandiRange(0,19);

        if (_gamestate.CurrentPlayerRoom == rand || 
            (( _gamestate.batRooms?.Contains(rand) ?? false)) || 
            ((_gamestate.pitRooms?.Contains(rand)) ?? false)) 
        {
            SelectRoom();
        } 

        return rand;
    }


    public override void _Ready() {
        this.AddUserSignal("RoomChanged");
        this.Connect("RoomChanged", GetNode<worldgeom>("/root/worldgeom"), "HandleRoomDetector");

        this.AddUserSignal("StateChanged");
        this.Connect("StateChanged", GetNode("/root/GameState"), "HandleStateChanged");

        GD.Print(GetNode<Area>("Area"));
        Area area = GetNode<Area>("Area");

        GetNode<Area>("Area").Connect("body_entered",this,nameof(SignalRoomChange));
    }

    public void SignalRoomChange(Node node) {
        Vector3 translation = GetParent<CustRoom>().Translation;
        EmitSignal("RoomChanged", new Godot.Collections.Array{null,null,SelectRoom(),translation});
    }

    [Signal]
    public delegate void CheckGameState();
    [Signal]
    public delegate void RoomChanged(int olddirection, int oldroom, int currentroom, Vector3 translation);


}
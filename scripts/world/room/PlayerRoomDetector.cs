using Godot;

class PlayerRoomDetector : Area {
    public PlayerRoomDetector() {
        this.Connect("body_entered", this, "Body_entered");
        this.AddUserSignal("RoomChanged");
        this.Connect("RoomChanged", GetNode<worldgeom>("/root/worldgeom"), "HandleRoomDetector");
    }

    public void Body_entered(Node node) {
        if (node.Name == "Player") {            
            // GD.Print("inside ", GetParent<CustRoom>().roomindex);
            // GD.Print("drawing order ", GetParent<CustRoom>().drawnroom);
            GameState _gamestate = GetNode<GameState>("/root/GameState");

            int olddirection = GetParent<CustRoom>().drawnroom;
            int oldroom = _gamestate.CurrentPlayerRoom;
            int currentroom = GetParent<CustRoom>().roomindex;
            Vector3 translation = GetParent<CustRoom>().Translation;

            EmitSignal("RoomChanged", new Godot.Collections.Array{olddirection,oldroom,currentroom,translation});

            
            _gamestate.CurrentPlayerRoom = GetParent<CustRoom>().roomindex;
        }
    }

    [Signal]
    public delegate void RoomChanged(int olddirection, int oldroom, int currentroom, Vector3 translation);

}
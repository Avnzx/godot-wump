using Godot;

class PlayerRoomDetector : Area {
    public PlayerRoomDetector() {
        this.Connect("body_entered", this, "Body_entered");
        this.AddUserSignal("RoomChanged");
        // this.Connect("RoomChanged")
    }

    public void Body_entered(Node node) {
        if (node.Name == "Player") {            
            GD.Print("inside ", GetParent<CustRoom>().roomindex);
            GD.Print("drawing order ", GetParent<CustRoom>().drawnroom);

            GameState _gamestate = GetNode<GameState>("/root/GameState");
            _gamestate.CurrentPlayerRoom = GetParent<CustRoom>().roomindex;
        }
    }

    [Signal]
    delegate void RoomChanged(int olddirection, int oldroom, int currentroom);

}
using Godot;

class PlayerRoomDetector : Area {
    public PlayerRoomDetector() {
        this.Connect("body_entered", this, "Body_entered");
    }

    public void Body_entered(Node node) {
        if (node.Name == "Player") {            
            GD.Print("inside ", GetParent<CustRoom>().roomindex);
        }
    }

}
using Godot;
using System;

public class deathdetect : Area {

    public override void _Ready(){
        this.Connect("body_entered", this, nameof(ActuallyDies));
    }

    public void ActuallyDies(Node node) {
        // GD.Print("died to wumpus");
        GetNode<SceneManager>("/root/SceneManager").GotoEndScene(SceneManager.EndReason.Wumpus);
        
    }

}

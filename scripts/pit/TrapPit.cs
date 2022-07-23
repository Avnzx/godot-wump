using Godot; 

public class TrapPit : Area {

    public override void _Ready() {
        GD.Print("pit trap init");
        GetNode<GameState>("/root/GameState").Connect("GameStateChanged", this, "HandleStateChange");
        this.Connect("body_entered", this, "Fallen");
    }

    public void Fallen(Node node) {
        GetNode<SceneManager>("/root/SceneManager").GotoEndScene(SceneManager.EndReason.Pit);
        GD.Print("died to fall");
    }

    public void HandleStateChange() {
        GD.Print("updated transform");
        this.Translation = new Vector3(
            GetNode<KinematicBody>("../Player").Translation.x,
            -20f,
            GetNode<KinematicBody>("../Player").Translation.z
        );
    }    

}
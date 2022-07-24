class NewGame : Godot.Button {

    public override void _Ready(){
        this.Connect("pressed", this, "DoNewGame");
    }

    public void DoNewGame() {
        GetNode<SceneManager>("/root/SceneManager").NewGame();
    }
}
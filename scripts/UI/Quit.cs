class Quit : Godot.Button {

    public override void _Ready(){
        this.Connect("pressed", this, "DoQuit");
    }

    public void DoQuit() {
        GetNode<SceneManager>("/root/SceneManager").Quit();
    }
}
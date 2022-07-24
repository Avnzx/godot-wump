class TitleScreen : Godot.Button {

    public override void _Ready(){
        this.Connect("pressed", this, "DoTitleScreen");
    }

    public void DoTitleScreen() {
        GetNode<SceneManager>("/root/SceneManager").DeferredGotoScene("res://scenes/menus/MainMenu.tscn");
    }
}
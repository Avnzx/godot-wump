using Godot;
using System;
using GoDotTest;
using Shouldly;

public class SceneManager : Node {

    public Node? CurrentScene { get; set; }
    private PackedScene? NextScene { get; set; }
    private Viewport? root;

    public override void _Ready() {
        root = GetNode<Viewport>("/root");
        SetProcessInternal(true);
        
        // only works for the last scene loaded (Autoloads > scenes)
        CurrentScene = root.GetChild(root.GetChildCount() - 1);
        //CurrentScene = this.GetChild(this.GetChildCount() - 1);


        GD.Print("Game loaded");
        GD.Print(CurrentScene);

        var testEnv = TestEnvironment.From(OS.GetCmdlineArgs());
        if (testEnv.ShouldRunTests) {
            DeferredGotoScene("res://tests/test.tscn");
        } else {
            DeferredGotoScene("res://scenes/world/world.tscn");
        }

    }

    public void DeferredGotoScene(string path) {
        // Deferred call, safe to remove scene
        CurrentScene?.QueueFree();
        NextScene = ResourceLoader.Load<PackedScene>(path);
        // Instantiate and add
        CurrentScene = NextScene.Instance();
        root!.CallDeferred("add_child", CurrentScene);
        // + compat w/SceneTree.change_scene() API.
        // Godot does not like non-root nodes being parents
        // to current scenes
        GetTree().CurrentScene = CurrentScene; 
    }

    public void DeferredRemoveScene(string path) {
        Node remscene = ResourceLoader.Load<Node>(path);
        remscene.QueueFree();
    }

    // public void gotoscene("res::pathToScene", string array scenes to destroy, bool destroy all scenes)

}

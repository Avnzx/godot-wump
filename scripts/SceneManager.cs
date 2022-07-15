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
            // go to our default scene if there is an exception
            // allow for going to other scenes in editor
            DeferredGotoScene("res://scenes/world/world.tscn");
            try{
                foreach (var item in OS.GetCmdlineArgs()) {
                    DeferredGotoScene(item);
                }
            } catch {}
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

    public void AddScene(string path){
        PackedScene? _scene;
        Node? _nextscene;
        _scene = ResourceLoader.Load<PackedScene>(path);
        _nextscene = _scene.Instance();

        root!.CallDeferred("add_child", _nextscene);
    }

    // probably not a good idea (e.g. UI nodes)
    public void RemoveLatestScene(){
        Node? _scene = root!.GetChild(root.GetChildCount() - 1);
        GD.Print(_scene);
        GD.Print(root!.GetChild(root.GetChildCount()));
        //_scene?.QueueFree();
        GD.Print("attempted remove scene");
        
    }

    // public void gotoscene("res::pathToScene", string array scenes to destroy, bool destroy all scenes)

}

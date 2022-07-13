using Godot;
using System;
using GoDotTest;
using Shouldly;

public class Main : Node {
    public override void _Ready() {
        GD.Print("Game loaded"); 
        var testEnv = TestEnvironment.From(OS.GetCmdlineArgs());
        if (testEnv.ShouldRunTests) {
            GetTree().ChangeScene("res://tests/test.tscn");
        } else {
            GetTree().ChangeScene("res://scenes/world/world.tscn");
        }
  }
}

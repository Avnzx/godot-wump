using Godot;
using System;
using System.Reflection;
using GoDotTest;

public class test : Node {
    public override async void _Ready()
        => await GoTest.RunTests(Assembly.GetExecutingAssembly(), this);
}

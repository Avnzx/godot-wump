using Godot;
using System;

class NewState : Node {

    private void WumpusRoom() {
        GameState _gamestate = GetNode<GameState>("/root/GameState");

        RandomNumberGenerator rng = new RandomNumberGenerator();
        int rand = rng.RandiRange(0,19);
        if (_gamestate.CurrentPlayerRoom != rand) {
            _gamestate.CurrentMonsterRoom = rand;
        } else {
            WumpusRoom();
        }
    }


    public override void _Ready() {
        WumpusRoom();
        this.AddUserSignal("CheckGameState");
        this.Connect("CheckGameState", GetNode<GameState>("/root/GameState"), "CheckGameState");
        this.EmitSignal("CheckGameState");

    }

    [Signal]
    public delegate void CheckGameState();

}
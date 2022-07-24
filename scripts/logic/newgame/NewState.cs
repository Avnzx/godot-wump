using Godot;
using System;

class NewState : Node {

    RandomNumberGenerator rng = new RandomNumberGenerator();

    private void ClearGameState() {
        GameState _gamestate = GetNode<GameState>("/root/GameState");
        _gamestate.batRooms = new Godot.Collections.Array<int>();
        _gamestate.pitRooms = new Godot.Collections.Array<int>();
    }


    private void WumpusRoom() {
        GameState _gamestate = GetNode<GameState>("/root/GameState");

        
        rng.Randomize();

        int rand = rng.RandiRange(0,19);
        if (_gamestate.CurrentPlayerRoom != rand) {
            _gamestate.CurrentMonsterRoom = rand;
        } else {
            WumpusRoom();
        }
    }

    private void PitRoom() {
        GameState _gamestate = GetNode<GameState>("/root/GameState");

        rng.Randomize();

        int rand = rng.RandiRange(0,19);
        if (_gamestate.CurrentPlayerRoom != rand 
        && !_gamestate.pitRooms!.Contains(rand) 
        && !_gamestate.batRooms!.Contains(rand)
        && _gamestate.CurrentMonsterRoom != rand) {
           _gamestate.pitRooms.Add(rand);
        } else {
            PitRoom();
        }

    }

    private void TeleporterRoom() {
        GameState _gamestate = GetNode<GameState>("/root/GameState");

        rng.Randomize();

        int rand = rng.RandiRange(0,19);
        if (_gamestate.CurrentPlayerRoom != rand 
        && !_gamestate.pitRooms!.Contains(rand) 
        && !_gamestate.batRooms!.Contains(rand)
        && _gamestate.CurrentMonsterRoom != rand ) {
            _gamestate.batRooms.Add(rand);
        } else {
            TeleporterRoom();
        }
    }


    public override void _Ready() {
        ClearGameState();
        WumpusRoom();
        PitRoom();
        PitRoom();
        TeleporterRoom();
        TeleporterRoom();

        GD.Print("new game state created!");

        this.AddUserSignal("CheckGameState");
        this.Connect("CheckGameState", GetNode<GameState>("/root/GameState"), "CheckGameState");
        this.EmitSignal("CheckGameState");

    }

    [Signal]
    public delegate void CheckGameState();

}
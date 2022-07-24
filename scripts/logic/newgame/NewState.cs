using Godot;
using System;

class NewState : Node {

    RandomNumberGenerator rng = new RandomNumberGenerator();

    // set the game state back to its default values
    // we cannot unload the gamestate because it is a singleton
    // and other objects depend on its instance
    private void ClearGameState() {
        GameState _gamestate = GetNode<GameState>("/root/GameState");
        _gamestate.batRooms = new Godot.Collections.Array<int>();
        _gamestate.pitRooms = new Godot.Collections.Array<int>();
        _gamestate.NumArrows = 5;
    }


    private void WumpusRoom() {
        GameState _gamestate = GetNode<GameState>("/root/GameState");

        // The RNG Must be randomised otherwise it will return the same
        // value every time from its default seed value
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

        // The RNG Must be randomised otherwise it will return the same
        // value every time from its default seed value
        rng.Randomize();

        // Make sure that the player is not in the room
        // we are putting the trap in
        // The wumpus is not in the room
        // There is no other trap in the room
        // And that there is no trap of the same type in the room
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


        // The RNG Must be randomised otherwise it will return the same
        // value every time from its default seed value
        rng.Randomize();

        
        // Make sure that the player is not in the room
        // we are putting the trap in
        // The wumpus is not in the room
        // There is no other trap in the room
        // And that there is no trap of the same type in the room
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
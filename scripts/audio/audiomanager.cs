using Godot;

class audiomanager : AudioStreamPlayer {

    public override void _Ready() {
        GetNode<GameState>("/root/GameState").Connect("CheckGameState", this, "CheckGameState");
    }

    public void CheckGameState() {
        GameState _gamestate = GetNode<GameState>("/root/GameState");

        if (((Godot.Collections.Array) _gamestate.adjacency[_gamestate.CurrentPlayerRoom]).Contains(_gamestate.CurrentMonsterRoom)) {
            // play wumpus audio
            GD.Print("playing wumpus audio");
        }
    }



}
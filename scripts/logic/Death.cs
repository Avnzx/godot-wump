using Godot;

class Death : Node {
    public override void _Ready() {
        GetNode<GameState>("/root/GameState").Connect("CheckGameState", this, "CheckGameState");
    }

    public void CheckGameState() {
        GameState _gamestate = GetNode<GameState>("/root/GameState");

        if (_gamestate.CurrentPlayerRoom == _gamestate.CurrentMonsterRoom) {
            GD.Print("die");
        }
    }
}
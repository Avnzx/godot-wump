using Godot;

class audiomanager : AudioStreamPlayer {

    public override void _Ready() {
        GetNode<GameState>("/root/GameState").Connect("CheckGameState", this, "CheckGameState");
    }

    public void CheckGameState() {
        GameState _gamestate = GetNode<GameState>("/root/GameState");

        GD.Print(_gamestate.CurrentPlayerRoom);

        // Check if the wumpus is adjacent to us
        if (((Godot.Collections.Array) _gamestate.adjacency[_gamestate.CurrentPlayerRoom]).Contains(_gamestate.CurrentMonsterRoom)) {
            // play wumpus audio
            GD.Print("playing wumpus audio");
        }

        //  Check if there are any bats next to us
        if (_gamestate.batRooms != null) {
            for (int i = 0; i < _gamestate.batRooms.Count; i++){
               if (((Godot.Collections.Array) _gamestate.adjacency[_gamestate.CurrentPlayerRoom]).Contains(_gamestate.batRooms[i])) {
                    GD.Print("playing teleporter audio");
                } 
            }
        }

        // Check if there are any bottomless pits next to us
        if (_gamestate.pitRooms != null) {
            for (int i = 0; i < _gamestate.pitRooms.Count; i++){
                if (((Godot.Collections.Array) _gamestate.adjacency[_gamestate.CurrentPlayerRoom]).Contains(_gamestate.pitRooms[i])) {
                    GD.Print("playing pit audio");
                }
            }
        }

    }

}
using Godot;

class Death : Node {
    public override void _Ready() {
        SetPhysicsProcess(false);
        GetNode<GameState>("/root/GameState").Connect("CheckGameState", this, "CheckGameState");
    }

    public void CheckGameState() {
        GameState _gamestate = GetNode<GameState>("/root/GameState");

        if (GetNodeOrNull<Spatial>("/root/worldgeom/RoomList") == null)
            return;

        if (_gamestate.CurrentPlayerRoom == _gamestate.CurrentMonsterRoom) {
            DeathToWumpus();
        }
    }

    public void DeathToWumpus() {
        // GD.Print("dying to wumpus");
        spawnintl = GetNode<Spatial>("/root/worldgeom/RoomList");
        Vector3 spawn = Vector3.Zero;
        spawn.y = 60;

        player = GetNode<Spatial>("/root/worldgeom/Player");

        _monster = (Spatial) ResourceLoader.Load<PackedScene>("res://assets/monster/Monster.tscn").Instance();
		_monster.Translation = spawn;
        translation = spawn;
        spawnintl.CallDeferred("add_child", _monster);

        SetPhysicsProcess(true);
    }

    Spatial? spawnintl;
    Spatial? _monster;
    Spatial? player;
    Vector3 translation = Vector3.Zero;

    public override void _PhysicsProcess(float delta) {
        if (player != null && _monster != null) {
            try {
            translation.y =  Mathf.Lerp(translation.y, 2, 0.1f);
            translation.x = Mathf.Lerp(translation.x, player!.Translation.x - spawnintl!.Translation.x, 0.1f);
            translation.z = Mathf.Lerp(translation.z, player.Translation.z - spawnintl.Translation.z, 0.1f);
            _monster!.Translation = translation; } catch {}
        }
    }

}
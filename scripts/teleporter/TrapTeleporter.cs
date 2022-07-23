using Godot;
using System;

class TrapTeleporter : Spatial {

    private int SelectRoom() {
        GameState _gamestate = GetNode<GameState>("/root/GameState");

        RandomNumberGenerator rng = new RandomNumberGenerator();

        Incorrect:

        rng.Randomize();
        int rand = rng.RandiRange(0,19);

        if (_gamestate.CurrentPlayerRoom == rand || 
            (( _gamestate.batRooms?.Contains(rand) ?? false)) || 
            ((_gamestate.pitRooms?.Contains(rand)) ?? false)) 
        {
            goto Incorrect;
        } else {
            goto Correct;
        }

        Correct:
            return rand;
    }


    public override void _Ready() {
        this.AddUserSignal("RoomChanged");
        this.Connect("RoomChanged", GetNode<worldgeom>("/root/worldgeom"), "HandleRoomDetector");

        this.AddUserSignal("StateChanged");
        this.Connect("StateChanged", GetNode("/root/GameState"), "HandleStateChanged");

        GD.Print(GetNode<Area>("Area"));
        Area area = GetNode<Area>("Area");

        GetNode<Area>("Area").Connect("body_entered",this,nameof(SignalRoomChange));

        player = GetNode<playercontroller>("../../../Player");
        player.inputenabled = false;
        _translation = ((Spatial) GetParent().GetParent()).Translation;
    }

    private playercontroller? player;
    private Vector3 _translation; 

    public void SignalRoomChange(Node node) {
        player!.inputenabled = true;
        Vector3 translation = GetParent<CustRoom>().Translation;

        EmitSignal("RoomChanged", new Godot.Collections.Array{null,null,SelectRoom(),translation});
    }

    public override void _PhysicsProcess(float delta) {
        GD.Print(_translation);
        Vector3 playertransform = new Vector3();

        playertransform.x = Mathf.Lerp(player!.Translation.x, _translation.x, 0.005f);
        playertransform.z = Mathf.Lerp(player!.Translation.z, _translation.z, 0.005f);
        playertransform.y = player.Translation.y;

        player.Translation = playertransform;
    }

    [Signal]
    public delegate void CheckGameState();
    [Signal]
    public delegate void RoomChanged(int olddirection, int oldroom, int currentroom, Vector3 translation);


}
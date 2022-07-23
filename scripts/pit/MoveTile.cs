using Godot;

class MoveTile : Spatial {
    
    CustRoom? _room;
    Vector3 translation = new Vector3();
    float time = 0;

    public MoveTile(CustRoom room) {
        _room = room;
        translation = _room.Translation;
    }

    public override void _PhysicsProcess(float delta){
        time += delta;

        translation.x = _room.Translation.x;
        translation.z = _room.Translation.z;

        translation.y = Mathf.Lerp(_room.Translation.y, -800f*time, 0.0005f);

        _room.Translation = translation;
        
    }

}
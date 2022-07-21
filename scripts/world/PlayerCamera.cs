using System; 
using Godot; 

public class PlayerCamera : Camera {
    public override void _Ready() {
        // _follow = GetNode<RoomFactory>("RoomList");
        _camera = this;
    }

    private void GetObjects() {
        // _follow = (RoomFactory) GetNode("/root/worldgeom/RoomList");
        _follow = (KinematicBody) GetNode("/root/worldgeom/Player");
        GD.Print("camera processing");
    }

    public override void _Process(float delta) {
 
        if (_camera != null && _follow != null){
            Vector3 camtrans = _camera.Translation;
            Vector3 followtrans = _follow.Translation;

            camtrans.y = 90f;
            camtrans.x = Mathf.Lerp(camtrans.x, followtrans.x + 65, 0.03f);
            camtrans.z = Mathf.Lerp(camtrans.z, followtrans.z + 65, 0.03f);

            _camera.Translation = camtrans;


        } else {
            GetObjects();
        }
    }


    Camera? _camera;
    Spatial? _follow;

}
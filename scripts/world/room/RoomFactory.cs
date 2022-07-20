using Godot;
using System;


public class RoomFactory : Spatial {

    private RoomManager _manager = new RoomManager();
	public RoomFactory() {
        this.Name = "RoomList";
    }

    public void Initialise(NodePath path) {
        GetNode(path).AddChild(_manager,true);
        _manager.Roomlist = this.GetPath();
    }

    public CustRoom NewRoom() {

		CustRoom _room = new CustRoom();
		_room.Name = "hexroom";
		this.AddChild(_room,true);

		float _halfpolygon = CoordHelper.PolygonFlatToFlatDistance(6,15f); 

		// TODO: add a proper collider

		var color = new Color(
				(float) GD.RandRange(0.1, 1.0), 
				(float) GD.RandRange(0.1, 1.0), 
				(float) GD.RandRange(0.1, 1.0),1);

		var color2 = new Color(
				(float) GD.RandRange(0.3, 1.0), 
				(float) GD.RandRange(0.4, 1.0), 
				(float) GD.RandRange(0.5, 1.0),1);

		SpatialMaterial mat = new SpatialMaterial();
		SpatialMaterial mat2 = new SpatialMaterial();


		MeshInstance mi = new MeshInstance();
		ArrayMesh arrmesh1 =  CoordHelper.CreatePolygonMesh(vertices: 6, radius: 15f );
		mi.Mesh = arrmesh1;

		mat.AlbedoColor = color;
		mat2.AlbedoColor = color2;

		for (int i = 2; i < arrmesh1.GetSurfaceCount(); i++)
		{
			arrmesh1.SurfaceSetMaterial(i,mat);
		}
		arrmesh1.SurfaceSetMaterial(1,mat2);
		_room.AddChild(mi,true);
        mi.CreateConvexCollision();

		
		ArrayMesh boundarrmesh =  CoordHelper.CreatePolygonMesh(vertices: 6, radius: 15f, thickness: 20f );
		MeshInstance _bound = new MeshInstance();
		_bound.Mesh = boundarrmesh;
		_bound.Name = "room-bound";
		_bound.Visible = false;
		_room.AddChild(_bound,true);

        return _room;
    }


	public CustRoom IllusionRoom() {

		CustRoom _room = new CustRoom();
		_room.Name = "fakeroom";
		this.AddChild(_room,true);

		float _halfpolygon = CoordHelper.PolygonFlatToFlatDistance(6,15f); 

		// TODO: add a proper collider

		var color = new Color(
				(float) GD.RandRange(0.1, 1.0), 
				(float) GD.RandRange(0.1, 1.0), 
				(float) GD.RandRange(0.1, 1.0),1);

		var color2 = new Color(
				(float) GD.RandRange(0.3, 1.0), 
				(float) GD.RandRange(0.4, 1.0), 
				(float) GD.RandRange(0.5, 1.0),1);

		SpatialMaterial mat = new SpatialMaterial();
		SpatialMaterial mat2 = new SpatialMaterial();


		MeshInstance mi = new MeshInstance();
		ArrayMesh arrmesh1 =  CoordHelper.CreatePolygonMesh(vertices: 6, radius: 15f );
		mi.Mesh = arrmesh1;

		mat.AlbedoColor = color;
		mat2.AlbedoColor = color2;

		for (int i = 2; i < arrmesh1.GetSurfaceCount(); i++)
		{
			arrmesh1.SurfaceSetMaterial(i,mat);
		}
		arrmesh1.SurfaceSetMaterial(1,mat2);
		_room.AddChild(mi,true);
        mi.CreateConvexCollision();

		
		ArrayMesh boundarrmesh =  CoordHelper.CreatePolygonMesh(vertices: 6, radius: 15f, thickness: 20f );
		MeshInstance _bound = new MeshInstance();
		_bound.Mesh = boundarrmesh;
		_bound.Name = "room-bound";
		_bound.Visible = false;
		_room.AddChild(_bound,true);


		// TODO: Add Area3D node to detect collisions 
		// hopefully also direction of collisions 
		Area detectionarea = new Area();
		// move it up by one as to not detect the room's own staticbody

		ConvexPolygonShape detectionshape = new ConvexPolygonShape();
		detectionshape.Points = CoordHelper.CreatePolygonVertices(vertices: 6, radius: 14f, thickness: 4f);

		detectionarea.ShapeOwnerAddShape(
			detectionarea.CreateShapeOwner(detectionarea),
			detectionshape);

		detectionarea.CollisionMask = GameConstants.player_collision_layer;

		_room.AddChild(detectionarea,true);		
		detectionarea.SetScript(ResourceLoader.Load("res://scripts/world/room/PlayerRoomDetector.cs"));

        return _room;
    }

	public void RemoveRoom(CustRoom remove) {
		this.RemoveChild(remove);
		remove.QueueFree();
	}

	public void RemoveRoomGroup(CustRoom[] remove) {
		for (int i = 0; i < remove.Length; i++) {
			this.RemoveChild(remove[i]);
			remove[i].QueueFree();
					}
	}

}
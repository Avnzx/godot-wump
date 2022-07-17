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

    public Room NewRoom() {

		Room _room = new Room();
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


	public Room IllusionRoom() {

		Room _room = new Room();
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
		// hopefully also direction of collisions ffs

        return _room;
    }

	public void RemoveRoom(Room remove) {
		this.RemoveChild(remove);
		remove.QueueFree();
	}

	public void RemoveRoomGroup(Room[] remove) {
		for (int i = 0; i < remove.Length; i++) {
			this.RemoveChild(remove[i]);
			remove[i].QueueFree();
					}
	}
}
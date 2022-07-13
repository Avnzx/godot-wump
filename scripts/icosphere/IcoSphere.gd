extends Node

onready var functions = preload("res://scripts/util/spheregeom.gd").new()
var array_mesh = ArrayMesh.new()
var surface = SurfaceTool.new()

func create_face(lla1, lla2, lla3):
	var corner1 = functions.lla_to_xyz(lla1);	var corner2 = functions.lla_to_xyz(lla2);	var corner3 = functions.lla_to_xyz(lla3)
	var corners = PoolVector3Array()
	var arrays = []
	
	corners.push_back(corner1)
	corners.push_back(corner2)
	corners.push_back(corner3)
	
	arrays.resize(ArrayMesh.ARRAY_MAX)
	arrays[ArrayMesh.ARRAY_VERTEX] = corners
	
	array_mesh.add_surface_from_arrays(Mesh.PRIMITIVE_TRIANGLES,arrays)
	return corners

func is_adjacent(face1, face2) -> bool:
		var common = 0
		for i in range(3):
			for j in range(3):
				if face1[i] == face2[j]:
					common += 1
		if common == 2:
			return true
		else:
			return false



func _ready():
	surface.begin(Mesh.PRIMITIVE_TRIANGLES)

	var lla1 = Vector3(0, -58.5, 0)
	var lla2 = Vector3(0, 58.5, 0)
	var lla3 = Vector3(180, 58.5, 0)
	var lla4 = Vector3(180, -58.5, 0)
	var lla5 = Vector3(90, -31.5, 0)
	var lla6 = Vector3(90, 31.5, 0)
	var lla7 = Vector3(-90, 31.5, 0)
	var lla8 = Vector3(-90, -31.5, 0)
	var lla9 = Vector3(-31.5, 0, 0)
	var lla10 = Vector3(31.5, 0, 0)
	var lla11 = Vector3(148.5, 0, 0)
	var lla12 = Vector3(-148.5, 0, 0)


	var faces = [
	create_face(lla2, lla3, lla7),
	create_face(lla2, lla6, lla3),
	create_face(lla6, lla11, lla3),
	create_face(lla3, lla11, lla12),
	create_face(lla3, lla12, lla7),
	create_face(lla8, lla7, lla12),
	create_face(lla9, lla7, lla8),
	create_face(lla9, lla2, lla7),
	create_face(lla10, lla2, lla9),
	create_face(lla10, lla6, lla2),
	create_face(lla10, lla5, lla6),
	create_face(lla5, lla11, lla6),
	create_face(lla11, lla5, lla4),
	create_face(lla11, lla4, lla12),
	create_face(lla12, lla4, lla8),
	create_face(lla1, lla9, lla8),
	create_face(lla1, lla10, lla9),
	create_face(lla5, lla10, lla1),
	create_face(lla5, lla1, lla4),
	create_face(lla4, lla1, lla8)
	]

	print( is_adjacent(faces[0],faces[1]))
	print( is_adjacent(faces[2], faces[3]))
	#print(faces[0][0] == faces[1][0])

	#for face in faces:
	
	surface.generate_normals()
	surface.generate_tangents()
	
	#var mat = preload("res://spheremat.tres")
	var rng = RandomNumberGenerator.new()
	
	for i in range(array_mesh.get_surface_count()):
		var mat = SpatialMaterial.new()
		var color = Color(rng.randf_range(0.5, 1.0), rng.randf_range(0.2, 1.0), rng.randf_range(0.7, 1.0),1)
		mat.albedo_color = color
		array_mesh.surface_set_material(i,mat)
	
	
	print(faces[0])
	print(functions.calc_surface_normal_newell_method(faces[0]))
	
	
	for i in range (array_mesh.get_surface_count()):
		pass
	
	var mi = MeshInstance.new()
	print(array_mesh)
	mi.mesh = array_mesh
	add_child(mi)
	
	# creates child StaticBody, parenting a CollisionShape (MeshInstance  -> StaticBody -> CollisionShape)
	mi.create_trimesh_collision()
	
	
	var collider = ConcavePolygonShape.new()
	var staticbody = StaticBody.new()
	mi.add_child(staticbody)
	#staticbody.add_child(collider)
	#mi.material_override = preload("res://spheremat.tres")

	
	print(array_mesh.get_surface_count())
	
	

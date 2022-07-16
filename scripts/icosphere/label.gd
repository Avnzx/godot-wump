extends Label

# Declare member variables here. Examples:
onready var raycast = RayCast.new()

# Called when the node enters the scene tree for the first time.
func _ready():
	self.text = str("Placeholder")
	add_child(raycast)

	# Replace with function body.



func _input(ev):
	if ev is InputEventMouseButton and ev.pressed and ev.button_index == 1:
		var ray_length = 1000
		var camera = $"../../Gimbal/Camera"
		var fromPos = camera.project_ray_origin(ev.position)
		var toPos = fromPos + camera.project_ray_normal(ev.position) * ray_length
		
		raycast.set_translation(fromPos)
		raycast.set_cast_to(toPos)
		raycast.set_enabled(true)
		raycast.set_collide_with_areas(true)
		#var space_state = get_world().direct_space_state()
		#var result = space_state.intersect_ray(Vector2(0, 0), Vector2(50, 100))


func _physics_process(delta):
	self.text = str("Mouse speed: %s" % MouseSpeed.get_instant_mouse_speed())
	if(raycast.is_colliding()):
		pass
		#print(raycast.get_collider())





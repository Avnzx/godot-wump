extends Spatial

onready var gimbal = $Gimbal

# mouse properties
var invert_y = false
var invert_x = false
var mouse_sensitivity = 0.002
var _drag = false

#camera properties
var max_zoom = 200
var min_zoom = 50
var zoom = 100
var zoom_req = 100
var zoom_speed = 0.1

#ease
var x_rotation = 0
var y_rotation = 0


func _ready():
	pass

func _process(_delta):
	#fix rotation just in case
	gimbal.orthonormalize()
	#zoom smoothly
	zoom = lerp(zoom,zoom_req,zoom_speed)
	$"Gimbal/Camera".size = zoom
	
	if x_rotation and not _drag:
		x_rotation = lerp(x_rotation,0,0.05)
		gimbal.rotate_object_local(Vector3.UP, x_rotation * mouse_sensitivity * -1)
	if y_rotation and not _drag:
		y_rotation = lerp(y_rotation,0,0.05)
		gimbal.rotate_object_local(Vector3.RIGHT, y_rotation * mouse_sensitivity * -1)


func _unhandled_input(event):
	# set a variable and change cursor
	if (event is InputEventMouseButton and event.button_mask == BUTTON_MASK_LEFT):
		_drag = true
		Input.set_default_cursor_shape(Input.CURSOR_DRAG)
	if (event is InputEventMouseButton and event.button_index == BUTTON_LEFT and not event.pressed):
		_drag = false
		Input.set_default_cursor_shape(Input.CURSOR_ARROW)
	
	if event.is_action_pressed("toggle_map"):
		SceneManager.DeferredGotoScene("res://scenes/world/world.tscn");
	
	if event.is_action_pressed("ui_zoom_in") and not _drag:
		zoom_req -= 10
		zoom_req = clamp(zoom_req, min_zoom, max_zoom)
	if event.is_action_pressed("ui_zoom_out") and not _drag:
		zoom_req += 10
		zoom_req = clamp(zoom_req, min_zoom, max_zoom)
	
	if event is InputEventMouseMotion and _drag:
		if event.relative.x != 0:
			x_rotation = clamp(event.relative.x, -20, 20)
			var x_rot = 1 if invert_x else -1
			gimbal.rotate_object_local(Vector3.UP, x_rot * x_rotation * mouse_sensitivity)
		if event.relative.y != 0:
			y_rotation = clamp(event.relative.y, -20, 20)
			var y_rot = 1 if invert_y else -1
			gimbal.rotate_object_local(Vector3.RIGHT, y_rot * y_rotation * mouse_sensitivity)

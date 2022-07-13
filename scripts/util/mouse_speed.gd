extends Node

class_name MouseSpeedHelper

export var mousetimeout = 0.2 #seconds
var time = 0
var mousespeed = Vector2(0,0)
var mousespeedbuffer = []

func get_instant_mouse_speed() -> Vector2:
	return mousespeed

func get_average_mouse_speed() -> Vector2: # unimplemented
	return Vector2(0,0)

func _input(ev):
	if ev is InputEventMouseMotion:
		mousespeed = Input.get_last_mouse_speed()
		time = 0

func _physics_process(delta):
	time += delta
	if time > mousetimeout:
		time = 0
		mousespeed = Vector2(0.0,0.0)


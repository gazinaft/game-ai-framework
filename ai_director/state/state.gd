extends Object

class_name State

signal processed

var _executable: Executable
var _logic: StateLogic

var edges: Array[Edge] = []

func _init(executable: Executable):
    _executable = executable

func start(actor) -> void:
   _logic = _executable.logic
   _logic.processed.connect(func(): processed.emit())
   _logic.actor = actor
   _logic._ready()

func process(delta: float) -> void:
    _logic._process(delta)
extends Object

class_name Edge

var _from: State
var _to: State

func _init(from: State, to: State):
    _from = from
    _to = to

func is_traversable() -> bool:
    return true

func traverse() -> State:
    return _to
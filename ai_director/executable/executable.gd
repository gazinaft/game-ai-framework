class_name Executable

var _path: String

var logic: StateLogic :
    get:
        return load(_path).new() as StateLogic

func _init(path: String):
    _path = path
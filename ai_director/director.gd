extends Node
class_name Director

var _edge_chain: EdgeChain

var root: State
var current_state: State
var actor

func _init(graph: State):
	root = graph

func _ready() -> void:
	switch_state()

func _process(delta) -> void:
	current_state.process(delta)

func switch_state():
	var next_state: State
	if current_state == null:
		next_state = root
	else:
		next_state = _edge_chain.choose_next_state(current_state.edges)

	if current_state.processed.is_connected(switch_state):
		current_state.processed.disconnect(switch_state)
	
	next_state.processed.connect(switch_state)
	current_state = next_state
	current_state.start(actor)

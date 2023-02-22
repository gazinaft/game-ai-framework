extends Object

class_name EdgeChain

var next: EdgeChain

func choose_next_state(edges: Array[Edge]) -> State:
    return next.choose_next_state(edges)
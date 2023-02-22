extends EdgeChain

func choose_next_state(edges: Array[Edge]) -> State:
    var rng = RandomNumberGenerator.new()

    return edges[rng.randi_range(0, len(edges))].traverse()
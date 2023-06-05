def load_map() -> list[list[int]]:
    with open('input.txt', 'r') as f:
        puzzle_input = f.read()

    tree_map: list[list[int]] = []

    lines = puzzle_input.split('\n')
    for i in range(len(lines)):
        tree_map.append([])
        for j in range(len(lines[i])):
            tree_map[i].append(int(lines[i][j]))

    return tree_map
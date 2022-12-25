import load_map


def main():
    tree_map = load_map.load_map()

    highest_score = 0

    for i in range(len(tree_map)):
        for j in range(len(tree_map[i])):
            if i == 0 or j == 0:
                continue
            current_tree = tree_map[i][j]
            current_tree_score = 1
            direction_count = 0
            for k in range(i-1, -1, -1):
                direction_count += 1
                if tree_map[k][j] >= current_tree:
                    break
            current_tree_score *= direction_count
            direction_count = 0
            for k in range(j-1, -1, -1):
                direction_count += 1
                if tree_map[i][k] >= current_tree:
                    break
            current_tree_score *= direction_count
            direction_count = 0
            for k in range(i+1, len(tree_map)):
                direction_count += 1
                if tree_map[k][j] >= current_tree:
                    break
            current_tree_score *= direction_count
            direction_count = 0
            for k in range(j+1, len(tree_map[i])):
                direction_count += 1
                if tree_map[i][k] >= current_tree:
                    break
            current_tree_score *= direction_count

            if current_tree_score > highest_score:
                highest_score = current_tree_score

    print(f"Highest score: {highest_score}")


if __name__ == "__main__":
    main()
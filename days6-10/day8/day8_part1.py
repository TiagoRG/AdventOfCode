import load_map


def main():
    tree_map = load_map.load_map()

    visible_from_outside_count = 0
    not_visible_from_outside_count = 0
    for i in range(len(tree_map)):
        for j in range(len(tree_map)):
            if i == 0 or i == len(tree_map)-1 or j == 0 or j == len(tree_map)-1:
                visible_from_outside_count += 1
            elif (max(tree_map[i][:j]) < tree_map[i][j]
                    or max(tree_map[i][j+1:]) < tree_map[i][j]
                    or max(get_column_until_i(tree_map, j, i)) < tree_map[i][j]
                    or max(get_column_from_i(tree_map, j, i+1)) < tree_map[i][j]):
                visible_from_outside_count += 1
            else:
                not_visible_from_outside_count += 1

    print(f"Visible from outside: {visible_from_outside_count}\nNot visible from outside: {not_visible_from_outside_count}")


def get_column_until_i(lst: list[list[int]], column_id: int, end_i: int) -> list[int]:
    column: list[int] = []
    for i in range(end_i):
        for j in range(len(lst[i])):
            if j == column_id:
                column.append(lst[i][j])
    return column


def get_column_from_i(lst: list[list[int]], column_id: int, begin_i: int) -> list[int]:
    column: list[int] = []
    for i in range(begin_i, len(lst)):
        for j in range(len(lst[i])):
            if j == column_id:
                column.append(lst[i][j])
    return column


if __name__ == "__main__":
    main()
def main() -> None:
    with open('input.txt', 'r') as f:
        input_content = f.read()
        crate_input_content, moves_input_content = input_content.split('\n\n')

    crates_dictionary = {
        1: [],
        2: [],
        3: [],
        4: [],
        5: [],
        6: [],
        7: [],
        8: [],
        9: [],
    }
    crates_dictionary_builder(crates_dictionary, crate_input_content)
    move_list = move_list_builder(moves_input_content)

    print(crates_dictionary)

    for move in move_list:
        crates_moved = crates_dictionary[move[1]][:move[0]]
        for i in range(move[0]):
            crates_dictionary[move[1]].remove(crates_dictionary[move[1]][0])
        for crate in crates_moved[::-1]:  # Remove [::-1] for part 1
            crates_dictionary[move[2]].insert(0, crate)
        print(crates_dictionary)

    for crates in crates_dictionary.values():
        print(crates[0], end='')


def crates_dictionary_builder(c_dict: dict, content: str) -> None:
    content_lines = content[:-1].splitlines()
    for line in content_lines:
        first_crate = 0
        if line[first_crate] == '[':
            c_dict[1].append(line[1])
        while '[' in line[first_crate+1:]:
            try:
                first_crate = line.index('[', first_crate+1)
            except ValueError:
                print('a')
            c_dict[first_crate//4+1] += line[first_crate+1]


def move_list_builder(content: str) -> list[tuple]:
    moves_content = content.splitlines()
    move_list = []
    for move in moves_content:
        move = move[5:]
        moved, rest = move.split(' from ')
        moved_from, moved_to = rest.split(' to ')
        move_list.append((int(moved), int(moved_from), int(moved_to)))
    return move_list


if __name__ == "__main__":
    main()
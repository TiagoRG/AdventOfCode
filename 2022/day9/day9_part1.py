def main():
    with open('input.txt', 'r') as f:
        head_moves = f.readlines()
    head_moves = [move.split() for move in head_moves]

    head_pos, tail_pos = [0,0], [0,0]
    pos_list = []
    for move in head_moves:
        pos_list.append(tuple(tail_pos))
        if move[0] == 'U':
            for i in range(int(move[1])):
                head_pos[1] += 1
                if validate_pos(head_pos, tail_pos):
                    pos_list.append(tuple(tail_pos))
                    continue
                if head_pos[0] == tail_pos[0]:
                    tail_pos[1] += 1
                    pos_list.append(tuple(tail_pos))
                elif head_pos[0] == tail_pos[0] + 1:
                    tail_pos[1] += 1
                    pos_list.append(tuple(tail_pos))
                    tail_pos[0] += 1
                    pos_list.append(tuple(tail_pos))
                elif head_pos[0] == tail_pos[0] - 1:
                    tail_pos[1] += 1
                    pos_list.append(tuple(tail_pos))
                    tail_pos[0] -= 1
                    pos_list.append(tuple(tail_pos))
        elif move[0] == 'D':
            for i in range(int(move[1])):
                head_pos[1] -= 1
                if validate_pos(head_pos, tail_pos):
                    pos_list.append(tuple(tail_pos))
                    continue
                if head_pos[0] == tail_pos[0]:
                    tail_pos[1] -= 1
                    pos_list.append(tuple(tail_pos))
                elif head_pos[0] == tail_pos[0] + 1:
                    tail_pos[1] -= 1
                    pos_list.append(tuple(tail_pos))
                    tail_pos[0] += 1
                    pos_list.append(tuple(tail_pos))
                elif head_pos[0] == tail_pos[0] - 1:
                    tail_pos[1] -= 1
                    pos_list.append(tuple(tail_pos))
                    tail_pos[0] -= 1
                    pos_list.append(tuple(tail_pos))
        elif move[0] == 'R':
            for i in range(int(move[1])):
                head_pos[0] += 1
                if validate_pos(head_pos, tail_pos):
                    pos_list.append(tuple(tail_pos))
                    continue
                if head_pos[1] == tail_pos[1]:
                    tail_pos[0] += 1
                    pos_list.append(tuple(tail_pos))
                elif head_pos[1] == tail_pos[1] + 1:
                    tail_pos[0] += 1
                    pos_list.append(tuple(tail_pos))
                    tail_pos[1] += 1
                    pos_list.append(tuple(tail_pos))
                elif head_pos[1] == tail_pos[1] - 1:
                    tail_pos[0] += 1
                    pos_list.append(tuple(tail_pos))
                    tail_pos[1] -= 1
                    pos_list.append(tuple(tail_pos))
        elif move[0] == 'L':
            for i in range(int(move[1])):
                head_pos[0] -= 1
                if validate_pos(head_pos, tail_pos):
                    pos_list.append(tuple(tail_pos))
                    continue
                if head_pos[1] == tail_pos[1]:
                    tail_pos[0] -= 1
                    pos_list.append(tuple(tail_pos))
                elif head_pos[1] == tail_pos[1] + 1:
                    tail_pos[0] -= 1
                    pos_list.append(tuple(tail_pos))
                    tail_pos[1] += 1
                    pos_list.append(tuple(tail_pos))
                elif head_pos[1] == tail_pos[1] - 1:
                    tail_pos[0] -= 1
                    pos_list.append(tuple(tail_pos))
                    tail_pos[1] -= 1
                    pos_list.append(tuple(tail_pos))

    pos_set = set(pos_list)
    pos_list = list(pos_set)
    print(len(pos_list))

    #print(f"Repeated positions: {pos_count}")


def validate_pos(h: list[int], t: list[int]) -> bool:
    return -2 < h[0] - t[0] < 2 and -2 < h[1] - t[1] < 2


if __name__ == "__main__":
    main()
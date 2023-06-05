def main():
    with open('input.txt', 'r') as f:
        stream = f.read()

    for i in range(len(stream)):
        if validate(stream[i:i+14]):
            print(i+14)
            return
    print('No valid marker')


def validate(part_string: str) -> bool:
    for i in range(len(part_string)):
        for j in range(len(part_string)):
            if i != j and part_string[i] == part_string[j]:
                return False
    return True


if __name__ == "__main__":
    main()

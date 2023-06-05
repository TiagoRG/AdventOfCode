def main():
    directory_contents = {}
    with open('input.txt', 'r') as f:
        input_lines = f.readlines()

    line_index = 0
    while line_index < len(input_lines):
        if input_lines[line_index].startswith('$ cd'):
            if input_lines[line_index][5:] not in ['..', '/']:
                directory_contents[input_lines[line_index][5:]] = []
            line_index += 1
            continue
        elif input_lines[line_index].startswith('$ ls'):
            line_index += 1
            while not input_lines[line_index].startswith('$'):
                ...


def current_dir_size(dir: str) -> int:
    ...



if __name__ == "__main__":
    main()
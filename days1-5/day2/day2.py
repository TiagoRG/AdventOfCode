with open('inputs/input2.txt', 'r') as f:
    # Part 1
    matches = [rpsMatch.split(' ') for rpsMatch in f.read().split('\n')]
    score = 0
    for rpsMatch in matches:
        if rpsMatch[0] == 'A':
            if rpsMatch[1] == 'X':
                score += 1 + 3
            elif rpsMatch[1] == 'Y':
                score += 2 + 6
            elif rpsMatch[1] == 'Z':
                score += 3
        elif rpsMatch[0] == 'B':
            if rpsMatch[1] == 'X':
                score += 1
            elif rpsMatch[1] == 'Y':
                score += 2 + 3
            elif rpsMatch[1] == 'Z':
                score += 3 + 6
        elif rpsMatch[0] == 'C':
            if rpsMatch[1] == 'X':
                score += 1 + 6
            elif rpsMatch[1] == 'Y':
                score += 2
            elif rpsMatch[1] == 'Z':
                score += 3 + 3
    print(f"Total score: {score}")

    # Part 2
    roundN = 1
    score = 0
    for rpsMatch in matches:
        if rpsMatch[0] == 'A':
            if rpsMatch[1] == 'X':
                score += 3
            elif rpsMatch[1] == 'Y':
                score += 1 + 3
            elif rpsMatch[1] == 'Z':
                score += 2 + 6
        elif rpsMatch[0] == 'B':
            if rpsMatch[1] == 'X':
                score += 1
            elif rpsMatch[1] == 'Y':
                score += 2 + 3
            elif rpsMatch[1] == 'Z':
                score += 3 + 6
        elif rpsMatch[0] == 'C':
            if rpsMatch[1] == 'X':
                score += 2
            elif rpsMatch[1] == 'Y':
                score += 3 + 3
            elif rpsMatch[1] == 'Z':
                score += 1 + 6
    print(f"Total score: {score}")



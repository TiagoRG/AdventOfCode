with open('input.txt', 'r') as f:
    stream = f.read()
print([i+4 for i in range(len(stream)) if i < (len(stream)-4) and stream[i] not in stream[i + 1:i + 4] and stream[i + 1] not in stream[i + 2:i + 4] and stream[i + 2] != stream[i + 3]][0])

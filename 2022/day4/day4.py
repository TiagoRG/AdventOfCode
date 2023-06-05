with open('input.txt', 'r') as f:
    inputContent = f.read().splitlines()
    containedCount = 0
    intersectCount = 0
    for line in inputContent:
        pair = line.split(',')
        startRange1, endRange1 = pair[0].split('-')
        startRange2, endRange2 = pair[1].split('-')
        if ((int(startRange1) >= int(startRange2) and int(endRange1) <= int(endRange2))
        or (int(startRange2) >= int(startRange1) and int(endRange2) <= int(endRange1))):
            containedCount += 1

        if ((int(endRange1) >= int(startRange2) and int(startRange1) <= int(endRange2))
        or (int(endRange2) >= int(startRange1) and int(startRange2) <= int(endRange1))):
            intersectCount += 1

print("Contained Count:", containedCount)
print("Intersect Count:", intersectCount)

def main():
    with open('calories', 'r') as f:
        # Part 1
        calories = [line for line in f.read().split('\n')]
        caloriesPerDay = [0] * len(calories)
        index = 0
        for calorie in calories:
            if calorie == '':
                index += 1
            else:
                caloriesPerDay[index] += int(calorie)
        nOfElfs = caloriesPerDay.index(0)
        caloriesPerElf = caloriesPerDay[:nOfElfs]
        print(f"Top elf calorie count: {max(caloriesPerElf)}")

        # Part 2
        sumOfCaloriesOfTopThree = []
        for i in range(3):
            sumOfCaloriesOfTopThree.append(max(caloriesPerElf))
            caloriesPerElf.remove(max(caloriesPerElf))
        print(f"Sum of top three elfs: {sum(sumOfCaloriesOfTopThree)}")


if __name__ == '__main__':
    main()

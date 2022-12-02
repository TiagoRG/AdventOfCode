import os
from datetime import date


def main(args):
    currentDay = date.today().day
    reposDirectory = f"/home/{os.getlogin()}/repos/advent-of-code-2022"
    dayGroups = [(int(days[4:].split('-')[0]), int(days[4:].split('-')[1])) for days in [directory for directory in os.listdir(reposDirectory) if directory.startswith('days')][::-1]]
    indexOfDayGroup = -1
    for dayGroup in dayGroups:
        if dayGroup[0] < currentDay < dayGroup[1]:
            indexOfDayGroup = dayGroups.index(dayGroup)
            break
    currentDayGroupDir = f"{reposDirectory}/days{dayGroups[indexOfDayGroup][0]}-{dayGroups[indexOfDayGroup][1]}"
    currentDayDir = f"{currentDayGroupDir}/day{currentDay}"
    
    if len(args) > 1:
        if args[1] == "run":
            os.system(f"python3 {currentDayDir}/day{currentDay}.py")
            return

    if f"day{currentDay}" not in os.listdir(currentDayGroupDir):
        os.system(f"mkdir {currentDayDir}")
    os.system(f"vim {currentDayDir}/day{currentDay}.py")


import sys
if __name__ == "__main__":
    main(sys.argv)

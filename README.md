# 🚀 Martian Robots - Mars Rover Simulation

This is a clean and testable implementation of the **Mars Rover problem** — adapted for both **Web API** and **Console CLI**. It supports extensible architecture, SOLID principles, and is designed for future scalability.

---

## 🧰 Technologies Used
- **.NET 8**
- **CLI**
- **ASP.NET Core Web API**
- **NUnit + FakeItEasy**
- **Clean Architecture**
- **SOLID principles**
- **Dependency Injection**

---

## 🏛️ Architecture Overview
- **MartianRobots.Api/** – REST API project
- **MartianRobots.Cli/** – Console interface
- **MartianRobots.Application/** – Use cases and services
- **MartianRobots.Domain/** – Core business logic
- **MartianRobots.Common/** – Enums, Constants, Helpers, Validators
- **MartianRobots.Dto/** – DTOs and Mappers
- **MartianRobots.Tests/** – Unit tests
 
---

## 📦 Design Patterns Used

| Pattern               | Purpose                             |
|-----------------------|-------------------------------------|
| **Command Pattern**   | Encapsulates `L`, `R`, `F` instructions |
| **Factory Pattern**   | Creates commands dynamically         |
| **Strategy (implicit)**| Direction handling logic             |
| **Value Objects**     | Represent `Coordinates`, `Direction` safely |
| **Mapper Layer**      | Converts DTOs to domain models       |

---

## 🚀 How to Run
### ✅ Console CLI
```bash
dotnet run --project MartianRobots.Cli
Enter plateau upper-right coordinates (e.g. 5 3): 5 3
Enter rover position (e.g. 1 1 E): 1 1 E
Enter command sequence (e.g. LFLFLFLFF): RFRFRFRF
...
```
### ✅ Web API
```bash
dotnet run --project MartianRobots.Api
```
Then send POST request to:
<code>POST /Rover</code>

```json
{
  "plateauSizeX": 5,
  "plateauSizeY": 3,
  "roverConfigurations": [
    {
      "initialPositionX": 1,
      "initialPositionY": 1,
      "initialDirection": "E",
      "commands": "RFRFRFRF"
    },
    {
      "initialPositionX": 3,
      "initialPositionY": 2,
      "initialDirection": "N",
      "commands": "FRRFLLFFRRFLL"
    },
    {
      "initialPositionX": 0,
      "initialPositionY": 3,
      "initialDirection": "W",
      "commands": "LLFFFLFLFL"
    }
  ]
}
```

Response example:
```json
[
  { "finalPositionX": 1, "finalPositionY": 1, "finalDirection": "E", "isRoverLost": false },
  { "finalPositionX": 3, "finalPositionY": 3, "finalDirection": "N", "isRoverLost": true },
  { "finalPositionX": 2, "finalPositionY": 3, "finalDirection": "S", "isRoverLost": false }
]
```

---

## 🚧 What Could Be Improved
| Area               | Suggestion                            |
|-----------------------|-------------------------------------|
| **Logging**   | Add ILogger<T> support |
| **Persistence**   | Optionally store missions and results         |
| **Visualization**| Optional map renderer in CLI or web frontend             |
| **Real concurrency**     | Run rovers in parallel or simulate delays |
| **Command parsing**      | Accept input from file or stream       |

---

## 🧩 Problem
The surface of Mars can be modelled by a rectangular grid around which robots are able to move according to instructions provided from Earth. You are to write a program that determines each sequence of robot positions and reports the final position of the robot.

A robot position consists of a grid coordinate (a pair of integers: x-coordinate followed by y-coordinate) and an orientation (N, S, E, W for north, south, east, and west). A robot instruction is a string of the letters "L", "R", and "F" which represent, respectively, the instructions:

Left (L): the robot turns left 90 degrees and remains on the current grid point.

Right (R): the robot turns right 90 degrees and remains on the current grid point.

Forward (F): the robot moves forward one grid point in the direction of the current orientation and maintains the same orientation.

The direction North corresponds to the direction from grid point (x, y) to grid point (x, y+1). There is also a possibility that additional command types may be required in the future and provision should be made for this.

Since the grid is rectangular and bounded, a robot that moves off an edge of the grid is lost forever. However, lost robots leave a robot scent that prohibits future robots from dropping off the world at the same grid point. The scent is left at the last grid position the robot occupied before disappearing over the edge. An instruction to move "off" the world from a grid point from which a robot has been previously lost is simply ignored by the current robot.

🔡 Input
The first line of input is the upper-right coordinates of the rectangular world. The lower-left coordinates are assumed to be 0 0.

The remaining input consists of a sequence of robot positions and instructions (two lines per robot).
A position consists of:

Two integers specifying the initial coordinates of the robot

An orientation (N, S, E, or W)

All values are separated by whitespace on one line.
A robot instruction is a string of the letters "L", "R", and "F" on one line.
Each robot is processed sequentially — i.e., it finishes executing its instructions before the next one begins.

Maximum coordinate value: 50

Maximum instruction string length: < 100 characters

For each robot position/instruction, the output should indicate:

The final grid position and orientation of the robot

If a robot falls off the edge of the grid, append the word "LOST"

### Sample input
5 3

1 1 E

RFRFRFRF

3 2 N

FRRFLLFFRRFLL

0 3 W

LLFFFLFLFL

### Sample output
1 1 E

3 3 N LOST

2 3 S

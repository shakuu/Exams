function solve(args) {
    let boardSize = args[0].split(' ').map(Number),
        debrisCoords = args[1].split(';').map(str => {
            return str.split(' ').map(Number);
        }),
        numberOfCommands = +args[2],
        commands = args.splice(2),
        board = [],
        tanks = [],
        KocetoCounter = 4,
        CukiCounter = 4;

    const debris = 'debris',
        empty = '0';

    // Create Koceto tanks
    for (let tankNr = 0; tankNr < 4; tankNr += 1) {
        tanks[`Tank ${tankNr}`] = {
            'name': `Tank ${tankNr}`,
            'row': 0,
            'col': tankNr,
            'id': tankNr
        };
    }

    // Create Cuki tanks
    let tankNr = 4;
    for (let index = 0; index < 4; index += 1) {
        tanks[`Tank ${tankNr}`] = {
            'name': `Tank ${tankNr}`,
            'row': boardSize[0] - 1,
            'col': boardSize[1] - 1 - index,
            'id': tankNr
        };

        tankNr += 1;
    }

    // Create the board.
    for (let row = 0; row < boardSize[0]; row += 1) {
        board[row] = [];

        for (let col = 0; col < boardSize[1]; col += 1) {
            board[row][col] = empty;
        }
    }

    // Add Debris to the board.
    for (let index in debrisCoords) {
        let curCoord = debrisCoords[index];

        board[curCoord[0]][curCoord[1]] = debris;
    }

    // Add tanks to the board.
    for (let index in tanks) {
        let tank = tanks[index];
        board[tank.row][tank.col] = tank.name;
    }

    // // test print
    // console.log(board);
    // console.log(commands);

    function getTankName(id) {
        return `Tank ${+id}`;
    }

    function getDelta(strDirection) {
        let dir = strDirection + '';

        if (dir === 'l') {
            return {
                "deltaRow": 0,
                "deltaCol": -1
            };
        } else if (dir === 'r') {
            return {
                "deltaRow": 0,
                "deltaCol": 1
            };
        } else if (dir === 'u') {
            return {
                "deltaRow": -1,
                "deltaCol": 0
            };
        } else if (dir === 'd') {
            return {
                "deltaRow": 1,
                "deltaCol": 0
            };
        }
    }

    function moveTank(command) {
        let tankName = getTankName(+command[1]),
            numberOfMoves = +command[2],
            moveDirection = getDelta(command[3]);

        for (let nr = 0; nr < numberOfMoves; nr += 1) {
            // Stop if 
            let nextRow = tanks[tankName].row + moveDirection.deltaRow,
                nextCol = tanks[tankName].col + moveDirection.deltaCol;

            // Out of bounds
            if (!board[nextRow] || !board[nextRow][nextCol]) {
                // Stop
                break;
            }

            // Debris
            if (board[nextRow][nextCol] === debris) {
                break;
            }

            if (board[nextRow][nextCol] !== empty) {
                break;
            }

            // Clear current board cell
            board[tanks[tankName].row][tanks[tankName].col] = empty;

            tanks[tankName].row = nextRow;
            tanks[tankName].col = nextCol;

            // Mark next board cell
            board[tanks[tankName].row][tanks[tankName].col] = tanks[tankName].name;
        }
    }

    // Shoot.
    function shoot(command) {
        let tankName = getTankName(+command[1]),
            shotDirection = getDelta(command[2]),
            shotCoordinates = {
                "row": tanks[tankName].row,
                "col": tanks[tankName].col
            };

        while (true) {
            shotCoordinates.row += shotDirection.deltaRow;
            shotCoordinates.col += shotDirection.deltaCol;

            // Stop on out of bounds
            if (!board[shotCoordinates.row] || !board[shotCoordinates.row][shotCoordinates.col]) {
                break;
            }

            // Destroy if not empty
            if (board[shotCoordinates.row][shotCoordinates.col] !== empty) {
                let boardContent = board[shotCoordinates.row][shotCoordinates.col];

                // if tank remove and decrement player counters.
                if (tanks[boardContent]) {
                    console.log(`${tanks[boardContent].name} is gg`);

                    if (tanks[boardContent].id < 4) {
                        KocetoCounter -= 1;
                    } else if (tanks[boardContent].id >= 4) {
                        CukiCounter -= 1;
                    }
                }

                board[shotCoordinates.row][shotCoordinates.col] = empty;
                break;
            }
        }

        // Check Game Over
        if (KocetoCounter === 0 || CukiCounter === 0) {
            return true;
        } else {
            return false;
        }
    }

    // Parse commands
    let GameOver = false;

    for (let lineNr = 1; lineNr <= +commands[0]; lineNr += 1) {
        let curCommand = commands[lineNr].split(' ');


        if (curCommand[0] === 'mv') {
            // MOVE
            moveTank(curCommand);
        } else if (curCommand[0] === 'x') {
            // SHOOT
            GameOver = shoot(curCommand);
        }

        if (GameOver) {
            break;
        }

    }

    if (KocetoCounter === 0) {
        console.log('Koceto is gg');
    } else {
        console.log('Cuki is gg');
    }
}

const test2 = [
    '10 10',
    '1 0;1 1;1 2;1 3;1 4;4 1;4 2;4 3;4 4',
    '8',
    'mv 4 9 u',
    'x 4 l',
    'x 4 l',
    'x 4 l',
    'x 0 r',
    'mv 0 9 r',
    'mv 5 1 r',
    'x 5 u'
];

const test1 = [
    '5 5',
    '2 0;2 1;2 2;2 3;2 4',
    '13',
    'mv 7 2 l',
    'x 7 u',
    'x 0 d',
    'x 6 u',
    'x 5 u',
    'x 2 d',
    'x 3 d',
    'mv 4 1 u',
    'mv 4 4 l',
    'mv 1 1 l',
    'x 4 u',
    'mv 4 2 r',
    'x 2 d'
];

const test3 = [
    '10 5',
    '1 0;1 1;1 2;1 3;1 4;3 1;3 3;4 0;4 2;4 4',
    '43',
    'mv 6 5 r',
    'mv 0 6 d',
    'x 0 d',
    'x 0 d',
    'x 6 u',
    'x 6 u',
    'x 6 u',
    'x 6 u',
    'x 6 u',
    'x 7 u',
    'x 7 u',
    'x 7 u',
    'x 7 u',
    'x 7 u',
    'x 3 d',
    'x 3 d',
    'x 3 d',
    'x 3 d',
    'x 3 d',
    'x 4 u',
    'x 4 u',
    'x 4 u',
    'x 4 u',
    'x 4 u',
    'x 0 r',
    'mv 0 6 d',
    'mv 0 9 r',
    'x 0 d',
    'mv 0 1 l',
    'x 0 d',
    'mv 0 1 l',
    'x 0 d',
    'mv 0 1 l',
    'x 0 d',
    'mv 0 1 l',
    'x 0 d',
    'mv 0 1 l',
    'x 0 d',
    'mv 0 1 l',
    'x 0 d',
    'mv 0 1 l',
    'x 0 d',
    'mv 0 1 l',
    'x 0 d'
];

solve(test1);

module.exports = {
    solve
};
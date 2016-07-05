function solve(args) {
    let numbers = args[0].split(' ').map(Number),
        curSum = 0,
        maxSum = Number.MIN_VALUE;

    curSum += numbers[0];

    for (let index = 1; index < numbers.length; index += 1) {
        curSum += numbers[index];

        if (numbers[index] > numbers[index - 1] && numbers[index] > numbers[index + 1]) {
            maxSum = curSum > maxSum ? curSum : maxSum;

            curSum = numbers[index];
        }
    }

    maxSum = curSum > maxSum ? curSum : maxSum;

    console.log(maxSum);
}

const task1_test1 = [
    "5 1 7 4 8"
];

solve(task1_test1);

module.exports = {
    solve
};
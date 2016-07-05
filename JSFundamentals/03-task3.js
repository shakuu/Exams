function solve(args) {
    let selectors = [];

    let property = {
        'weight': 0,
        'value': ''
    };

    // Weight:
    // new selectors
    // new property

    // Regex
    let matchWeight = /@(\d+)/,
        matchSelector = /(([a-z|0-9])+) ?\{ ?(@\d+)?/im,
        matchProperty = /(([a-z|0-9|-])+): ?([^:; \{\}@]*);/im,
        matchClosingBracket = /^(\s*)?}(\s*)?$/im;

    let currentSelector = '',
        currentWeight = 0,
        scopeStack = [],
        selectorHistory = [],
        weightStack = [];

    const empty = 'initial';
    currentSelector = empty;

    // scopeStack.push(empty);

    // REMOVE SPACES
    for (let lineNr = 0; lineNr < args.length; lineNr += 1) {
        let line = (args[lineNr] + '').replace(/\s\s+/gim, ' ').trim();
        let weightIsChanged = false,
            weightSingleLine = false;

        // Apply new weight at the end of the line
        if (matchWeight.test(line)) {
            // Get current weight
            // Remove it from the line
            if (matchSelector.test(line)) {
                weightStack.push(currentWeight);
            }

            if (matchProperty.test(line)) {
                weightSingleLine = true;
                weightStack.push(currentWeight);
            }

            let match = line.match(matchWeight);
            currentWeight = +match[1];
            // weightIsChanged = true;

            line = line.replace(matchWeight, '').trim();
        }

        if (matchSelector.test(line)) {
            // Get the Selector 
            // Add to selectors
            // Set current selector
            let match = line.match(matchSelector);

            if (!selectors[match[1]]) {
                selectors[match[1]] = [];
                selectorHistory.push(match[1]);
            }

            scopeStack.push(currentSelector);
            weightStack.push(currentWeight);
            currentSelector = match[1];
        } else if (matchProperty.test(line)) {
            // Search for property
            // Eval weight
            // Add value/ change value
            let match = line.match(matchProperty);

            // If it's a new property.
            if (!selectors[currentSelector][match[1]]) {
                selectors[currentSelector][match[1]] = {
                    "value": match[3],
                    "wieght": +currentWeight
                };
            } else {
                let prevWeight = +selectors[currentSelector][match[1]].wieght;

                if (currentWeight > prevWeight) {
                    selectors[currentSelector][match[1]].value = match[3];
                }
            }

            if (weightSingleLine) {
                currentWeight = weightStack.pop();
                weightSingleLine = false;
            }
        } else if (line.trim() === '}') {
            currentSelector = scopeStack.pop();
            currentWeight = weightStack.pop();
        }

    }
    selectorHistory.sort();

    // div { font-size: 20px; }
    let output = [],
        curSelectorOutput = [];
    for (let sel of selectorHistory) {
        // console.log(sel);

        curSelectorOutput = [];
        // console.log(selectors[sel][0]);
        for (let prop in selectors[sel]) {
            curSelectorOutput.push(`${prop}: ${selectors[sel][prop].value}; }`);
        }

        curSelectorOutput.sort();

        for (let item of curSelectorOutput) {
            output.push(`${sel} { ${item}`);
        }
    }

    console.log(output.join('\r\n'));
}

const test1 = [
    'li {',
    '    font-size: 2px;',
    '    font-weight: bold;',
    '}',
    'div {',
    '    font-size: 20px; @5',
    '}',
    'div { @4',
    '    font-size: 17px;',
    '}',
    '@19',
    'li {',
    '    font-size: 42px;',
    '    color: red; @9',
    '}'
];

solve(test1);


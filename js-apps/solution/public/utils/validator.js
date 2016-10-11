const validator = (() => {
    const REGEX_LIBRARY = {
        URL_PATTERN: /((([A-Za-z]{3,9}:(?:\/\/)?)(?:[-;:&=\+\$,\w]+@)?[A-Za-z0-9.-]+|(?:www.|[-;:&=\+\$,\w]+@)[A-Za-z0-9.-]+)((?:\/[\+~%\/.\w-_]*)?\??(?:[-\+=&;%@.\w_]*)#?(?:[\w]*))?)/,
        USERNAME: /^[A-Za-z0-9_\.]+$/,
        PASSWORD: /^[A-Za-z0-9]+$/,
    };

    const CONSTRAINTS = {
        USERNAME: {
            MIN_LENGTH: 6,
            MAX_LENGTH: 30
        },
        MATERIAL_TITLE: {
            MIN_LENGTH: 6,
            MAX_LENGTH: 100
        }
    };

    function isStringWithLength(str, min, max) {
        min = min || CONSTRAINTS.STRING.MIN_LENGTH;
        max = max || CONSTRAINTS.STRING.MAX_LENGTH;

        let isValid = true;

        const type = typeof str;
        if (type !== 'string') {
            isValid = false;
        }

        const length = str.length;
        if (!(min <= length && length <= max)) {
            isValid = false;
        }

        return isValid;
    }

    function validateUrl(url) {
        if (!url || url.length === 0) {
            return;
        }
        //copied from http://stackoverflow.com/questions/5717093/check-if-a-javascript-string-is-an-url#answer-5717133
        if (!REGEX_LIBRARY.URL_PATTERN.test(url)) {
            return {
                message: 'Invalid url'
            };
        }
    }

    function isValidUsername(username) {
        var isStringWithValidLength = isStringWithLength(username, CONSTRAINTS.USERNAME.MIN_LENGTH, CONSTRAINTS.USERNAME.MAX_LENGTH);
        var containsValidSymbols = REGEX_LIBRARY.USERNAME.test(username);

        return isStringWithValidLength && containsValidSymbols;
    }

    function isValidPassword(password) {
        var isStringWithValidLength = isStringWithLength(password, CONSTRAINTS.USERNAME.MIN_LENGTH, CONSTRAINTS.USERNAME.MAX_LENGTH);
        var containsValidSymbols = REGEX_LIBRARY.PASSWORD.test(password);

        return isStringWithValidLength && containsValidSymbols;
    }

    function isValidTitle(title) {
        var isStringWithValidLength = isStringWithLength(password, CONSTRAINTS.MATERIAL_TITLE.MIN_LENGTH, CONSTRAINTS.MATERIAL_TITLE.MAX_LENGTH);
        return isStringWithValidLength;
    }

    return {
        user: {
            isValidUsername,
            isValidPassword
        },
        material: {
            isValidTitle
        },
        validateUrl,
        string: {
            isStringWithLength
        }
    };
})();
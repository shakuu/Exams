// const hash = window.location.hash;
const queryParamsHelper = (() => {
    function getQueryParamsFromHash(hash) {
        hash = String(hash);

        let params = {};
        if (hash.indexOf('?') < 0) {
            return params;
        }

        const inputParams = hash.split('?')[1].split('&');
        inputParams.forEach(param => {
            const split = param.split('=');
            params[split[0]] = split[1];
        });

        return params;
    }

    function addParamsToHash(hash, params) {
        const hashParams = getQueryParamsFromHash(hash);
        const hashParamsKeys = Object.keys(hashParams);

        let appendedHash = getHashWithoutParams(hash);
        appendedHash = `${appendedHash}?`;

        const paramsKeys = Object.keys(params);
        paramsKeys.forEach(key => {
            appendedHash = `${appendedHash}${key}=${params[key]}&`;
        });

        hashParamsKeys.forEach(key => {
            if (paramsKeys.indexOf(key) < 0) {
                appendedHash = `${appendedHash}${key}=${hashParams[key]}&`;
            }
        });

        const trimmedAppendedHash = appendedHash.substring(0, appendedHash.length - 1);
        return trimmedAppendedHash;
    }

    function getHashWithoutParams(hash) {
        hash = String(hash);
        const indexOfSeparator = hash.indexOf('?');
        if (indexOfSeparator < 0) {
            return hash;
        }

        const hashWithoutParams = hash.substring(0, indexOfSeparator - 1);
        return hashWithoutParams;
    }

    return {
        getHashWithoutParams,
        getQueryParamsFromHash,
        addParamsToHash
    };
})();
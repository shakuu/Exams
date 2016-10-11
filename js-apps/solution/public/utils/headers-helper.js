const headersHelper = (() => {
    const HEADERS = {
        X_AUTH_KEY: 'x-auth-key',
        X_SESSION_KEY: 'X-SessionKey'
    };

    function addXAuthKeyHeader(headers = {}) {
        const loggedUser = credentialManager.isLogged();
        if (loggedUser) {
            const user = credentialManager.load();
            headers[HEADERS.X_AUTH_KEY] = user.authKey;
        } else {
            throw new Error('Not logged in');
        }

        return headers;
    }

     function addXSessionKeyHeader(headers = {}) {
        const loggedUser = credentialManager.isLogged();
        if (loggedUser) {
            const user = credentialManager.load();
            headers[HEADERS.X_SESSION_KEY] = user.sessionKey;
        } else {
            throw new Error('Not logged in');
        }

        return headers;
    }

    return {
        addXAuthKeyHeader,
        addXSessionKeyHeader
    };
})();
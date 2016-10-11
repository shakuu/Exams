const ajaxRequester = (() => {
    function get(url, headers = {}) {
        const encodedUrl = encodeURI(url);
        return new Promise((resolve, reject) => {
            $.ajax({
                    url: encodedUrl,
                    method: 'GET',
                    contentType: 'application/json',
                    headers: headers
                })
                .done(resolve)
                .fail(reject);
        });
    }

    function postJSON(url, json, headers = {}) {
        const encodedUrl = encodeURI(url);
        return new Promise((resolve, reject) => {
            $.ajax({
                    url: encodedUrl,
                    method: 'POST',
                    contentType: 'application/json',
                    data: JSON.stringify(json),
                    headers: headers
                })
                .done(resolve)
                .fail(reject);
        });
    }

    function putJSON(url, json, headers = {}) {
        const encodedUrl = encodeURI(url);
        return new Promise((resolve, reject) => {
            $.ajax({
                    url: encodedUrl,
                    method: 'PUT',
                    contentType: 'application/json',
                    data: JSON.stringify(json),
                    headers: headers
                })
                .done(resolve)
                .fail(reject);
        });
    }

    return {
        get,
        postJSON,
        putJSON
    };
})();
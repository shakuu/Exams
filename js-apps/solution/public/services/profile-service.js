const profileDataService = (() => {
    const URLS = {
        GET_PROFILE: 'api/profiles'
    };

    function loadProfile(username) {
        const urlWithUsername = `${URLS.GET_PROFILE}/${username}`;
        return ajaxRequester.get(urlWithUsername);
    }

    return {
        loadProfile
    };
})();
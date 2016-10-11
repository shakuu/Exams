const profileController = (() => {
    function loadProfile(containerId, username) {
        const content = $(containerId);

        return Promise.all([
                handlebarsViewLoader.load('profile'),
                profileDataService.loadProfile(username)
            ])
            .then(([view, data]) => {
                const user = data.result;
                const html = view(user);
                content.html(html);
                return user;
            })
            .catch((err) => {
                console.log(err);
                toastr.error(err.responseText);
            });
    }

    return {
        loadProfile
    };
})();
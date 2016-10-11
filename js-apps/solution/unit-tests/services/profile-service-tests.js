describe('profileDataService', () => {
    describe('loadProfile', () => {
        it('Should invoke ajaxRequester.get with correct url.', (done) => {
            const username = 'user';
            const correctUrl = `api/profiles/user`;

            const mockAjaxRequester = sinon.mock(ajaxRequester);
            mockAjaxRequester
                .expects('get')
                .returns(new Promise((resolve, reject) => {
                    resolve();
                }))
                .withArgs(correctUrl)
                .once();

            profileDataService.loadProfile(username)
                .then(() => {
                    mockAjaxRequester.verify();
                })
                .then(done, done);
        });
    });
});
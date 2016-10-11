describe('handlebarsViewLoader', () => {
    describe('load', () => {
        it('Should invoke ajaxRequester.get with correct url', (done) => {
            const viewName = 'test';
            const validUrl = 'views/test.html';

            const stubHandlebarsCompile = sinon.stub(Handlebars, 'compile');

            const mockAjaxRequester = sinon.mock(ajaxRequester);
            mockAjaxRequester.expects('get')
                .returns(new Promise((resolve, reject) => {
                    resolve();
                }))
                .withArgs('views/test.html')
                .once();

            handlebarsViewLoader.load(viewName)
                .then(() => {
                    mockAjaxRequester.verify();
                })
                .then(() => {
                    Handlebars.compile.restore();
                })
                .then(done, done);
        });

        it('Should invoke Handlebars.compile with correct data.', (done) => {
            const html = '<h1>it works</h1>';

            const viewName = 'test';
            const validUrl = 'views/test.html';

            const stubAjaxRequesterGet = sinon.stub(ajaxRequester, 'get');
            stubAjaxRequesterGet.returns(new Promise((resolve, reject) => {
                resolve(html);
            }));

            const mockHandlebars = sinon.mock(Handlebars);
            mockHandlebars
                .expects('compile')
                .withArgs(html)
                .once();

            handlebarsViewLoader.load(viewName)
                .then(() => {
                    mockHandlebars.verify();
                })
                .then(() => {
                    ajaxRequester.get.restore();
                })
                .then(done, done);
        });
    });
});
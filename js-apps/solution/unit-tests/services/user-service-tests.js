describe('userDataService', () => {
    describe('getAllMaterials', () => {
        it('Should invoke ajaxRequester.get with correct url and header', (done) => {
            const correctUrl = 'api/user-materials';

            const fakeHeader = {
                fake: 'header'
            };

            const stubHeadersHelper = sinon.stub(headersHelper, 'addXAuthKeyHeader');
            stubHeadersHelper.returns(fakeHeader);

            const mockAjaxRequester = sinon.mock(ajaxRequester);
            mockAjaxRequester
                .expects('get')
                .returns(new Promise((resolve, reject) => {
                    resolve();
                }))
                .withArgs(correctUrl, fakeHeader)
                .once();

            userDataService.getAllMaterials()
                .then(() => {
                    mockAjaxRequester.verify();
                })
                .then(() => {
                    headersHelper.addXAuthKeyHeader.restore();
                })
                .then(done, done);
        });
    });

    describe('getMaterialsFromCategory', () => {
        it('Should invoke ajaxRequester.get with correct url and header', (done) => {
            const fakeCategory = 'fake';
            const correctUrl = 'api/user-materials/fake';

            const fakeHeader = {
                fake: 'header'
            };

            const stubHeadersHelper = sinon.stub(headersHelper, 'addXAuthKeyHeader');
            stubHeadersHelper.returns(fakeHeader);

            const mockAjaxRequester = sinon.mock(ajaxRequester);
            mockAjaxRequester
                .expects('get')
                .returns(new Promise((resolve, reject) => {
                    resolve();
                }))
                .withArgs(correctUrl, fakeHeader)
                .once();

            userDataService.getMaterialsFromCategory(fakeCategory)
                .then(() => {
                    mockAjaxRequester.verify();
                })
                .then(() => {
                    headersHelper.addXAuthKeyHeader.restore();
                })
                .then(done, done);
        });
    });

    describe('setInitialMaterialCategory', () => {
        it('Should invoke ajaxRequester.postJSON with correct parameters', (done) => {
            const correctUrl = 'api/user-materials';

            const fakeMaterial = {
                id: 'fake',
                category: 'material'
            };

            const fakeHeader = {
                fake: 'header'
            };

            const stubHeadersHelper = sinon.stub(headersHelper, 'addXAuthKeyHeader');
            stubHeadersHelper.returns(fakeHeader);

            const mockAjaxRequester = sinon.mock(ajaxRequester);
            mockAjaxRequester
                .expects('postJSON')
                .returns(new Promise((resolve, reject) => {
                    resolve();
                }))
                .withArgs(correctUrl, fakeMaterial, fakeHeader)
                .once();

            userDataService.setInitialMaterialCategory(fakeMaterial)
                .then(() => {
                    mockAjaxRequester.verify();
                })
                .then(() => {
                    headersHelper.addXAuthKeyHeader.restore();
                })
                .then(done, done);
        });
    });

    describe('updateMaterialCatergory', () => {
        it('Should invoke ajaxRequester.putJSON with correct parameters', (done) => {
            const correctUrl = 'api/user-materials';

            const fakeMaterial = {
                id: 'fake',
                category: 'material'
            };

            const fakeHeader = {
                fake: 'header'
            };

            const stubHeadersHelper = sinon.stub(headersHelper, 'addXAuthKeyHeader');
            stubHeadersHelper.returns(fakeHeader);

            const mockAjaxRequester = sinon.mock(ajaxRequester);
            mockAjaxRequester
                .expects('putJSON')
                .returns(new Promise((resolve, reject) => {
                    resolve();
                }))
                .withArgs(correctUrl, fakeMaterial, fakeHeader)
                .once();

            userDataService.updateMaterialCatergory(fakeMaterial)
                .then(() => {
                    mockAjaxRequester.verify();
                })
                .then(() => {
                    headersHelper.addXAuthKeyHeader.restore();
                })
                .then(done, done);
        });
    });
});
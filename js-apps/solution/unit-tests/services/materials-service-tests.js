describe('materialsDataService', () => {
    describe('getAllMaterials', () => {
        it('Should invoke ajaxRequester.get with correct url.', (done) => {
            const correctUrl = `api/materials`;

            const mockAjaxRequester = sinon.mock(ajaxRequester);
            mockAjaxRequester
                .expects('get')
                .returns(new Promise((resolve, reject) => {
                    resolve();
                }))
                .withArgs(correctUrl)
                .once();

            materialsDataService.getAllMaterials()
                .then(() => {
                    mockAjaxRequester.verify();
                })
                .then(done, done);
        });
    });

    describe('getMaterialsWithId', () => {
        it('Should invoke ajaxRequester.get with correct url.', (done) => {
            const materialId = 'someId';
            const correctUrl = `api/materials/someId`;

            const mockAjaxRequester = sinon.mock(ajaxRequester);
            mockAjaxRequester
                .expects('get')
                .returns(new Promise((resolve, reject) => {
                    resolve();
                }))
                .withArgs(correctUrl)
                .once();

            materialsDataService.getMaterialWithId(materialId)
                .then(() => {
                    mockAjaxRequester.verify();
                })
                .then(done, done);
        });
    });

    describe('createNewMaterial', () => {
        it('Should invoke ajaxRequester.postJSON with correct params', (done) => {
            const correctUrl = 'api/materials';

            const fakeHeader = {
                fake: 'header'
            };

            const fakeMaterial = {
                title: 'title',
                text: 'text',
                img: 'img'
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


            materialsDataService.createNewMaterial(fakeMaterial)
                .then(() => {
                    mockAjaxRequester.verify();
                })
                .then(() => {
                    headersHelper.addXAuthKeyHeader.restore();
                })
                .then(done, done);
        });
    });

    describe('createCommentForMaterialWithId', () => {
        it('Should invoke ajaxRequester.putJSON with correct params', (done) => {
            const fakeMaterialId = 'fakeId';
            const correctUrl = 'api/materials/fakeId/comments';

            const fakeHeader = {
                fake: 'header'
            };

            const fakeComment = {
                commentText:'text'
            };

            const stubHeadersHelper = sinon.stub(headersHelper, 'addXAuthKeyHeader');
            stubHeadersHelper.returns(fakeHeader);

            const mockAjaxRequester = sinon.mock(ajaxRequester);
            mockAjaxRequester
                .expects('putJSON')
                .returns(new Promise((resolve, reject) => {
                    resolve();
                }))
                .withArgs(correctUrl, fakeComment, fakeHeader)
                .once();


            materialsDataService.createCommentForMaterialWithId(fakeComment, fakeMaterialId)
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
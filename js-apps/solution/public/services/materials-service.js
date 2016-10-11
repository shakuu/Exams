const materialsDataService = (() => {
    const URLS = {
        GET: 'api/materials',
        POST: 'api/materials'
    };

    function getAllMaterials(params) {
        let url = URLS.GET;
        if (params) {
            url = queryParamsHelper.addParamsToHash(url, params);
        }

        return ajaxRequester.get(url);
    }

    function createNewMaterial(material) {
        const headers = headersHelper.addXAuthKeyHeader();
        return ajaxRequester.postJSON(URLS.POST, material, headers);
    }

    function getMaterialWithId(id) {
        const urlWithId = `${URLS.GET}/${id}`;
        return ajaxRequester.get(urlWithId);
    }

    function createCommentForMaterialWithId(comment, materialId) {
        const headers = headersHelper.addXAuthKeyHeader();
        const urlWithId = `${URLS.POST}/${materialId}/comments`;
        return ajaxRequester.putJSON(urlWithId, comment, headers);
    }

    return {
        getAllMaterials,
        getMaterialWithId,
        createNewMaterial,
        createCommentForMaterialWithId
    };
})();
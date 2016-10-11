const userDataService = (() => {
    const URLS = {
        ALL: 'api/user-materials',
        UPDATE: 'api/user-materials'
    };

    function getAllMaterials() {
        const headers = headersHelper.addXAuthKeyHeader();
        return ajaxRequester.get(URLS.ALL, headers);
    }

    function getMaterialsFromCategory(category) {
        const headers = headersHelper.addXAuthKeyHeader();
        const urlWithCategory = `${URLS.ALL}/${category}`;
        return ajaxRequester.get(urlWithCategory, headers);
    }

    function setInitialMaterialCategory(material) {
        const headers = headersHelper.addXAuthKeyHeader();
        return ajaxRequester.postJSON(URLS.UPDATE, material, headers);
    }

    function updateMaterialCatergory(material) {
        const headers = headersHelper.addXAuthKeyHeader();
        return ajaxRequester.putJSON(URLS.UPDATE, material, headers);
    }

    return {
        getAllMaterials,
        getMaterialsFromCategory,
        setInitialMaterialCategory,
        updateMaterialCatergory
    };
})();
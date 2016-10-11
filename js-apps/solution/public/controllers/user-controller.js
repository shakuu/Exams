const userController = (() => {
    function getAllUserMaterials(containerId) {
        const content = $(containerId);

        return Promise.all([
                handlebarsViewLoader.load('user-materials'),
                userDataService.getAllMaterials()
            ])
            .then(([view, data]) => {
                const materials = data.result;
                const html = view(materials);
                content.html(html);
                return materials;
            })
            .catch((err) => {
                console.log(err);
                toastr.error(err.responseText);
            });
    }

    function getUserMaterialsFromCategory(containerId, category) {
        const content = $(containerId);

        return Promise.all([
                handlebarsViewLoader.load('user-materials'),
                userDataService.getMaterialsFromCategory(category)
            ])
            .then(([view, data]) => {
                const materials = data.result;
                const html = view(materials);
                content.html(html);
                return materials;
            })
            .catch((err) => {
                console.log(err);
                toastr.error(err.responseText);
            });
    }

    function updateUserMaterialCategory(material) {
        return userDataService.setInitialMaterialCategory(material)
            .then(() => {
                toastr.success('Assigned category');
            })
            .catch((err) => {
                userDataService.updateMaterialCatergory(material)
                    .then(() => {
                        toastr.success('Assigned category');
                    })
                    .catch((err) => {
                        console.log(err);
                        toastr.error(err.responseText);
                    });
            });
    }

    return {
        getAllUserMaterials,
        getUserMaterialsFromCategory,
        updateUserMaterialCategory
    };
})();
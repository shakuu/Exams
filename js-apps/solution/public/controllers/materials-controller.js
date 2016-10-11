const materialsController = (() => {
    function loadAllMaterials(containerId, params) {
        const content = $(containerId);

        return Promise.all([
                handlebarsViewLoader.load('materials'),
                materialsDataService.getAllMaterials(params)
            ])
            .then(([view, data]) => {
                const materials = data.result;
                const html = view(materials);
                content.html(html);
                return materials;
            })
            .then(() => {
                const isUserLogged = credentialManager.isLogged();
                if (!isUserLogged) {
                    return;
                }

                handlebarsViewLoader.load('materials-create')
                    .then((view) => {
                        const html = view();
                        content.prepend(html);
                    })
                    .then(() => {
                        const tbMaterialTitle = content.find('#tb-material-title');
                        const tbMaterialDescription = content.find('#tb-material-description');
                        const tbMaterialImage = content.find('#tb-material-image');

                        const btnMaterialCreate = content.find('#btn-material-create');
                        btnMaterialCreate.on('click', (ev) => {
                            const material = {
                                title: tbMaterialTitle.val(),
                                description: tbMaterialDescription.val(),
                                img: tbMaterialImage.val() || ''
                            };

                            const hasValidTitle = validator.material.isValidTitle(material.title);
                            if (!hasValidTitle) {
                                throw new Error('Invalid title');
                            }

                            const isValidUrl = validator.validateUrl(material.url);
                            if (!isValidUrl) {
                                material.url = '';
                            }

                            materialsDataService.createNewMaterial(material)
                                .then(() => {
                                    toastr.success('Successfully created new material');
                                    loadAllMaterials(containerId);
                                });
                        });
                    });
            })
            .catch((err) => {
                console.log(err);
                toastr.error(err.responseText);
            });
    }

    function getMaterialWithId(containerId, materialId) {
        const content = $(containerId);

        return Promise.all([
                handlebarsViewLoader.load('materials-expand'),
                materialsDataService.getMaterialWithId(materialId)
            ])
            .then(([view, data]) => {
                const material = data.result;
                const html = view(material);

                content.html(html);
                return material;
            })
            .then((material) => {
                const tbCommentText = content.find('#tb-comment-text');
                const materialId = material.id;

                const btnCommentAdd = content.find('#btn-comment-add');
                btnCommentAdd.on('click', (ev) => {
                    const userLoggedIn = credentialManager.isLogged();
                    if (!userLoggedIn) {
                        toastr.error('Login first');
                        return;
                    }

                    const comment = {
                        commentText: tbCommentText.val()
                    };

                    materialsDataService.createCommentForMaterialWithId(comment, materialId)
                        .then((material) => {
                            toastr.success('Successfully created comment');
                            getMaterialWithId(containerId, materialId);
                        })
                        .catch((err) => {
                            console.log(err);
                            toastr.error(err.responseText);
                        });
                });
            })
            .catch((err) => {
                console.log(err);
                toastr.error(err.responseText);
            });
    }

    return {
        loadAllMaterials,
        getMaterialWithId
    };
})();
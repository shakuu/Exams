const appRouter = (() => {
    let containerId = '#content';
    const router = new Navigo(null, true);

    router.on('/home', () => {
        const hash = window.location.hash;
        const params = queryParamsHelper.getQueryParamsFromHash(hash);

        materialsController.loadAllMaterials(containerId, params)
            .then(() => {
                paginationAddon.paginate(containerId, '.material', 5);
            })
            .then(() => {
                searchAddon.createQuery(containerId, '.material');
            })
            .then(() => {
                navbarController.displayControls();
            });

        // https://github.com/krasimir/navigo/issues/21
        router._lastRouteResolved = null;
    });

    router.on('/login', () => {
        loginController.main(containerId)
            .then(() => {
                navbarController.displayControls();
            });
    });

    router.on('/logout', () => {
        loginController.logout()
            .then(() => {
                navbarController.displayControls();
            });
    });

    router.on('/materials/:id', (params) => {
        const materialId = params.id;
        materialsController.getMaterialWithId(containerId, materialId)
            .then(() => {
                navbarController.displayControls();
            });
    });

    router.on('/profiles', () => {
        loginController.displayAllUsers(containerId)
            .then(() => {
                const predicate = (el) => {
                    const username = $(el).find('#username').html();
                    return username;
                };

                domTreeHelpers.orderElementsBy(containerId, '.user', predicate);
            })
            .then(() => {
                paginationAddon.paginate(containerId, '.user', 5);
            })
            .then(() => {
                navbarController.displayControls();
            });
    });

    router.on('/users/:username', (params) => {
        const username = params.username;
        profileController.loadProfile(containerId, username)
            .then(() => {
                navbarController.displayControls();
            });
    });

    router.on('/profiles/:username', (params) => {
        const username = params.username;
        profileController.loadProfile(containerId, username)
            .then(() => {
                navbarController.displayControls();
            });
    });

    router.on('/user-materials/:category/:id', (params) => {
        const userIsLogged = credentialManager.isLogged();
        if (!userIsLogged) {
            router.navigate('/login');
            return;
        }

        const category = params.category;
        const id = params.id;
        const material = {
            id: id,
            category: category
        };

        userController.updateUserMaterialCategory(material)
            .then(() => {
                navbarController.displayControls();
            })
            .then(() => {
                router.navigate('/home');
            });
    });

    router.on('/user-materials/:category', (params) => {
        const userIsLogged = credentialManager.isLogged();
        if (!userIsLogged) {
            router.navigate('/login');
            return;
        }

        const category = params.category;
        userController.getUserMaterialsFromCategory(containerId, category)
            .then(() => {
                navbarController.displayControls();
            });
    });

    router.on('/user-materials', () => {
        const userIsLogged = credentialManager.isLogged();
        if (!userIsLogged) {
            router.navigate('/login');
            return;
        }

        userController.getAllUserMaterials(containerId)
            .then(() => {
                navbarController.displayControls();
            });
    });

    router.on(() => {
        router.navigate('/home');
    });

    router.notFound(() => {
        router.navigate('/home');
    });

    function start(container) {
        containerId = container;
        router.resolve();
    }

    return {
        start
    };
})();
const appRouter = ((containerId) => {
    const router = new Navigo(null, true);

    router.on(() => {
        router.navigate('/home');
    });

    router.notFound(() => {
        homeController.load(containerId);
    });

    return router;
})();
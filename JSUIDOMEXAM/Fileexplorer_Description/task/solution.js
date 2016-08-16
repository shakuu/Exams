function solve() {
    return function (fileContentsByName) {
        // you solution
        var root = $('.file-explorer');
        var filePreview = root.siblings('.file-preview');

        root.find('.dir-item').addClass('collapsed');

        root.on('click', '.dir-item', toggleDirectoryCollapse);
        root.on('click', '.file-item', displayFileContent);
        root.on('click', '.del-btn', deleteItem);

        var addButton = root.children('.add-wrapper').children('.add-btn').first();
        var addInput = root.children('.add-wrapper').children('input').first();

        var itemsRoot = root.children('.items').first();
        var itemTemplate = itemsRoot
            .children('.file-item')
            .first();

        addButton.on('click', function () {
            addButton.removeClass('visible');
            addInput.addClass('visible');
        });

        addInput.on('keydown', function (event) {
            if (event.keyCode !== 13) {
                return;
            }
            addButton.addClass('visible');
            addInput.removeClass('visible');

            var text = addInput.val();
            addInput.val('');

            // Directory 
            if (text.indexOf('/') >= 0) {
                var path = text.split('/');

                // var nameExists = checkDuplicateFileNames(path[1]);
                var folder = findFolder(path[0]);

                if (folder) {
                    var newItem = itemTemplate.clone();
                    newItem.children('.item-name')
                        .first().html(path[1]);
                    newItem.appendTo(folder.children('.items').first());
                }
                return;
            }

            // NO DUPLICATE FILE NAMES
            // var nameExists = checkDuplicateFileNames(text);
            // if (nameExists) {
            //     return;
            // }

            var newItem = itemTemplate.clone();
            newItem.children('.item-name')
                .first().html(text);
            newItem.appendTo(itemsRoot);
        });

        function findFolder(name) {
            var allDirItems = root.find('.dir-item');
            var foundItem = null;

            [].forEach.call(allDirItems, function (el, index) {
                var curName = $(el).children('.item-name').first().html();

                if (curName === name) {
                    foundItem = $(el);
                }
            });
            return foundItem;
        }

        function checkDuplicateFileNames(name) {
            var allFiles = root.find('.file-item');
            var exists = false;

            [].forEach.call(allFiles, function (el, index) {
                var current = $(el).children('.item-name').first().html();

                if (current === name) {
                    exists = true;
                }
            });
            return exists;
        }

        // WORKING
        function deleteItem(event) {
            var clicked = $(event.target);
            if (!clicked.hasClass('item')) {
                clicked = clicked.parents('.item').first();
            }
            clicked.remove();
        }

        // WORKING
        function displayFileContent(event) {
            var clicked = $(event.target);
            if (!clicked.hasClass('file-item')) {
                clicked = clicked.parents('.file-item').first();
            }
            var fileName = clicked.children('.item-name').first().html();

            var content = '';
            if (fileContentsByName[fileName]) {
                content = fileContentsByName[fileName];
            }
            filePreview.children('.file-content').first().text(content);
        }
        // WORKING
        function toggleDirectoryCollapse(event) {
            var clicked = $(event.target);

            if (clicked.hasClass('file-item') || clicked.parents().first().hasClass('file-item')) {
                return;
            }

            if (!clicked.hasClass('dir-item')) {
                clicked = clicked.parents('.dir-item');
            }
            clicked.toggleClass('collapsed');
        }
    };
}

if (typeof module !== 'undefined') {
    module.exports = solve;
}
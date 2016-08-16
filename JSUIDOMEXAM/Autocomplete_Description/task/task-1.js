/* globals document, window, console */

function solve() {
    return function (selector, initialSuggestions) {
        if (typeof selector !== 'string') {
            return;
        }

        var root = document.querySelector(selector);

        if (!root) {
            return;
        }

        var suggestionsList = root.querySelector('.suggestions-list');

        var templateSuggestion = document.createElement('li');
        templateSuggestion.className += ' suggestion';
        var templateSuggestionLink = document.createElement('a');
        templateSuggestionLink.className += ' suggestion-link';
        templateSuggestionLink.href = '#';
        templateSuggestion.appendChild(templateSuggestionLink);
        templateSuggestion.style.display = 'none';

        var fragment = document.createDocumentFragment();
        if (Array.isArray(initialSuggestions) && initialSuggestions.length > 0) {
            // uniqueArray = initialSuggestions.filter(function (item, pos) {
            //     return initialSuggestions.indexOf(item) === pos;
            // });
            // debugger;
            var lowercase = [];
            initialSuggestions.forEach(function (el, index) {
                lowercase.push(el.toLowerCase());
            });

            uniqueArray = initialSuggestions.filter(function (item, pos, self) {
                var loweritem = item.toLowerCase();

                return lowercase.indexOf(loweritem) == pos;
            });

            uniqueArray.forEach(function (el, i) {
                templateSuggestionLink.innerHTML = el;
                var newSuggestion = templateSuggestion.cloneNode(true);
                fragment.appendChild(newSuggestion);
            });
        }
        suggestionsList.appendChild(fragment);

        // ADD 
        var addButton = root.querySelector('.btn-add');
        var addInput = root.querySelector('.tb-pattern');
        addButton.addEventListener('click', function () {
            var text = addInput.value;
            addInput.value = '';

            // CHEKC IF ALREADY EXISTS
            var exists = false;
            [].forEach.call(allSuggestions, function (element) {
                var content = element.querySelector('a').innerHTML.toLowerCase();
                if (content === text.toLowerCase()) {
                    exists = true;
                }
            });
            if (exists) {
                return;
            }

            templateSuggestionLink.innerHTML = text;
            var newSuggestion = templateSuggestion.cloneNode(true);
            suggestionsList.appendChild(newSuggestion);
        });

        var allSuggestions = document.getElementsByClassName('suggestion');
        addInput.addEventListener('input', updateSuggestions);

        function updateSuggestions() {
            var query = addInput.value.toLowerCase();

            for (var i = 0, len = allSuggestions.length; i < len; i += 1) {
                var content = allSuggestions[i].querySelector('a').innerHTML.toLowerCase();

                if (query === '') {
                    allSuggestions[i].style.display = 'none';
                    continue;
                }

                if (content.indexOf(query) < 0) {
                    allSuggestions[i].style.display = 'none';
                } else {
                    allSuggestions[i].style.display = '';
                }
            }
        }

        suggestionsList.addEventListener('click', autocomplete);

        function autocomplete(event) {
            var clicked = event.target;

            while (clicked.parentNode && clicked.className.indexOf('suggestion') < 0) {
                clicked = clicked.parentNode;
            }
            if (clicked.className.indexOf('suggestion-link') >= 0) {
                clicked = clicked.parentNode;
            }
            addInput.value = clicked.querySelector('.suggestion-link').innerHTML;
            updateSuggestions();

            // needed ?
            [].forEach.call(allSuggestions, function (el) {
                el.style.display = 'none';
            });
        }
    };
}

module.exports = solve;
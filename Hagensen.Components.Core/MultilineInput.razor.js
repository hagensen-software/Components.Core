export function initialize(element) {
    element.addEventListener('mousedown', function (event) { selectAll = false; })
    element.addEventListener('blur', function (event) { selectAll = true; window.getSelection().empty(); })
    element.addEventListener('focus', function (event) { if (selectAll) window.getSelection().selectAllChildren(element); })
}

export function getInnerHTML(element) {
    return element.getInnerHTML()
}

export function select(element) {
    element.focus();
}

var selectAll = true;
export function initialize(element) {
    modalElement = element;
    element.addEventListener("keydown", (e) => {
        const event = new CustomEvent('modalkeydown', {
            bubbles: true,
            detail: e.key
        });
        element.dispatchEvent(event);

        if (e.key == "Tab") {
            if (e.shiftKey) {
                focusPrevious(e.target);
            } else {
                focusNext(e.target)
            }
            e.preventDefault();
        }
    });

}

export function rememberActiveElement() {
    activeElement = document.activeElement;
}

export function restoreActiveElement() {
    restoreFocus(activeElement);
}

export function focusFirstChildElement(element) {
    if (element == null)
        return;
    return focusFirst(element.firstElementChild);
}


function restoreFocus(element) {
    if (element == null)
        return;
    element.focus();
    if (document.activeElement != element)
        restoreFocus(element.parentElement);
}

function focusFirst(element) {
    if (element == null) {
        console.log('focusable not found')
        return false;
    }
    element.focus();
    if (document.activeElement == element) {
        console.log(element.outerHTML);
        return true;
    }
    if (focusFirst(element.firstElementChild)) {
        return true;
    }
    return focusFirst(element.nextElementSibling);
}

function focusNext(element) {
    if ((element == null) || (element == modalElement)) {
        return false;
    }
    var nextElement = element.firstElementChild;
    if (nextElement == null) {
        nextElement = element.nextElementSibling;
    }
    if (nextElement == null) {
        var parent = element.parentElement;
        while (parent.nextElementSibling == null) {
            if (parent == modalElement)
                break;
            parent = parent.parentElement;
        }
        if (parent != modalElement) {
            nextElement = parent.nextElementSibling;
        }
    }
    if (nextElement == null) {
        nextElement = element.parentElement.nextElementSibling
    }
    if (nextElement == null) {
        return false;
    }
    if (nextElement == modalElement)
        return true;
    nextElement.focus();
    if (document.activeElement == nextElement) {
        return true;
    }
    return focusNext(nextElement);
}

function focusPrevious(element) {
    if ((element == null) || (element == modalElement)) {
        return false;
    }

    var prevElement = element.previousElementSibling;
    if (prevElement == null) {
        var parent = element.parentElement;
        while (parent.previousElementSibling == null) {
            if (parent == modalElement)
                break;
            parent = parent.parentElement;
        }
        if (parent != modalElement) {
            parent.focus();
            if (document.activeElement == parent) {
                return true;
            }
            prevElement = parent.previousElementSibling;
        } else {
            return false;
        }
    }
    if (prevElement != null) {
        var lastChild = prevElement;
        while (lastChild.lastElementChild != null) {
            lastChild = lastChild.lastElementChild;
        }
        prevElement = lastChild;
    }
    prevElement.focus();
    if (document.activeElement == prevElement) {
        return true;
    }
    return focusPrevious(prevElement);
}


var modalElement = null;
var activeElement = null;
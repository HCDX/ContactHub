var app_name = "ContactHub";

function isVisible(e) {
    //returns true is should be visible to user.
    if (typeof e == "string") {
        e = xGetElementById(e);
        if (e == null) { return false; }
    }
    while (e.nodeName.toLowerCase() != 'body' && e.style.display.toLowerCase() != 'none' && e.style.visibility.toLowerCase() != 'hidden') {
        e = e.parentNode;
    }
    if (e.nodeName.toLowerCase() == 'body') {
        return true;
    } else {
        return false;
    }
}

function xGetElementById(e) {
    if (typeof (e) == 'string') {
        if (document.getElementById) e = document.getElementById(e);
        else if (document.all) e = document.all[e];
        else e = null;
    }
    return e;
}

     
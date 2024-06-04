

function updateMerch(elid) {
    document.getElementById("" + elid).submit();
}

function deleteMerch(elid) {

}

function addMerch() {

}

function makeHandlers(elid) {
    let opts = document.getElementsByClassName("merchOpts " + elid);
    for (let i = 0; i < opts.length; i++) {
        opts[i].onchange = updateMerch(elid);
    }
}
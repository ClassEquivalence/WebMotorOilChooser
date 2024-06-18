

function updateMerch(elid) {
    let formData = new FormData(document.getElementById("" + elid));
    fetch(PathPut,
        {
            method: 'PUT',
            body: formData
        });
}

function deleteMerch(elid) {
    fetch(PathDelete + elid,
        {
            method: 'DELETE',
            body: {
                id: elid
            }
        });
    document.getElementById("" + elid).remove();
}

async function addMerch() {
    let response = await fetch(PathCreate,
        {
            method: 'GET'
        });
    let string = await response.text();
    //let t = await json;
    //document.getElementById("merchesGlobalContainer").append(string);
    document.getElementById("merchesGlobalContainer").innerHTML += string;
    globalMakeHandlers();
}

function makeHandlers(elid) {
    let opts = document.getElementsByClassName("merchOpts " + elid);
    for (let i = 0; i < opts.length; i++) {
        opts[i].onchange = function () { updateMerch(elid); }
    }
    let sstr = "del " + elid;
    console.log(sstr);
    document.getElementById("del " + elid).onclick = function () { deleteMerch(elid); }
}

function globalMakeHandlers() {
    let merchForms = document.getElementsByClassName("merchForm");
    for (let i = 0; i < merchForms.length; i++) {
        makeHandlers(merchForms[i].id);

    }
}


globalMakeHandlers();
document.getElementById("addEl").onclick = function () { addMerch(); }
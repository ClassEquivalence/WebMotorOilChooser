function fetchUpdateForm(path, Form) {
    let formData = new FormData(Form);
    fetch(path,
        {
            method: 'PUT',
            body: formData
        });
}

function fetchDeleteForm(path, objId) {
    fetch(path + "/" + objId,
        {
            method: 'DELETE',
            body: {
                id: objId
            }
        });
}

async function fetchAddForm(path) {
    let response = await fetch(path,
        {
            method: 'POST'
        });
    return await response.text();
}

async function fetchAddForm(path, olderElId) {
    let fd = new FormData();
    fd.append("id", olderElId);
    let response = await fetch(path,
        {
            method: 'POST',
            body: fd
        });
    return await response.text();
}

function makeInputEventsByForm(path, form) {
    let inputs = form.getElementsByTagName("INPUT");
    let selects = form.getElementsByTagName("SELECT");
    let buttons = form.getElementsByTagName("BUTTON");
    for (let i = 0; i < inputs.length; i++) {
        inputs[i].oninput = () => { fetchUpdateForm(path, form); }
        //console.log("inputs " + inputs[i]);
    }
    for (let i = 0; i < selects.length; i++) {
        selects[i].oninput = () => { fetchUpdateForm(path, form); }
        //console.log("selects " + selects[i]);
    }
    for (let i = 0; i < buttons.length; i++) {
        buttons[i].onclick = () => { fetchUpdateForm(path, form); }
        //console.log("buttons " + buttons[i]);
    }
}
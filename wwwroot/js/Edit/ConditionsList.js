//OilTypeCondClass @cond.id
//SAECondClass @cond.id
//APICondClass @cond.id
//указать paths для addConds

async function addCondSet() {
    let response = await fetch(addCondSetPath);
    let resptext = await response.text();
    let condSetEl = document.getElementById("condsGlobalContainer");
    condSetEl.innerHTML += resptext;
}

async function addAPICond(condSetId) {
    let fd = new FormData();
    fd.append("id", condSetId);
    let response = await fetch(addAPICondPath,
        {
            method: 'POST',
            body: fd
        });
    let condAPIEl = document.getElementById("APICondClass " + condSetId);
    condAPIEl.innerHTML += await response.text();


}

async function addSAECond(condSetId) {
    let fd = new FormData();
    fd.append("id", condSetId);
    let response = await fetch(addSAECondPath,
        {
            method: 'POST',
            body: fd
        });
    let condSAEEl = document.getElementById("SAECondClass " + condSetId);
    condSAEEl.innerHTML += await response.text();
}

async function addOilCond(condSetId) {
    let fd = new FormData();
    fd.append("id", condSetId);
    let response = await fetch(addOilCondPath,
        {
            method: 'POST',
            body: fd
        });
    let condOilEl = document.getElementById("OilCondClass " + condSetId);
    condOilEl.innerHTML += await response.text();
}

function makeAPICondEvents(condunitId) {
    let condForm = document.getElementById("APICondClassForm " + condunitId);
    let cn = condForm.childNodes;
    for (let i = 0; i < cn.length; i++) {
        if (cn[i].nodeName == "INPUT" || cn[i].nodeName=="BUTTON") {
            cn[i].oninput = () => {
                let fd = new FormData(condForm);
                fetch(putAPICondPath,
                    {
                        method: 'PUT',
                        body: fd
                    });
            }
        }
    }
}

function makeSAECondEvents(condunitId) {
    let condForm = document.getElementById("SAECondClassForm " + condunitId);
    let cn = condForm.childNodes;
    for (let i = 0; i < cn.length; i++) {
        if (cn[i].nodeName == "INPUT" || cn[i].nodeName == "BUTTON") {
            cn[i].oninput = () => {
                let fd = new FormData(condForm);
                fetch(putSAECondPath,
                    {
                        method: 'PUT',
                        body: fd
                    });
            }
        }
    }
}

function makeOilCondEvents(condunitId) {
    let condForm = document.getElementById("OilCondClassForm " + condunitId);
    let inputs = condForm.getElementsByTagName("INPUT");
    let buttonInputs = condForm.getElementsByTagName("BUTTON");
    for (let i = 0; i < inputs.length; i++) {
        inputs[i].oninput = () => {
            let fd = new FormData(condForm);
            fetch(putOilCondPath,
                {
                    method: 'PUT',
                    body: fd
                });
        }
    }
    for (let i = 0; i < buttonInputs.length; i++) {
        buttonInputs[i].oninput = () => {
            let fd = new FormData(condForm);
            fetch(putOilCondPath,
                {
                    method: 'PUT',
                    body: fd
                });
        }
    }
}


function makeCondSetEvents(condSetId) {
    JSON.
}
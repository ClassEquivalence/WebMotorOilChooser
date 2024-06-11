//OilTypeCondClass @cond.id
//SAECondClass @cond.id
//APICondClass @cond.id
//указать paths для addConds

let addCondSetPath="/Edit/addCondSet";

let addOilCondPath ="/Edit/addOilCond";
let addSAECondPath ="/Edit/addSAECond";
let addAPICondPath ="/Edit/addAPICond";

let putOilCondPath ="/Edit/putOilCond";
let putSAECondPath ="/Edit/putSAECond";
let putAPICondPath ="/Edit/putAPICond";

let delCondSetPath ="/Edit/delCondSet";

let delOilCondPath ="/Edit/delOilCond";
let delSAECondPath ="/Edit/delSAECond";
let delAPICondPath ="/Edit/delAPICond";


async function addCondSet() {
    let response = await fetch(addCondSetPath);
    let resptext = await response.text();
    let condSetEl = document.getElementById("condsGlobalContainer");
    condSetEl.innerHTML += resptext;

    let condSetIdsList = getCondSetIds();
    for (let i = 0; i < condSetIdsList.length; i++) {
        makeCondSetEvents(condSetIdsList[i]);
    }
}

async function addCond(condSetId, addCondPath, putCondPath, delCondPath, condType) {
    let fd = new FormData();
    fd.append("id", condSetId);
    let response = await fetch(addCondPath,
        {
            method: 'POST',
            body: fd
        });
    let condEl = document.getElementById(condType + "Class " + condSetId);
    let resptext = await response.text();
    condEl.innerHTML += resptext;

    let condSetIdsList = getCondSetIds();
    for (let i = 0; i < condSetIdsList.length; i++) {
        makeCondSetEvents(condSetIdsList[i]);
    }
}

function getCondSetIds() {
    let l = [];
    let elements = document.getElementsByClassName("condset");
    for (let i = 0; i < elements.length; i++) {
        l.push(elements[i].dataset.condsetid);
    }
    return l;
}

function getCondIds(condSetId, condType) {
    let l = [];
    let csContlist = document.getElementsByClassName("condset");
    let csCont;
    for (let i = 0; i < csContlist.length; i++) {
        if (csContlist[i].dataset.condsetid == condSetId) {
            csCont = csContlist[i];
            break;
        }
    }
    let condsCont = csCont.getElementsByClassName("condclass");
    let cCont;
    for (let i = 0; i < condsCont.length; i++) {
        if (condsCont[i].dataset.condtype == condType) {
            cCont = condsCont[i];
            break;
        }
    }
    let formsWithIds = cCont.getElementsByClassName("condunit");

    for (let i = 0; i < formsWithIds.length; i++) {
        l.push(formsWithIds[i].dataset.condid);
    }
    return l;
}


function makeCondSetEvents(condSetId) {
    let APICondIdList = getCondIds(condSetId, "APICond");
    let OilCondIdList = getCondIds(condSetId, "OilCond");
    let SAECondIdList = getCondIds(condSetId, "SAECond");
    for (let i = 0; i < APICondIdList.length; i++) {
        makeCondEvents("APICond", APICondIdList[i], putAPICondPath, delAPICondPath);
    }
    for (let i = 0; i < OilCondIdList.length; i++) {
        makeCondEvents("OilCond", OilCondIdList[i], putOilCondPath, delOilCondPath);
    }
    for (let i = 0; i < SAECondIdList.length; i++) {
        makeCondEvents("SAECond", SAECondIdList[i], putSAECondPath, delSAECondPath);
    }

    let apibtnlist = document.getElementsByClassName("AddAPICond");
    let apibtn;
    for (let i = 0; i < apibtnlist.length; i++) {
        if (apibtnlist[i].value == condSetId)
            apibtn = apibtnlist[i];
    }
    apibtn.onclick = () => {
        addCond(condSetId, addAPICondPath, putAPICondPath, delAPICondPath, "APICond");
    }

    let oilbtnlist = document.getElementsByClassName("AddOilCond");
    let oilbtn;
    for (let i = 0; i < oilbtnlist.length; i++) {
        if (oilbtnlist[i].value == condSetId)
            oilbtn = oilbtnlist[i];
    }
    oilbtn.onclick = () => {
        addCond(condSetId, addOilCondPath, putOilCondPath, delOilCondPath, "OilCond");
    }

    let saebtnlist = document.getElementsByClassName("AddSAECond");
    let saebtn;
    for (let i = 0; i < saebtnlist.length; i++) {
        if (saebtnlist[i].value == condSetId)
            saebtn = saebtnlist[i];
    }
    saebtn.onclick = () => {
        addCond(condSetId, addSAECondPath, putSAECondPath, delSAECondPath, "SAECond");
    }

    let delbtns = document.getElementsByName("delCondSet");
    let delbtn;
    for (let i = 0; i < delbtns.length; i++) {
        if (delbtns[i].value == condSetId)
            delbtn = delbtns[i];
    }
    delbtn.onclick = () => {
        let fd = new FormData();
        fd.append("id", condSetId);
        fetch(delCondSetPath,
            {
                method: 'DELETE',
                body: fd
            });

        let condsets = document.getElementsByClassName("condset");
        for (let i = 0; i < condsets.length; i++) {
            if (condsets[i].dataset.condsetid == condSetId) {
                condsets[i].remove();
                break;
            }
        }
    }
}

function makeCondEvents(condType, condId, putpath, delpath) {
    let formelmts = document.getElementsByTagName("FORM");
    let form;
    for (let i = 0; i < formelmts.length; i++) {
        if (formelmts[i].dataset.condtype == condType &&
            formelmts[i].dataset.condid == condId) {
            form = formelmts[i];
            break;
        }
    }

    let inputs = form.getElementsByTagName("INPUT");
    let buttonInputs = form.getElementsByTagName("BUTTON");
    let selectInputs = form.getElementsByTagName("SELECT");
    for (let i = 0; i < inputs.length; i++) {
        inputs[i].oninput = () => {
            let fd = new FormData(form);
            fetch(putpath,
                {
                    method: 'PUT',
                    body: fd
                });
        }
    }
    for (let i = 0; i < buttonInputs.length; i++) {
        buttonInputs[i].onclick = () => {
            let fd = new FormData(form);
            fetch(putpath,
                {
                    method: 'PUT',
                    body: fd
                });
        }
    }
    for (let i = 0; i < selectInputs.length; i++) {
        selectInputs[i].oninput = () => {
            let fd = new FormData(form);
            fetch(putpath,
                {
                    method: 'PUT',
                    body: fd
                });
        }
    }

    let delbtns = document.getElementsByName("delCond");
    let delbtn;
    for (let i = 0; i < delbtns.length; i++) {
        if (delbtns[i].dataset.condtype == condType
            && delbtns[i].value == condId)
            delbtn = delbtns[i];
    }
    delbtn.onclick = () => { delCond(condType, condId, delpath) };
}

function delCond(condType, condId, delPath) {
    let fd = new FormData();
    fd.append("id", condId);
    fetch(delPath,
        {
            method: 'DELETE',
            body: fd
        });
    let formelmts = document.getElementsByClassName("condunit");
    let form;
    for (let i = 0; i < formelmts.length; i++) {
        if (formelmts[i].dataset.condtype == condType
            && formelmts[i].dataset.condid == condId)
            form = formelmts[i];
    }
    let btns = document.getElementsByName("delCond");
    let btn;
    for (let i = 0; i < btns.length; i++) {
        if (btns[i].dataset.condtype == condType
            && btns[i].value == condId)
            btn = btns[i];
    }
    form.remove();
    btn.remove();
}

let condSetIdsList = getCondSetIds();

for (let i = 0; i < condSetIdsList.length; i++) {
    makeCondSetEvents(condSetIdsList[i]);
}

document.getElementById("addSet").onclick = addCondSet;


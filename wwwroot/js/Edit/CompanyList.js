/*

fetchUpdateForm(path, Form)
fetchDeleteForm(path, objId)
fetchAddForm(path)
fetchAddForm(path, olderElId)
makeInputEventsByForm(path, form)

*/

let addCompanyPath = "AddCompany";
let addStorePath = "AddStore";
let delCompanyPath = "DelCompany";
let delStorePath = "DelStore";
let updCompanyPath = "UpdCompany";
let updStorePath = "UpdStore";


function makeEvents() {
    var companyforms = document.getElementsByClassName("companyForm");
    for (let i = 0; i < companyforms.length; i++) {
        makeInputEventsByForm(updCompanyPath, companyforms[i]);
        console.log(companyforms[i]);
    }
    var storeforms = document.getElementsByClassName("companyStore");
    for (let i = 0; i < storeforms.length; i++) {
        makeInputEventsByForm(updStorePath, storeforms[i]);
        console.log(storeforms[i]);
    }

    var addStoreBtns = document.getElementsByClassName("addStoreBtn");
    for (let i = 0; i < addStoreBtns.length; i++) {
        addStoreBtns[i].onclick = () =>
        { addStore(addStoreBtns[i].dataset.companyid); }
    }
    
    var delCompanyBtns = document.getElementsByClassName("delCompanyBtn");
    var delStoreBtns = document.getElementsByClassName("delStoreBtn");
    for (let i = 0; i < delCompanyBtns.length; i++) {
        delCompanyBtns[i].onclick = () => {
            delCompany(delCompanyBtns[i].dataset.companyid);
        }
       // console.log("dcb: " + delCompanyBtns[i]);
    }
    for (let i = 0; i < delStoreBtns.length; i++) {
        delStoreBtns[i].onclick = () => {
            delStore(delStoreBtns[i].dataset.storeid);
        }
       // console.log("dsb: " + delStoreBtns[i]);
    }

    var addCompanyBtn = document.getElementById("addCompanyBtn");
    addCompanyBtn.onclick = addCompany;
}

async function addCompany() {
    let globalcont = document.getElementsByClassName("companies")[0];
    let resptext = await fetchAddForm(addCompanyPath);
    globalcont.innerHTML += resptext;
    makeEvents();
}

async function addStore(companyId) {
    let companyCont = document.getElementById("company " + companyId);
    let resptext = await fetchAddForm(addStorePath, companyId);
    let storesCont = companyCont.getElementsByClassName("stores")[0];
    storesCont.innerHTML += resptext;
    makeEvents();
}

function delCompany(companyId) {
    fetchDeleteForm(delCompanyPath, companyId);
    document.getElementById("company " + companyId).remove();
    makeEvents();
}

function delStore(storeId) {
    fetchDeleteForm(delStorePath, storeId);
    document.getElementById("store " + storeId).remove();
    document.getElementById("storeDelBtn " + storeId).remove();
    makeEvents();
}

function updCompany(companyId) {
    let form = document.getElementById("company " + companyId);
    fetchUpdateForm(updCompanyPath, form);
}

function updStore(storeId) {
    let form = document.getElementById("store " + storeId);
    fetchUpdateForm(updStorePath, form);
}


makeEvents();
/*

fetchUpdateForm(path, Form)
fetchDeleteForm(path, objId)
fetchAddForm(path)
fetchAddForm(path, olderElId)
makeInputEventsByForm(path, form)

*/

let updCompanyPath = "coUpdCompany";

let addStorePath = "coAddStore";
let delStorePath = "coDelStore";
let updStorePath = "coUpdStore";


function makeEvents() {
    var companyForm = document.getElementById("companyForm");
    makeInputEventsByForm(updCompanyPath, companyForm);

    var storeforms = document.getElementsByClassName("companyStore");
    for (let i = 0; i < storeforms.length; i++) {
        makeInputEventsByForm(updStorePath, storeforms[i]);
        //console.log(storeforms[i]);
    }

    var addStoreBtn = document.getElementById("addStoreBtn");
    addStoreBtn.onclick = addStore;
    
    var delStoreBtns = document.getElementsByClassName("delStoreBtn");
    for (let i = 0; i < delStoreBtns.length; i++) {
        delStoreBtns[i].onclick = () => {
            delStore(delStoreBtns[i].dataset.storeid);
        }
       // console.log("dsb: " + delStoreBtns[i]);
    }
}


async function addStore() {
    let resptext = await fetchAddForm(addStorePath);
    let storesCont = document.getElementsByClassName("stores")[0];
    storesCont.innerHTML += resptext;
    makeEvents();
}

function delStore(storeId) {
    fetchDeleteForm(delStorePath, storeId);
    document.getElementById("storeCont " + storeId).remove();
    makeEvents();
}

function updStore(storeId) {
    let form = document.getElementById("store " + storeId);
    fetchUpdateForm(updStorePath, form);
}

function updCompany() {
    let form = document.getElementById("companyForm");
    fetchUpdateForm(updCompanyPath, form);
}


makeEvents();
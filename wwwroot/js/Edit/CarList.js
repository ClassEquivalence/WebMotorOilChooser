/*

fetchUpdateForm(path, Form)
fetchDeleteForm(path, objId)
fetchAddForm(path)
fetchAddForm(path, olderElId)
makeInputEventsByForm(path, form)

*/

let addCarPath = "AddCar";
let delCarPath = "DelCar";
let updCarPath = "UpdCar";


function makeEvents() {
    var carforms = document.getElementsByClassName("carForm");
    for (let i = 0; i < carforms.length; i++) {
        makeInputEventsByForm(updCarPath, carforms[i]);
        //console.log(companyforms[i]);
    }

    
    var delCarBtns = document.getElementsByClassName("delCarBtn");
    for (let i = 0; i < delCarBtns.length; i++) {
        delCarBtns[i].onclick = () => {
            delCar(delCarBtns[i].dataset.carid);
        }
    }

    var addCarBtn = document.getElementById("addCarBtn");
    addCarBtn.onclick = addCar;
}

async function addCar() {
    let globalcont = document.getElementsByClassName("cars")[0];
    let resptext = await fetchAddForm(addCarPath);
    globalcont.innerHTML += resptext;
    makeEvents();
}


function delCar(carId) {
    fetchDeleteForm(delCarPath, carId);
    document.getElementById("car " + carId).remove();
    makeEvents();
}


function updCar(carId) {
    let form = document.getElementById("carForm " + carId);
    fetchUpdateForm(updCompanyPath, form);
    makeEvents();
}

makeEvents();
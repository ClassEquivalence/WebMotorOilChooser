let elements = document.getElementsByClassName("filterinput");

async function call_grid_update() {
    console.log("emited");
    let formData = new FormData(document.getElementById("FiltersForm"));

    let response = await fetch(fpath,
        {
            method: 'POST',
            body: formData
        });
    let string = await response.text();
    //let t = await json;
    document.getElementById("oilstable").innerHTML = string;
    
}


for (let i = 0; i < elements.length; i++) {
    console.log("lol");
    console.log(elements[i]);
    elements.item(i).oninput = call_grid_update;
    //document.getElementById("FiltersForm").
}
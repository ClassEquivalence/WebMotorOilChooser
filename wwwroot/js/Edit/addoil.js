
document.getElementById("oilImgInput").oninput = DrawImage;
function DrawImage() {
    document.getElementById("imgEditContainer").hidden = false;
    document.getElementById("imgScale").hidden = false;
    let image = document.getElementById("oilImgInput");
    image.files.item(0);
    let img = new Image();
    img.src = URL.createObjectURL(image.files.item(0));
    console.log("inputted");
    console.log(img.src);
    document.getElementById("img").src = img.src;
}

var pos1, pos2, pos3, pos4, scale = 1;
var elmt = document.getElementById("img");
function dragImg(e) {
    pos1 = pos3 - e.clientX;
    pos2 = pos4 - e.clientY;
    pos3 = e.clientX;
    pos4 = e.clientY;
    // set the element's new position:
    elmt.style.top = (elmt.offsetTop - pos2) + "px";
    elmt.style.left = (elmt.offsetLeft - pos1) + "px";
}

document.getElementById("img").onmousedown = prepareToMove;

function prepareToMove(e) {
    elmt.onmousemove = dragImg;
    pos3 = e.clientX;
    pos4 = e.clientY;
    elmt.onmouseup = closeDragElement;
    // call a function whenever the cursor moves:
    elmt.onmousemove = dragImg;
}
function closeDragElement() {
    // stop moving when mouse button is released:
    elmt.onmouseup = null;
    elmt.onmousemove = null;
}

function setScale() {
    elmt.style.scale = Math.log10(document.getElementById("imgScale").value);
}
document.getElementById("imgScale").onchange = setScale;

function submitForm() {
    //<input name="imgx" hidden>
    //    <input name="imgy" hidden>
    //    <input name="imgscale" hidden>
    console.log("sdfasdf");
    document.getElementById("imgScale").value = elmt.style.scale;
    document.getElementById("imgx").value = elmt.style.left;
    document.getElementById("imgy").value = elmt.style.top;
    document.getElementById("oilForm").submit();
}

document.getElementById("sendData").onclick = submitForm;

if (editOilState) {
    document.getElementById("imgEditContainer").hidden = false;
    document.getElementById("imgScale").hidden = false;

    document.getElementById("img").setAttribute("src", editOilImgSrc);
}


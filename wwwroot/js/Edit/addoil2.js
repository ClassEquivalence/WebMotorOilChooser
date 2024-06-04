var el = document.getElementById('imgEditContainer');
var vanilla = new Croppie(el, {
    viewport: { width: 320, height: 400 },
    boundary: { width: 500, height: 500 },
    showZoomer: true,
});

document.getElementById('oilImgInput').oninput = ()=>{
    let image = document.getElementById("oilImgInput");
    //let img = new Image();
    //img.src = URL.createObjectURL(image.files.item(0));
    document.getElementById("imgEditContainer").hidden = false;

    vanilla.bind({
    url: URL.createObjectURL(image.files.item(0))
});

}


//on button click
document.getElementById('sendData').onclick = () => {
    vanilla.result('blob', 'viewport', 'jpeg').then(function (blob) {
        // do something with cropped blob
        let f2 = new File([blob], "File.jpeg");
        let fl = new DataTransfer();
        fl.items.add(f2);
        document.getElementById("oilImgInput").files = fl.files;
        document.getElementById("oilForm").submit();
    });
}

if (editOilState) {
    document.getElementById("imgEditContainer").hidden = false;

    vanilla.bind({
        url: editOilImgSrc
    });
}
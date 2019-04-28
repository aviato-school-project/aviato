var xhr;
if (window.XMLHttpRequest) {
    xhr = new XMLHttpRequest();
}
if (xhr == null) {
    console.log("Your browser does not support XMLHTTP!");
}

xhr.open("GET", "../Home/SkupiLetove");
xhr.send(null);

let vremena = [];
var sadrzaj;

(xhr.onreadystatechange = function () {
    if (xhr.status === 200 && xhr.readyState === 4) {
        let xhrt = xhr.responseText;
        conParser(xhrt);
    }
})();

function conParser(xhrt) {
    sadrzaj = JSON.parse(xhrt);
    popuniKalendar(sadrzaj);
};

function popuniKalendar(sadrzaj) {
    for (let c in sadrzaj) {
        let vreme = new Date(Number(sadrzaj[c].VremePoletanja.substring(6, 19)))
        let letovi = {
            title: sadrzaj[c].LetId,
            start: vreme,
            allDay: true
        };
        vremena.push(letovi);
        ispisiKalendar(vremena);
    }
}

let //datumiUKalendaru = document.querySelectorAll('.fc-content'),
    datumZaPrikaz = document.querySelector('.idLeta'),
    kalendar = document.querySelector('.idLeta');

//kalendar.onclick = () => {
//    if (kalendar.class == "table") {
//        console.log("this is table")
//    }
//    else {
//        console.log("this is calendar")
//    }
//}
//for (let datum of datumiUKalendaru) {
//    datum.onclick = () => {
//        console.log(datum.innerHTML);
//    }
//}
window.onclick = (e) => {
    if (e.target.className == "fc-title") {
        datumZaPrikaz.value = e.target.innerHTML;
        console.log(datumZaPrikaz.value)
        console.log(e.target)
    }
    if (e.target.className == "fc-content") {
        datumZaPrikaz.value = e.target.innerText;
        console.log(datumZaPrikaz.value)
    }
}
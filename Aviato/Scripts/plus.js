var plus = document.querySelectorAll(".fa-plus-circle"),
    minus = document.querySelectorAll(".fa-minus-circle"),
    target = document.querySelector(".target"),  // staviti klasu na element koji treba da se umnožava, uklanja
    container = document.querySelector(".ovdedodaj"); // staviti klasu na kontejner elemenata koji se umnožavaju uklanjaju

for (let p of plus) {
    p.onclick = () => plusClick();
}
for (let m = 1; m < minus.length; m++) {
    minus[m].onclick = (e) => minusClick(e);
}

function plusClick() {
    let clone = target.cloneNode(true);
    container.appendChild(clone);
    clone.querySelector(".fa-plus-circle").onclick = () => plusClick();
    clone.querySelector(".fa-minus-circle").onclick = (e) => minusClick(e);
    insertCalendar();
};

function minusClick(e) {

    let zaUklanjanje = e.target.parentElement;

    if (e.target.parentElement.classList.contains("target")) {
        container.removeChild(zaUklanjanje);
    }
    else {
        nadjiMetu(zaUklanjanje)
    }
    function nadjiMetu(ukloni) {
        if (ukloni.classList.contains("target") || ukloni.class == "target") {
            container.removeChild(ukloni);
        }
        else {
            ukloni = ukloni.parentNode
            nadjiMetu(ukloni);
        }
    }
};
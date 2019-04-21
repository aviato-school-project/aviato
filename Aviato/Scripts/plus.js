var plus = document.querySelector(".fa-plus-circle"),
    minus = document.querySelector(".fa-minus-circle"),
    target = document.querySelector(".target"),  // staviti klassu na element koji treba da se umnožava, uklanja
    container = document.querySelector(".ovdedodaj"); // staviti klasu na kontejner elemenata koji se umnožavaju uklanjaju

plus.onclick = () => plusClick();
minus.onclick = () => minusClick();

function plusClick() {
    let clone = target.cloneNode(true);
    container.appendChild(clone);
    clone.querySelector(".fa-plus-circle").onclick = () => plusClick();
    clone.querySelector(".fa-minus-circle").onclick = (e) => minusClick(e);
};

function minusClick(e) {
    container.removeChild(e.target.parentElement);
};
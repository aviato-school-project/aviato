//@Model.JeziciIzJSa

function JeziciZaUnos() {
    var okidac = document.querySelector(".okidac"),
        pass = document.querySelector("#pass");
    let jeziciZaUnos = [];
    okidac.onclick = () => {
        let izabraniJezici = document.querySelectorAll(".izabraniJezik"); // Lista DOM nodova

        for (let jezik of izabraniJezici) {
            jeziciZaUnos.push(Number(jezik.value)); // Prepoznaje odabrani jezik u trenutno posmatranom select polju, i ubacuje ga u array
        }
        jeziciZaUnos = [...new Set(jeziciZaUnos)]; // Vraća samo jedinstvene jezike
        pass.value = jeziciZaUnos;
        return jeziciZaUnos;
    }
}

JeziciZaUnos();
let okidac = document.querySelector(".okidac");
var moguceVrednosti = []; // Lista sve jezike koje je moguće izabrati kao "Engleski"
window.onload = () => {
    /*
     * Pošto pri učitavanju podataka iz baze, 
     * vrednosti bivaju upisane u innerHTML input elementa
     * umesto u value, ova funkcija ih vraća u value
     */
    let polja = document.querySelectorAll('.polje'),
        datumi = document.querySelectorAll('.datum');
    let stvarneVrednosti = [];
    if (document.URL.split('/')[3] !== "Let") {
        let node = polja[0].options; // iščitava iz bilo kog polja koji su sve jezici dostupni
        if (node != undefined) {
            for (let i = 1; i < node.length; i++) {
                moguceVrednosti.push(node[i].label);
            }

            for (let polje of polja) {
                let stvarnaVrednost = polje.options[0].innerHTML; // u innerHTML-u 0-tog rezultata je jezik koga baza vraća kao "Engleski"
                stvarneVrednosti.push(moguceVrednosti.indexOf(stvarnaVrednost) + 1); // pravi array stvarno odabranih jezika, kao njihove id-ijeve
                polje.value = moguceVrednosti.indexOf(stvarnaVrednost) + 1; // dodeljuje kao value stvarno izabran jezik
            }
        }
    }

    /*
     * Ovaj loop prikazuje samo datum, umesto datuma i vremena u datetime polju
     */
    
    for (let datum of datumi) {
        datum.value = datum.defaultValue.split(" ")[0];
    }
}

/**
 * Očitava vrednost kao primary key, npr 1, a vraća ljudski prepoznatljiv string,
 * kao npr "Engleski" ili "Sreten Dudić"
 * u svrhu obaveštavanja korisnika kad odabere dve iste vrednosti
 * kada će se na ekranu ispisati koja vrednost je uneta dva puta.
 */

function vratiPrave(val) {
    let pozicija,
        polje = document.querySelector('.polje').options;
    
    //if (document.URL.split('/')[3] == "Let" && document.URL.split('/')[4] == "Create") {
        
        
    //    console.log($0.textContent.split("\n"))
    //    let duplikat = polje.options[polje.options.selectedIndex].innerHTML;
    //    return duplikat
    //}
    //if (document.URL.split('/')[3] == "Let") {
    //    let polje = document.querySelectorAll('.polje');
    //    for (let p of polje) {
    //        if (p.value == val) {
    //            var duplikat = p.textContent.split('\n')[p.options.selectedIndex];
    //        }
    //    }
    //    return duplikat
    //}
    if (!document.querySelector('#RoleName') == null) {
        pozicija = document.querySelector('#RoleName').value;
    }
    if (pozicija == "Stjuard") {
        let duplikat = polje[val - 1].innerHTML;
        return duplikat;
    }
    else if (pozicija == "Mehaničar" || document.URL.split('/')[4] == "Edit") {
        let duplikat = polje[val].innerHTML;
        return duplikat;
    }
    //else if (document.URL.split('/')[4] == "Edit") {
    //    let duplikat = polje[val].innerHTML;
    //    return duplikat
    //}
}

var pass = document.querySelector("#pass"),
    tipovi = document.querySelector("#TipoviZaUnos"), // Ovde se upisuje array koji se šalje kontroleru
    datumi = document.querySelector("#DatumiZaUnos");
    
    okidac.onclick = () => {
        let poljaZaUnos = document.querySelectorAll('.polje'),
            jeziciZaUnos = [],
            tipoviZaUnos = [],
            datumiZaUnos = [];

        let izabraniTipovi = document.querySelectorAll(".izabranaLicenca"), // Lista DOM nodova sa tipovima
            izabraniDatumi = document.querySelectorAll(".izabraniDatum"),
            izabraniJezici = document.querySelectorAll(".izabraniJezik"); // Lista DOM nodova

        for (let jezik of izabraniJezici) {
            if (jezik.value == "") {
                let trenutniJezik = jezik.options[0].innerHTML; // String kao "Engleski" za jezik koji je baza izbacila [0]
                jeziciZaUnos.push(moguceVrednosti.indexOf(trenutniJezik) + 1)
            }
            else {
                jeziciZaUnos.push(Number(jezik.value)); // Prepoznaje odabrani jezik u trenutno posmatranom select polju, i ubacuje ga u array}
            }
        }

        for (let i = 0; i < izabraniTipovi.length; i++) {
            if (izabraniTipovi[i].value == 0 || izabraniDatumi[i].value == "") {
                continue;
            }
            else if (tipoviZaUnos.includes(Number(izabraniTipovi[i].value))) {
                continue;
            }
            
            else {
                tipoviZaUnos.push(Number(izabraniTipovi[i].value)); // Prepoznaje odabrani tip u trenutno posmatranom select polju, i ubacuje ga u array
                datumiZaUnos.push(izabraniDatumi[i].value);
            }
        }

        /*
         * Proverava da se ne unese ista vrednost dva puta
         */
        let test = [];
        test[0] = poljaZaUnos[0].value;
        for (let i = 1; i < poljaZaUnos.length; i++) {
            console.log(poljaZaUnos[i])
            if (test.includes(poljaZaUnos[i].value)) {
                let val = poljaZaUnos[i].value;
                alert(`Uneli ste ${vratiPrave(val)} više puta, proverite svoj unos`);
                return false;
            }
            else {
                test.push(poljaZaUnos[i].value);
            }
        }

        jeziciZaUnos = [...new Set(jeziciZaUnos)]; // Vraća samo jedinstvene jezike
        pass.value = jeziciZaUnos;

        tipovi.value = tipoviZaUnos;
        datumi.value = datumiZaUnos;
    }

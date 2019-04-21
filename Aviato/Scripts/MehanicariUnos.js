
function MehanicariZaUnos() {
    var okidac = document.querySelector(".mehanicariOkidac"),
        tipovi = document.querySelector("#TipoviZaUnos"), // Ovde se upisuje array koji se šalje kontroleru
        datumi = document.querySelector("#DatumiZaUnos");
    
    okidac.onclick = () => {
        let tipoviZaUnos = [],
            datumiZaUnos = [];

        let izabraniTipovi = document.querySelectorAll(".izabranaLicenca"), // Lista DOM nodova sa tipovima
            izabraniDatumi = document.querySelectorAll(".izabraniDatum");

        for (let i = 0; i < izabraniTipovi.length; i++) {
            if (izabraniTipovi[i].value == 0 || izabraniDatumi[i].value == "") {
                continue;
            }
            else if (tipoviZaUnos.includes(Number(izabraniTipovi[i].value))) {
                continue;
            }
            //else if (tipoviZaUnos == []) {
            //    tipoviZaUnos.push(tipZaUnos);
            //}
            else {
                tipoviZaUnos.push(Number(izabraniTipovi[i].value)); // Prepoznaje odabrani tip u trenutno posmatranom select polju, i ubacuje ga u array
                datumiZaUnos.push(izabraniDatumi[i].value);
            }
        }
        tipovi.value = tipoviZaUnos;
        datumi.value = datumiZaUnos;
        console.log(tipoviZaUnos);
        console.log(datumiZaUnos);
        //return tipoviZaUnos;
    }
}

MehanicariZaUnos();
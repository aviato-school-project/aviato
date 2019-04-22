//@Model.JeziciIzJSa

function JeziciZaUnos() {
    var okidac = document.querySelector(".okidac"),
        pass = document.querySelector("#pass");
    
    okidac.onclick = () => {
        let jeziciZaUnos = [];
        let izabraniJezici = document.querySelectorAll(".izabraniJezik"); // Lista DOM nodova
        let moguciJezici = [], // Lista sve jezike koje je moguće izabrati kao "Engleski"
            nodeJezika = izabraniJezici[1].options;
        for (let i = 1; i < nodeJezika.length; i++) {
            moguciJezici.push(nodeJezika[i].label);
        }
        // for prolazi kroz padajuću listu i skuplja sve jezike koje je moguće izabrati
        // let test = izabraniJezici[1].options
        // console.log(moguciJezici); 
        // console.log(test[0].label);

        for (let jezik of izabraniJezici) {
            if (jezik.value == "") {
                let trenutniJezik = jezik.options[0].innerHTML; // String kao "Engleski" za jezik koji je baza izbacila [0]
                jeziciZaUnos.push(moguciJezici.indexOf(trenutniJezik) + 1)
            }
            else {
                jeziciZaUnos.push(Number(jezik.value)); // Prepoznaje odabrani jezik u trenutno posmatranom select polju, i ubacuje ga u array}
            }
        }
        jeziciZaUnos = [...new Set(jeziciZaUnos)]; // Vraća samo jedinstvene jezike
        pass.value = jeziciZaUnos;
        return jeziciZaUnos;
    }
}

JeziciZaUnos();
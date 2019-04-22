let dodatak = document.querySelector(".dodatniPodaciZaposlenog");
var strana = document.URL.split('/');
console.log(strana[3])
console.log(strana[4])

function UnosZaposlenog(vrednost) {
    switch (vrednost) {
        case "Pilot":
            $(".dodatniPodaciZaposlenog").load(`/${vrednost}/Create`);
            break;
        case "Mehaničar":
            $(".dodatniPodaciZaposlenog").load(`/Mehanicar/Create`);
            break;
        case "Stjuard":
            $(".dodatniPodaciZaposlenog").load(`/${vrednost}/Create`);
            break;
        default:
            dodatak.innerHTML = " ";
    }
}


    


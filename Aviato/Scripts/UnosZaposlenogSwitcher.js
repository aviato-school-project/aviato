let dodatak = document.querySelector(".dodatniPodaciZaposlenog");

function UnosZaposlenog(vrednost) {
    switch (vrednost) {
        case "Pilot":
            $(".dodatniPodaciZaposlenog").load("/Pilot/Create");
            break;
        case "Mehaničar":
            $(".dodatniPodaciZaposlenog").load("/Mehanicar/Create");
            break;
        case "Stjuard":
            $(".dodatniPodaciZaposlenog").load("/Stjuard/Create");
            break;
        default:
            dodatak.innerHTML = " ";
    }
}


    


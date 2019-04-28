$("input[required], select[required]").attr("oninvalid", "this.setCustomValidity('Molimo popunite ovo polje')");
$("input[required], select[required]").attr("oninput", "setCustomValidity('')");
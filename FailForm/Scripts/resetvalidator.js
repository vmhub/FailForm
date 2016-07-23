$("#form").data("unobtrusiveValidation", null);
$("#form").data("validator", null);
$.validator.unobtrusive.parse($("#form"));
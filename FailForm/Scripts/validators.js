jQuery.validator.addMethod("termsaccept", function (value, element, param) {
    return element.checked;
});
jQuery.validator.unobtrusive.adapters.addBool("termsaccept");


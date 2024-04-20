let notify = function () {
    toastr.options = {
        "closeButton": true,
        "debug": false,
        "newestOnTop": false,
        "progressBar": false,
        "positionClass": "toast-top-right",
        "preventDuplicates": false,
        "onclick": null,
        "showDuration": "300",
        "hideDuration": "1000",
        "timeOut": "5000",
        "extendedTimeOut": "1000",
        "showEasing": "swing",
        "hideEasing": "linear",
        "showMethod": "fadeIn",
        "hideMethod": "fadeOut"
    }

    let info = function (message) {
        toastr["info"](message)
    }

    let success = function (message) {
        toastr["success"](message)
    }

    let warning = function (message) {
        toastr["warning"](message)
    }

    let error = function (message) {
        toastr["error"](message)
    }

    return {
        info, success, warning, error
    }


}();
// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
$(function () {
    $("#loaderbody").addClass('hide');

    $(document).bind('ajaxStart', function () {
        loaderbody
        $("#loaderbody").removeClass('hide');
    }).bind('ajaxStop', function () {
        $("#loaderbody").addClass('hide');
    });
});

showInputModal = (url, title) => {
    if (title == null) {
        title = "Edit Reservation";
    }
    $.ajax({
        type: "GET",
        url: url,
        success: function (res) {
            $("#form-modal .modal-body").html(res);
            $("#form-modal .modal-title").html(title);
            $("#form-modal").modal('show');
        }
    });
}


SaveReservation = form => {
    console.log(form.action);
    try {
        $.ajax({
            type: "POST",
            url: form.action,
            data: new FormData(form),
            contentType: false,
            processData: false,
            success: function (res) {
                if (res.isValid) {
                    $("#form-modal .modal-body").html('');
                    $("#form-modal .modal-title").html('');
                    $("#form-modal").modal('hide');
                    $('#table-reservation').DataTable().ajax.reload();
                    $.notify('Submitted successfuly', { globalPosition: 'top right', className: "success" });
                }
                else {
                    $("#form-modal .modal-body").html(res.html);
                    $.notify('Submitted fail', { globalPosition: 'top right', className: "warn" });
                }
            },
            error: function (err) {
                console.log(err);
            }
        })
    } catch (ex) {
        console.log(ex)
    }

    //to prevent default form submit event
    return false;
}

DeleteReservation = form => {
    if (confirm('Are you sure to delete this record ?')) {
        try {
            $.ajax({
                type: "POST",
                url: form.action,               
                contentType: false,
                processData: false,
                success: function (res) {                    
                    $('#table-reservation').DataTable().ajax.reload();
                    $.notify('Delete successfuly', { globalPosition: 'top right', className: "success" });
                    console.log(res.message);
                },
                error: function (err) {
                    $.notify('Delete fail', { globalPosition: 'top right', className: "warn" });
                    console.log(err);
                }
            })
        } catch (ex) {
            console.log(ex);
        }
    }

    //to prevent default form submit event
    return false;
}
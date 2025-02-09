function showSuccessAlert(mensaje)
{
    Swal.fire({
        title: mensaje,
        icon: "success",
        draggable: true
    });
}

function showConfirmDeleteAlert(callback)
{
    Swal.fire({
        title: "Are you sure?",
        text: "You won't be able to revert this!",
        icon: "warning",
        showCancelButton: true,
        confirmButtonColor: "#3085d6",
        cancelButtonColor: "#d33",
        confirmButtonText: "Yes, delete it!"
    }).then((result) => {
        if (result.isConfirmed) {
            callback()
        }
    });
}

function showErrorAlert(mensaje)
{
    Swal.fire({
        icon: "error",
        title: "Oops...",
        text: mensaje,
    });
}
function showSuccessAlert(mensaje)
{
    Swal.fire({
        position: "top-end",
        icon: "success",
        title: mensaje,
        showConfirmButton: false,
        timer: 2000
    });
}

function showConfirmDeleteAlert(controller, action, id = "")
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
            window.location.href = "/" + controller + "/" + action + "/" + id;
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
function Delete(event, url) {
    event.preventDefault(); // Linkin otomatik olarak çalışmasını engelle
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
            $.ajax({
                type: "DELETE",
                url: url,
                success: function (response) {
                    if (response.success) {
                        window.location.href = response.redirectUrl;
                    } else {
                        Swal.fire({
                            title: "Error!",
                            text: "An error occurred while deleting the item.",
                            icon: "error"
                        });
                    }
                },
                error: function (xhr, status, error) {
                    Swal.fire({
                        title: "Error!",
                        text: "An error occurred while deleting the item.",
                        icon: "error"
                    });
                }
            });
        }
    });
}

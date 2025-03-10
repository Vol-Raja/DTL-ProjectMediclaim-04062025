$(function () {
    //$("#example1").DataTable({
    //    "responsive": true, "lengthChange": false, "autoWidth": false,
    //    "buttons": ["copy", "csv", "excel", "pdf", "print", "colvis"]
    //}).buttons().container().appendTo('#example1_wrapper .col-md-6:eq(0)');
    $('#empTbl').DataTable({
        "paging": true,
        "lengthChange": true,
        "searching": true,
        "ordering": true,
        "info": true,
        "autoWidth": false,
        "responsive": true,
    });
});

$(".deleteEmp").click(async function () {
    if (confirm("Are you sure you want to delete all details of employee?")) {
        const { value: text } = await Swal.fire({
            input: 'textarea',
            inputLabel: 'Delete Reason',
            inputPlaceholder: 'Type your reason here...',
            inputAttributes: {
                'aria-label': 'Type your reason here'
            },
            showCancelButton: true
        })        
        if (text) {
            $.ajax('/EmployeeRegistration/DeleteEmp', {
                type: 'POST',  // http method
                data: { employeeId: $(this).attr("data-empId"),deleteReason:text },  // data to submit
                success: function (data, status, xhr) {

                    if (data == "Deleted") {
                        toastrSuccess("Employee info delete successfully!");
                        setTimeout(function () { window.location.reload(); },5000)
                        
                    }
                    else {
                        toastrWarning("Please try again. Could not able delete employee info.")
                    }
                },
                error: function (jqXhr, textStatus, errorMessage) {
                    toastrError("Something went wrong!!!")
                }
            });
        }
    }
})

$(".generateCredential").click(function () {
    if (confirm("Are you sure you want to generate credential?")) {
        $.ajax('/EmployeeRegistration/GenerateCredentialById', {
            type: 'POST',  // http method
            data: { employeeId: $(this).attr("data-empId")},  // data to submit
            success: function (data, status, xhr) {

                if (data == "success") {
                    toastrSuccess("Employee credential has generated and sent to registerd email id and mobile number.");
                    setTimeout(function () { window.location.reload(); }, 5000)

                }
                else {
                    toastrWarning("Please try again. Could not able employee credential.")
                }
            },
            error: function (jqXhr, textStatus, errorMessage) {
                toastrError("Something went wrong!!!")
            }
        });
    }
})
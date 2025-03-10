$(function () {
    $('#tblInvestment').DataTable({
        "paging": true,
        "lengthChange": true,
        "searching": true,
        "ordering": false,
        "info": true,
        "autoWidth": false,
        "responsive": true,
    });
});
function DeleteInvestment(InvestmentId, rw) {
    if (confirm("Are you sure you want to delete all details of Investment?"))

        $.ajax('/FinanceManagement/DeleteInvestment', {
            type: 'POST',  // http method
            data: { InvestmentId: InvestmentId },  // data to submit
            success: function (data, status, xhr) {

                if (data == "Deleted") {
                    alert("Investment info delete successfully!")
                    /*var href = $('#adel').attr('href');

                    window.location.href = href;*/
                    $(rw).parents("tr").remove();
                    location.reload(true)
                }

            },
            error: function (jqXhr, textStatus, errorMessage) {
                $('p').append('Error' + errorMessage);
            }
        });
    else
        alert("Canceled Data Deletion...!!!");

}

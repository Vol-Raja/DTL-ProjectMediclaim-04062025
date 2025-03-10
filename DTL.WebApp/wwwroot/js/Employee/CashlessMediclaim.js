//changed by nirbhay    27/02/2025
$(document).ready(function () {

            $('#searchButton').on('click', function () {
                let searchTerm = $('#searchInput').val();
                loadData(searchTerm);
            });
            loadData(searchTerm = '');
            updateTotalTransactions();
            setInterval(updateTotalTransactions, 1000);
        });
        function updateTotalTransactions() {

            let count = 0;
            let activeuser = 0;
            let rejectuser = 0;

            // Get all rows in the transactions table
            const rows = document.querySelectorAll("#tblProcessingCashlessClaim tr");
            rows.forEach(row => {
                const cells = row.getElementsByTagName("td");
                if (cells.length > 7) {
                    const claimStatus = cells[7].innerText.trim(); // Get Status column (index 3)
                    if (claimStatus === "Pending") {
                        count++;
                    }
                    if (claimStatus === "Approved") {
                        activeuser++;
                    }

                    if (claimStatus === "Rejected") {
                        rejectuser++;
                    }
                }
            });

            document.getElementById("activeSessions").innerText = count;
            document.getElementById("totalUsers").innerText = activeuser;
            document.getElementById("revenue").innerText = rejectuser;
        }
        function loadData(searchTerm = '') {
            $.ajax({
                url: "/Mediclaim/Processing/GetMediclaimcashlessByPPO",
                type: "GET",
                dataType: "json",
                success: function (response) {
                    let tableBody = $("#tblProcessingCashlessClaim");
                    tableBody.empty(); // Clear old data
                    response.forEach(user => {
                        let claimStatus = '';
                        let remark = '';
                        if (
                            searchTerm &&
                            !user.nameOfPatient?.toLowerCase().includes(searchTerm.toLowerCase()) &&
                            !user.claimId?.toString().toLowerCase().includes(searchTerm.toLowerCase()) &&
                            !user.ppoNumber?.toString().toLowerCase().includes(searchTerm.toLowerCase())
                        ) {
                            return;
                        }

                        switch (user.claimStatusId) {
                            case 1:
                                claimStatus = '<span class="bg-warning Status_text">Pending</span>';
                                break;
                            case 2:
                                claimStatus = '<span class="bg-success Status_text">Approved</span>';

                                break;
                            case 3:
                                claimStatus = '<span class="bg-danger Status_text">Rejected</span>';

                                break;

                        }
                        let row = `<tr>
                                                            <td>${user.ppoNumber}</td>
                                                            <td>${user.createdDateString}</td>
                                                            <td>${user.claimId}</td>
                                                            <td>${user.amount}</td>
                                                            <td>${user.approvedAmount}</td>
                                                            <td>${user.cardCategory}</td>
                                                            <td>${user.nameOfPatient}</td>
                                                            <td>${claimStatus}</td>
<td>
<a href="/EmployeeDashboard/Mediclaim/ViewCashlessMediclaim/${user.claimId}"
                       title="Print Preview"
                       class="btn btn-primary btn-sm mr-2 btn_small">
                       <i class="far fa-eye"></i>
                    </a>

                                    </td>

</tr>`;
                        tableBody.append(row);
                    });
                },
                error: function (xhr, status, error) {
                    console.error("Error fetching data:", error);
                }
            });
}
//End
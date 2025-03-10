//var tempData = [];

$('#updateNomineeTbl').hide();
$('#NomineeDetails').validate({
    rules: {
        MemberName: {
            required: true,
        },
        Relationship: {
            required: true,
        },
        DateOfBirth: {
            required: true
        },
        //ContigencyReason: {
        //    required: true
        //},
        Commutation: {
            required: true
        },
        Arrear: {
            required: true
        },
        GuardianName: {
            required: function requiredfield() {
                var dob = new Date($("#DateOfBirth").val());
                if (dob != null) {
                    var diff_ms = Date.now() - dob.getTime();
                    var age_dt = new Date(diff_ms);
                    return (Math.abs(age_dt.getUTCFullYear() - 1970) < 18);
                }
            }
        },
        GuardianRelation: {
            required: function requiredfield() {
                var dob = new Date($("#DateOfBirth").val());
                if (dob != null) {
                    var diff_ms = Date.now() - dob.getTime();
                    var age_dt = new Date(diff_ms);
                    return (Math.abs(age_dt.getUTCFullYear() - 1970) < 18);
                }
            }
        },
        GuardianAddress: {
            required: function requiredfield() {
                var dob = new Date($("#DateOfBirth").val());
                if (dob != null) {
                    var diff_ms = Date.now() - dob.getTime();
                    var age_dt = new Date(diff_ms);
                    return (Math.abs(age_dt.getUTCFullYear() - 1970) < 18);
                }
            }
        }

    },
    messages: {
        MemberName: {
            required: "Please enter member name.",
        },
        Relationship: {
            required: "Please select relationship",
        },
        DateOfBirth: {
            required: "Please provide date of birth",
        },
        //ContigencyReason: {
        //    required: "Please provide a contigency reason",
        //},
        Commutation: {
            required: "Please provide commutation",
        },
        Arrear: {
            required: "Please provide arrear",
        },
        GuardianName: {
            required: "Please provide guardian name",
        },
        GuardianRelation: {
            required: "Please provide guardian relation"
        },
        GuardianAddress: {
            required: "Please provide guardian Address"
        }
    },
    errorElement: 'span',
    errorPlacement: function (error, element) {
        error.addClass('invalid-feedback');
        element.closest('.form-group').append(error);
    },
    highlight: function (element, errorClass, validClass) {
        $(element).addClass('is-invalid');
    },
    unhighlight: function (element, errorClass, validClass) {
        $(element).removeClass('is-invalid');
    }
});
$("#btnNomineeSave").click(function () {

    var myRows = [];
    var $headers = $("#nomineeTbl th");
    var $rows = $("#nomineeTbl tbody tr").each(function (index) {        
        $cells = $(this).find("td");
        myRows[index] = {};
        $cells.each(function (cellIndex) {            
            if ($($headers[cellIndex]).attr("data-col") != "Action") {
                myRows[index][$($headers[cellIndex]).attr("data-col")] = $(this).html();
            }
            if ($($headers[cellIndex]).attr("data-col") == "employeeRegistrationId") {
                myRows[index][$($headers[cellIndex]).attr("data-col")] = $("#employeeRegistrationId").val();
            }
        });
    });
    var myObj = {};
    myObj.nomineeDetailsModel = myRows;
    
        $.ajax({
            url: "/EmployeeRegistration/AddNominee",
            type: "POST",
            dataType: "json",
            data: myObj,
            success: function (data) {
                toastrSuccess("Data saved successfully!");
                $("#custom-tabs-three-serviceHistory-tab").trigger("click");
            },
            error: function (err) {
                toastrError("Something went wrong!!!");
            }
        });
    
});

$("#addNomineeTbl").click(function () {
    var totalPercentage = parseInt($("#Commutation").val()) + parseInt($("#Arrear").val());
    if (totalPercentage == 100) {
    if ($("#NomineeDetails").valid()) {
        var data = {
            memberName: $("#MemberName").val(),
            relationship: $("#Relationship option:selected").val(),
            dateOfBirth: $("#DateOfBirth").val(),
            guardianName: $("#GuardianName").val(),
            guardianRelation: $("#GuardianRelation").val(),
            guardianAddress: $("#GuardianAddress").val(),
            /*contigencyReason: $("#ContigencyReason").val(),*/
            commutation: $("#Commutation").val(),
            arrear: $("#Arrear").val()
        }
        //tempData.push(data);
        addToTbl(data);
        }
    } else {
        toastrError("Commutation and Arrear total should be 100%");
    }
});

$("#clearBtn").click(function () {
    clear();
});

function addToTbl(data) {
    var tblHTML = "<tr>" +
        "<td style='display: none'></td>" +
        "<td style='display: none'></td>" +
        "<td>" + data.memberName + "</td>" +
        "<td>" + data.relationship + "</td>" +
        "<td>" + data.guardianName + "</td>" +
        "<td>" + data.guardianRelation + "</td>" +
        "<td>" + data.guardianAddress + "</td>" +
        "<td>" + data.dateOfBirth + "</td>" +
        "<td>" + data.commutation + "</td>" +
        "<td>" + data.arrear + "</td>" +
        /*"<td>" + data.contigencyReason + "</td>" +*/
        "<td>" + "<a class='edit' href='JavaScript:void(0);' >Edit</a> &nbsp;&nbsp;" +
        "<a class='delete' href='JavaScript:void(0);'>Delete</a>" + "</td>" +
        "</tr>";
    $("#nomineeTbl tbody").append(tblHTML);
    clear();
}

$("#nomineeTbl").on("click", ".edit", function (e) {
    var row = $(this).closest('tr');
    $('#hfRowIndex').val($(row).index());
    var td = $(row).find("td");
    $("#MemberName").val($(td).eq(2).html());
    $("#Relationship").val($(td).eq(3).html());
    $("#GuardianName").val($(td).eq(4).html());
    $("#GuardianRelation").val($(td).eq(5).html());
    $("#GuardianAddress").val($(td).eq(6).html());
    $("#DateOfBirth").val($(td).eq(7).html());     
    $("#Commutation").val($(td).eq(8).html());
    $("#Arrear").val($(td).eq(9).html());
    /*$("#ContigencyReason").val($(td).eq(8).html());*/
    $('#addNomineeTbl').hide();
    $('#updateNomineeTbl').show();
});

$("#nomineeTbl").on("click", ".delete", function (e) {
    if (confirm("Are you sure want to delete this record!")) {
        $(this).closest('tr').remove();
        clear();
        $('#addNomineeTbl').show();
        $('#updateNomineeTbl').hide();
    } else {
        e.preventDefault();
    }
});

$('#updateNomineeTbl').on('click', function () {
    if ($("#NomineeDetails").valid()) {
        var data = {
            memberName: $("#MemberName").val(),
            relationship: $("#Relationship selected:option").val(),
            dateOfBirth: $("#DateOfBirth").val(),
            guardianName: $("#GuardianName").val(),
            guardianRelation: $("#GuardianRelation").val(),
            guardianAddress: $("#GuardianAddress").val(),
            /*contigencyReason: $("#ContigencyReason").val(),*/
            commutation: $("#Commutation").val(),
            arrear: $("#Arrear").val()
        }
        // update json
        //updateJsonData(data);
        $('#nomineeTbl tbody tr').eq($('#hfRowIndex').val()).find('td').eq(2).html(data.memberName);
        $('#nomineeTbl tbody tr').eq($('#hfRowIndex').val()).find('td').eq(3).html(data.relationship);
        $('#nomineeTbl tbody tr').eq($('#hfRowIndex').val()).find('td').eq(4).html(data.guardianName);
        $('#nomineeTbl tbody tr').eq($('#hfRowIndex').val()).find('td').eq(5).html(data.guardianRelation);
        $('#nomineeTbl tbody tr').eq($('#hfRowIndex').val()).find('td').eq(6).html(data.guardianAddress);
        $('#nomineeTbl tbody tr').eq($('#hfRowIndex').val()).find('td').eq(7).html(data.dateOfBirth);
        $('#nomineeTbl tbody tr').eq($('#hfRowIndex').val()).find('td').eq(8).html(data.commutation);
        $('#nomineeTbl tbody tr').eq($('#hfRowIndex').val()).find('td').eq(9).html(data.arrear);
       /* $('#nomineeTbl tbody tr').eq($('#hfRowIndex').val()).find('td').eq(8).html(data.contigencyReason);*/
        $('#addNomineeTbl').show();
        $('#updateNomineeTbl').hide();
        clear();
    }
});

function clear() {
  
    $("#MemberName").val("");
    $("#Relationship").val("");
    $("#DateOfBirth").val("");
    $("#GuardianName").val("");
    $("#GuardianRelation").val("");
    $("#GuardianAddress").val("");
    /*$("#ContigencyReason").val("");*/
    $("#Commutation").val("");
    $("#Arrear").val("");
}






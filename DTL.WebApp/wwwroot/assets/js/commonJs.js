$('#dependent').click(function () {
    if ($(this).attr('id') == 'dependent') {
        $('#showMe').show();
        let checkDependet = true;
        $(`.validateThisD`).each(function () {
            let input = $(this);
            if (validate(input) === false) {
                showAlert(input);
                checkDependet = false;
            }

            else {
                removeAlert(input);
            }
        });
    }
    else {
        $('#showMe').hide();
    }
});
$('#dependentSelf').click(function () {
    $('#showMe').hide();
});

$(`.submitBtn`).click(function (e) {
	e.preventDefault();
	let check = true;
	$(`.validateThis`).each(function () {
		let input = $(this);
		if (validate(input) === false) {
			showAlert(input);
			check = false;
		}
		else {
			removeAlert(input);
		}
	});

	if (check) {
		$(this).attr('disabled', true);
		$(`.formSubmit`).submit();
		console.log('form submited successfully')
	}
});

function validate(input) {
	if (input.attr('type') === 'email' || input.attr('email') === 'email') {
		if (input.val().trim().match(/^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\.)+))([a-zA-Z]{1,5}|[0-9]{1,3})(\]?)$/) == null) {
			return false;
		}
	} else if (input.attr('type') === 'number' && input.attr('data-type') === "mobile") {
		if (!input.val() || input.val().length != 10) {
			return false;
		}
	} else {
		if (input.val().trim() === '' || !input.val()) {
			return false;
		}
	}
	return true;
}

function showAlert(input) {
	let thisAlert = input.parent();
	$(thisAlert).children('.errorMessage').html($(input).attr('data-validate'));
	$(input).addClass('is-invalid');
}

function removeAlert(input) {
	let thisAlert = input.parent();
	$(thisAlert).children('.errorMessage').html('');
	$(input).removeClass('is-invalid');
}
// NonCashless Form
$('#claimType').change(function () {
    if ($('#claimType').val() == 'opd') {
        $('#opd').show();
        $('#disbursement').hide();
        $('#cd').hide();
        $(`.submitBtn`).click(function (e) {
            e.preventDefault();
            let checkopd = true;
            $(`.validateThisOPD`).each(function () {
                let input = $(this);
                if (validate(input) === false) {
                    showAlert(input);
                    checkopd = false;
                } else {
                    removeAlert(input);
                }
            });
        });
    }

    else if ($('#claimType').val() == 'Dispensary') {
        $('#opd').hide();
        $('#disbursement').show();
        $('#cd').hide();
        $(`.submitBtn`).click(function (e) {
            e.preventDefault();
            let checkdispensary = true;
            $(`.validateThisDispensary`).each(function () {
                let input = $(this);
                if (validate(input) === false) {
                    showAlert(input);
                    checkdispensary = false;
                } else {
                    removeAlert(input);
                }
            });
        });
    }
    else if ($('#claimType').val() == 'CD') {
        $('#opd').hide();
        $('#disbursement').hide();
        $('#cd').show();
        $(`.submitBtn`).click(function (e) {
            e.preventDefault();
            let checkcd = true;
            $(`.validateThisCD`).each(function () {
                let input = $(this);
                if (validate(input) === false) {
                    showAlert(input);
                    checkcd = false;
                } else {
                    removeAlert(input);
                }
            });
        });
    }
});
function isNumberKey(evt) {
    var charCode = (evt.which) ? evt.which : event.keyCode
    if (charCode > 31 && (charCode < 48 || charCode > 57))
        return false;

    return true;
}
function lettersOnly() {
    var charCode = event.keyCode;

    if ((charCode > 64 && charCode < 91) || (charCode > 96 && charCode < 123) || charCode == 32)

        return true;
    else
        return false;
}


function decimalNumberKey(evt, obj) {

    var charCode = (evt.which) ? evt.which : event.keyCode
    var value = obj.value;
    var dotcontains = value.indexOf(".") != -1;
    if (dotcontains)
        if (charCode == 46) return false;
    if (charCode == 46) return true;
    if (charCode > 31 && (charCode < 48 || charCode > 57))
        return false;
    return true;
}
function mobileNumber(event) {
    k = event.which;
    if ((k >= 96 && k <= 105) || k == 8) {
        if ($(this).val().length == 10) {
            if (k == 8) {
                return true;
            } else {
                event.preventDefault();
                return false;

            }
        }
    } else {
        event.preventDefault();
        return false;
    }

}


function inWords(num) {
    var a = ['', 'one ', 'two ', 'three ', 'four ', 'five ', 'six ', 'seven ', 'eight ', 'nine ', 'ten ', 'eleven ', 'twelve ', 'thirteen ', 'fourteen ', 'fifteen ', 'sixteen ', 'seventeen ', 'eighteen ', 'nineteen '];
    var b = ['', '', 'twenty', 'thirty', 'forty', 'fifty', 'sixty', 'seventy', 'eighty', 'ninety'];
    if ((num = num.toString()).length > 9) return 'overflow';
    n = ('000000000' + num).substr(-9).match(/^(\d{2})(\d{2})(\d{2})(\d{1})(\d{2})$/);
    if (!n) return; var str = '';
    str += (n[1] != 0) ? (a[Number(n[1])] || b[n[1][0]] + ' ' + a[n[1][1]]) + 'crore ' : '';
    str += (n[2] != 0) ? (a[Number(n[2])] || b[n[2][0]] + ' ' + a[n[2][1]]) + 'lakh ' : '';
    str += (n[3] != 0) ? (a[Number(n[3])] || b[n[3][0]] + ' ' + a[n[3][1]]) + 'thousand ' : '';
    str += (n[4] != 0) ? (a[Number(n[4])] || b[n[4][0]] + ' ' + a[n[4][1]]) + 'hundred ' : '';
    str += (n[5] != 0) ? ((str != '') ? 'and ' : '') + (a[Number(n[5])] || b[n[5][0]] + ' ' + a[n[5][1]]) + 'only ' : '';
    return str;
}

Number.prototype.inWords = function () { return inWords(this); };


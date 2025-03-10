function chkNotEmpty(input){
	var value = document.getElementById(input).value;
	
	if(value!=""){
		document.getElementById(input+"_error").innerHTML="";
		document.getElementById(input).style.borderColor="green";	
		return true;
	}else{
		document.getElementById(input+"_error").innerHTML="This value is required.";
		//document.getElementById(input).focus();
		document.getElementById(input).style.borderColor="red";		
		return false;
	}
}

function chkNotZero(input){
	var value = document.getElementById(input).value;
	if(value!="0"){
		document.getElementById(input+"_error").innerHTML="";
		document.getElementById(input).style.borderColor="green";	
		return true;
	}else{
		document.getElementById(input+"_error").innerHTML="This value is required.";
		document.getElementById(input).style.borderColor="red";	
		//document.getElementById(input).focus();
		return false;
	}
}

function notRadioEmpty(input){
	 var value = document.querySelector('input[name="'+input+'"]:checked');

	if(value==null){
		document.getElementById(input+"_error").innerHTML="This value is required.";
		return false;
	}else{
		document.getElementById(input+"_error").innerHTML="";	
		return true;
	}
}

function chkNumber(input){
	var value = document.getElementById(input).value;
	if(value!=""){
		document.getElementById(input+"_error").innerHTML="";
		document.getElementById(input).style.borderColor="green";	
		if(!/^[0-9]+$/.test(value)){
			document.getElementById(input+"_error").innerHTML="This value must be in digits.";
			document.getElementById(input).style.borderColor="red";	
			return false;
		}else{
			document.getElementById(input+"_error").innerHTML="";
			document.getElementById(input).style.borderColor="green";	
			return true;
		}
	}else{
		document.getElementById(input+"_error").innerHTML="This value is required.";
		document.getElementById(input).style.borderColor="red";	
		return false;
	}
	

}

function chkAlphabets(input){
	var value = document.getElementById(input).value;
	if(value!=""){
		document.getElementById(input+"_error").innerHTML="";
		document.getElementById(input).style.borderColor="green";	
		if(!/^[A-Za-z]+$/.test(value)){
			document.getElementById(input+"_error").innerHTML="This value must be in character.";
			document.getElementById(input).style.borderColor="red";	
			return false;
		}else{
			document.getElementById(input+"_error").innerHTML="";
			document.getElementById(input).style.borderColor="green";	
			return true;
		}
	}else{
		document.getElementById(input+"_error").innerHTML="This value is required.";
		document.getElementById(input).style.borderColor="red";	
		return false;
	}
	

}

function chkCalculation(input){
	var value = document.getElementById(input).value;
	if(value!=""){
		document.getElementById(input+"_error").innerHTML="";
		document.getElementById(input).style.borderColor="green";	
		if(value=="0.0"){
			document.getElementById(input+"_error").innerHTML="This value is invalid.";
			document.getElementById(input).style.borderColor="red";	
			return false;
		}else{
			document.getElementById(input+"_error").innerHTML="";
			document.getElementById(input).style.borderColor="green";	
			return true;
		}
	}else{
		document.getElementById(input+"_error").innerHTML="This value is required.";
		document.getElementById(input).style.borderColor="red";	
		return false;
	}
	
}


function chkMobile(input){
	var value = document.getElementById(input).value;
	if(value!=""){
		document.getElementById(input+"_error").innerHTML="";
		document.getElementById(input).style.borderColor="green";	
		if(!/^[7,8,9]{1}[0-9]{9}$/.test(value)){
			document.getElementById(input+"_error").innerHTML="Mobile No must be of exact 10 digits.";
			document.getElementById(input).style.borderColor="red";	
			return false;
		}else{
			document.getElementById(input+"_error").innerHTML="";
			document.getElementById(input).style.borderColor="green";	
			return true;
		}
	}else{
		document.getElementById(input+"_error").innerHTML="This value is required.";
		document.getElementById(input).style.borderColor="red";	
		return false;
	}
}

function chkEmail(input){
	var value = document.getElementById(input).value;
	if(value!=""){
		document.getElementById(input+"_error").innerHTML="";
		document.getElementById(input).style.borderColor="green";	
		var emailcheck = /^(([^<>()\[\]\\.,;:\s@"]+(\.[^<>()\[\]\\.,;:\s@"]+)*)|(".+"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/;
		if(!(emailcheck.test(value))){
			document.getElementById(input+"_error").innerHTML="This value should be a valid email.";
			document.getElementById(input).style.borderColor="red";	
			return false;
		}else{
			document.getElementById(input+"_error").innerHTML="";
			document.getElementById(input).style.borderColor="green";	
			return true;
		}
	}else{
		document.getElementById(input+"_error").innerHTML="This value is required.";
		document.getElementById(input).style.borderColor="red";	
		return false;
	}
}

function chkNumberNSize(input){
	var value = document.getElementById(input).value;
	if(value!=""){
		document.getElementById(input+"_error").innerHTML="";
		document.getElementById(input).style.borderColor="green";	
		if(!/^[0-9]{4}$/.test(value)){
			document.getElementById(input+"_error").innerHTML="This value required exact 4 digits.";
			document.getElementById(input).style.borderColor="red";	
			return false;
		}else{
			document.getElementById(input+"_error").innerHTML="";
			document.getElementById(input).style.borderColor="green";	
			return true;
		}
	}else{
		document.getElementById(input+"_error").innerHTML="This value is required.";
		document.getElementById(input).style.borderColor="red";	
		return false;
	}
}
	
function chkFloatNumber(input){
	var value = document.getElementById(input).value;
	if(value!=""){
		document.getElementById(input+"_error").innerHTML="";
		document.getElementById(input).style.borderColor="green";	
		if(!/^[0-9]+[.]*[0-9]*$/.test(value)){
			document.getElementById(input+"_error").innerHTML="This value is invalid.";
			document.getElementById(input).style.borderColor="red";	
			return false;
		}else{
			document.getElementById(input+"_error").innerHTML="";
			document.getElementById(input).style.borderColor="green";	
			return true;
		}
	}else{
		document.getElementById(input+"_error").innerHTML="This value is required.";
		document.getElementById(input).style.borderColor="red";	
		return false;
	}
	}
function chkNotEmptyDropdown(input){
	   
	var value = document.getElementById(input).value;

	if(value!="0"){
		document.getElementById(input+"_error").innerHTML="";
		document.getElementById(input).style.borderColor="green";	
		return true;
	}else{
		document.getElementById(input+"_error").innerHTML="This value is required.";
		document.getElementById(input).style.borderColor="red";	
		//document.getElementById(input).focus();
		return false;
	}
}

function chkPincode(input){
	var value = document.getElementById(input).value;
	if(value!=""){
		document.getElementById(input+"_error").innerHTML="";
		document.getElementById(input).style.borderColor="green";	
		if(!/^[0-9]{6}$/.test(value)){
			document.getElementById(input+"_error").innerHTML="This value required exactly 6 digits.";
			document.getElementById(input).style.borderColor="red";	
			return false;
		}else{
			document.getElementById(input+"_error").innerHTML="";
			document.getElementById(input).style.borderColor="green";	
			return true;
		}
	}else{
		document.getElementById(input+"_error").innerHTML="This value is required.";
		document.getElementById(input).style.borderColor="red";	
		return false;
	}
}

function chkYear(input){
	var value = document.getElementById(input).value;
	if(value!=""){
		document.getElementById(input+"_error").innerHTML="";
		document.getElementById(input).style.borderColor="green";	
		if(!/^[0-9]{4}[-][0-9]{2}$/.test(value)){
			document.getElementById(input+"_error").innerHTML="This value must be in eg.2022-23 format.";
			document.getElementById(input).style.borderColor="red";	
			return false;
		}else{
			document.getElementById(input+"_error").innerHTML="";
			document.getElementById(input).style.borderColor="green";	
			
			var myArray = value.split("-");
			var year1 = myArray[0].slice(-2);
			var year2 = myArray[1]; 
			
			if(year2-year1!=1){
				document.getElementById(input+"_error").innerHTML="This is invalid year.";
				document.getElementById(input).style.borderColor="red";	
				return false;
			}else{
				document.getElementById(input+"_error").innerHTML="";
				document.getElementById(input).style.borderColor="green";	
				return true;
			}
			
		}
	}else{
		document.getElementById(input+"_error").innerHTML="This value is required.";
		document.getElementById(input).style.borderColor="red";	
		return false;
	}
}

function chkRatio(input){
	var value = document.getElementById(input).value;
	if(value!=""){
		document.getElementById(input+"_error").innerHTML="";
		document.getElementById(input).style.borderColor="green";	
		if(!/^[0-9][:][0-9]$/.test(value)){
			document.getElementById(input+"_error").innerHTML="This value must be in eg.1:2 format.";
			document.getElementById(input).style.borderColor="red";	
			return false;
		}else{
			document.getElementById(input+"_error").innerHTML="";
			document.getElementById(input).style.borderColor="green";	
			return true;
		}
	}else{
		document.getElementById(input+"_error").innerHTML="This value is required.";
		document.getElementById(input).style.borderColor="red";	
		return false;
	}
}

function chkOTP(input){
	var value = document.getElementById(input).value;
	if(value!=""){
		document.getElementById(input+"_error").innerHTML="";
		document.getElementById(input).style.borderColor="green";	
		if(!/^[0-9]{6}$/.test(value)){
			document.getElementById(input+"_error").innerHTML="value must be digits.";
			document.getElementById(input).style.borderColor="red";	
			return false;
		}else{
			document.getElementById(input+"_error").innerHTML="";
			document.getElementById(input).style.borderColor="green";	
			return true;
		}
	}else{
		document.getElementById(input+"_error").innerHTML="This value is required.";
		document.getElementById(input).style.borderColor="red";	
		return false;
	}
}

function chkFileEmpty(input){
	
if( document.getElementById(input).files.length == 0 ){
	document.getElementById(input+"_error").innerHTML="file is required";
	document.getElementById(input).style.borderColor="red";	
	return false;
}else{
	document.getElementById(input+"_error").innerHTML="";
			document.getElementById(input).style.borderColor="green";	
			return true;
}
}


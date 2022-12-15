var PhotoInput = document.getElementById('imageUpload');
var firstName = document.getElementById('firstName');
var lastName = document.getElementById('lastName');
var MI = document.getElementById('MI');
var email = document.getElementById('email');
var mobileNumber = document.getElementById('mobileNumber');
var privilege = document.getElementById('privilege');



function validateFirstName() {
    if (!firstName.value) {
        firstName.classList.add("border-danger");
        return 0
    } else {
        firstName.classList.remove("border-danger");
        return 1
    }
}

function validateLastName() {
    if (!lastName.value) {
        lastName.classList.add("border-danger");
        return 0
    } else {
        lastName.classList.remove("border-danger");
        return 1
    }
}



function validateEmail() {
    if (!email.value) {
        email.classList.add("border-danger");
        return 0;
    }

    var validEmailRegex = /^[a-zA-Z0-9.!#$%&'*+/=?^_`{|}~-]+@[a-zA-Z0-9-]+(?:\.[a-zA-Z0-9-]+)*$/;
    if (!email.value.match(validEmailRegex)) {
        email.classList.add("border-danger");
        return 0;
    }

    email.classList.remove("border-danger");
    return 1;
}

function validateMobileNumber() {
    if (!mobileNumber.value) {
        mobileNumber.classList.add("border-danger");
        return 0;
    }

    var validMobileNumberRegex = /^(09|639)\d{9}$/;
    if (!mobileNumber.value.match(validMobileNumberRegex)) {
        mobileNumber.classList.add("border-danger");
        return 0;
    }

    mobileNumber.classList.remove("border-danger");
    return 1;
}
firstName.addEventListener('focusout', validateFirstName);
lastName.addEventListener('focusout', validateLastName);
email.addEventListener('focusout', validateEmail);
mobileNumber.addEventListener('focusout', validateMobileNumber);

const submits = document.getElementById('btnSubmit');

submits.addEventListener('click', () => {

    validateFirstName()
    validateLastName()
    validateEmail()
    validateMobileNumber()
  

    if (Boolean(validateFirstName()) && Boolean(validateLastName()) && Boolean(validateEmail())  && Boolean(validateMobileNumber())   ) {
        var formData = new FormData();
        formData.append('First Name', firstName.value);
        formData.append('Last Name', lastName.value);
        if (!MI.value) {
            MI.value = " ";
        }

        formData.append('Middle Initial', MI.value);
        formData.append('Email', email.value);
        formData.append('Mobile Number', mobileNumber.value);


        if (PhotoInput.files[0]) {
            formData.append('Photo', PhotoInput.files[0]);
        } 
        formData.append('Privilege', privilege.value);
        
        $.ajax({
            type: 'POST',
            url: "SubmitUser",
            data: formData,
            cache: false,
            contentType: false,
            processData: false,
            success: function (result) {
                document.getElementById("cancel").click();
            },
            error: function (result) {
                alert("Fail");
            }
        });

    } 





})

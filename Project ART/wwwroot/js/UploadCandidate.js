var firstName = document.getElementById('firstName');
var lastName = document.getElementById('lastName');
var mi = document.getElementById('mi');
var email = document.getElementById('email');
var mobileNumber = document.getElementById('mobileNumber');
var website = document.getElementById('website');
var province = document.getElementById('province');
var city = document.getElementById('city');
var JobID = document.getElementById('JobID');
var ResumeInput = document.getElementById('ResumeInput');
var introductionVideo = document.getElementById('introductionVideo');
var PhotoInput = document.getElementById('imageUpload');
var flag = 1

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

function validateProvince() {
    if (province.value === "Select Province") {
        province.classList.add("border-danger");
        return 0;
    }

    province.classList.remove("border-danger");
    return 1;
}

function validateCity() {
    if (city.value === "Select City") {
        city.classList.add("border-danger");
        return 0;
    }

    city.classList.remove("border-danger");
    return 1;
}


function validateJob() {
    if (JobID.value === "0") {
        JobID.classList.add("border-danger");
        return 0;
    }
    JobID.classList.remove("border-danger");
    return 1;
}


function validateResume() {

    if (!ResumeInput.value) {
 
        ResumeInput.classList.add("form-control");
        ResumeInput.classList.add("border-danger");
        return 0;
    }

    ResumeInput.classList.remove("form-control");
    ResumeInput.classList.remove("border-danger");
    return 1;
}

function validateVideo() {
    if (!introductionVideo.value) {
        introductionVideo.classList.add("form-control");
        introductionVideo.classList.add("border-danger");
        return 0;
    }

    introductionVideo.classList.remove("form-control");
    introductionVideo.classList.remove("border-danger");
    
    return 1;
}


firstName.addEventListener('focusout', validateFirstName);
lastName.addEventListener('focusout', validateLastName);
email.addEventListener('focusout', validateEmail);
mobileNumber.addEventListener('focusout', validateMobileNumber);
province.addEventListener('change', validateProvince);
city.addEventListener('change', validateCity);
JobID.addEventListener('change', validateJob);
ResumeInput.addEventListener('change', validateResume);
introductionVideo.addEventListener('change', validateVideo);


const submitCandidateDetails = document.getElementById('btnSubmit');
const spinner = document.getElementById('spinner');

submitCandidateDetails.addEventListener('click', () => {
    spinner.innerHTML = '<div class="spinner-border spinner-border-sm" role="status"> </div >';
    validateFirstName()
    validateLastName()
    validateEmail()
    validateMobileNumber()
    validateProvince();
    validateCity();
    validateResume();
    validateVideo();
    validateJob()

    if (Boolean(validateFirstName()) && Boolean(validateLastName()) && Boolean(validateEmail()) && Boolean(validateJob()) && Boolean(validateMobileNumber()) && Boolean(validateProvince()) && Boolean(validateCity()) &&  Boolean(validateResume()) && Boolean(validateVideo())) {
        var formData = new FormData();
        formData.append('First Name', firstName.value);
        formData.append('Last Name', lastName.value);
        if (!mi.value) {
            mi.value = " ";
        }
        
        formData.append('Middle Initial', mi.value);
        formData.append('Email', email.value);
        formData.append('Mobile Number', mobileNumber.value);
        formData.append('Website', website.value);
        if (!website.value) {
            website.value = " ";
        }
        formData.append('Province', province.value);
        formData.append('City', city.value);
        formData.append('JobID', JobID.value);

        if (PhotoInput.files[0]) {      
            formData.append('Photo', PhotoInput.files[0]);
        }

        formData.append('Resume', ResumeInput.files[0]);
        formData.append('Introduction Video', introductionVideo.files[0]);
        $.ajax({
            type: 'POST',
            url: "SaveCandidateDetails",
            data: formData,
            cache: false,
            contentType: false,
            processData: false,
            success: function (result) {
                document.getElementById("Cancel").click();
            },
            error: function (result) {
                alert("Fail");
            }
        });
            
    } else {
        spinner.innerHTML = 'Add';
    }

   



})
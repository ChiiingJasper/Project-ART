'use strict';

/* globals MediaRecorder */

let mediaRecorder;
let recordedBlobs;

const recordedVideo = document.querySelector('video#recorded');
const recordButton = document.querySelector('button#record');
recordButton.addEventListener('click', () => {
    if (recordButton.textContent === 'Start Recording') {
        startRecording();
        startCountdown();
    } else {
        stopRecording();
        clearInterval(intervalHandle);
        recordButton.textContent = 'Start Recording';
        playButton.disabled = false;
        downloadButton.disabled = false;
        codecPreferences.disabled = false;
    }
});

const playButton = document.querySelector('button#play');
playButton.addEventListener('click', () => {
    const superBuffer = new Blob(recordedBlobs, { type: 'video/mp4' });
    recordedVideo.src = null;
    recordedVideo.srcObject = null;
    recordedVideo.src = window.URL.createObjectURL(superBuffer);
    recordedVideo.controls = true;
    recordedVideo.play();
});


document.getElementById("close").addEventListener("click", stopVideo);


const downloadButton = document.querySelector('button#download');
downloadButton.addEventListener('click', () => {
    let introVidLabel = document.getElementById('introductionVideo');
    let firstName = document.getElementById('firstName').value;
    let lastName = document.getElementById('lastName').value;
    introVidLabel.innerHTML = firstName + "_" + lastName + ".mp4";
    recorded.pause();
    recorded.currentTime = 0;
});

function handleDataAvailable(event) {
    console.log('handleDataAvailable', event);
    if (event.data && event.data.size > 0) {
        recordedBlobs.push(event.data);
    }
}

function startRecording() {
    recordedBlobs = [];
    let options = { mimeType: 'video/webm;codecs=vp9,opus' };

    try {
        mediaRecorder = new MediaRecorder(window.stream, options);
    } catch (e) {
        console.error('Exception while creating MediaRecorder:', e);
        errorMsgElement.innerHTML = `Exception while creating MediaRecorder: ${JSON.stringify(e)}`;
        return;
    }

    console.log('Created MediaRecorder', mediaRecorder, 'with options', options);
    recordButton.textContent = 'Stop Recording';
    playButton.disabled = true;
    downloadButton.disabled = true;
    mediaRecorder.onstop = (event) => {
        console.log('Recorder stopped: ', event);
        console.log('Recorded Blobs: ', recordedBlobs);
    };
    mediaRecorder.ondataavailable = handleDataAvailable;
    mediaRecorder.start();
    console.log('MediaRecorder started', mediaRecorder);
}

function stopRecording() {
    mediaRecorder.stop();
}

function handleSuccess(stream) {
    recordButton.disabled = false;
    console.log('getUserMedia() got stream:', stream);
    window.stream = stream;

    const gumVideo = document.querySelector('video#gum');
    gumVideo.srcObject = stream;

}

function PostBlob(blob) {
    //FormData
    var formData = new FormData();
    formData.append('video-blob', blob);

    // POST the Blob
    $.ajax({
        type: 'POST',
        url: "SaveRecoredFile",
        data: formData,
        cache: false,
        contentType: false,
        processData: false,
        success: function (result) {
            if (result) {
                console.log('Success');
            }
        },
        error: function (result) {
            console.log(result);
        }
    })
}

async function init(constraints) {
    try {
        const stream = await navigator.mediaDevices.getUserMedia(constraints);
        handleSuccess(stream);
    } catch (e) {
        console.error('navigator.getUserMedia error:', e);
        errorMsgElement.innerHTML = `navigator.getUserMedia error:${e.toString()}`;
    }
}

document.querySelector('button#start').addEventListener('click', async () => {
    document.querySelector('button#start').disabled = true;
    document.querySelector('button#stop').disabled = false;
    const constraints = {
        audio: true,
        video: {
            width: 1280, height: 720
        }
    };
    console.log('Using media constraints:', constraints);
    await init(constraints);
});

document.querySelector('button#stop').addEventListener('click', async () => {
    document.querySelector('button#stop').disabled = true;
    document.querySelector('button#start').disabled = false;
    const video = document.querySelector('video#gum');
    const mediaStream = video.srcObject;
    await mediaStream.getTracks().forEach(track => track.stop());
    video.srcObject = null;
});

document.querySelector('button#closeVideo').addEventListener('click', async () => {
    document.querySelector('button#stop').disabled = true;
    document.querySelector('button#start').disabled = false;
    const video = document.querySelector('video#gum');
    const mediaStream = video.srcObject;
    await mediaStream.getTracks().forEach(track => track.stop());
    video.srcObject = null;
});

const submitCandidate = document.querySelector('button#download');
downloadButton.addEventListener('click', () => {
    let introVidLabel = document.getElementById('introductionVideo');
    let firstName = document.getElementById('firstName').value;
    let lastName = document.getElementById('lastName').value;
    introVidLabel.innerHTML = firstName + "_" + lastName + ".mp4";
});

var firstName = document.getElementById('firstName');
var lastName = document.getElementById('lastName');
var mi = document.getElementById('mi');
var email = document.getElementById('email');
var mobileNumber = document.getElementById('mobileNumber');
var website = document.getElementById('website');
var province = document.getElementById('province');
var city = document.getElementById('city');
var JobID = document.getElementById('JobID');
var PhotoInput = document.getElementById('PhotoInput');
var ResumeInput = document.getElementById('ResumeInput');
var introductionVideo = document.getElementById('introductionVideo');
var flag = 1

function validateFirstName() {
    if (!firstName.value) {
        firstName.classList.add("red-border");
        document.getElementById("validateFirstName").innerHTML = "Please enter your first name";
        return 0
    } else {
        firstName.classList.remove("red-border");
        document.getElementById("validateFirstName").innerHTML = "";
        return 1
    }
}

function validateLastName() {
    if (!lastName.value) {
        lastName.classList.add("red-border");
        document.getElementById("validateLastName").innerHTML = "Please enter your last name";
        return 0
    } else {
        lastName.classList.remove("red-border");
        document.getElementById("validateLastName").innerHTML = "";
        return 1
    }
}

function validateEmail() {
    if (!email.value) {
        email.classList.add("red-border");
        document.getElementById("validateEmail").innerHTML = "Please enter your email";
        return 0;
    }

    var validEmailRegex = /^[a-zA-Z0-9.!#$%&'*+/=?^_`{|}~-]+@[a-zA-Z0-9-]+(?:\.[a-zA-Z0-9-]+)*$/;
    if (!email.value.match(validEmailRegex)) {
        email.classList.add("red-border");
        document.getElementById("validateEmail").innerHTML = "Please enter a valid email";
        return 0;
    }

    email.classList.remove("red-border");
    document.getElementById("validateEmail").innerHTML = "";
    return 1;
}

function validateMobileNumber() {
    if (!mobileNumber.value) {
        mobileNumber.classList.add("red-border");
        document.getElementById("validateMobileNumber").innerHTML = "Please enter your mobile number";
        return 0;
    }

    var validMobileNumberRegex = /((^(\+)(\d){12}$)|(^\d{11}$))/;
    if (!mobileNumber.value.match(validMobileNumberRegex)) {
        mobileNumber.classList.add("red-border");
        document.getElementById("validateMobileNumber").innerHTML = "Please enter a valid mobile number";
        return 0;
    }

    mobileNumber.classList.remove("red-border");
    document.getElementById("validateMobileNumber").innerHTML = "";
    return 1;
}

function validateProvince() {
    if (province.value === "Select Province *") {
        province.classList.add("red-border");
        document.getElementById("validateProvince").innerHTML = "Please select a province";
        return 0;
    }

    province.classList.remove("red-border");
    document.getElementById("validateProvince").innerHTML = "";
    return 1;
}

function validateCity() {
    if (city.value === "Select City *") {
        city.classList.add("red-border");
        document.getElementById("validateCity").innerHTML = "Please select a city";
        return 0;
    }

    city.classList.remove("red-border");
    document.getElementById("validateCity").innerHTML = "";
    return 1;
}

function validatePhoto() {
    var photoBorder = document.getElementById('photoBorder');
    if (PhotoLabel.innerHTML === "2x2 Photo *") {
        photoBorder.classList.add("red-border");
        document.getElementById("validatePhoto").innerHTML = "Please upload a photo";
        return 0;
    }
    
    photoBorder.classList.remove("red-border");
    document.getElementById("validatePhoto").innerHTML = "";
    return 1;
}

function validateResume() {
    var resumeBorder = document.getElementById('resumeBorder');
    if (ResumeLabel.innerHTML === "Upload CV *") {
        resumeBorder.classList.add("red-border");
        document.getElementById("validateResume").innerHTML = "Please upload a resume";
        return 0;
    }

    resumeBorder.classList.remove("red-border");
    document.getElementById("validateResume").innerHTML = "";
    return 1;
}

function validateVideo() {
    var videoBorder = document.getElementById('videoBorder');
    if (introductionVideo.innerHTML === "Introduction Video *") {
        videoBorder.classList.add("red-border");
        document.getElementById("validateVideo").innerHTML = "Please upload a video";
        return 0;
    }

    videoBorder.classList.remove("red-border");
    document.getElementById("validateVideo").innerHTML = "";
    return 1;
}

firstName.addEventListener('focusout', validateFirstName);
lastName.addEventListener('focusout', validateLastName);
email.addEventListener('focusout', validateEmail);
mobileNumber.addEventListener('focusout', validateMobileNumber);
province.addEventListener('change', validateProvince);
city.addEventListener('change', validateCity);
PhotoInput.addEventListener('change', validatePhoto);
ResumeInput.addEventListener('change', validateResume);
document.getElementById('download').addEventListener('click', validateVideo);

const submitCandidateDetails = document.getElementById('submitButton');
submitCandidateDetails.addEventListener('click', () => {

    submitCandidateDetails.innerHTML = '<div class="spinner-border spinner-border-sm" role="status"> </div >';

    validateFirstName()
    validateLastName()
    validateEmail()
    validateMobileNumber()
    validateProvince();
    validateCity();
    validatePhoto();
    validateResume();
    validateVideo();

    if (Boolean(validateFirstName()) && Boolean(validateLastName()) && Boolean(validateEmail()) && Boolean(validateMobileNumber()) && Boolean(validateProvince()) && Boolean(validateCity()) && Boolean(validatePhoto()) && Boolean(validateResume()) && Boolean(validateVideo())) {
        const blob = new Blob(recordedBlobs, { type: 'video/mp4' });
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
        formData.append('Photo', PhotoInput.files[0]);
        formData.append('Resume', ResumeInput.files[0]);
        formData.append('Introduction Video', blob);
        $.ajax({
            type: 'POST',
            url: "../SaveCandidateDetails",
            data: formData,
            cache: false,
            contentType: false,
            processData: false,
            success: function (result) {
                if (result) {
                    $(':input', '#userDetails')
                        .not(':button, :submit, :reset, :hidden')
                        .val('')
                        .prop('checked', false);

                    province.value = "Select Province *";
                    city.value = "Select City *";
                    PhotoLabel.innerHTML = "2x2 Photo *";
                    ResumeLabel.innerHTML = "Upload CV *";
                    introductionVideo.innerHTML = "Introduction Video *";
                    $("#successModal").modal('show');
                    submitCandidateDetails.innerHTML = 'Submit';
                }
            },
            error: function (result) {
                console.log(result);
                submitCandidateDetails.innerHTML = 'Submit';
            }
        });
    } else {
        submitCandidateDetails.innerHTML = 'Submit';
    }
    
    
});


var secondsRemaining;
var intervalHandle;

function tick() {
    // grab the h1
    var timeDisplay = document.getElementById("time");

    // turn the seconds into mm:ss
    var min = Math.floor(secondsRemaining / 60);
    var sec = secondsRemaining - (min * 60);

    //add a leading zero (as a string value) if seconds less than 10
    if (sec < 10) {
        sec = "0" + sec;
    }

    // concatenate with colon
    var message = min.toString() + ":" + sec;

    // now change the display
    timeDisplay.innerHTML = message;

    // stop is down to zero
    if (secondsRemaining <= 10) {
        timeDisplay.classList.add("text-danger");
    }

    if (secondsRemaining === 0) {
        clearInterval(intervalHandle);
        recordButton.click()
    }


    //subtract from seconds remaining
    secondsRemaining--;

}

function startCountdown() {
    secondsRemaining = 1 * 60;
    intervalHandle = setInterval(tick, 1000);
}


var video = document.getElementById("recorded");
function stopVideo() {
    video.pause();
    video.currentTime = 0;
}


$("#stopVideo").on('click', function () {
    stopVideo();
});
'use strict';

/* globals MediaRecorder */

let mediaRecorder;
let recordedBlobs;




const recordedVideo = document.querySelector('video#recorded');
const recordButton = document.querySelector('button#record');
recordButton.addEventListener('click', () => {
    if (recordButton.textContent === 'Start Recording') {
        startRecording();
    } else {
        stopRecording();
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

const downloadButton = document.querySelector('button#download');
downloadButton.addEventListener('click', () => {
    let introVidLabel = document.getElementById('introductionVideo');
    let firstName = document.getElementById('firstName').value;
    let lastName = document.getElementById('lastName').value;
    introVidLabel.innerHTML = firstName + "_" + lastName+".mp4";
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

const submitCandidate = document.querySelector('button#download');
downloadButton.addEventListener('click', () => {
    let introVidLabel = document.getElementById('introductionVideo');
    let firstName = document.getElementById('firstName').value;
    let lastName = document.getElementById('lastName').value;
    introVidLabel.innerHTML = firstName + "_" + lastName + ".mp4";
});

const submitCandidateDetails = document.getElementById('submitButton');
submitCandidateDetails.addEventListener('click', () => {
    const blob = new Blob(recordedBlobs, { type: 'video/mp4' });
    var formData = new FormData();
    formData.append('First Name', document.getElementById('firstName').value);
    formData.append('Last Name', document.getElementById('lastName').value);
    formData.append('Middle Initial', document.getElementById('mi').value);
    formData.append('Email', document.getElementById('email').value);
    formData.append('Mobile Number', document.getElementById('mobileNumber').value);
    formData.append('Website', document.getElementById('website').value);
    formData.append('Province', document.getElementById('province').value);
    formData.append('City', document.getElementById('city').value);
    formData.append('Photo', document.getElementById('PhotoInput').files[0]);
    formData.append('Resume', document.getElementById('ResumeInput').files[0]);
    formData.append('Introduction Video', blob);
    $.ajax({
        type: 'POST',
        url: "SaveCandidateDetails",
        data: formData,
        cache: false,
        contentType: false,
        processData: false,
        success: function (result) {
            if (result) {
                alert('Success');
            }
        },
        error: function (result) {
            console.log(result);
        }
    });
});
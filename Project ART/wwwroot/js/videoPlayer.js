var video = document.getElementById("videoPlayer");
function stopVideo() {
    video.pause();
    video.currentTime = 0;
}

document.getElementById("videoModalPlayer").addEventListener("click", function () {
    document.getElementById("close").addEventListener("click", stopVideo);
});




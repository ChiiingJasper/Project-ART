﻿let icon = document.getElementById("iconchange");
let status = document.getElementById("status");
let Approve = document.getElementById("approve");

Approve.addEventListener("click", function () {
    icon.classList.remove("bg-warning")
    icon.classList.add("bg-success")
    status.innerHTML = "<i class=\"bg-success\" id=\"iconchange\"></i> approved"



    
});




var video = document.getElementById("videoPlayer");
function stopVideo() {
    video.pause();
    video.currentTime = 0;
}

document.getElementById("close").addEventListener("click", stopVideo);

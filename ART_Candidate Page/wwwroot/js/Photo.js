var photoInput = document.getElementById('PhotoInput');
var photoLabel = document.getElementById('PhotoLabel');

photoInput.addEventListener("change", function () {
    photoLabel.innerHTML = photoInput.files[0].name;
});

var resumeInput = document.getElementById('ResumeInput');
var resumeLabel = document.getElementById('ResumeLabel');

resumeInput.addEventListener("change", function () {
    resumeLabel.innerHTML = resumeInput.files[0].name;
});

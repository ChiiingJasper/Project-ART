document.getElementById('changeImage').addEventListener('click', openDialog);

function openDialog() {
    document.getElementById('imageUpload').click();
}

var imageInput = document.getElementById('imageUpload');
var profpic = document.getElementById('profpic');

document.getElementById('imageUpload').addEventListener("change", function () {
    let image = document.getElementById('profilePic');
    image.src = URL.createObjectURL(imageInput.files[0]);
    profpic.value = imageInput.files[0].name
});
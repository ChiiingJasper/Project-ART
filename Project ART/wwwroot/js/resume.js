const dropArea = document.querySelector(".drop_box"),
    button = dropArea.querySelector("button"),
    dragText = dropArea.querySelector("header"),
    input = dropArea.querySelector("input");
let file;
var filename;

button.onclick = () => {
    input.click();
};

input.addEventListener("change", function (e) {
    var fileName = e.target.files[0].name;
    let filedata = `
    <h4>${fileName}</h4>
    <input type="file" hidden id="File1" name="File1" />
    <button type="submit" class="btn btn-success">Upload</button>`;
    dropArea.innerHTML = filedata;
    const fileInput = document.querySelector('input[type="file"]');
    const dataTransfer = new DataTransfer();
    dataTransfer.items.add(e.target.files[0]);
    fileInput.files = dataTransfer.files;
});

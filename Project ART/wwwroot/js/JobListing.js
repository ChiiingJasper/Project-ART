$(document).ready(function () {
    $("#addResponsibility").click(function () {
        $("#responsibilityDiv").clone().appendTo("#addResponsibilityHere");
    });

    $("#addQualification").click(function () {
        $("#qualificationDiv").clone().appendTo("#addQualificationHere");
    });

    $("#addBenefit").click(function () {
        $("#benefitDiv").clone().appendTo("#addBenefitHere");
    });
});


document.getElementById('changeImage').addEventListener('click', openDialog);

function openDialog() {
    document.getElementById('imageUpload').click();
}

var imageInput = document.getElementById('imageUpload');

document.getElementById('imageUpload').addEventListener("change", function () {
    let image = document.getElementById('profilePic');
    image.src = URL.createObjectURL(imageInput.files[0]);
    profpic.value = imageInput.files[0].name
});




const submit = document.getElementById('submitJob');
submit.addEventListener('click', () => {
    
    var formData = new FormData();

    

    formData.append('icon', document.getElementById('imageUpload').files[0]);
    formData.append('jobInput', document.getElementById('jobInput').value);
    formData.append('jobDesc', document.getElementById('jobDesc').value);

    formData.append('responsibility', document.getElementById('responsibility').value);
    formData.append('responsibilityDesc', document.getElementById('responsibilityDesc').value);
    
    formData.append('qualification', document.getElementById('qualification').value);
    formData.append('qualificationDesc', document.getElementById('qualificationDesc').value);

    formData.append('benefit', document.getElementById('benefit').value);
    formData.append('benefitDesc', document.getElementById('benefitDesc').value);

    formData.append('dateEnd', document.getElementById('dateEnd').value);
    formData.append('vacancy', document.getElementById('vacancy').value);
    formData.append('salary', document.getElementById('salary').value);
    formData.append('province', document.getElementById('province').value);
    formData.append('city', document.getElementById('city').value);
    formData.append('jobNature', document.getElementById('jobNature').value);


    $.ajax({
        type: 'POST',
        url: "SubmitJobListing",
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
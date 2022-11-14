var responsibilityList = {};
var appendNum = 1;
    $("#addResponsibility").click(function () {
        appendNum++;
        child = $("#responsibilityDiv").clone();
        child.attr("id", "responsibilityDiv" + appendNum);
        child.find('input[type=text]').val('');
        child.find("button").removeClass("btn-success").addClass("btn-danger");
        child.find("button").addEventListener("click", Delete(this));
        child.find("i").removeClass("bi-plus").addClass("bi-x");
        child.find("#addResponsibility").prop("id", "deleteResponsibility" + appendNum);
        child.find("#responsibility").prop("id", "responsibility" + appendNum);
        child.appendTo("#addResponsibilityHere");
        appendNum++;
    });

    $("#addQualification").click(function () {
        child = $("#qualificationDiv").clone();
        child.find("button").removeClass("btn-success").addClass("btn-danger");
        child.find("i").removeClass("bi-plus").addClass("bi-x");
        child.appendTo("#addQualificationHere");
    });

    $("#addBenefit").click(function () {
        child = $("#benefitDiv").clone();
        child.find("button").removeClass("btn-success").addClass("btn-danger");
        child.find("i").removeClass("bi-plus").addClass("bi-x");
        child.appendTo("#addBenefitHere");
    });

function Delete(elem) {
    console.log(elem.value);
}


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


var today = new Date();
var dd = today.getDate() + 1;
var mm = today.getMonth() + 1; //January is 0!
var yyyy = today.getFullYear();

if (dd < 10) {
    dd = '0' + dd
}

if (mm < 10) {
    mm = '0' + mm
}

today = yyyy + '-' + mm + '-' + dd;
document.getElementById('dateEnd').setAttribute("min", today);
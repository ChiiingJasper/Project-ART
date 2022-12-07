
    $("#addResponsibility").click(function () {
        child = $("#responsibilityDiv").clone();
        child.find('input').val('');
        child.find('textarea').val('');
        child.find("button").removeClass("btn-success").addClass("btn-danger delete");
        child.find("i").removeClass("bi-plus").addClass("bi-x");
        $(child).hide().appendTo("#addResponsibilityHere").fadeIn();
        Delete();
    });

    $("#addQualification").click(function () {
        child = $("#qualificationDiv").clone();
        child.find('input').val('');
        child.find('textarea').val('');
        child.find("button").removeClass("btn-success").addClass("btn-danger delete");
        child.find("i").removeClass("bi-plus").addClass("bi-x");
        $(child).hide().appendTo("#addQualificationHere").fadeIn();
        Delete();
    });

    $("#addBenefit").click(function () {
        child = $("#benefitDiv").clone();
        child.find('input').val('');
        child.find('textarea').val('');
        child.find("button").removeClass("btn-success").addClass("btn-danger delete");
        child.find("i").removeClass("bi-plus").addClass("bi-x");
        $(child).hide().appendTo("#addBenefitHere").fadeIn();
        Delete();
    });




function Delete() {
    var deleteElement = document.getElementsByClassName("delete");
    var i;
    for (i = 0; i < deleteElement.length; i++) {
        deleteElement[i].onclick = function () {
            var div = this.parentElement.parentElement.parentElement;
            $(div).fadeOut(500, function () {
                div.remove();
            });
            
        }
    }
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

    var responsiblity = document.getElementsByClassName("responsibility");
    var responsiblityDesc = document.getElementsByClassName("responsibilityDesc");
    
    for (var i = 0; i < responsiblity.length; i++) {
        formData.append('responsibility'+i, responsiblity[i].value);
        formData.append('responsibilityDesc'+i, responsiblityDesc[i].value);
    }

    formData.append('responsibilityCount', responsiblity.length);



    var qualification = document.getElementsByClassName("qualification");
    var qualificationDesc = document.getElementsByClassName("qualificationDesc");
    for (var i = 0; i < qualification.length; i++) {
        formData.append('qualification' + i, qualification[i].value);
        formData.append('qualificationDesc' + i, qualificationDesc[i].value);
    }

    formData.append('qualificationCount', qualification.length);



    var benefit = document.getElementsByClassName("benefit");
    var benefitDesc = document.getElementsByClassName("benefitDesc");
    for (var i = 0; i < benefit.length; i++) {
        formData.append('benefit' + i, benefit[i].value);
        formData.append('benefitDesc' + i, benefitDesc[i].value);
    }
    formData.append('benefitCount', benefit.length);


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
                document.getElementById('cancelbtn').click();
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
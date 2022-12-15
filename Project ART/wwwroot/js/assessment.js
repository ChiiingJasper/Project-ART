const assessmentBtn = document.getElementById('assessmentbtn');
const interviewBtn = document.getElementById('interivewbtn');



assessmentBtn.addEventListener('click', () => {
    
    document.getElementById('assessmentInput').addEventListener("keyup", function () {
        let v = parseInt(this.value);
        if (v < 0) this.value = 0;
        if (v > 100) this.value = 100;
    });

    let submitAssessment = document.getElementById('assessmentSubmit');
    submitAssessment.addEventListener('click', () => {
        var formData = new FormData();
        formData.append('score', document.getElementById('assessmentInput').value)
        formData.append('candidateID', document.getElementById('candidateIDToSubmit').value);
        $.ajax({
            type: 'POST',
            url: "../SubmitAssessment",
            data: formData,
            cache: false,
            contentType: false,
            processData: false,
            success: function (result) {
                if (result) {
                    location.reload();
                }
            },
            error: function (result) {
                console.log(result);
            }
        });

    });
});



interviewBtn.addEventListener('click', () => {

    document.getElementById('interviewInput').addEventListener("keyup", function () {
        let v = parseInt(this.value);
        if (v < 0) this.value = 0;
        if (v > 100) this.value = 100;
    });

    let submitInterview = document.getElementById('interviewSubmit');
    submitInterview.addEventListener('click', () => {
        var formData = new FormData();
        formData.append('score', document.getElementById('interviewInput').value)
        formData.append('candidateID', document.getElementById('candidateIDToSubmit').value);
        $.ajax({
            type: 'POST',
            url: "../SubmitInterview",
            data: formData,
            cache: false,
            contentType: false,
            processData: false,
            success: function (result) {
                if (result) {
                    location.reload();
                }
            },
            error: function (result) {
                console.log(result);
            }
        });

    });
});


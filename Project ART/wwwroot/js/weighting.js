const personalityBtn = document.getElementById('personalityBtn');
const resumeBtn = document.getElementById('resumeBtn');
const examBtn = document.getElementById('examBtn');
const interviewBtn = document.getElementById('interviewBtn');


personalityBtn.addEventListener('click', () => {
    document.getElementById('personalityInput').addEventListener("keyup", function () {
        let v = parseInt(this.value);
        if (v < 0) this.value = 0;
        if (v > 100) this.value = 100;
    });

    let personalitySubmit = document.getElementById('personalitySubmit');
    personalitySubmit.addEventListener('click', () => {
        var formData = new FormData();
        formData.append('score', document.getElementById('personalityInput').value)
        $.ajax({
            type: 'POST',
            url: "Candidates/SubmitPersonalityScore",
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



resumeBtn.addEventListener('click', () => {
    document.getElementById('resumeInput').addEventListener("keyup", function () {
        let v = parseInt(this.value);
        if (v < 0) this.value = 0;
        if (v > 100) this.value = 100;
    });

    let resumeSubmit = document.getElementById('resumeSubmit');
    resumeSubmit.addEventListener('click', () => {
        var formData = new FormData();
        formData.append('score', document.getElementById('resumeInput').value)
        $.ajax({
            type: 'POST',
            url: "Candidates/SubmitResumeScore",
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


examBtn.addEventListener('click', () => {
    document.getElementById('examInput').addEventListener("keyup", function () {
        let v = parseInt(this.value);
        if (v < 0) this.value = 0;
        if (v > 100) this.value = 100;
    });

    let examSubmit = document.getElementById('examSubmit');
    examSubmit.addEventListener('click', () => {
        var formData = new FormData();
        formData.append('score', document.getElementById('examInput').value)
        $.ajax({
            type: 'POST',
            url: "Candidates/SubmitExamScore",
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

    let interviewSubmit = document.getElementById('interviewSubmit');
    interviewSubmit.addEventListener('click', () => {
        var formData = new FormData();
        formData.append('score', document.getElementById('interviewInput').value)
        $.ajax({
            type: 'POST',
            url: "Candidates/SubmitInterviewScore",
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
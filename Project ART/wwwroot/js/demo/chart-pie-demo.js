// Set new default font family and font color to mimic Bootstrap's default styling
Chart.defaults.global.defaultFontFamily = 'Nunito', '-apple-system,system-ui,BlinkMacSystemFont,"Segoe UI",Roboto,"Helvetica Neue",Arial,sans-serif';
Chart.defaults.global.defaultFontColor = '#858796';

// Pie Chart Example
var ctx = document.getElementById("myPieChart");
var ctx2 = document.getElementById("b5PieChart");

var Dominance = parseInt(document.getElementById("Dominance").innerHTML);
var Influence = parseInt(document.getElementById("Influence").innerHTML);
var Steadiness = parseInt(document.getElementById("Steadiness").innerHTML);
var Compliance = parseInt(document.getElementById("Compliance").innerHTML);





var myPieChart = new Chart(ctx, {
  type: 'doughnut',
  data: {
      labels: ["Dominance", "Influence", "Steadiness","Compliance"],
    datasets: [{
        data: [Dominance, Influence, Steadiness, Compliance],
        backgroundColor: ['#1cc88a', '#e74a3b','#4e73df','#f6c23e'],
        hoverBackgroundColor: ['#169c6b', '#e32d1c', '#2854d7', '#f4b20b'],
      hoverBorderColor: "rgba(234, 236, 1, 244)",
    }],
  },
  options: {
    maintainAspectRatio: false,
    tooltips: {
      backgroundColor: "rgb(255,255,255)",
      bodyFontColor: "#858796",
      borderColor: '#dddfeb',
      borderWidth: 1,
      xPadding: 15,
      yPadding: 15,
      displayColors: false,
      caretPadding: 10,
    },
    legend: {
      display: false
    },
    cutoutPercentage: 80,
  },
});


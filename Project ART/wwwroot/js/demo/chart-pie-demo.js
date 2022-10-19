// Set new default font family and font color to mimic Bootstrap's default styling
Chart.defaults.global.defaultFontFamily = 'Nunito', '-apple-system,system-ui,BlinkMacSystemFont,"Segoe UI",Roboto,"Helvetica Neue",Arial,sans-serif';
Chart.defaults.global.defaultFontColor = '#858796';

// Pie Chart Example
var ctx = document.getElementById("myPieChart");
var ctx2 = document.getElementById("b5PieChart");

var myPieChart = new Chart(ctx, {
  type: 'doughnut',
  data: {
      labels: ["Dominance", "Influence", "Conscientiousness", "Steadiness"],
    datasets: [{
      data: [40, 30, 20, 10],
        backgroundColor: ['#1cc88a', '#e74a3b', '#f6c23e','#4e73df'],
        hoverBackgroundColor: ['#169c6b', '#e32d1c', '#f4b20b','#2854d7'],
      hoverBorderColor: "rgba(234, 236, 244, 1)",
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


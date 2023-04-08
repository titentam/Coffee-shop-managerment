//Chart area
var ctx = document.getElementById('myLineChart').getContext('2d');
var myChart = new Chart(ctx, {
    type: 'line',
    data: {
        labels: ['1/2020', '2/2020', '3/2020', '4/2020', '5/2020', '6/2020'],
        datasets: [{
            label: 'Doanh thu',
            data: [1200, 1400, 1300, 1600, 1800, 2000],
            backgroundColor: 'blue',
            borderColor: 'red'
        },
        {
            label: 'vốn',
            data: [1020, 1200, 1700, 1100, 1600, 1000],
            backgroundColor: 'black',
            borderColor: 'yellow'

        }]
    },
    options: {
        title: {
            display: true,
            text: 'Biểu đồ doanh thu'
        },
        scales: { // thiết lập trục x và trục y
            yAxes: [{
                ticks: {
                    beginAtZero: true,
                    callback: function (value, index, values) {
                        return value.toLocaleString() + ' triệu VNĐ'; // định dạng chuỗi nhãn trục y
                    }
                },
                scaleLabel: {
                    display: true,
                    labelString: 'Doanh thu (triệu VNĐ)'
                }
            }],
            xAxes: [{
                ticks: {
                    autoSkip: false, // không bỏ qua các nhãn trục x
                },
                scaleLabel: {
                    display: true,
                    labelString: 'Tháng năm 2020'
                }
            }]
        }
    }
});
var donut = document.getElementById('myDonutChart').getContext('2d');
var myDonutChart = new Chart(donut, {
    type: 'doughnut',
    data: {
        labels: [
            'Red',
            'Blue',
            'Yellow'
        ],
        datasets: [{
            label: 'My First Dataset',
            data: [300, 50, 100],
            backgroundColor: [
                'rgb(255, 99, 132)',
                'rgb(54, 162, 235)',
                'rgb(255, 205, 86)'
            ],
            hoverOffset: 4
        }]
    }
})



// get combobox element
const myComboBox = document.getElementById("myComboBox");

function getSelectedOption() {
    //myComboBox.addEventListener("change", function () {
    //    const selectedOption = myComboBox.options[myComboBox.selectedIndex].text;
    //    return selectedOption;
    //});
    return myComboBox.options[myComboBox.selectedIndex].text;
}

// get search text
const search_input = document.getElementById("search-input");

function getSearchText() {
    //myComboBox.addEventListener("change", function () {
    //    const selectedOption = myComboBox.options[myComboBox.selectedIndex].text;
    //    return selectedOption;
    //});
    return search_input.value;
}

// responsive header
// When the user scrolls down 500px from the top of the document, show the button
let scrollup = document.getElementById("scrollUp");
window.onscroll = function () { scrollFunction() };
function scrollFunction() {
    if (document.body.scrollTop > 500 || document.documentElement.scrollTop > 500) { scrollup.style.display = "block"; }
    else { scrollup.style.display = "block"; }
}
// When the user clicks on the button, scroll to the top of the document
scrollup.onclick = function () { topFunction() };
function topFunction() {
    document.body.scrollTop = 0;
    document.documentElement.scrollTop = 0;
}
// scroll up
function myFunction() {
    var x = document.getElementById("Mynavbar");
    if (x.className === "Duynavbar") {
        x.className += " responsive";
        //x.style.display = "block";
    } else {
        x.className = "Duynavbar";
    }
}

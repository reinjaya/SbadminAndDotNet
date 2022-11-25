let employeesData;
let dataLength;

//$.ajax({
//    url: `https://localhost:7042/api/Employee`,
//    type: "GET"
//}).done((res) => {
//    employeesData = res.data;
//    console.log(employeesData)
//    dataLength = employeesData.dataLength;
//});

async function fetchData() {
    const response = await fetch(`https://localhost:7042/api/Employee`);
    // waits until the request completes...
    const employees = await response.json();
    employeesData = employees.data;
    dataLength = employeesData.length;

    Highcharts.chart('clientChart', {
        colors: ['#01BAF2', '#71BF45', '#FAA74B', '#DC143C', '#D2691E', '#C71585'],
        chart: {
            type: 'pie'
        },
        accessibility: {
            point: {
                valueSuffix: '%'
            }
        },
        title: {
            text: 'Employee Placement Chart'
        },
        tooltip: {
            pointFormat: '{series.name}: <b>{point.percentage:.0f}%</b>'
        },
        plotOptions: {
            pie: {
                allowPointSelect: true,
                cursor: 'pointer',
                dataLabels: {
                    enabled: true
                },
                showInLegend: true
            }
        },
        series: [{
            name: null,
            colorByPoint: true,
            data: [{
                name: 'Not Working in Client',
                y: countClient('Not Working in Client')
            }, {
                name: 'Google',
                y: countClient('Google')
            }, {
                name: 'Amazon',
                sliced: true,
                selected: true,
                y: countClient('Amazon')
            }, {
                name: 'Meta',
                y: countClient('Meta')
            }, {
                name: 'Netflix',
                y: countClient('Netflix')
            }, {
                name: 'Apple',
                y: countClient('Apple')

            }]
        }]
    });

    Highcharts.chart('cityChart', {
        chart: {
            type: 'bar',
            zoomType: 'y'
        },
        title: {
            text: 'Chart of cities where employees live'
        },
        tooltip: {
            pointFormat: '{series.name}: <b>{point.percentage:.0f}%</b>'
        },
        xAxis: {
            categories: [
                'Jakarta',
                'Bandung',
                'Semarang',
                'Surabaya'
            ],
            title: {
                text: null
            },
            accessibility: {
                description: 'City'
            }
        },
        yAxis: {
            min: 0,
            max: 40,
            tickInterval: 10,
            title: {
                text: null
            },
            accessibility: {
                description: 'Number of employees living',
                rangeDescription: 'Range: 0 to 40%.'
            },
            labels: {
                overflow: 'justify',
                format: '{value}%'
            }
        },
        plotOptions: {
            bar: {
                dataLabels: {
                    enabled: true,
                    format: '{y}%'
                }
            }
        },
        tooltip: {
            valueSuffix: '%',
            stickOnContact: true,
            backgroundColor: 'rgba(255, 255, 255, 0.93)'
        },
        legend: {
            enabled: false
        },
        series: [
            {
                name: 'Number of employees living',
                color: '#a5d6a7',
                borderColor: '#60A465',
                data: [countCity('Jakarta'), countCity('Bandung'), countCity('Semarang'), countCity('Surabaya')]
            }
        ]
    });

    Highcharts.chart("genderChart", {
        chart: {
            type: "column",
            zoomType: "y"
        },
        title: {
            text: "Gender Distribution Bar"
        },
        xAxis: {
            categories: [
                "Male",
                "Female",
            ],
            title: {
                text: null
            },
            accessibility: {
                description: "Genders"
            }
        },
        yAxis: {
            min: 0,
            tickInterval: 2,
            title: {
                text: "Employee gender distribution"
            },
            labels: {
                overflow: "justify",
                format: "{value}"
            }
        },
        tooltip: {
            stickOnContact: true,
            backgroundColor: "rgba(255, 255, 255, 0.93)"
        },
        legend: {
            enabled: false
        },
        series: [
            {
                name: "Employee gender distribution",
                data: [countGender("Male"), countGender("Female")],
                borderColor: "#5997DE"
            }
        ]
    });

}

function countClient(name) {
    let count = 0;
    employeesData.forEach(employee => {
        if (employee.clientName == name) {
            count++;
        }
    });

    count = count * 100 / dataLength;

    return Math.round(count * 10) / 10;
}

function countGender(gender) {
    let count = 0;
    employeesData.forEach(employee => {
        if (employee.gender == gender) {
            count++;
        }
    });
    return count;
}

function countCity(city) {
    let count = 0;
    employeesData.forEach(employee => {
        if (employee.city == city) {
            count++;
        }
    });
    count = count * 100 / dataLength;

    return Math.round(count * 10) / 10;
}

function dataSalary(salary) {
    let count = 0;
    employeesData.forEach(employee => {
        if (employee.clientName > salary) {
            count++;
        }
    });
    return count;
}

fetchData();
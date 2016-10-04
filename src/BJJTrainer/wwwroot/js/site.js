
// Write your Javascript code.
function displayPieChart(result) {
    google.charts.load('current', { 'packages': ['corechart'] });
    google.charts.setOnLoadCallback(drawChart);
    function drawChart() {

        var filteredJson = [];
        var jsonResult = JSON.parse(result);
        var index;

        for (var i = 0; i < jsonResult.length; i++) {
            index = newDataContains(jsonResult[i][key]);
            if (index == -1) {
                filteredJson.push({[key]: jsonResult[i][key], time: jsonResult[i].time });
            } else {
                filteredJson[index].time += jsonResult[i].time;
            }
        }

        function newDataContains(key) {
            for (var i = 0; i < filteredJson.length; i++) {
                if (JSON.stringify(filteredJson[i][key]) == JSON.stringify[key])) {
                    return i;
                    break;
                }
            }
            return -1;
        }

        //console.log(filteredJson);
        var jsonFinal = [];
        $.each(filteredJson, function (i, obj) {
            jsonFinal.push([obj[key], obj.time]);
        });

        var data = google.visualization.arrayToDataTable(jsonFinal, true);

        var options = {
            title: 'Positions'
        };

        var chart = new google.visualization.PieChart(document.getElementById('piechart'));

        chart.draw(data, options);
    }

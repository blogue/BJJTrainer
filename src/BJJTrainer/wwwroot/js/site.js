
// Formats json for Google Charts API.

function formatJson(jsonResult) {

    var filteredJson = [];
    var index;

    for (var i = 0; i < jsonResult.length; i++) {
        index = newDataContains(jsonResult[i].type);
        if (index == -1) {
            filteredJson.push({ type: jsonResult[i].type, time: jsonResult[i].time });
        } else {
            filteredJson[index].time += jsonResult[i].time;
        }
    }

    function newDataContains(type) {
        for (var i = 0; i < filteredJson.length; i++) {
            if (JSON.stringify(filteredJson[i].type) == JSON.stringify(type)) {
                return i;
                break;
            }
        }
        return -1;
    }
    var jsonFinal = [];
    $.each(filteredJson, function (i, obj) {
        jsonFinal.push([obj.type, obj.time]);
    });

    return jsonFinal;
}

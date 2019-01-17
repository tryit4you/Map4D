$(function () {
    register();
    getListCity();
});
function register() {
    $('#metismenu').metisMenu({
        toggle: false
    });
    $('.polygonItems').off('click').on('click', function (e) {
        e.preventDefault();
        var code = $(this).data('id');
        drawPolygon(code);
    });
}
function drawPolygon(code) {
    $.ajax({
        url: '/drawpolygon/GetShapesByCode',
        data: { code: code },
        type: 'post',
        dataType: 'json',
        success: function (res) {
            var data = res.data;
            if (data.length !== 0) {
                console.log(data);
                L.geoJSON(data, {
                    style: function (feature) {
                        return { color: feature.properties.color };
                    }
                }).bindPopup(function (layer) {
                    return layer.feature.properties.description;
                }).addTo(map);
            }
        }
    });
}

$(function () {
    register();

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
    var map = L.map('map', {
        center: [51.505, -0.09],
        zoom: 13
    });
    $.ajax({
        url: '/drawpolygon/GetShapesByCode',
        data: { code: code },
        type: 'post',
        dataType: 'json',
        success: function (res) {


            console.log(res.data);
            L.geoJSON(res.data, {
                style: function (feature) {
                    return { color: feature.properties.color };
                }
            }).bindPopup(function (layer) {
                return layer.feature.properties.description;
            }).addTo(map);

        }
    });
}

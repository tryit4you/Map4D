var map;
$(function () {
    register();
    cities();
    loadCenter(16.036918, 108.218510);

});
function loadCenter(lat,lng,message) {
    var paramMapDefault = {
        lat: lat,
        lng: lng,
        zoom: 8,
        mode: "2d"
    };
    paramMap = {
        mode: paramMapDefault.mode,
        center: { lat: paramMapDefault.lat, lon: paramMapDefault.lng },
        tilt: 60,
        rotation: 0,
        zoom: paramMapDefault.zoom,
        plugins: [
            //MapGL.SearchComponent
        ]
    };
    map = MapGL.initMap("xinkciti-map", paramMap);
    map.leaflet.on('click', function (e) {
        getPolygonDetail(e.latlng.lat, e.latlng.lng);
    });
    if (message !== undefined) {
        L.popup()
            .setLatLng([lat, lng])
            .setContent(message)
            .openOn(map.leaflet);
    }
}
function getPolygonDetail(lat, lng) {
    $.ajax({
        url: '/polygondetail/GetDetailByLatLng',
        data: {
            lat: lat,
            lng: lng
        },
        type: 'post',
        dataType: 'json',
        success: function (res) {
            console.log(res.message);
            var message = "<div style='display: inline-flex;'><div style='margin-right: 5px;'><img style='height: 30px;width: 30px;' src='/Assets/uploads/no-street-view.png' /></div><div><b>" + res.details.Ward + "," + res.details.District + "," + res.details.City + "</b><br/>" + lat + "," + lng + "</div>";
            loadCenter(lat, lng, message);
        }
    });
}

function drawPolygon(shapes, pointCenter) {
    loadCenter(pointCenter.Lat, pointCenter.Lng);
    var jsonObj = JSON.parse(shapes);
    var draw = new L.GeoJSON(jsonObj);
    map.leaflet.addLayer(draw);
    
    
}
function register() {

    $('a.polygonItems').on('click', function () {
        $(this).addClass('active');
    });
        $('a.polygonItems').on('click', function () {
            $(this).addClass('active');
        });

        $('.polygonItems').off('click').on('click', function (e) {
            $('.polygonItems').removeClass('active');
            //e.preventDefault();
            var code = $(this).data('id');
            var cityId = $(this).data('city');
            $(this).addClass('active');
            $('#wards').html('');
            getShapes(code);
            dictrict(cityId);
        });
        $('.polygonItems-dictrict').off('click').on('click', function (e) {
            $('.polygonItems-dictrict').removeClass('active');
            e.preventDefault();
            var code = $(this).data('id');
            $(this).addClass('active');
            var dictrictId = $(this).data('dictrict');
            getShapes(code);
            ward(dictrictId);
        });
        $('.polygonItems-ward').off('click').on('click', function (e) {
            $('.polygonItems-ward').removeClass('active');
            e.preventDefault();
            $(this).addClass('active');
            var code = $(this).data('id');
            getShapes(code);
        });

    }

function cities() {
    $.ajax({
        url: '/drawpolygon/listcity',
        type: 'post',
        dataType: 'json',
        success: function (res) {
            var html = '';
            var data = res.data;
            var template = $('#city-template').html();
            $.each(data, function (i, item) {
                html += Mustache.render(template, {
                    cityId:item.Id,
                    code: item.Code,
                    name:item.Name
                });
            });
            $('#cities').html(html);
            register();
        }
    });
}
function dictrict(cityId) {
    $.ajax({
        url: '/drawpolygon/listdictrict',
        data: {
            cityId: cityId
        },
        type: 'post',
        dataType: 'json',
        success: function (res) {
            var html = '';
            var data = res.data;
            var template = $('#dictrict-template').html();
            $.each(data, function (i, item) {
                html += Mustache.render(template, {
                    dictrictId:item.Id,
                    code: item.Code,
                    name: item.Name
                });
            });
            $('#dictricts').html(html);

      
            register();
        }
    });
}
function ward(dictrictId) {
    $.ajax({
        url: '/drawpolygon/ListWard',
        data: {
            dictrictId: dictrictId
        },
        type: 'post',
        dataType: 'json',
        success: function (res) {
            var html = '';
            var data = res.data;
            var template = $('#ward-template').html();
            $.each(data, function (i, item) {
                html += Mustache.render(template, {
                    wardId:item.Id,
                    code: item.Code,
                    name: item.Name
                });
            });
            $('#wards').html(html);
            
            register();
        }
    });
}

function getShapes(code) {
    $.ajax({
        url: '/drawpolygon/GetShapes',
        data: {
            code: code
        },
        type: 'post',
        dataType: 'json',
        success: function (res) {
            var shapes = res.shapes;
            var pointCenter = res.pointCenter;
            drawPolygon(shapes, pointCenter);
        }
    });
}
$(function () {
    register();
    cities();
});
function register() {
    $('#metismenu').metisMenu({
        toggle: false
    });
    $('.polygonItems').off('click').on('click', function (e) {
        e.preventDefault();
        var code = $(this).data('id');
        var cityId = $(this).data('city');
        dictrict(cityId, code);
    });
    $('.polygonItems-dictrict').off('click').on('click', function (e) {
        e.preventDefault();
        var code = $(this).data('id');
        var dictrictId = $(this).data('dictrict');
        ward(dictrictId, code);
    });
    $('.polygonItems-ward').off('click').on('click', function (e) {
        e.preventDefault();
        var code = $(this).data('id');
        getward(code);
    });
    
}
//function initialize() {
//    map = new google.maps.Map(document.getElementById('map'), {
//        center: { "lat": 16.0801596580643, "lng": 108.218930205985 },
//        zoom: 20,
//        mapTypeId: 'roadmap'
//    });
//}

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
function dictrict(cityId,code) {
    $.ajax({
        url: '/drawpolygon/listdictrict',
        data: {
            cityId: cityId,
            code:code
        },
        type: 'post',
        dataType: 'json',
        success: function (res) {
            var html = '';
            var data = res.data;
            var shapes = res.shapes;
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
function ward(dictrictId,code) {
    $.ajax({
        url: '/drawpolygon/ListWard',
        data: {
            dictrictId: dictrictId,
            code: code
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
function getward(code) {
    $.ajax({
        url: '/drawpolygon/getward',
        data: {
            code: code
        },
        type: 'post',
        dataType: 'json',
        success: function (res) {
            var shapes = res.shapes;
            register();
        }
    });
}
//function DrawShape(code) {
//    $.ajax({
//        url: '/drawpolygon/GetShapeWard',
//        data: { code: code },
//        type: 'post',
//        dataType: 'json',
//        success: function (res) {
//            var data = res.data.Table;
//            console.log(res.data);
//            var bermudaTriangle = new google.maps.Polygon({
//                paths: data,
//                strokeColor: '#FF0000',
//                strokeOpacity: 0.8,
//                strokeWeight: 2,
//                fillColor: '#FF0000',
//                fillOpacity: 0.35
//            });
//            bermudaTriangle.setMap(map);
//        }
//    });
//}

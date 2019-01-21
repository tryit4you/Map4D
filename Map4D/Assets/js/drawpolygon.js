
$(function () {
    register();
    cities();
    //Khởi tạo bản đồ với tham số mặc định
    InitialMap(16.036918, 108.218510);
    $("#popup").hide();
});


function hidePopup() {
    $("#popup").hide();
}

//Hàm khởi tạo bản đồ với tham số lat,lng
function InitialMap(lat, lng) {
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
  
}

//Hàm khởi tạo sự kiện khi click, hoặc thao tác với giao diện
function register() {
    //$('#modalDetail').modal({ backdrop: 'static', keyboard: false });
    $('.polygonItems').off('click').on('click', function (e) {
        $('.polygonItems').removeClass('active');
        //e.preventDefault();
        var code = $(this).data('id');
        var cityId = $(this).data('city');
        $(this).addClass('active');
        getDetail(code);
        //$('#modalDetail').modal('show');
        $("#popup").show();

        getShapes(code);
        dictricts(cityId);
    });
    $('.polygonItems-dictrict').off('click').on('click', function (e) {
        $('.polygonItems-dictrict').removeClass('active');
        e.preventDefault();
        var code = $(this).data('id');
        $(this).addClass('active');
        var dictrictId = $(this).data('dictrict');
        getDetail(code);
        getShapes(code);
        wards(dictrictId);
        $("#popup").show();
    });
    $('.polygonItems-ward').off('click').on('click', function (e) {
        $('.polygonItems-ward').removeClass('active');
        e.preventDefault();
        $(this).addClass('active');
        var code = $(this).data('id');
        var ward = $('a.polygonItems-ward.active').html();
        showModelDetail("", "", ward);
        getShapes(code);
        $("#popup").show();
    });
    $("#menu-close").on('click', function (e) {
        e.preventDefault();
        $("#sidebar-wrapper").toggleClass("active");
    });
    $("#menu-toggle").on('click', function (e) {
        e.preventDefault();
        $("#sidebar-wrapper").toggleClass("active");
    });
}

//Hàm vẽ đường viền của tỉnh thành phố, quận huyện...
//shapes tham số là dữ liệu để vẽ vào leafletjs
function drawPolygon(shapes, pointCenter) {
    InitialMap(pointCenter.Lat, pointCenter.Lng);
    var jsonObj = JSON.parse(shapes);
    var draw = new L.GeoJSON(jsonObj);
    map.leaflet.addLayer(draw);
}

//Hàm load danh sách thành phố
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
                    cityId: item.Id,
                    code: item.Code,
                    name: item.Name
                });
            });
            $('#cities').html(html);
            register();
        }
    });
}

//Hàm load danh sách quận huyện
function dictricts(cityId) {
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
                    dictrictId: item.Id,
                    code: item.Code,
                    name: item.Name
                });
            });
            $('#dictricts').html(html);

            register();
        }
    });
}

//Hàm load danh sách xã/phường
function wards(dictrictId) {
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
                    wardId: item.Id,
                    code: item.Code,
                    name: item.Name
                });
            });
            $('#wards').html(html);

            register();
        }
    });
}

//Hàm lấy dữ liệu truyền vào hàm vẽ
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

//Hàm lấy thông tin về tỉnh thành phố ,quận huyện đang chọn
function getDetail(code) {
    $.ajax({
        url: '/polygondetail/GetDetailObject',
        type: 'post',
        data: { code: code },
        dataType: 'json',
        success: function (res) {
            $('#popup').html('');
            var html = res.htmlCode;
            $('#popup').html(html);
        }
    });
}
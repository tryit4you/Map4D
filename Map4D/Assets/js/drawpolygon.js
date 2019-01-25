var draw ;
$(function () {
    $('#DetailObjectProperties').hide();
    register();
    //Khởi tạo bản đồ với tham số mặc định
    InitialMap(16.036918, 108.218510,8);
   // loadTreeList();
    $('.tree li:has(ul)').addClass('parent_li').find(' > span');
    $('.tree li.parent_li > span').on('click', function (e) {
        var children = $(this).parent('li.parent_li').find(' > ul > li');
        if (children.is(":visible")) {
            children.hide('fast');
        } else {
            $(this).parent('li.parent_li').children().removeAttr('style');
            children.show('fast');
        }


        e.stopPropagation();
    });
    $('.level1').find("ul").hide();
    $('.tree').ready(function () {
        $('.tree').fadeIn(2000);
    });
});
//Hàm khởi tạo bản đồ với tham số lat,lng
function InitialMap(lat, lng,zoom) {
    var paramMapDefault = {
        lat: lat,
        lng: lng,
        zoom: zoom,
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
    $('#close-popup').click(function () {
        $('#DetailObjectProperties').fadeOut(1000);
    });
    $('.polygonItems').off('click').on('click', function (e) {
        $('.polygonItems').removeClass('active');
        //e.preventDefault();
        var code = $(this).data('id');
        var cityId = $(this).data('city');
        $(this).addClass('active');
        getDetail(code);
        $('#DetailObjectProperties').show();

        getShapes(code);
    });
    $('.polygonItems-district').off('click').on('click', function (e) {
        $('.polygonItems-dictrict').removeClass('active');
        e.preventDefault();
        var code = $(this).data('id');
        $(this).addClass('active');
        getDetail(code);
        getShapes(code);
        $('#DetailObjectProperties').show();
    });
    $('.polygonItems-ward').off('click').on('click', function (e) {
        $('.polygonItems-ward').removeClass('active');
        e.preventDefault();
        $(this).addClass('active');
        var code = $(this).data('id');
        getDetail(code);
        getShapes(code);
        $('#DetailObjectProperties').show();
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
function drawPolygon(shapes, pointCenter,zoom) {
    map.leaflet.setView(new L.LatLng(pointCenter.Lat, pointCenter.Lng), zoom);
    if (draw !== undefined) {
        map.leaflet.removeLayer(draw);
    }
    var jsonObj = JSON.parse(shapes);
    geojs = jsonObj;
    draw = new L.GeoJSON(jsonObj);
    map.leaflet.addLayer(draw);
}
//Hàm lấy dữ liệu truyền vào hàm vẽ
function getShapes(code) {
    var zoom = 0;
    $.ajax({
        url: '/drawpolygon/GetShapes',
        data: {
            code: code
        },
        type: 'post',
        dataType: 'json',
        success: function (res) {
  
            switch (code.length) {
                case 6:
                    zoom = 8;
                    break;
                case 9:
                    zoom = 12;
                    break;
                case 12:
                    zoom = 14;
                    break;
                default:
                    zoom = 8;
                    break;
            }
            var shapes = res.shapes;
            var pointCenter = res.pointCenter;
            console.log(zoom);
            drawPolygon(shapes, pointCenter, zoom);
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
            $('#DetailObjectProperties').html('');
            var html = res.htmlCode;
            $('#DetailObjectProperties').html(html);
            register();
        }
    });
}
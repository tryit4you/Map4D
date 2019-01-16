$(function () {
    register();
    getListCity();
});
function register() {
    $('#metismenu').metisMenu({
        toggle: false
    });
}
function getListCity() {
    $.ajax({
        url: '/drawpolygon/listcity',
        type: 'post',
        dataType: 'json',
        success: function (res) {
            var cityList = res.data;
            if (cityList.length !== 0) {

            }
        }
    });
}

$(function () {
    InitMap();
});
var map;

var InitMap = function () {
    map = new AMap.Map('mapdiv', {
        //resizeEnable: true,
        //rotateEnable: true,
        dragEnable: true,
        zoomEnable: true,
        //设置可缩放的级别
        //zooms: [3,18],
        //传入2D视图，设置中心点和缩放级别
        view: new AMap.View2D({
            center: new AMap.LngLat(116.397428, 39.90923),
            zoom: 10
        }),
        //初始化时，加载google地图 
        //layers: [new AMap.TileLayer({
        //    tileUrl: "http://mt{1,2,3,0}.google.cn/vt/lyrs=m@142&hl=zh-CN&gl=cn&x=[x]&y=[y]&z=[z]&s=Galil" //取图地址
        //})]
    });
    //在地图中添加ToolBar插件
    map.plugin(["AMap.ToolBar"], function () {
        var toolBar = new AMap.ToolBar();
        map.addControl(toolBar);

    });
}

var LoadPoints = function (accountid) {
    $.ajax({
        type: "POST",
        url: "../../../../admin/Subscribes/GetSubscribesList",
        data: "accountid=" + accountid,
        dataType: "json",
        success: function (data) {
            console.log(data);

            for (var i = 0; i < data.length; i++) {
                var markerdata = data[i];
                //var markerdata = {
                //    id: data[i].SubscribeID,
                //    title: data[i].NickName,
                //    openid: data[i].OpenID,
                //    imgurl: data[i].HeadImgUrl
                //};
                console.log(markerdata);
                AddMarker(data[i].Longitude, data[i].Latitude, markerdata);
                if (i === data.length - 1) {
                    MapPanTo(data[i].Longitude, data[i].Latitude);
                    map.setZoom(10);
                }
            }
        }
    });
}

var AddMarker = function (lng, lat, data) {



    //var htmlstr = "<img style='border:2px solid #fff;border-radius:0;' width='64' height='64' src='" + data.imgurl + "&width=64&height=64'>";
    var htmlstr = "<img class='img-circle' style='border:2px solid #fff;'  width='64' height='64' src='" + data.HeadImgUrl + "'>";
    var marker = new AMap.Marker({
        //icon: "http://webapi.amap.com/images/marker_sprite.png",
        //icon: imgurl,
        offset: new AMap.Pixel(-32, -64),

        content: htmlstr,
        title: data.NickName,
        extData: data,
        position: new AMap.LngLat(lng, lat),
        topWhenClick: true
    });
    marker.setMap(map);  //在地图上添加点

    //鼠标点击marker弹出自定义的信息窗体
    AMap.event.addListener(marker, 'mouseover', function (e) {
        //console.log(e)
        //console.log(marker)
        console.log(marker.getExtData());
        var exdata = marker.getExtData();


        //var content = "<p style='font-size:1.5em;color:#27aaff;'>" + aaaa.title + "</p><img src='" + aaaa.imgurl + "' width='300' height='300'>";


        var content = "<table>"
            + "<tr><td rowspan='3'><img src='" + exdata.HeadImgUrl + "' width='100' height='100' />"
        + "</td><td style='padding:5px;font-size:20px; font-weight:bold; text-align: center;'>" + exdata.NickName + "</td>"
        +"</tr><tr><td style='padding:5px; text-align: center;'>" + exdata.Country + exdata.Province + exdata.City + "</td>"
            + "</tr><tr><td style='padding:5px; color: #808080; text-align: center;'>" + exdata.SubscribeTimeString + " 关注</td>"
            + "</tr></table>";


        infoWindow.setContent(content);
        infoWindow.open(map, marker.getPosition());


    });

    AMap.event.addListener(marker, 'mouseout', function (e) {
        map.clearInfoWindow();


    });
}
var MapPanTo = function (lng, lat) {
    var lnglatXY = new AMap.LngLat(lng, lat);
    map.panTo(lnglatXY);
};
//实例化信息窗体
var infoWindow = new AMap.InfoWindow({
    //isCustom: true,  //使用自定义窗体
    //content: info
    offset: new AMap.Pixel(0, -32)//-113, -140
});


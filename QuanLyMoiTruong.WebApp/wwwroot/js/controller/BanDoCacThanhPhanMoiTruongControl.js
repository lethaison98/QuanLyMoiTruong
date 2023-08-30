if (typeof (BanDoCacThanhPhanMoiTruongControl) == "undefined") BanDoCacThanhPhanMoiTruongControl = {};
var countDenHan = 0;
BanDoCacThanhPhanMoiTruongControl = {
    Init: function () {
        BanDoCacThanhPhanMoiTruongControl.RegisterEvents();
    },
    RegisterEvents: function () {
        var map = L.map('mapid').setView([18.957736, 105.610247], 10);
        L.tileLayer('https://tile.openstreetmap.org/{z}/{x}/{y}.png', {
            maxZoom: 19,
            attribution: '<a href="https://dongnam.nghean.gov.vn/" target="_blank">Ban quản lý khu kinh tế Đông Nam</a>'
        }).addTo(map);
        map.options.minZoom = 9;
        map.options.maxZoom = 14;
        var successIcon = L.icon({
            iconUrl: '/img/map/dot_success.png',
            iconSize: [13, 13],
        });
        var warningIcon = L.icon({
            iconUrl: '/img/map/dot_warning.png',
            iconSize: [13, 13],
        });
        var primaryIcon = L.icon({
            iconUrl: '/img/map/dot_primary.png',
            iconSize: [13, 13],
        });
        var infoIcon = L.icon({
            iconUrl: '/img/map/dot_info.png',
            iconSize: [13, 13],
        });
        var dangerIcon = L.icon({
            iconUrl: '/img/map/dot_danger.png',
            iconSize: [13, 13],
        });


        Get({
            "url": localStorage.getItem("API_URL") + "/DiemQuanTrac/GetAll",
            callback: function (res) {
                if (res.Success) {
                    var place = [];
                    var tablebody = "";
                    $.each(res.Data, function (i, item) {
                        switch (item.Loai) {
                            case "K":
                                place[i] = L.marker([item.ViDo, item.KinhDo], { icon: successIcon }).addTo(map);
                                place[i].bindPopup(item.DiaChi);
                                var rowtable = `<tr class="rowtable" data-index =` + i + ` ><td><span class="badge bg-success">` + item.TenDiemQuanTrac + `</span></td><td><a href = "#">` + item.DiaChi + `</a></td></tr>`
                                tablebody += rowtable;
                                break;
                            case "N":
                                place[i] = L.marker([item.ViDo, item.KinhDo], { icon: dangerIcon }).addTo(map);
                                place[i].bindPopup(item.DiaChi);
                                var rowtable = `<tr class="rowtable" data-index =` + i + ` ><td><span class="badge bg-danger">` + item.TenDiemQuanTrac + `</span></td><td><a href = "#">` + item.DiaChi + `</a></td></tr>`
                                tablebody += rowtable;
                                break;
                            case "B":
                                place[i] = L.marker([item.ViDo, item.KinhDo], { icon: primaryIcon }).addTo(map);
                                place[i].bindPopup(item.DiaChi);
                                var rowtable = `<tr class="rowtable" data-index =` + i + ` ><td><span class="badge bg-primary">` + item.TenDiemQuanTrac + `</span></td><td><a href = "#">` + item.DiaChi + `</a></td></tr>`
                                tablebody += rowtable;
                                break;
                            case "M":
                                place[i] = L.marker([item.ViDo, item.KinhDo], { icon: infoIcon }).addTo(map);
                                place[i].bindPopup(item.DiaChi);
                                var rowtable = `<tr class="rowtable" data-index =` + i + ` ><td><span class="badge bg-info">` + item.TenDiemQuanTrac + `</span></td><td><a href = "#">` + item.DiaChi + `</a></td></tr>`
                                tablebody += rowtable;
                                break;
                            default:
                                place[i] = L.marker([item.ViDo, item.KinhDo], { icon: warningIcon }).addTo(map);
                                place[i].bindPopup(item.DiaChi);
                                var rowtable = `<tr class="rowtable" data-index =` + i + ` ><td><span class="badge bg-warning">` + item.TenDiemQuanTrac + `</span></td><td><a href = "#">` + item.DiaChi + `</a></td></tr>`
                                tablebody += rowtable;
                        }

                    });
                    $("#tbl-dsDiemQuanTrac tbody").append(tablebody);
                    $(".rowtable").off('click').on('click', function () {
                        var index = $(this).attr("data-index");
                        place[index].openPopup();
                    });
                }
            }
        });
    },
}

$(document).ready(function () {
    BanDoCacThanhPhanMoiTruongControl.Init();
});


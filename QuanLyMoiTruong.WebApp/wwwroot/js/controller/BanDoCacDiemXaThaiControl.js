if (typeof (BanDoCacDiemXaThaiControl) == "undefined") BanDoCacDiemXaThaiControl = {};
var countDenHan = 0;
var map = L.map('mapid').setView([18.957736, 105.610247], 10);

BanDoCacDiemXaThaiControl = {
    Init: function () {
        BanDoCacDiemXaThaiControl.RegisterEvents();
    },

    LoadDuLieuRaBanDo: function () {
        map.remove()
        map = L.map('mapid').setView([18.957736, 105.610247], 10);
        L.tileLayer('https://tile.openstreetmap.org/{z}/{x}/{y}.png', {
            maxZoom: 19,
            attribution: '<a href="https://dongnam.nghean.gov.vn/" target="_blank">Ban quản lý khu kinh tế Đông Nam</a>'
        }).addTo(map);
        map.options.minZoom = 9;
        map.options.maxZoom = 18;

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
            "url": localStorage.getItem("API_URL") + "/DiemQuanTrac/GetDuLieuCacDiemXaThaiLenBanDo",
            data: {
                "keyword": $("#searchDiemQuanTrac").val(),
                "loai": $('.ddLoaiDiemQuanTrac option:selected').val(),
            },
            callback: function (res) {
                if (res.Success) {
                    var place = [];
                    var tablebody = "";
                    $.each(res.Data, function (i, item) {
                        switch (item.Loai) {
                            case "KT":
                                place[i] = L.marker([item.ViDo, item.KinhDo], { icon: successIcon }).addTo(map);
                                if (item.DsKetQuaQuanTrac != null) {
                                    var text = "<b><i>" + item.TenDiemQuanTrac +": " + item.DiaChi + "</i></b>";
                                    $.each(item.DsKetQuaQuanTrac, function (i, kq) {
                                        text += "<br/>" + kq.ChiTieu + ": " + kq.GiaTri + ": " + kq.DonViTinh;
                                    });
                                    place[i].bindPopup(text);

                                } else {
                                    place[i].bindPopup("<b><i>" + item.TenDiemQuanTrac + ": " + item.DiaChi + "</i></b>");
                                }
                                var rowtable = `<tr class="rowtable" data-index =` + i + ` ><td><span class="badge bg-success">` + item.TenDiemQuanTrac + `</span></td><td><a href = "#">` + item.DiaChi + `</a></td></tr>`
                                tablebody += rowtable;
                                break;
                            case "NT":
                                place[i] = L.marker([item.ViDo, item.KinhDo], { icon: dangerIcon }).addTo(map);
                                if (item.DsKetQuaQuanTrac != null) {
                                    var text = "<b><i>" + item.TenDiemQuanTrac + ": " + item.DiaChi + "</i></b>";
                                    $.each(item.DsKetQuaQuanTrac, function (i, kq) {
                                        text += "<br/>" + kq.ChiTieu + ": " + kq.GiaTri + ": " + kq.DonViTinh;
                                    });
                                    place[i].bindPopup(text);

                                } else {
                                    place[i].bindPopup("<b><i>" + item.TenDiemQuanTrac + ": " + item.DiaChi + "</i></b>");
                                }
                                var rowtable = `<tr class="rowtable" data-index =` + i + ` ><td><span class="badge bg-danger">` + item.TenDiemQuanTrac + `</span></td><td><a href = "#">` + item.DiaChi + `</a></td></tr>`
                                tablebody += rowtable;
                                break;
                            default:
                                place[i] = L.marker([item.ViDo, item.KinhDo], { icon: warningIcon }).addTo(map);
                                if (item.DsKetQuaQuanTrac != null) {
                                    var text = "<b><i>" + item.TenDiemQuanTrac + ": " + item.DiaChi + "</i></b>";
                                    $.each(item.DsKetQuaQuanTrac, function (i, kq) {
                                        text += "<br/>" + kq.ChiTieu + ": " + kq.GiaTri + ": " + kq.DonViTinh;
                                    });
                                    place[i].bindPopup(text);

                                } else {
                                    place[i].bindPopup(item.TenDiemQuanTrac + ": " +item.DiaChi + "<br/> Chưa có dữ liệu");
                                }
                                var rowtable = `<tr class="rowtable" data-index =` + i + ` ><td><span class="badge bg-warning">` + item.TenDiemQuanTrac + `</span></td><td><a href = "#">` + item.DiaChi + `</a></td></tr>`
                                tablebody += rowtable;
                        }

                    });
                    $("#tbl-dsDiemQuanTrac tbody").html("");
                    $("#tbl-dsDiemQuanTrac tbody").append(tablebody);
                    $(".rowtable").off('click').on('click', function () {
                        var index = $(this).attr("data-index");
                        place[index].openPopup();
                    });
                }
            }
        });
    },
    RegisterEvents: function () {
        var self = this;
        self.LoadDuLieuRaBanDo();
        $('.ddLoaiDiemQuanTrac').on('change', function () {
            self.LoadDuLieuRaBanDo();
        });
        $('#BanDoDiemXa-tab').off('click').on('click', function () {
            self.LoadDuLieuRaBanDo();
        });
        $(document).on('keypress', function (e) {
            if (e.which == 13) {
                self.LoadDuLieuRaBanDo();
            }
        });

    },
}

$(document).ready(function () {
    BanDoCacDiemXaThaiControl.Init();
});


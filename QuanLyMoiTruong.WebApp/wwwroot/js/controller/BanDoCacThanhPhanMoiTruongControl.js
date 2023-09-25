if (typeof (BanDoCacThanhPhanMoiTruongControl) == "undefined") BanDoCacThanhPhanMoiTruongControl = {};
var countDenHan = 0;
var map = L.map('mapid').setView([18.957736, 105.610247], 10);
L.tileLayer('https://tile.openstreetmap.org/{z}/{x}/{y}.png', {
    maxZoom: 19,
    attribution: '<a href="https://dongnam.nghean.gov.vn/" target="_blank">Ban quản lý khu kinh tế Đông Nam</a>'
}).addTo(map);
map.options.minZoom = 9;
map.options.maxZoom = 14;

BanDoCacThanhPhanMoiTruongControl = {
    Init: function () {
        BanDoCacThanhPhanMoiTruongControl.RegisterEvents();
    },
    LoadDanhSachThanhPhanMoiTruong: function () {
        var self = this;
        Get({
            url: localStorage.getItem("API_URL") + "/ThanhPhanMoiTruong/GetAll",
            callback: function (res) {
                if (res.Success) {
                    $.each(res.Data, function (i, item) {
                        $('.ddThanhPhanMoiTruong').append('<option value=' + item.IdThanhPhanMoiTruong + '>' + item.TenThanhPhanMoiTruong + '</option>');
                    })
                    $('.ddThanhPhanMoiTruong').on('change', function () {
                        self.LoadDuLieuRaBanDo();
                    });
                    $('.ddThanhPhanMoiTruong').trigger('change');

                }
            }
        });
    },
    LoadDuLieuRaBanDo: function () {
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
            "url": localStorage.getItem("API_URL") + "/DiemQuanTrac/GetDuLieuLenBanDo",
            data: {
                "idThanhPhanMoiTruong": $('.ddThanhPhanMoiTruong option:selected').val()
            },
            callback: function (res) {
                if (res.Success) {
                    var place = [];
                    var tablebody = "";
                    $.each(res.Data, function (i, item) {
                        switch (item.Loai) {
                            case "K":
                                place[i] = L.marker([item.ViDo, item.KinhDo], { icon: successIcon }).addTo(map);
                                if (item.DsKetQuaQuanTrac != null) {
                                    var text = "<b><i>" + item.TenDiemQuanTrac +": " + item.DiaChi + "</i></b>";
                                    $.each(item.DsKetQuaQuanTrac, function (i, kq) {
                                        text += "<br/>" + kq.ChiTieu + ": " + kq.GiaTri + ": " + kq.DonViTinh;
                                    });
                                    place[i].bindPopup(text);

                                } else {
                                    place[i].bindPopup(item.DiaChi + "<br/> Chưa có dữ liệu");
                                }
                                var rowtable = `<tr class="rowtable" data-index =` + i + ` ><td><span class="badge bg-success">` + item.TenDiemQuanTrac + `</span></td><td><a href = "#">` + item.DiaChi + `</a></td></tr>`
                                tablebody += rowtable;
                                break;
                            case "N":
                                place[i] = L.marker([item.ViDo, item.KinhDo], { icon: dangerIcon }).addTo(map);
                                if (item.DsKetQuaQuanTrac != null) {
                                    var text = "<b><i>" + item.TenDiemQuanTrac + ": " + item.DiaChi + "</i></b>";
                                    $.each(item.DsKetQuaQuanTrac, function (i, kq) {
                                        text += "<br/>" + kq.ChiTieu + ": " + kq.GiaTri + ": " + kq.DonViTinh;
                                    });
                                    place[i].bindPopup(text);

                                } else {
                                    place[i].bindPopup(item.DiaChi + "<br/> Chưa có dữ liệu");
                                }
                                var rowtable = `<tr class="rowtable" data-index =` + i + ` ><td><span class="badge bg-danger">` + item.TenDiemQuanTrac + `</span></td><td><a href = "#">` + item.DiaChi + `</a></td></tr>`
                                tablebody += rowtable;
                                break;
                            case "B":
                                place[i] = L.marker([item.ViDo, item.KinhDo], { icon: primaryIcon }).addTo(map);
                                if (item.DsKetQuaQuanTrac != null) {
                                    var text = "<b><i>" + item.TenDiemQuanTrac + ": " + item.DiaChi + "</i></b>";
                                    $.each(item.DsKetQuaQuanTrac, function (i, kq) {
                                        text += "<br/>" + kq.ChiTieu + ": " + kq.GiaTri + ": " + kq.DonViTinh;
                                    });
                                    place[i].bindPopup(text);

                                } else {
                                    place[i].bindPopup(item.DiaChi + "<br/> Chưa có dữ liệu");
                                }
                                var rowtable = `<tr class="rowtable" data-index =` + i + ` ><td><span class="badge bg-primary">` + item.TenDiemQuanTrac + `</span></td><td><a href = "#">` + item.DiaChi + `</a></td></tr>`
                                tablebody += rowtable;
                                break;
                            case "M":
                                place[i] = L.marker([item.ViDo, item.KinhDo], { icon: infoIcon }).addTo(map);
                                if (item.DsKetQuaQuanTrac != null) {
                                    var text = "<b><i>" + item.TenDiemQuanTrac + ": " + item.DiaChi + "</i></b>";
                                    $.each(item.DsKetQuaQuanTrac, function (i, kq) {
                                        text += "<br/>" + kq.ChiTieu + ": " + kq.GiaTri + ": " + kq.DonViTinh;
                                    });
                                    place[i].bindPopup(text);

                                } else {
                                    place[i].bindPopup(item.DiaChi + "<br/> Chưa có dữ liệu");
                                }
                                var rowtable = `<tr class="rowtable" data-index =` + i + ` ><td><span class="badge bg-info">` + item.TenDiemQuanTrac + `</span></td><td><a href = "#">` + item.DiaChi + `</a></td></tr>`
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
                                    place[i].bindPopup(item.DiaChi + "<br/> Chưa có dữ liệu");
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
        self.LoadDanhSachThanhPhanMoiTruong();
    },
}

$(document).ready(function () {
    BanDoCacThanhPhanMoiTruongControl.Init();
});


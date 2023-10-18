if (typeof (ThanhPhanMoiTruongChiTietControl) == "undefined") ThanhPhanMoiTruongChiTietControl = {};
var url = window.location.pathname;
var id = url.substring(url.lastIndexOf('/') + 1);
ThanhPhanMoiTruongChiTietControl = {
    Init: function () {
        ThanhPhanMoiTruongChiTietControl.RegisterEvents();
    },
    LoadThongTinThanhPhanMoiTruong: function () {
        Get({
            "url": localStorage.getItem("API_URL") + "/ThanhPhanMoiTruong/GetById?idThanhPhanMoiTruong=" + id,
            callback: function (res) {
                if (res.Success) {
                    var dsfile = "";
                    $.each(res.Data.FileTaiLieu, function (j, file) {
                        if (file.LoaiFileTaiLieu == "TongHopKetQuaQuanTrac") {
                            dsfile += ` <div class="row">
                                    <div class="col-lg-3 col-md-4 label">Bảng tổng hợp kết quả quan trắc</div>
                                    <div class="col-lg-9 col-md-8"><a href = "` + localStorage.getItem('API_URL').replace("api", "") + file.File.LinkFile + '" target="_blank">' +  file.File.TenFile + `</a></div>
                                </div>`;
                        } else {
                            dsfile += ` <div class="row">
                                    <div class="col-lg-3 col-md-4 label">Báo cáo kết quả quan trắc</div>
                                    <div class="col-lg-9 col-md-8"><a href = "` + localStorage.getItem('API_URL').replace("api", "") + file.File.LinkFile + '" target="_blank">' +  file.File.TenFile + `</a></div>
                                </div>`;
                        }
                    });
                    $('.pagetitle h1').text(res.Data.TenThanhPhanMoiTruong);
                    $("#thong-tin-tpmt").html("");
                    var html = `<div class="row">
                                    <div class="col-lg-3 col-md-4 label ">Báo cáo quan trắc môi trường</div>
                                    <div class="col-lg-9 col-md-8">`+ res.Data.TenThanhPhanMoiTruong + `</div>
                                </div>
                                <div class="row">
                                    <div class="col-lg-3 col-md-4 label">Năm</div>
                                    <div class="col-lg-9 col-md-8">`+ res.Data.Nam + `</div>
                                </div>
                                <div class="row">
                                    <div class="col-lg-3 col-md-4 label">Lần</div>
                                    <div class="col-lg-9 col-md-8">`+ res.Data.Lan + `</div>
                                </div>
                                <div class="row">
                                    <div class="col-lg-3 col-md-4 label">Ghi chú</div>
                                    <div class="col-lg-9 col-md-8">`+ res.Data.GhiChu + `</div>
                                </div>`;
                    $('#thong-tin-tpmt').append(html);
                    $('#thong-tin-tpmt').append(dsfile);
                }
            }
        });
    },
    LoadKetQuaQuanTrac: function () {
        Get({
            "url": localStorage.getItem("API_URL") + "/KetQuaQuanTrac/GetAllByIdThanhPhanMoiTruong",
            data: {
                "idThanhPhanMoiTruong": id
            },
            callback: function (res) {
                if (res.Success) {
                    var dsKQ = "";
                    $.each(res.Data, function (j, kq) {
                        var stt = j + 1;
                        dsKQ += "<tr><td>" + stt + "</td><td>" + kq.TenDiemQuanTrac + "</td><td>" + kq.ChiTieu + "</td><td>" + kq.GiaTri + "</td><td>" + kq.DonViTinh + "</td></tr>"

                    });
                    //$("#danh-sach-ket-qua-quan-trac").append(dsKQ);

                }
            }
        });
    },
    LoadDuLieuQuanTracKhongKhi: function () {
        Get({
            "url": localStorage.getItem("API_URL") + "/KetQuaQuanTrac/DuLieuQuanTracMoiTruong",
            data: {
                "idThanhPhanMoiTruong": id,
                "loai": "K"
            },
            callback: function (res) {
                if (res.Success) {
                    var dsKQ = "";
                    $.each(res.Data, function (j, kq) {
                        var stt = j + 1;
                        console.log(kq.DsKetQua)
                        dsKQ += "<tr><td>" + stt + "</td><td>" + kq.ChiTieu + "</td><td>" + kq.DonVi + "</td><td style='background-color: #cfe2ff'>" + kq.DsKetQua[0].GiaTri + "</td><td style='background-color: #a2dbdd'>" + kq.DsKetQua[1].GiaTri + "</td><td style= 'background-color: #d1e7dd'>" + kq.DsKetQua[2].GiaTri + "</td><td style= 'background-color: #f8d7da'>" + kq.DsKetQua[3].GiaTri + "</td><td style= 'background-color: #fff3cd'>" + kq.DsKetQua[4].GiaTri + "</td><td style= 'background-color: #cff4fc'>" + kq.DsKetQua[5].GiaTri + "</td><td style='background-color: #acdfb6'>" + kq.DsKetQua[6].GiaTri + "</td><td style='background-color: #dfdaac'>" + kq.DsKetQua[7].GiaTri + "</td><td style='background-color: #ced4da'>" + kq.DsKetQua[8].GiaTri + "</td></tr>"

                    });
                    $("#du-lieu-quan-trac-khong-khi").append(dsKQ);

                }
            }
        });
    },
    LoadDuLieuQuanTracNuocBien: function () {
        Get({
            "url": localStorage.getItem("API_URL") + "/KetQuaQuanTrac/DuLieuQuanTracMoiTruong",
            data: {
                "idThanhPhanMoiTruong": id,
                "loai": "B"
            },
            callback: function (res) {
                if (res.Success) {
                    var dsKQ = "";
                    $.each(res.Data, function (j, kq) {
                        var stt = j + 1;
                        dsKQ += "<tr><td>" + stt + "</td><td>" + kq.ChiTieu + "</td><td>" + kq.DonVi + "</td><td style='background-color: #cfe2ff'>" + kq.DsKetQua[0].GiaTri + "</td><td style='background-color: #a2dbdd'>" + kq.DsKetQua[1].GiaTri + "</td><td style= 'background-color: #d1e7dd'>" + kq.DsKetQua[2].GiaTri + "</td></tr>"

                    });
                    $("#du-lieu-quan-trac-nuoc-bien").append(dsKQ);

                }
            }
        });
    },
    LoadDuLieuQuanTracNuocMat: function () {
        Get({
            "url": localStorage.getItem("API_URL") + "/KetQuaQuanTrac/DuLieuQuanTracMoiTruong",
            data: {
                "idThanhPhanMoiTruong": id,
                "loai": "M"
            },
            callback: function (res) {
                if (res.Success) {
                    var dsKQ = "";
                    $.each(res.Data, function (j, kq) {
                        var stt = j + 1;
                        console.log(kq.DsKetQua)
                        dsKQ += "<tr><td>" + stt + "</td><td>" + kq.ChiTieu + "</td><td>" + kq.DonVi + "</td><td style='background-color: #cfe2ff'>" + kq.DsKetQua[0].GiaTri + "</td><td style='background-color: #a2dbdd'>" + kq.DsKetQua[1].GiaTri + "</td><td style= 'background-color: #d1e7dd'>" + kq.DsKetQua[2].GiaTri + "</td><td style= 'background-color: #f8d7da'>" + kq.DsKetQua[3].GiaTri + "</td><td style= 'background-color: #fff3cd'>" + kq.DsKetQua[4].GiaTri + "</td><td style= 'background-color: #cff4fc'>" + kq.DsKetQua[5].GiaTri + "</td><td style='background-color: #acdfb6'>" + kq.DsKetQua[6].GiaTri + "</td></tr>"

                    });
                    $("#du-lieu-quan-trac-nuoc-mat").append(dsKQ);

                }
            }
        });
    },
    LoadDuLieuQuanTracNuocNgam: function () {
        Get({
            "url": localStorage.getItem("API_URL") + "/KetQuaQuanTrac/DuLieuQuanTracMoiTruong",
            data: {
                "idThanhPhanMoiTruong": id,
                "loai": "N"
            },
            callback: function (res) {
                if (res.Success) {
                    var dsKQ = "";
                    $.each(res.Data, function (j, kq) {
                        var stt = j + 1;
                        console.log(kq.DsKetQua)
                        dsKQ += "<tr><td>" + stt + "</td><td>" + kq.ChiTieu + "</td><td>" + kq.DonVi + "</td><td style='background-color: #cfe2ff'>" + kq.DsKetQua[0].GiaTri + "</td><td style='background-color: #a2dbdd'>" + kq.DsKetQua[1].GiaTri + "</td>></tr>"

                    });
                    $("#du-lieu-quan-trac-nuoc-ngam").append(dsKQ);

                }
            }
        });
    },
    RegisterEvents: function () {
        var self = this;
        self.LoadThongTinThanhPhanMoiTruong();
        self.LoadKetQuaQuanTrac();
        self.LoadDuLieuQuanTracKhongKhi();
        self.LoadDuLieuQuanTracNuocBien();
        self.LoadDuLieuQuanTracNuocMat();
        self.LoadDuLieuQuanTracNuocNgam();
    },
}

$(document).ready(function () {
    ThanhPhanMoiTruongChiTietControl.Init();
});


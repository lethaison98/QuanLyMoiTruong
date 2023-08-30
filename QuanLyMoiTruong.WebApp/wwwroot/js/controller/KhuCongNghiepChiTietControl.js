if (typeof (KhuCongNghiepChiTietControl) == "undefined") KhuCongNghiepChiTietControl = {};
var url = window.location.pathname;
var id = url.substring(url.lastIndexOf('/') + 1);
KhuCongNghiepChiTietControl = {
    Init: function () {
        KhuCongNghiepChiTietControl.LoadThongTinKhuCongNghiep();
        KhuCongNghiepChiTietControl.RegisterEvents();
    },
    LoadThongTinKhuCongNghiep: function () {
        Get({
            "url": localStorage.getItem("API_URL") + "/KhuCongNghiep/GetById?idKhuCongNghiep=" + id,
            callback: function (res) {
                if (res.Success) {
                    var dsDuAn = "";
                    $.each(res.Data.DsDuAn, function (j, duan) {
                        var stt = j + 1;
                        dsDuAn += "<tr><td>" + stt + "</td><td>" + duan.TenDuAn + "</td><td>" + duan.TenDoanhNghiep + "</td><td>" + duan.DiaChi + "</td><td>" + duan.GhiChu + "</td></tr>"
                                
                    });
                    $('.pagetitle h1').text(res.Data.TenKhuCongNghiep);
                    $("#thong-tin-kcn").html("");
                    var html = `<div class="row">
                                    <div class="col-lg-3 col-md-4 label ">Tên khu công nghiệp</div>
                                    <div class="col-lg-9 col-md-8">`+ res.Data.TenKhuCongNghiep + `</div>
                                </div>

                                <div class="row">
                                    <div class="col-lg-3 col-md-4 label">Tên chủ đầu tư</div>
                                    <div class="col-lg-9 col-md-8">`+ res.Data.TenChuDauTu + `</div>
                                </div>

                                <div class="row">
                                    <div class="col-lg-3 col-md-4 label">Địa chỉ</div>
                                    <div class="col-lg-9 col-md-8">`+ res.Data.DiaDiem + `</div>
                                </div>

                                <div class="row">
                                    <div class="col-lg-3 col-md-4 label">Trong khu kinh tế Đông Nam</div>
                                    <div class="col-lg-9 col-md-8">`+ (res.Data.ThuocKhuKinhTe == true?"Có" : "Không") + `</div>
                                </div>
                                <div class="row">
                                    <div class="col-lg-3 col-md-4 label">Ghi chú</div>
                                    <div class="col-lg-9 col-md-8">`+ res.Data.GhiChu + `</div>
                                </div>
                                <div class="row">
                                    <div class="col-lg-3 col-md-4 label">Danh sách dự án</div>
                                </div>`;
                    $("#thong-tin-kcn").append(html);
                    $("#danh-sach-du-an").append(dsDuAn);
                }
            }
        });
    },
    RegisterEvents: function () {
        var self = this;
        $('#ThongTinKCN-tab').trigger('click');
        $('#ThongTinKCN-tab').off('click').on('click', function () {
            self.LoadThongTinKhuCongNghiep();
        });
        $('#GiayPhepMoiTruong-tab').off('click').on('click', function () {
            GiayPhepMoiTruongControl.RegisterEventsGiayPhepMoiTruong();
        });
        $('#BaoCaoBaoVeMoiTruong-tab').off('click').on('click', function () {
            BaoCaoBaoVeMoiTruongControl.RegisterEventsBaoCaoBaoVeMoiTruong();
        });
        $('#BaoCaoQuanTracMoiTruong-tab').off('click').on('click', function () {
            BaoCaoQuanTracMoiTruongControl.RegisterEventsBaoCaoQuanTracMoiTruong();
        });
        $('#HoSoKiemTraXuPhat-tab').off('click').on('click', function () {
            HoSoKiemTraXuPhatControl.RegisterEventsHoSoKiemTraXuPhat();
        });
    },
}

$(document).ready(function () {
    KhuCongNghiepChiTietControl.Init();
});


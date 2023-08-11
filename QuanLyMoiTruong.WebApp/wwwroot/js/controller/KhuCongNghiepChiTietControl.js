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
                    $('.pagetitle h1').text(res.Data.TenKhuCongNghiep);
                    $("#thong-tin-kcn").html("");
                    var html = `<div class="row">
                                    <div class="col-lg-3 col-md-4 label ">Tên khu công nghiệp</div>
                                    <div class="col-lg-9 col-md-8" data-name="TenDuAn">`+ res.Data.TenKhuCongNghiep + `</div>
                                </div>

                                <div class="row">
                                    <div class="col-lg-3 col-md-4 label">Tên chủ đầu tư</div>
                                    <div class="col-lg-9 col-md-8" data-name="TenDuAn">`+ res.Data.TenChuDauTu + `</div>
                                </div>

                                <div class="row">
                                    <div class="col-lg-3 col-md-4 label">Địa chỉ</div>
                                    <div class="col-lg-9 col-md-8">`+ res.Data.DiaDiem + `</div>
                                </div>

                                <div class="row">
                                    <div class="col-lg-3 col-md-4 label">Ghi chú</div>
                                    <div class="col-lg-9 col-md-8">`+ res.Data.GhiChu + `</div>
                                </div>`;
                    $("#thong-tin-kcn").append(html);
                }
            }
        });
    },
    RegisterEvents: function () {
        var self = this;
        $('#ThongTinDuAn-tab').trigger('click');
        $('#ThongTinDuAn-tab').off('click').on('click', function () {
            self.LoadThongTinKhuCongNghiep();
        });
        $('#BaoCaoBaoVeMoiTruongKCN-tab').off('click').on('click', function () {
            BaoCaoBaoVeMoiTruongKCNControl.RegisterEventsBaoCaoBaoVeMoiTruongKCN();
        });
        $('#BaoCaoQuanTracMoiTruong-tab').off('click').on('click', function () {
            BaoCaoQuanTracMoiTruongControl.RegisterEventsBaoCaoQuanTracMoiTruong();
        });
    },
}

$(document).ready(function () {
    KhuCongNghiepChiTietControl.Init();
});


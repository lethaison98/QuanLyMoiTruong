if (typeof (DuAnChiTietControl) == "undefined") DuAnChiTietControl = {};
var url = window.location.pathname;
var id = url.substring(url.lastIndexOf('/') + 1);
DuAnChiTietControl = {
    Init: function () {
        DuAnChiTietControl.LoadThongTinDuAn();
        DuAnChiTietControl.RegisterEvents();
    },
    LoadThongTinDuAn: function () {
        Get({
            "url": localStorage.getItem("API_URL") + "/DuAn/GetById?idDuAn=" + id,
            callback: function (res) {
                if (res.Success) {
                    $('.pagetitle h1').text(res.Data.TenDuAn);
                    $("#thong-tin-du-an").html("");
                    var html = `<div class="row">
                                    <div class="col-lg-3 col-md-4 label ">Tên dự án/cơ sở</div>
                                    <div class="col-lg-9 col-md-8" data-name="TenDuAn">`+ res.Data.TenDuAn + `</div>
                                </div>

                                <div class="row">
                                    <div class="col-lg-3 col-md-4 label">Chủ đầu tư</div>
                                    <div class="col-lg-9 col-md-8" data-name="TenDoanhNghiep">`+ res.Data.TenDoanhNghiep + `</div>
                                </div>
                                <div class="row">
                                    <div class="col-lg-3 col-md-4 label">Địa chỉ</div>
                                    <div class="col-lg-9 col-md-8">`+ res.Data.DiaChi + `</div>
                                </div>

                                <div class="row">
                                    <div class="col-lg-3 col-md-4 label">Tên người đại diện</div>
                                    <div class="col-lg-9 col-md-8">`+ res.Data.TenNguoiDaiDien + `</div>
                                </div>

                                <div class="row">
                                    <div class="col-lg-3 col-md-4 label">Tên người phụ trách môi trường</div>
                                    <div class="col-lg-9 col-md-8">`+ res.Data.TenNguoiPhuTrachTNMT + `</div>
                                </div>

                                <div class="row">
                                    <div class="col-lg-3 col-md-4 label">Giấy phép đăng ký kinh doanh</div>
                                    <div class="col-lg-9 col-md-8">`+ res.Data.GiayPhepDKKD + `</div>
                                </div>
                                <div class="row">
                                    <div class="col-lg-3 col-md-4 label">Thuộc khu công nghiệp</div>
                                    <div class="col-lg-9 col-md-8" data-name="TenDuAn">`+ (res.Data.TenKhuCongNghiep == "" ? "Không" : res.Data.TenKhuCongNghiep) + `</div>
                                </div>
                                <div class="row">
                                    <div class="col-lg-3 col-md-4 label">Trong khu kinh tế Đông Nam</div>
                                    <div class="col-lg-9 col-md-8" data-name="TenDuAn">`+ (res.Data.ThuocKhuKinhte == false ? "Không" : "Có") + `</div>
                                </div>
                                <div class="row">
                                    <div class="col-lg-3 col-md-4 label">Ghi chú</div>
                                    <div class="col-lg-9 col-md-8">`+ res.Data.GhiChu + `</div>
                                </div>`;
                    $("#thong-tin-du-an").append(html);
                }
            }
        });
    },
    RegisterEvents: function () {
        var self = this;
        $('#ThongTinDuAn-tab').trigger('click');
        $('#ThongTinDuAn-tab').off('click').on('click', function () {
            self.LoadThongTinDuAn();
        });
        $('#GiayPhepMoiTruong-tab').off('click').on('click', function () {
            GiayPhepMoiTruongControl.RegisterEventsGiayPhepMoiTruong();
        });
        $('#BaoCaoBaoVeMoiTruong-tab').off('click').on('click', function () {
            BaoCaoBaoVeMoiTruongControl.RegisterEventsBaoCaoBaoVeMoiTruong();
        });
        $('#HoSoKiemTraXuPhat-tab').off('click').on('click', function () {
            HoSoKiemTraXuPhatControl.RegisterEventsHoSoKiemTraXuPhat();
        });
    },
}

$(document).ready(function () {
    DuAnChiTietControl.Init();
});


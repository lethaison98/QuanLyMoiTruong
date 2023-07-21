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
                    $('.pagetitle h1').text(res.Data.TenDuAn + " - " + res.Data.TenDoanhNghiep);
                }
            }
        });
    },
    RegisterEvents: function () {
        $('#GiayPhepMoiTruong-tab').trigger('click');
        $('#GiayPhepMoiTruong-tab').off('click').on('click', function () {
            GiayPhepMoiTruongControl.RegisterEventsGiayPhepMoiTruong();
        });
        $('#BaoCaoBaoVeMoiTruongHangNam-tab').off('click').on('click', function () {
            BaoCaoBaoVeMoiTruongHangNamControl.RegisterEventsBaoCaoBaoVeMoiTruongHangNam();
        });
    },
}

$(document).ready(function () {
    DuAnChiTietControl.Init();
});


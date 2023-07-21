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
                }
            }
        });
    },
    RegisterEvents: function () {
        $('#BaoCaoBaoVeMoiTruongKCN-tab').trigger('click');

        $('#BaoCaoBaoVeMoiTruongKCN-tab').off('click').on('click', function () {
            BaoCaoBaoVeMoiTruongKCNControl.RegisterEventsBaoCaoBaoVeMoiTruongKCN();
        });
        $('#GiayPhepMoiTruong-tab').off('click').on('click', function () {
            GiayPhepMoiTruongControl.RegisterEventsGiayPhepMoiTruong();
        });
    },
}

$(document).ready(function () {
    KhuCongNghiepChiTietControl.Init();
});


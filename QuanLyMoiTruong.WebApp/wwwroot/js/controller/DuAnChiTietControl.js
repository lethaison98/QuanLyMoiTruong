if (typeof (DuAnChiTietControl) == "undefined") DuAnChiTietControl = {};
var url = window.location.pathname;
var id = url.substring(url.lastIndexOf('/') + 1);
DuAnChiTietControl = {
    Init: function () {
        DuAnChiTietControl.RegisterEvents();
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


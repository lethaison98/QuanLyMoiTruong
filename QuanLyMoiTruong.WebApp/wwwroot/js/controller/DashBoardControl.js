if (typeof (DashBoardControl) == "undefined") DashBoardControl = {};
var countDenHan = 0;
DashBoardControl = {
    Init: function () {
        DashBoardControl.RegisterEvents();
    },
    RegisterEvents: function () {
        var self = this;
        self.LoadSoLuongDuAn();
        self.LoadSoLuongKhuCongNghiep();
        self.LoadSoLuongThanhPhanMoiTruong();
    },
    LoadSoLuongDuAn: function () {
        Get({
            url: localStorage.getItem("API_URL") + '/DuAn/GetAllPaging',
            callback: function (res) {
                if (res.Success) {
                    $("#count-DuAn").text(res.Data.TotalCount);
                }
            }
        });
    },
    LoadSoLuongKhuCongNghiep: function () {
        Get({
            url: localStorage.getItem("API_URL") + '/KhuCongNghiep/GetAllPaging',
            callback: function (res) {
                if (res.Success) {
                    $("#count-KhuCongNghiep").text(res.Data.TotalCount);
                }
            }
        });
    },
    LoadSoLuongThanhPhanMoiTruong: function () {
        Get({
            url: localStorage.getItem("API_URL") + '/ThanhPhanMoiTruong/GetAllPaging',
            callback: function (res) {
                if (res.Success) {
                    console.log(res);
                    $("#count-ThanhPhanMoiTruong").text(res.Data.TotalCount);
                }
            }
        });
    },
    LoadSoLuongBaoCao: function () {
        //Get({
        //    url: localStorage.getItem("API_URL") + '/DuAn/GetAllPaging',
        //    callback: function (res) {
        //        if (res.Success) {
        //            console.log(res);
        //            $("#count-BaoCao").text(res.Data.TotalCount);
        //        }
        //    }
        //});
    },
}

$(document).ready(function () {
    DashBoardControl.Init();
});


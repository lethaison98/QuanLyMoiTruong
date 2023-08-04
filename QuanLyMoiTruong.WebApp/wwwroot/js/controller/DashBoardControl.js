if (typeof (DashBoardControl) == "undefined") DashBoardControl = {};
var countDenHan = 0;
DashBoardControl = {
    Init: function () {
        DashBoardControl.RegisterEvents();
    },
    RegisterEvents: function () {
        Get({
            "url": localStorage.getItem("API_URL") + "/DuAn/GetAll",
            callback: function (res) {
                if (res.Success) {
                    console.log(res);
                }
                else {
                    console.log('Có lỗi xảy ra')
                }
            }
        });
    },
}

$(document).ready(function () {
    DashBoardControl.Init();
});


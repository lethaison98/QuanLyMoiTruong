if (typeof (DashBoardControl) == "undefined") DashBoardControl = {};
var countDenHan = 0;
DashBoardControl = {
    Init: function () {
        DashBoardControl.RegisterEvents();
    },
    RegisterEvents: function () {
    },
}

$(document).ready(function () {
    DashBoardControl.Init();
});


if (typeof (BaoCaoThongKeNguonThaiChiTietControl) == "undefined") BaoCaoThongKeNguonThaiChiTietControl = {};
var url = window.location.pathname;
var id = url.substring(url.lastIndexOf('/') + 1);
BaoCaoThongKeNguonThaiChiTietControl = {
    Init: function () {
        BaoCaoThongKeNguonThaiChiTietControl.RegisterEvents();
    },
    LoadThongTinBaoCaoThongKeNguonThai: function () {
        Get({
            "url": localStorage.getItem("API_URL") + "/BaoCaoThongKeNguonThai/GetById?idBaoCaoThongKeNguonThai=" + id,
            callback: function (res) {
                if (res.Success) {
                    var dsfile = "";
                    $.each(res.Data.FileTaiLieu, function (j, file) {
                        if (file.LoaiFileTaiLieu == "TongHopSoLieuThongKeNguonThai") {
                            dsfile += ` <div class="row">
                                    <div class="col-lg-3 col-md-4 label">Bảng tổng hợp kết quả quan trắc</div>
                                    <div class="col-lg-9 col-md-8"><a href = "` + localStorage.getItem('API_URL').replace("api", "") + file.File.LinkFile + '" target="_blank">' + file.File.TenFile + `</a></div>
                                </div>`;
                        } else {
                            dsfile += ` <div class="row">
                                    <div class="col-lg-3 col-md-4 label">Báo cáo kết quả quan trắc</div>
                                    <div class="col-lg-9 col-md-8"><a href = "` + localStorage.getItem('API_URL').replace("api", "") + file.File.LinkFile + '" target="_blank">' + file.File.TenFile + `</a></div>
                                </div>`;
                        }
                    });
                    $('.pagetitle h1').text(res.Data.TenBaoCaoThongKeNguonThai);
                    $("#thong-tin-tpmt").html("");
                    var html = `<div class="row">
                                    <div class="col-lg-3 col-md-4 label ">Tên báo cáo</div>
                                    <div class="col-lg-9 col-md-8">`+ res.Data.TenBaoCao + `</div>
                                </div>
                                <div class="row">
                                    <div class="col-lg-3 col-md-4 label">Năm</div>
                                    <div class="col-lg-9 col-md-8">`+ res.Data.Nam + `</div>
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
    LoadKetQuaThongKeNguonThai: function () {
        Get({
            "url": localStorage.getItem("API_URL") + "/BaoCaoThongKeNguonThai/GetKetQuaThongKeNguonThaiByIdBaoCao",
            data: {
                "idBaoCaoThongKeNguonThai": id
            },
            callback: function (res) {
                if (res.Success) {
                    var dsKQ = "";
                    $.each(res.Data, function (j, kq) {
                        var stt = j + 1;
                        if (stt != res.Data.length) {
                            dsKQ += "<tr><td>" + stt + "</td><td>" + kq.TenKhuCongNghiep + "</td><td style='background-color:#edd7c9; text-align:right'>" + kq.NuocThaiSinhHoat + "</td><td style='background-color: #cfe2ff; text-align:right'>" + kq.NuocThaiSanXuat + "</td><td style='background-color: #a2dbdd; text-align:right'>" + kq.NuocThaiTaiSuDung + "</td><td style= 'background-color: #d1e7dd; text-align:right'>" + kq.LuuLuongDauNoi + "</td><td style= 'background-color: #f8d7da; text-align:right'>" + kq.KhiThai + "</td><td style= 'background-color: #fff3cd; text-align:right'>" + kq.ChatThaiRanSinhHoat + "</td><td style= 'background-color: #cff4fc; text-align:right'>" + kq.ChatThaiRanSanXuat + "</td><td style='background-color: #acdfb6; text-align:right'>" + kq.ChatThaiRanTaiSuDung + "</td><td style='background-color: #dfdaac; text-align:right'>" + kq.TongChatThaiRan + "</td><td style='background-color: #ced4da; text-align:right'>" + kq.ChatThaiNguyHai + "</td></tr>"
                        } else {
                            dsKQ += "<tr style='font-weight: bold'><td></td><td>" + kq.TenKhuCongNghiep + "</td><td style='background-color:#edd7c9; text-align:right'>" + kq.NuocThaiSinhHoat + "</td><td style='background-color: #cfe2ff; text-align:right'>" + kq.NuocThaiSanXuat + "</td><td style='background-color: #a2dbdd; text-align:right'>" + kq.NuocThaiTaiSuDung + "</td><td style= 'background-color: #d1e7dd; text-align:right'>" + kq.LuuLuongDauNoi + "</td><td style= 'background-color: #f8d7da; text-align:right'>" + kq.KhiThai + "</td><td style= 'background-color: #fff3cd; text-align:right'>" + kq.ChatThaiRanSinhHoat + "</td><td style= 'background-color: #cff4fc; text-align:right'>" + kq.ChatThaiRanSanXuat + "</td><td style='background-color: #acdfb6; text-align:right'>" + kq.ChatThaiRanTaiSuDung + "</td><td style='background-color: #dfdaac; text-align:right'>" + kq.TongChatThaiRan + "</td><td style='background-color: #ced4da; text-align:right'>" + kq.ChatThaiNguyHai + "</td></tr>"
                        }

                    });
                    $("#danh-sach-ket-qua-thong-ke-nguon-thai").append(dsKQ);

                }
            }
        });
    },

    RegisterEvents: function () {
        var self = this;
        self.LoadThongTinBaoCaoThongKeNguonThai();
        self.LoadKetQuaThongKeNguonThai();
    },
}

$(document).ready(function () {
    BaoCaoThongKeNguonThaiChiTietControl.Init();
});


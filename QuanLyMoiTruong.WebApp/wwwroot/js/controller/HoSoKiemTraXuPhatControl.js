if (typeof (HoSoKiemTraXuPhatControl) == "undefined") HoSoKiemTraXuPhatControl = {};
var url = window.location.pathname;
var type = "";
var idDuAn = 0;
var idKhuCongNghiep = 0;
if (url.indexOf("DuAn") != -1) {
    idDuAn = url.substring(url.lastIndexOf('/') + 1);
    type = "DuAn";
}
if (url.indexOf("KhuCongNghiep") != -1) {
    idKhuCongNghiep = url.substring(url.lastIndexOf('/') + 1);
    type = "KhuCongNghiep";
}
HoSoKiemTraXuPhatControl = {
    Init: function () {
        HoSoKiemTraXuPhatControl.RegisterEventsHoSoKiemTraXuPhat();
    },
    DrawIconFile: function (linkfile) {
        var iconfile = "";
        if (linkfile.split('.').pop() == "pdf") {
            iconfile = '<i class="fa fa-file-pdf">&nbsp;</i>';
        } else if (linkfile.split('.').pop() == "xls" || linkfile.split('.').pop() == "xlsx") {
            iconfile = '<i class="fa fa-file-excel">&nbsp;</i>';

        } else if (linkfile.split('.').pop() == "doc" || linkfile.split('.').pop() == "docx") {
            iconfile = '<i class="fa fa-file-word">&nbsp;</i>';
        } else {
            iconfile = '<i class="fa fa-file">&nbsp;</i>';
        }
        return iconfile;
    },

    LoadDanhSachHoSoKiemTraXuPhat: function () {
        var self = this;
        var urlApi = "";
        if (type == "DuAn") {
            urlApi = localStorage.getItem("API_URL") + "/HoSoKiemTraXuPhat/GetHoSoKiemTraXuPhatByDuAn?idDuAn=" + idDuAn;
        };
        if (type == "KhuCongNghiep") {
            urlApi = localStorage.getItem("API_URL") + "/HoSoKiemTraXuPhat/GetHoSoKiemTraXuPhatByKhuCongNghiep?idKhuCongNghiep=" + idKhuCongNghiep;
        };
        var $tab = $('#bordered-HoSoKiemTraXuPhat');
        var $popup = $('#popup-form-ho-so-kiem-tra-xu-phat');
        $tab.find("#accordion-HoSoKiemTraXuPhat").html('');
        Get({
            "url": urlApi,
            callback: function (res) {
                if (res.Success) {
                    $.each(res.Data, function (i, value) {
                        var html1 = "";
                        var html2 = "";
                        var html3 = "";
                        var html4 = "";
                        var html5 = "";
                        $.each(value.FileTaiLieu, function (j, file) {
                            switch (file.LoaiFileTaiLieu) {
                                case "HoSoKiemTraXuPhat":
                                    var iconfile = self.DrawIconFile(file.File.LinkFile);
                                    html1 += '<span class="pt-1"><a href = "' + localStorage.getItem('API_URL').replace("api", "") + file.File.LinkFile + '">' + iconfile + file.File.TenFile + '</a></span>';
                                    break;
                                default:
                                    var iconfile = self.DrawIconFile(file.File.LinkFile);
                                    html5 += '<span class="pt-1"><a href = "' + localStorage.getItem('API_URL').replace("api", "") + file.File.LinkFile + '">' + iconfile + file.File.TenFile + '</a></span>';
                            }

                        });
                        var tag = `<div class="accordion-item">
                                        <h2 class="accordion-header" id="heading_`+ value.IdHoSoKiemTraXuPhat + `">
                                            <button class="accordion-button" type="button" data-bs-toggle="collapse" data-bs-target="#collapse_`+ value.IdHoSoKiemTraXuPhat + `" aria-expanded="true" aria-controls="collapse_` + value.IdHoSoKiemTraXuPhat + `">
                                                `+ value.TenHoSo + `</button>
                                        </h2>
                                        <div id="collapse_` + value.IdHoSoKiemTraXuPhat + `" class="accordion-collapse collapse show" aria-labelledby="heading_` + value.IdHoSoKiemTraXuPhat + `" data-bs-parent="#accordionExample">
                                            <div class="accordion-body">
                                                <strong>1. Hồ sơ thanh kiểm tra, xử phạt vi phạm hành chính lĩnh vực bảo vệ môi trường</strong>
                                                <p class="row">`+ html1 + `
                                                </p>
                                                <hr>
                                                <strong>2. Tài liệu khác</strong>
                                                <p class="row">`+ html5 + `
                                                </p>
                                            </div>
                                            <div  style="float:right">
                                                <button type="button" class="btn btn-secondary btn-sm btn-sua" data-id=`+ value.IdHoSoKiemTraXuPhat + `>Chỉnh sửa</button>
                                                <button type="button" class="btn btn-danger btn-sm btn-xoa " data-id=`+ value.IdHoSoKiemTraXuPhat + `>Xóa</button>
                                            </div>
                                            </br><hr>
                                        </div>
                                    </div>`
                        $tab.find("#accordion-HoSoKiemTraXuPhat").append(tag);
                        $tab.find('.btn-sua').off('click').on('click', function () {
                            var idGiayPhep = $(this).attr("data-id");
                            $popup.modal('show');
                            self.ResetPopup();
                            Get({
                                "url": localStorage.getItem("API_URL") + "/HoSoKiemTraXuPhat/GetById?idHoSoKiemTraXuPhat=" + idGiayPhep,
                                callback: function (res) {
                                    if (res.Success) {
                                        $popup.find('.modal-header').text("Chỉnh sửa Hồ sơ thanh kiểm tra, xử phạt vi phạm hành chính lĩnh vực bảo vệ môi trường")
                                        FillFormData('#FormHoSoKiemTraXuPhat', res.Data);

                                        $.each(res.Data.FileTaiLieu, function (i, item) {
                                            var $tr = $popup.find("#tempFileTable").html();
                                            $popup.find("#tblFileHoSoKiemTraXuPhat tbody").append($tr);
                                            $popup.find("#tblFileHoSoKiemTraXuPhat tbody tr:last").find('[data-name="MoTa"]').val(item.MoTa);
                                            $popup.find("#tblFileHoSoKiemTraXuPhat tbody tr:last").find('[data-name="LoaiFileTaiLieu"]').val(item.LoaiFileTaiLieu);
                                            $popup.find("#tblFileHoSoKiemTraXuPhat tbody tr:last td:first").append('<a href = "' + localStorage.getItem('API_URL').replace("api", "") + item.File.LinkFile + '" data-id="' + item.IdFileTaiLieu + '" data-IdFile = "' + item.IdFile + '">' + item.File.TenFile + '</a>');
                                            $popup.find(".tr-remove").off('click').on('click', function () {
                                                $(this).parents('tr:first').remove();
                                            });
                                        });
                                        self.RegisterEventsPopupHoSoKiemTraXuPhat();

                                    }
                                }
                            });
                        });
                        $tab.find('.btn-xoa').off('click').on('click', function () {
                            var idGiayPhep = $(this).attr("data-id");
                            if (confirm("Xác nhận xóa?") == true) {
                                Delete({
                                    "url": localStorage.getItem("API_URL") + "/HoSoKiemTraXuPhat/Delete?idHoSoKiemTraXuPhat=" + idGiayPhep,
                                    callback: function (res) {
                                        if (res.Success) {
                                            toastr.success('Thực hiện thành công', 'Thông báo')
                                            self.LoadDanhSachHoSoKiemTraXuPhat();
                                        } else {
                                            toastr.error(res.Message, 'Có lỗi xảy ra')
                                        }
                                    }
                                });
                            }
                        });
                    });
                }
                else {
                    toastr.error(res.Message, 'Có lỗi xảy ra')
                }
            }
        });
    },
    ResetPopup: function () {
        var $popup = $('#popup-form-ho-so-kiem-tra-xu-phat');
        ResetForm("#FormHoSoKiemTraXuPhat");
        $popup.find('[data-name="IdHoSoKiemTraXuPhat"]').val(0);
        $popup.find("#tblFileHoSoKiemTraXuPhat tbody").html('');
    },
    LoadChiTietHoSoKiemTraXuPhat: function () {
        var self = this;
        var $popup = $('#popup-form-ho-so-kiem-tra-xu-phat');
        Get({
            "url": localStorage.getItem("API_URL") + "/HoSoKiemTraXuPhat/GetById?id=" + idGiayPhep,
            callback: function (res) {
            }
        });
    },
    InsertUpdateHoSoKiemTraXuPhat: function () {
        var self = this;
        var isValidate = ValidateForm($('#FormHoSoKiemTraXuPhat'));
        if (isValidate) {
            var $popup = $('#popup-form-ho-so-kiem-tra-xu-phat');
            var data = LoadFormData("#FormHoSoKiemTraXuPhat");
            var listFileTaiLieu = [];
            $('#tblFileHoSoKiemTraXuPhat tbody tr').each(function () {
                var $tr = $(this);
                listFileTaiLieu.push({
                    IdFileTaiLieu: $(this).find("td:first a").attr("data-id"),
                    IdFile: $(this).find("td:first a").attr("data-idfile"),
                    NhomTaiLieu: "HoSoKiemTraXuPhat",
                    LoaiFileTaiLieu: $(this).find('[data-name="LoaiFileTaiLieu"] option:selected').val(),
                    MoTa: $(this).find('[data-name="MoTa"]').val(),
                })

            });
            data.IdDuAn = idDuAn;
            data.IdKhuCongNghiep = idKhuCongNghiep;
            data.FileTaiLieu = listFileTaiLieu;
            Post({
                "url": localStorage.getItem("API_URL") + "/HoSoKiemTraXuPhat/InsertUpdate",
                "data": data,
                callback: function (res) {
                    if (res.Success) {
                        toastr.success('Thực hiện thành công', 'Thông báo')
                        self.LoadDanhSachHoSoKiemTraXuPhat();
                        $popup.find('.btn-danger').trigger('click');
                    }
                    else {
                        toastr.error(res.Message, 'Có lỗi xảy ra')
                    }
                }
            });
        }
    },

    RegisterEventsHoSoKiemTraXuPhat: function () {
        var self = this;
        var $tab = $('#bordered-HoSoKiemTraXuPhat');
        var $popup = $('#popup-form-ho-so-kiem-tra-xu-phat');
        $tab.find('#btnCreateHoSoKiemTraXuPhat').off('click').on('click', function () {
            $popup.modal('show');
            $popup.find('.modal-header').text("Thêm mới Hồ sơ thanh kiểm tra, xử phạt vi phạm hành chính lĩnh vực bảo vệ môi trường");
            self.ResetPopup();
            self.RegisterEventsPopupHoSoKiemTraXuPhat();
        });
        self.LoadDanhSachHoSoKiemTraXuPhat();
    },
    RegisterEventsPopupHoSoKiemTraXuPhat: function () {
        var self = this;
        var $popup = $('#popup-form-ho-so-kiem-tra-xu-phat');

        // Phần xử lý file
        $popup.find('#btnSelectFileHoSoKiemTraXuPhat').off('click').on('click', function () {
            $popup.find('#fileHoSoKiemTraXuPhat').trigger("click");
        });

        if ($popup.find('#fileHoSoKiemTraXuPhat').length > 0) {
            $popup.find('#fileHoSoKiemTraXuPhat')[0].value = "";
            $popup.find('#fileHoSoKiemTraXuPhat').off('change').on('change', function (e) {
                var $this = this;
                var file = $popup.find('#fileHoSoKiemTraXuPhat')[0].files.length > 0 ? $popup.find('#fileHoSoKiemTraXuPhat')[0].files : null;
                if (file != null) {
                    var dataFile = new FormData();
                    if (type == "DuAn") {
                        dataFile.append("NhomTaiLieu", "DuAn");
                    } else {
                        dataFile.append("NhomTaiLieu", "KhuCongNghiep");
                    }
                    dataFile.append("IdTaiLieu", id);
                    for (var i = 0; i < file.length; i++) {
                        dataFile.append("File", file[i]);
                    }
                    $.ajax({
                        url: localStorage.getItem("API_URL") + "/File/UploadFile",
                        type: "POST",
                        headers: {
                            'Authorization': 'Bearer ' + localStorage.getItem("ACCESS_TOKEN")
                        },
                        cache: false,
                        contentType: false,
                        processData: false,
                        data: dataFile,
                        success: function (res) {
                            if (res.Success) {
                                for (var i = 0; i < res.Data.length; i++) {
                                    var $tr = $popup.find("#tempFileTable").html();
                                    $popup.find("#tblFileHoSoKiemTraXuPhat tbody").append($tr);
                                    $popup.find("#tblFileHoSoKiemTraXuPhat tbody tr:last td:first").append('<a href = "#" data-id="0" data-IdFile = "' + res.Data[i] + '">' + file[i].name + '</a>');
                                    $popup.find(".tr-remove").off('click').on('click', function () {
                                        $(this).parents('tr:first').remove();
                                    });
                                }
                            } else {
                                alert("Upload không thành công");
                            }
                        }
                    });
                }
            });
        }
        //Hết phần xử lý file

        $popup.find(".btn-primary").off('click').on('click', function () {
            self.InsertUpdateHoSoKiemTraXuPhat();
        });
    },
}

$(document).ready(function () {
    HoSoKiemTraXuPhatControl.Init();
});


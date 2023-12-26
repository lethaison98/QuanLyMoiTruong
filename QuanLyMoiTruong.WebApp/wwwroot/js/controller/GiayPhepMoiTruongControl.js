if (typeof (GiayPhepMoiTruongControl) == "undefined") GiayPhepMoiTruongControl = {};
var url = window.location.pathname;
var id = url.substring(url.lastIndexOf('/') + 1);
var type = "";
if (url.indexOf("DuAn") != -1) {
    idDuAn = url.substring(url.lastIndexOf('/') + 1);
    type = "DuAn";
}
if (url.indexOf("KhuCongNghiep") != -1) {
    idKhuCongNghiep = url.substring(url.lastIndexOf('/') + 1);
    type = "KhuCongNghiep";
}
GiayPhepMoiTruongControl = {
    Init: function () {
        GiayPhepMoiTruongControl.RegisterEventsGiayPhepMoiTruong();
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

    LoadDanhSachGiayPhepMoiTruong: function () {
        var self = this;
        var $popup = $('#popup-form-giay-phep-moi-truong');
        var $tab = $('#bordered-GiayPhepMoiTruong');
        $tab.find('#accordion-GiayPhepMoiTruong').html('');
        var urlApi = "";
        if (type == "DuAn") {
            urlApi = localStorage.getItem("API_URL") + "/GiayPhepMoiTruong/GetGPMTByDuAn?idDuAn=" + idDuAn;
        };
        if (type == "KhuCongNghiep") {
            urlApi = localStorage.getItem("API_URL") + "/GiayPhepMoiTruong/GetGPMTByKhuCongNghiep?idKhuCongNghiep=" + idKhuCongNghiep;
        };
        Get({
            "url": urlApi,
            callback: function (res) {
                if (res.Success) {
                    $.each(res.Data, function (i, value) {
                        var html1 = "";
                        var html2 = "";
                        var html3 = "";

                        $.each(value.FileTaiLieu, function (j, file) {
                            switch (file.LoaiFileTaiLieu) {
                                case "GiayPhepMoiTruong":
                                    var iconfile = self.DrawIconFile(file.File.LinkFile);
                                    html1 += '<span class="pt-1"><a href = "' + localStorage.getItem('API_URL').replace("api", "") + file.File.LinkFile + '"target="_blank">' + iconfile + file.File.TenFile + '</a></span>';
                                    break;
                                case ("BaoCaoDanhGiaTacDongMoiTruong"):
                                    var iconfile = self.DrawIconFile(file.File.LinkFile);
                                    html2 += '<span class="pt-1"><a href = "' + localStorage.getItem('API_URL').replace("api", "") + file.File.LinkFile + '"target="_blank">' + iconfile + file.File.TenFile + '</a></span>';
                                    break;
                                default:
                                    var iconfile = self.DrawIconFile(file.File.LinkFile);
                                    html3 += '<span class="pt-1"><a href = "' + localStorage.getItem('API_URL').replace("api", "") + file.File.LinkFile + '"target="_blank">' + iconfile + file.File.TenFile + '</a></span>';
                            }

                        });
                        var tag = `<div class="accordion-item">
                                        <h2 class="accordion-header" id="heading_`+ value.IdGiayPhepMoiTruong + `">
                                            <button class="accordion-button" type="button" data-bs-toggle="collapse" data-bs-target="#collapse_`+ value.IdGiayPhepMoiTruong + `" aria-expanded="true" aria-controls="collapse_` + value.IdGiayPhepMoiTruong + `">
                                                `+ value.SoGiayPhep + ' ngày ' + value.NgayCap + `
                                            </button>
                                        </h2>
                                        <div id="collapse_` + value.IdGiayPhepMoiTruong + `" class="accordion-collapse collapse show" aria-labelledby="heading_` + value.IdGiayPhepMoiTruong + `" data-bs-parent="#accordionExample">
                                            <div class="accordion-body">
                                                <strong>1. Giấy phép môi trường</strong>
                                                <p class="row">`+ html1 + `
                                                </p>
                                                <hr>
                                                <strong>2. Báo cáo đánh giá tác động môi trường</strong>
                                                <p class="row">`+ html2 + `
                                                </p>
                                                <hr>
                                                <strong>3. Tài liệu khác</strong>
                                                <p class="row">`+ html3 + `
                                                </p>
                                            </div>
                                            <div  style="float:right">
                                                <button type="button" class="btn btn-secondary btn-sm btn-sua" data-id=`+ value.IdGiayPhepMoiTruong + `>Chỉnh sửa</button>
                                                <button type="button" class="btn btn-danger btn-sm btn-xoa " data-id=`+ value.IdGiayPhepMoiTruong + `>Xóa</button>
                                            </div>
                                            </br><hr>
                                        </div>
                                    </div>`
                        $tab.find('#accordion-GiayPhepMoiTruong').append(tag);
                        $tab.find('.btn-sua').off('click').on('click', function () {
                            var idGiayPhep = $(this).attr("data-id");
                            $popup.modal('show');
                            self.ResetPopup();
                            Get({
                                "url": localStorage.getItem("API_URL") + "/GiayPhepMoiTruong/GetById?idGiayPhepMoiTruong=" + idGiayPhep,
                                callback: function (res) {
                                    if (res.Success) {
                                        FillFormData('#FormGiayPhepMoiTruong', res.Data);

                                        $.each(res.Data.FileTaiLieu, function (i, item) {
                                            var $tr = $popup.find("#tempFileTable").html();
                                            $popup.find("#tblFileGiayPhepMoiTruong tbody").append($tr);
                                            $popup.find("#tblFileGiayPhepMoiTruong tbody tr:last").find('[data-name="MoTa"]').val(item.MoTa);
                                            $popup.find("#tblFileGiayPhepMoiTruong tbody tr:last").find('[data-name="LoaiFileTaiLieu"]').val(item.LoaiFileTaiLieu);
                                            $popup.find("#tblFileGiayPhepMoiTruong tbody tr:last td:first").append('<a href = "' + localStorage.getItem('API_URL').replace("api", "") + item.File.LinkFile + '" data-id="' + item.IdFileTaiLieu + '" data-IdFile = "' + item.IdFile + '" target="_blank">' + item.File.TenFile + '</a>');
                                            $popup.find(".tr-remove").off('click').on('click', function () {
                                                $(this).parents('tr:first').remove();
                                            });
                                        });
                                        self.RegisterEventsPopupGiayPhepMoiTruong();

                                    }
                                }
                            });
                        });
                        $tab.find('.btn-xoa').off('click').on('click', function () {
                            var idGiayPhep = $(this).attr("data-id");
                            if (confirm("Xác nhận xóa?") == true) {
                                Delete({
                                    "url": localStorage.getItem("API_URL") + "/GiayPhepMoiTruong/Delete?idGiayPhepMoiTruong=" + idGiayPhep,
                                    callback: function (res) {
                                        if (res.Success) {
                                            toastr.success('Thực hiện thành công', 'Thông báo')
                                            self.LoadDanhSachGiayPhepMoiTruong();
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
        var $popup = $('#popup-form-giay-phep-moi-truong');
        $popup.find('.modal-header').text("Thêm mới hồ sơ thủ tục môi trường");
        ResetForm("#FormGiayPhepMoiTruong");
        $popup.find('[data-name="IdGiayPhepMoiTruong"]').val(0);
        $popup.find("#tblFileGiayPhepMoiTruong tbody").html('');
    },
    LoadChiTietGiayPhepMoiTruong: function () {
        var self = this;
        var $popup = $('#popup-form-giay-phep-moi-truong');
        Get({
            "url": localStorage.getItem("API_URL") + "/GiayPhepMoiTruong/GetById?id=" + idGiayPhep,
            callback: function (res) {
            }
        });
    },
    InsertUpdateGiayPhepMoiTruong: function () {
        var self = this;
        var isValidate = ValidateForm($('#FormGiayPhepMoiTruong'));
        if (isValidate) {
            var $popup = $('#popup-form-giay-phep-moi-truong');
            var data = LoadFormData("#FormGiayPhepMoiTruong");
            var listFileTaiLieu = [];
            $('#tblFileGiayPhepMoiTruong tbody tr').each(function () {
                var $tr = $(this);
                listFileTaiLieu.push({
                    IdFileTaiLieu: $(this).find("td:first a").attr("data-id"),
                    IdFile: $(this).find("td:first a").attr("data-idfile"),
                    NhomTaiLieu: "GiayPhepMoiTruong",
                    LoaiFileTaiLieu: $(this).find('[data-name="LoaiFileTaiLieu"] option:selected').val(),
                    MoTa: $(this).find('[data-name="MoTa"]').val(),
                })

            });
            data.IdDuAn = idDuAn;
            data.IdKhuCongNghiep = idKhuCongNghiep;
            data.FileTaiLieu = listFileTaiLieu;
            Post({
                "url": localStorage.getItem("API_URL") + "/GiayPhepMoiTruong/InsertUpdate",
                "data": data,
                callback: function (res) {
                    if (res.Success) {
                        toastr.success('Thực hiện thành công', 'Thông báo')
                        self.LoadDanhSachGiayPhepMoiTruong();
                        $popup.find('.btn-danger').trigger('click');
                    }
                    else {
                        toastr.error(res.Message, 'Có lỗi xảy ra')
                    }
                }
            });
        }
    },

    RegisterEventsGiayPhepMoiTruong: function () {
        var self = this;
        var $tab = $('#bordered-GiayPhepMoiTruong');
        var $popup = $('#popup-form-giay-phep-moi-truong');
        $tab.find('#btnCreateGiayPhepMoiTruong').off('click').on('click', function () {
            $popup.modal('show');
            self.ResetPopup();
            self.RegisterEventsPopupGiayPhepMoiTruong();
        });
        self.LoadDanhSachGiayPhepMoiTruong();
    },
    RegisterEventsPopupGiayPhepMoiTruong: function () {
        var self = this;
        var $popup = $('#popup-form-giay-phep-moi-truong');

        // Phần xử lý file
        $popup.find('#btnSelectFileGiayPhepMoiTruong').off('click').on('click', function () {
            $popup.find('#fileGiayPhepMoiTruong').trigger("click");
        });

        if ($popup.find('#fileGiayPhepMoiTruong').length > 0) {
            $popup.find('#fileGiayPhepMoiTruong')[0].value = "";
            $popup.find('#fileGiayPhepMoiTruong').off('change').on('change', function (e) {
                var $this = this;
                var file = $popup.find('#fileGiayPhepMoiTruong')[0].files.length > 0 ? $popup.find('#fileGiayPhepMoiTruong')[0].files : null;
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
                                    $popup.find("#tblFileGiayPhepMoiTruong tbody").append($tr);
                                    $popup.find("#tblFileGiayPhepMoiTruong tbody tr:last td:first").append('<a href = "' + localStorage.getItem("API_URL").replace("api", "") + res.Data[i].LinkFile + '" data-id="0" data-IdFile = "' + res.Data[i].IdFile + '" target="_blank">' + file[i].name + '</a>');
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
            self.InsertUpdateGiayPhepMoiTruong();
        });
    },
}

$(document).ready(function () {
    GiayPhepMoiTruongControl.Init();
});


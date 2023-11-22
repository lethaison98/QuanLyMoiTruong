if (typeof (BaoCaoBaoVeMoiTruongControl) == "undefined") BaoCaoBaoVeMoiTruongControl = {};
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
if (url.indexOf("KhuKinhTe") != -1) {
    id= 9999;
    type = "KhuKinhTe";
}
BaoCaoBaoVeMoiTruongControl = {
    Init: function () {
        BaoCaoBaoVeMoiTruongControl.RegisterEventsBaoCaoBaoVeMoiTruong();
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

    LoadDanhSachBaoCaoBaoVeMoiTruong: function () {
        var self = this;
        var urlApi = "";
        if (type == "DuAn") {
            urlApi = localStorage.getItem("API_URL") + "/BaoCaoBaoVeMoiTruong/GetBaoCaoBVMTHangNamByDuAn?idDuAn=" + idDuAn;
        };
        if (type == "KhuCongNghiep") {
            urlApi = localStorage.getItem("API_URL") + "/BaoCaoBaoVeMoiTruong/GetBaoCaoBVMTHangNamByKhuCongNghiep?idKhuCongNghiep=" + idKhuCongNghiep;
        };
        if (type == "KhuKinhTe") {
            urlApi = localStorage.getItem("API_URL") + "/BaoCaoBaoVeMoiTruong/GetBaoCaoBVMTHangNamByKhuKinhTe";
        };
        var $tab = $('#bordered-BaoCaoBaoVeMoiTruong');
        var $popup = $('#popup-form-bao-cao-bao-ve-moi-truong-hang-nam');
        $tab.find("#accordion-BaoCaoBaoVeMoiTruong").html('');
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
                                case "BaoCaoBaoVeMoiTruong":
                                    var iconfile = self.DrawIconFile(file.File.LinkFile);
                                    html1 += '<span class="pt-1"><a href = "' + localStorage.getItem('API_URL').replace("api", "") + file.File.LinkFile + '"target="_blank">' + iconfile + file.File.TenFile + '</a></span>';
                                    break;
                                default:
                                    var iconfile = self.DrawIconFile(file.File.LinkFile);
                                    html5 += '<span class="pt-1"><a href = "' + localStorage.getItem('API_URL').replace("api", "") + file.File.LinkFile + '"target="_blank">' + iconfile + file.File.TenFile + '</a></span>';
                            }

                        });
                        var tag = `<div class="accordion-item">
                                        <h2 class="accordion-header" id="heading_`+ value.IdBaoCaoBaoVeMoiTruong + `">
                                            <button class="accordion-button" type="button" data-bs-toggle="collapse" data-bs-target="#collapse_`+ value.IdBaoCaoBaoVeMoiTruong + `" aria-expanded="true" aria-controls="collapse_` + value.IdBaoCaoBaoVeMoiTruong + `">
                                                `+ value.TenBaoCao + ' ngày ' + value.NgayBaoCao + `
                                            </button>
                                        </h2>
                                        <div id="collapse_` + value.IdBaoCaoBaoVeMoiTruong + `" class="accordion-collapse collapse show" aria-labelledby="heading_` + value.IdBaoCaoBaoVeMoiTruong + `" data-bs-parent="#accordionExample">
                                            <div class="accordion-body">
                                                <strong>1. Báo cáo công tác bảo vệ môi trường hàng năm</strong>
                                                <p class="row">`+ html1 + `
                                                </p>
                                                <hr>
                                                <strong>2. Tài liệu khác</strong>
                                                <p class="row">`+ html5 + `
                                                </p>
                                            </div>
                                            <div  style="float:right">
                                                <button type="button" class="btn btn-secondary btn-sm btn-sua" data-id=`+ value.IdBaoCaoBaoVeMoiTruong + `>Chỉnh sửa</button>
                                                <button type="button" class="btn btn-danger btn-sm btn-xoa " data-id=`+ value.IdBaoCaoBaoVeMoiTruong + `>Xóa</button>
                                            </div>
                                            </br><hr>
                                        </div>
                                    </div>`
                        $tab.find("#accordion-BaoCaoBaoVeMoiTruong").append(tag);
                        $tab.find('.btn-sua').off('click').on('click', function () {
                            var idGiayPhep = $(this).attr("data-id");
                            $popup.modal('show');
                            self.ResetPopup();
                            Get({
                                "url": localStorage.getItem("API_URL") + "/BaoCaoBaoVeMoiTruong/GetById?idBaoCaoBaoVeMoiTruong=" + idGiayPhep,
                                callback: function (res) {
                                    if (res.Success) {
                                        $popup.find('.modal-header').text("Chỉnh sửa báo cáo bảo vệ môi trường hàng năm")
                                        FillFormData('#FormBaoCaoBaoVeMoiTruong', res.Data);

                                        $.each(res.Data.FileTaiLieu, function (i, item) {
                                            var $tr = $popup.find("#tempFileTable").html();
                                            $popup.find("#tblFileBaoCaoBaoVeMoiTruong tbody").append($tr);
                                            $popup.find("#tblFileBaoCaoBaoVeMoiTruong tbody tr:last").find('[data-name="MoTa"]').val(item.MoTa);
                                            $popup.find("#tblFileBaoCaoBaoVeMoiTruong tbody tr:last").find('[data-name="LoaiFileTaiLieu"]').val(item.LoaiFileTaiLieu);
                                            $popup.find("#tblFileBaoCaoBaoVeMoiTruong tbody tr:last td:first").append('<a href = "' + localStorage.getItem('API_URL').replace("api", "") + item.File.LinkFile + '" data-id="' + item.IdFileTaiLieu + '" data-IdFile = "' + item.IdFile + '"target="_blank">' + item.File.TenFile + '</a>');
                                            $popup.find(".tr-remove").off('click').on('click', function () {
                                                $(this).parents('tr:first').remove();
                                            });
                                        });
                                        self.RegisterEventsPopupBaoCaoBaoVeMoiTruong();

                                    }
                                }
                            });
                        });
                        $tab.find('.btn-xoa').off('click').on('click', function () {
                            var idGiayPhep = $(this).attr("data-id");
                            if (confirm("Xác nhận xóa?") == true) {
                                Delete({
                                    "url": localStorage.getItem("API_URL") + "/BaoCaoBaoVeMoiTruong/Delete?idBaoCaoBaoVeMoiTruong=" + idGiayPhep,
                                    callback: function (res) {
                                        if (res.Success) {
                                            toastr.success('Thực hiện thành công', 'Thông báo')
                                            self.LoadDanhSachBaoCaoBaoVeMoiTruong();
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
        var $popup = $('#popup-form-bao-cao-bao-ve-moi-truong-hang-nam');
        ResetForm("#FormBaoCaoBaoVeMoiTruong");
        $popup.find('[data-name="IdBaoCaoBaoVeMoiTruong"]').val(0);
        $popup.find("#tblFileBaoCaoBaoVeMoiTruong tbody").html('');
    },
    LoadChiTietBaoCaoBaoVeMoiTruong: function () {
        var self = this;
        var $popup = $('#popup-form-bao-cao-bao-ve-moi-truong-hang-nam');
        Get({
            "url": localStorage.getItem("API_URL") + "/BaoCaoBaoVeMoiTruong/GetById?id=" + idGiayPhep,
            callback: function (res) {
            }
        });
    },
    InsertUpdateBaoCaoBaoVeMoiTruong: function () {
        var self = this;
        var isValidate = ValidateForm($('#FormBaoCaoBaoVeMoiTruong'));
        if (isValidate) {
            var $popup = $('#popup-form-bao-cao-bao-ve-moi-truong-hang-nam');
            var data = LoadFormData("#FormBaoCaoBaoVeMoiTruong");
            var listFileTaiLieu = [];
            $('#tblFileBaoCaoBaoVeMoiTruong tbody tr').each(function () {
                var $tr = $(this);
                listFileTaiLieu.push({
                    IdFileTaiLieu: $(this).find("td:first a").attr("data-id"),
                    IdFile: $(this).find("td:first a").attr("data-idfile"),
                    NhomTaiLieu: "BaoCaoBaoVeMoiTruong",
                    LoaiFileTaiLieu: $(this).find('[data-name="LoaiFileTaiLieu"] option:selected').val(),
                    MoTa: $(this).find('[data-name="MoTa"]').val(),
                })

            });
            data.IdDuAn = idDuAn;
            data.IdKhuCongNghiep = idKhuCongNghiep;
            if (type == "KhuKinhTe") {
                data.KhuKinhTe = true;
            }
            data.LoaiBaoCao = type;
            data.FileTaiLieu = listFileTaiLieu;
            Post({
                "url": localStorage.getItem("API_URL") + "/BaoCaoBaoVeMoiTruong/InsertUpdate",
                "data": data,
                callback: function (res) {
                    if (res.Success) {
                        toastr.success('Thực hiện thành công', 'Thông báo')
                        self.LoadDanhSachBaoCaoBaoVeMoiTruong();
                        $popup.find('.btn-danger').trigger('click');
                    }
                    else {
                        toastr.error(res.Message, 'Có lỗi xảy ra')
                    }
                }
            });
        }
    },

    RegisterEventsBaoCaoBaoVeMoiTruong: function () {
        var self = this;
        if (type == "KhuKinhTe") {
            $('#btnCreateBaoCaoBaoVeMoiTruong').off('click').on('click', function () {
                var $popup = $('#popup-form-bao-cao-bao-ve-moi-truong-hang-nam');
                $popup.modal('show');
                $popup.find('.modal-header').text("Thêm mới báo cáo bảo vệ môi trường khu kinh tế");
                self.ResetPopup();
                self.RegisterEventsPopupBaoCaoBaoVeMoiTruong();
            });
            self.LoadDanhSachBaoCaoBaoVeMoiTruong();
        } else {
            var $tab = $('#bordered-BaoCaoBaoVeMoiTruong');
            var $popup = $('#popup-form-bao-cao-bao-ve-moi-truong-hang-nam');
            $tab.find('#btnCreateBaoCaoBaoVeMoiTruong').off('click').on('click', function () {
                $popup.modal('show');
                $popup.find('.modal-header').text("Thêm mới báo cáo bảo vệ môi trường hàng năm");
                self.ResetPopup();
                self.RegisterEventsPopupBaoCaoBaoVeMoiTruong();
            });
            self.LoadDanhSachBaoCaoBaoVeMoiTruong();
        }
    },
    RegisterEventsPopupBaoCaoBaoVeMoiTruong: function () {
        var self = this;
        var $popup = $('#popup-form-bao-cao-bao-ve-moi-truong-hang-nam');

        // Phần xử lý file
        $popup.find('#btnSelectFileBaoCaoBaoVeMoiTruong').off('click').on('click', function () {
            $popup.find('#fileBaoCaoBaoVeMoiTruong').trigger("click");
        });

        if ($popup.find('#fileBaoCaoBaoVeMoiTruong').length > 0) {
            $popup.find('#fileBaoCaoBaoVeMoiTruong')[0].value = "";
            $popup.find('#fileBaoCaoBaoVeMoiTruong').off('change').on('change', function (e) {
                var $this = this;
                var file = $popup.find('#fileBaoCaoBaoVeMoiTruong')[0].files.length > 0 ? $popup.find('#fileBaoCaoBaoVeMoiTruong')[0].files : null;
                if (file != null) {
                    var dataFile = new FormData();
                    dataFile.append("NhomTaiLieu", type);
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
                                    $popup.find("#tblFileBaoCaoBaoVeMoiTruong tbody").append($tr);
                                    $popup.find("#tblFileBaoCaoBaoVeMoiTruong tbody tr:last td:first").append('<a href = "' + localStorage.getItem("API_URL").replace("api", "") + res.Data[i].LinkFile + '" data-id="0" data-IdFile = "' + res.Data[i].IdFile + '" target="_blank">' + file[i].name + '</a>');
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
            self.InsertUpdateBaoCaoBaoVeMoiTruong();
        });
    },
}

$(document).ready(function () {
    BaoCaoBaoVeMoiTruongControl.Init();
});


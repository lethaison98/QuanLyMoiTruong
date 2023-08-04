if (typeof (BaoCaoBaoVeMoiTruongKCNControl) == "undefined") BaoCaoBaoVeMoiTruongKCNControl = {};
var url = window.location.pathname;
var id = url.substring(url.lastIndexOf('/') + 1);
BaoCaoBaoVeMoiTruongKCNControl = {
    Init: function () {
        BaoCaoBaoVeMoiTruongKCNControl.RegisterEventsBaoCaoBaoVeMoiTruongKCN();
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

    LoadDanhSachBaoCaoBaoVeMoiTruongKCN: function () {
        var self = this;
        var $tab = $('#bordered-BaoCaoBaoVeMoiTruongKCN');
        var $popup = $('#popup-form-bao-cao-bao-ve-moi-truong-kcn');
        $tab.find("#accordion-BaoCaoBaoVeMoiTruongKCN").html('');
        Get({
            "url": localStorage.getItem("API_URL") + "/BaoCaoBaoVeMoiTruongKCN/GetBaoCaoBVMTByKCN?idKhuCongNghiep=" + id,
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
                                case "BaoCaoBaoVeMoiTruongKCN":
                                    var iconfile = self.DrawIconFile(file.File.LinkFile);
                                    html1 += '<span class="pt-1"><a href = "' + localStorage.getItem('API_URL').replace("api", "") + file.File.LinkFile + '">' + iconfile + file.File.TenFile + '</a></span>';
                                    break;
                                default:
                                    var iconfile = self.DrawIconFile(file.File.LinkFile);
                                    html5 += '<span class="pt-1"><a href = "' + localStorage.getItem('API_URL').replace("api", "") + file.File.LinkFile + '">' + iconfile + file.File.TenFile + '</a></span>';
                            }

                        });
                        var tag = `<div class="accordion-item">
                                        <h2 class="accordion-header" id="heading_`+ value.IdBaoCaoBaoVeMoiTruongKCN + `">
                                            <button class="accordion-button" type="button" data-bs-toggle="collapse" data-bs-target="#collapse_`+ value.IdBaoCaoBaoVeMoiTruongKCN + `" aria-expanded="true" aria-controls="collapse_` + value.IdBaoCaoBaoVeMoiTruongKCN + `">
                                                `+ value.TenBaoCao + ' ngày ' + value.NgayBaoCao + `
                                            </button>
                                        </h2>
                                        <div id="collapse_` + value.IdBaoCaoBaoVeMoiTruongKCN + `" class="accordion-collapse collapse show" aria-labelledby="heading_` + value.IdBaoCaoBaoVeMoiTruongKCN + `" data-bs-parent="#accordionExample">
                                            <div class="accordion-body">
                                                <strong>1. Báo cáo công tác bảo vệ môi trường khu công nghiệp</strong>
                                                <p class="row">`+ html1 + `
                                                </p>
                                                <hr>
                                                <strong>2. Tài liệu khác</strong>
                                                <p class="row">`+ html5 + `
                                                </p>
                                            </div>
                                            <div  style="float:right">
                                                <button type="button" class="btn btn-secondary btn-sm btn-sua" data-id=`+ value.IdBaoCaoBaoVeMoiTruongKCN + `>Chỉnh sửa</button>
                                                <button type="button" class="btn btn-danger btn-sm btn-xoa " data-id=`+ value.IdBaoCaoBaoVeMoiTruongKCN + `>Xóa</button>
                                            </div>
                                            </br><hr>
                                        </div>
                                    </div>`
                        $tab.find("#accordion-BaoCaoBaoVeMoiTruongKCN").append(tag);
                        $tab.find('.btn-sua').off('click').on('click', function () {
                            var idGiayPhep = $(this).attr("data-id");
                            $popup.modal('show');
                            self.ResetPopup();
                            Get({
                                "url": localStorage.getItem("API_URL") + "/BaoCaoBaoVeMoiTruongKCN/GetById?idBaoCaoBaoVeMoiTruongKCN=" + idGiayPhep,
                                callback: function (res) {
                                    if (res.Success) {
                                        $popup.find('.modal-header').text("Chỉnh sửa báo cáo bảo vệ môi trường khu công nghiệp")
                                        FillFormData('#FormBaoCaoBaoVeMoiTruongKCN', res.Data);

                                        $.each(res.Data.FileTaiLieu, function (i, item) {
                                            var $tr = $popup.find("#tempChiTietQuyetDinhThueDat").html();
                                            $popup.find("#tblFileBaoCaoBaoVeMoiTruongKCN tbody").append($tr);
                                            $popup.find("#tblFileBaoCaoBaoVeMoiTruongKCN tbody tr:last").find('[data-name="MoTa"]').val(item.MoTa);
                                            $popup.find("#tblFileBaoCaoBaoVeMoiTruongKCN tbody tr:last").find('[data-name="LoaiFileTaiLieu"]').val(item.LoaiFileTaiLieu);
                                            $popup.find("#tblFileBaoCaoBaoVeMoiTruongKCN tbody tr:last td:first").append('<a href = "' + localStorage.getItem('API_URL').replace("api", "") + item.File.LinkFile + '" data-id="' + item.IdFileTaiLieu + '" data-IdFile = "' + item.IdFile + '">' + item.File.TenFile + '</a>');
                                            $popup.find(".tr-remove").off('click').on('click', function () {
                                                $(this).parents('tr:first').remove();
                                            });
                                        });
                                        self.RegisterEventsPopupBaoCaoBaoVeMoiTruongKCN();

                                    }
                                }
                            });
                        });
                        $tab.find('.btn-xoa').off('click').on('click', function () {
                            var idGiayPhep = $(this).attr("data-id");
                            if (confirm("Xác nhận xóa?") == true) {
                                Delete({
                                    "url": localStorage.getItem("API_URL") + "/BaoCaoBaoVeMoiTruongKCN/Delete?idBaoCaoBaoVeMoiTruongKCN=" + idGiayPhep,
                                    callback: function (res) {
                                        if (res.Success) {
                                            toastr.success('Thực hiện thành công', 'Thông báo')
                                            self.LoadDanhSachBaoCaoBaoVeMoiTruongKCN();
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
        var $popup = $('#popup-form-bao-cao-bao-ve-moi-truong-kcn');
        ResetForm("#FormBaoCaoBaoVeMoiTruongKCN");
        $popup.find('[data-name="IdBaoCaoBaoVeMoiTruongKCN"]').val(0);
        $popup.find("#tblFileBaoCaoBaoVeMoiTruongKCN tbody").html('');
    },
    LoadChiTietBaoCaoBaoVeMoiTruongKCN: function () {
        var self = this;
        var $popup = $('#popup-form-bao-cao-bao-ve-moi-truong-kcn');
        Get({
            "url": localStorage.getItem("API_URL") + "/BaoCaoBaoVeMoiTruongKCN/GetById?id=" + idGiayPhep,
            callback: function (res) {
            }
        });
    },
    InsertUpdateBaoCaoBaoVeMoiTruongKCN: function () {
        var self = this;
        var isValidate = ValidateForm($('#FormBaoCaoBaoVeMoiTruongKCN'));
        if (isValidate) {
            var $popup = $('#popup-form-bao-cao-bao-ve-moi-truong-kcn');
            var data = LoadFormData("#FormBaoCaoBaoVeMoiTruongKCN");
            var listFileTaiLieu = [];
            $('#tblFileBaoCaoBaoVeMoiTruongKCN tbody tr').each(function () {
                var $tr = $(this);
                listFileTaiLieu.push({
                    IdFileTaiLieu: $(this).find("td:first a").attr("data-id"),
                    IdFile: $(this).find("td:first a").attr("data-idfile"),
                    NhomTaiLieu: "BaoCaoBaoVeMoiTruongKCN",
                    LoaiFileTaiLieu: $(this).find('[data-name="LoaiFileTaiLieu"] option:selected').val(),
                    MoTa: $(this).find('[data-name="MoTa"]').val(),
                })

            });
            data.IdKhuCongNghiep = id;
            data.FileTaiLieu = listFileTaiLieu;
            Post({
                "url": localStorage.getItem("API_URL") + "/BaoCaoBaoVeMoiTruongKCN/InsertUpdate",
                "data": data,
                callback: function (res) {
                    if (res.Success) {
                        toastr.success('Thực hiện thành công', 'Thông báo')
                        self.LoadDanhSachBaoCaoBaoVeMoiTruongKCN();
                        $popup.find('.btn-danger').trigger('click');
                    }
                    else {
                        toastr.error(res.Message, 'Có lỗi xảy ra')
                    }
                }
            });
        }
    },

    RegisterEventsBaoCaoBaoVeMoiTruongKCN: function () {
        var self = this;
        var $tab = $('#bordered-BaoCaoBaoVeMoiTruongKCN');
        var $popup = $('#popup-form-bao-cao-bao-ve-moi-truong-kcn');
        $tab.find('#btnCreateBaoCaoBaoVeMoiTruongKCN').off('click').on('click', function () {
            $popup.modal('show');
            $popup.find('.modal-header').text("Thêm mới báo cáo bảo vệ môi trường khu công nghiệp");
            self.ResetPopup();
            self.RegisterEventsPopupBaoCaoBaoVeMoiTruongKCN();
        });
        self.LoadDanhSachBaoCaoBaoVeMoiTruongKCN();
    },
    RegisterEventsPopupBaoCaoBaoVeMoiTruongKCN: function () {
        var self = this;
        var $popup = $('#popup-form-bao-cao-bao-ve-moi-truong-kcn');

        // Phần xử lý file
        $popup.find('#btnSelectFileBaoCaoBaoVeMoiTruongKCN').off('click').on('click', function () {
            $popup.find('#fileBaoCaoBaoVeMoiTruongKCN').trigger("click");
        });

        if ($popup.find('#fileBaoCaoBaoVeMoiTruongKCN').length > 0) {
            $popup.find('#fileBaoCaoBaoVeMoiTruongKCN')[0].value = "";
            $popup.find('#fileBaoCaoBaoVeMoiTruongKCN').off('change').on('change', function (e) {
                var $this = this;
                var file = $popup.find('#fileBaoCaoBaoVeMoiTruongKCN')[0].files.length > 0 ? $popup.find('#fileBaoCaoBaoVeMoiTruongKCN')[0].files : null;
                if (file != null) {
                    var dataFile = new FormData();
                    dataFile.append("NhomTaiLieu", "KhuCongNghiep");
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
                                    var $tr = $popup.find("#tempChiTietQuyetDinhThueDat").html();
                                    $popup.find("#tblFileBaoCaoBaoVeMoiTruongKCN tbody").append($tr);
                                    $popup.find("#tblFileBaoCaoBaoVeMoiTruongKCN tbody tr:last td:first").append('<a href = "#" data-id="0" data-IdFile = "' + res.Data[i] + '">' + file[i].name + '</a>');
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
            self.InsertUpdateBaoCaoBaoVeMoiTruongKCN();
        });
    },
}

$(document).ready(function () {
    BaoCaoBaoVeMoiTruongKCNControl.Init();
});


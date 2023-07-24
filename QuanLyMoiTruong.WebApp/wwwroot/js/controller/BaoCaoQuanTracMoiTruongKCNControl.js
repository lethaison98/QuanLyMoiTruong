﻿if (typeof (BaoCaoQuanTracMoiTruongKCNControl) == "undefined") BaoCaoQuanTracMoiTruongKCNControl = {};
var url = window.location.pathname;
var id = url.substring(url.lastIndexOf('/') + 1);
BaoCaoQuanTracMoiTruongKCNControl = {
    Init: function () {
        BaoCaoQuanTracMoiTruongKCNControl.RegisterEventsBaoCaoQuanTracMoiTruongKCN();
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

    LoadDanhSachBaoCaoQuanTracMoiTruongKCN: function () {
        var self = this;
        var $tab = $('#bordered-BaoCaoQuanTracMoiTruongKCN');
        var $popup = $('#popup-form-bao-cao-quan-trac-moi-truong-kcn');
        $tab.find("#accordion-BaoCaoQuanTracMoiTruongKCN").html('');
        Get({
            "url": localStorage.getItem("API_URL") + "/BaoCaoQuanTracMoiTruongKCN/GetBaoCaoQTMTByKCN?idKhuCongNghiep=" + id,
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
                                case "BaoCaoQuanTracMoiTruongKCN":
                                    var iconfile = self.DrawIconFile(file.File.LinkFile);
                                    html1 += '<span class="pt-1"><a href = "' + localStorage.getItem('API_URL').replace("api", "") + file.File.LinkFile + '">' + iconfile + file.File.TenFile + '</a></span>';
                                    break;
                                default:
                                    var iconfile = self.DrawIconFile(file.File.LinkFile);
                                    html5 += '<span class="pt-1"><a href = "' + localStorage.getItem('API_URL').replace("api", "") + file.File.LinkFile + '">' + iconfile + file.File.TenFile + '</a></span>';
                            }

                        });
                        var tag = `<div class="accordion-item">
                                        <h2 class="accordion-header" id="heading_`+ value.IdBaoCaoQuanTracMoiTruongKCN + `">
                                            <button class="accordion-button" type="button" data-bs-toggle="collapse" data-bs-target="#collapse_`+ value.IdBaoCaoQuanTracMoiTruongKCN + `" aria-expanded="true" aria-controls="collapse_` + value.IdBaoCaoQuanTracMoiTruongKCN + `">
                                                `+ value.TenBaoCao + ' ngày ' + value.NgayBaoCao + `
                                            </button>
                                        </h2>
                                        <div id="collapse_` + value.IdBaoCaoQuanTracMoiTruongKCN + `" class="accordion-collapse collapse show" aria-labelledby="heading_` + value.IdBaoCaoQuanTracMoiTruongKCN + `" data-bs-parent="#accordionExample">
                                            <div class="accordion-body">
                                                <strong>1. Báo cáo công tác quan trắc môi trường khu công nghiệp</strong>
                                                <p class="row">`+ html1 + `
                                                </p>
                                                <hr>
                                                <strong>2. Tài liệu khác</strong>
                                                <p class="row">`+ html5 + `
                                                </p>
                                            </div>
                                            <div  style="float:right">
                                                <button type="button" class="btn btn-secondary btn-sm btn-sua" data-id=`+ value.IdBaoCaoQuanTracMoiTruongKCN + `>Chỉnh sửa</button>
                                                <button type="button" class="btn btn-danger btn-sm btn-xoa " data-id=`+ value.IdBaoCaoQuanTracMoiTruongKCN + `>Xóa</button>
                                            </div>
                                            </br><hr>
                                        </div>
                                    </div>`
                        $tab.find("#accordion-BaoCaoQuanTracMoiTruongKCN").append(tag);
                        $tab.find('.btn-sua').off('click').on('click', function () {
                            var idGiayPhep = $(this).attr("data-id");
                            $popup.modal('show');
                            self.ResetPopup();
                            Get({
                                "url": localStorage.getItem("API_URL") + "/BaoCaoQuanTracMoiTruongKCN/GetById?idBaoCaoQuanTracMoiTruongKCN=" + idGiayPhep,
                                callback: function (res) {
                                    if (res.Success) {
                                        $popup.find('.modal-header').text("Chỉnh sửa báo cáo quan trắc môi trường khu công nghiệp")
                                        FillFormData('#FormBaoCaoQuanTracMoiTruongKCN', res.Data);

                                        $.each(res.Data.FileTaiLieu, function (i, item) {
                                            var $tr = $popup.find("#tempChiTietQuyetDinhThueDat").html();
                                            $popup.find("#tblFileBaoCaoQuanTracMoiTruongKCN tbody").append($tr);
                                            $popup.find("#tblFileBaoCaoQuanTracMoiTruongKCN tbody tr:last").find('[data-name="MoTa"]').val(item.MoTa);
                                            $popup.find("#tblFileBaoCaoQuanTracMoiTruongKCN tbody tr:last").find('[data-name="LoaiFileTaiLieu"]').val(item.LoaiFileTaiLieu);
                                            $popup.find("#tblFileBaoCaoQuanTracMoiTruongKCN tbody tr:last td:first").append('<a href = "' + localStorage.getItem('API_URL').replace("api", "") + item.File.LinkFile + '" data-id="' + item.IdFileTaiLieu + '" data-IdFile = "' + item.IdFile + '">' + item.File.TenFile + '</a>');
                                            $popup.find(".tr-remove").off('click').on('click', function () {
                                                $(this).parents('tr:first').remove();
                                            });
                                        });
                                        self.RegisterEventsPopupBaoCaoQuanTracMoiTruongKCN();

                                    }
                                }
                            });
                        });
                        $tab.find('.btn-xoa').off('click').on('click', function () {
                            var idGiayPhep = $(this).attr("data-id");
                            if (confirm("Xác nhận xóa?") == true) {
                                Delete({
                                    "url": localStorage.getItem("API_URL") + "/BaoCaoQuanTracMoiTruongKCN/Delete?idBaoCaoQuanTracMoiTruongKCN=" + idGiayPhep,
                                    callback: function (res) {
                                        if (res.Success) {
                                            toastr.success('Thực hiện thành công', 'Thông báo')
                                            self.LoadDanhSachBaoCaoQuanTracMoiTruongKCN();
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
        var $popup = $('#popup-form-bao-cao-quan-trac-moi-truong-kcn');
        ResetForm("#FormBaoCaoQuanTracMoiTruongKCN");
        $popup.find('[data-name="IdBaoCaoQuanTracMoiTruongKCN"]').val(0);
        $popup.find("#tblFileBaoCaoQuanTracMoiTruongKCN tbody").html('');
    },
    LoadChiTietBaoCaoQuanTracMoiTruongKCN: function () {
        var self = this;
        var $popup = $('#popup-form-bao-cao-quan-trac-moi-truong-kcn');
        Get({
            "url": localStorage.getItem("API_URL") + "/BaoCaoQuanTracMoiTruongKCN/GetById?id=" + idGiayPhep,
            callback: function (res) {
            }
        });
    },
    InsertUpdateBaoCaoQuanTracMoiTruongKCN: function () {
        var self = this;
        var $popup = $('#popup-form-bao-cao-quan-trac-moi-truong-kcn');
        var data = LoadFormData("#FormBaoCaoQuanTracMoiTruongKCN");
        var listFileTaiLieu = [];
        $('#tblFileBaoCaoQuanTracMoiTruongKCN tbody tr').each(function () {
            var $tr = $(this);
            listFileTaiLieu.push({
                IdFileTaiLieu: $(this).find("td:first a").attr("data-id"),
                IdFile: $(this).find("td:first a").attr("data-idfile"),
                NhomTaiLieu: "BaoCaoQuanTracMoiTruongKCN",
                LoaiFileTaiLieu: $(this).find('[data-name="LoaiFileTaiLieu"] option:selected').val(),
                MoTa: $(this).find('[data-name="MoTa"]').val(),
            })

        });
        data.IdKhuCongNghiep = id;
        data.FileTaiLieu = listFileTaiLieu;
        Post({
            "url": localStorage.getItem("API_URL") + "/BaoCaoQuanTracMoiTruongKCN/InsertUpdate",
            "data": data,
            callback: function (res) {
                if (res.Success) {
                    toastr.success('Thực hiện thành công', 'Thông báo')
                    self.LoadDanhSachBaoCaoQuanTracMoiTruongKCN();
                    $popup.find('.btn-danger').trigger('click');
                }
                else {
                    toastr.error(res.Message, 'Có lỗi xảy ra')
                }
            }
        });
    },

    RegisterEventsBaoCaoQuanTracMoiTruongKCN: function () {
        console.log(2222)
        var self = this;
        var $tab = $('#bordered-BaoCaoQuanTracMoiTruongKCN');
        var $popup = $('#popup-form-bao-cao-quan-trac-moi-truong-kcn');
        $tab.find('#btnCreateBaoCaoQuanTracMoiTruongKCN').off('click').on('click', function () {
            console.log(12345)
            $popup.modal('show');
            $popup.find('.modal-header').text("Thêm mới báo cáo quan trắc môi trường khu công nghiệp");
            self.ResetPopup();
            self.RegisterEventsPopupBaoCaoQuanTracMoiTruongKCN();
        });
        self.LoadDanhSachBaoCaoQuanTracMoiTruongKCN();
    },
    RegisterEventsPopupBaoCaoQuanTracMoiTruongKCN: function () {
        var self = this;
        var $popup = $('#popup-form-bao-cao-quan-trac-moi-truong-kcn');

        // Phần xử lý file
        $popup.find('#btnSelectFileBaoCaoQuanTracMoiTruongKCN').off('click').on('click', function () {
            $popup.find('#fileBaoCaoQuanTracMoiTruongKCN').trigger("click");
        });

        if ($popup.find('#fileBaoCaoQuanTracMoiTruongKCN').length > 0) {
            $popup.find('#fileBaoCaoQuanTracMoiTruongKCN')[0].value = "";
            $popup.find('#fileBaoCaoQuanTracMoiTruongKCN').off('change').on('change', function (e) {
                var $this = this;
                var file = $popup.find('#fileBaoCaoQuanTracMoiTruongKCN')[0].files.length > 0 ? $popup.find('#fileBaoCaoQuanTracMoiTruongKCN')[0].files : null;
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
                                    $popup.find("#tblFileBaoCaoQuanTracMoiTruongKCN tbody").append($tr);
                                    $popup.find("#tblFileBaoCaoQuanTracMoiTruongKCN tbody tr:last td:first").append('<a href = "#" data-id="0" data-IdFile = "' + res.Data[i] + '">' + file[i].name + '</a>');
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
            self.InsertUpdateBaoCaoQuanTracMoiTruongKCN();
        });
    },
}

$(document).ready(function () {
    BaoCaoQuanTracMoiTruongKCNControl.Init();
});

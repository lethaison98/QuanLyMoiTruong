if (typeof (ThanhPhanMoiTruongControl) == "undefined") ThanhPhanMoiTruongControl = {};
ThanhPhanMoiTruongControl = {
    Init: function () {
        ThanhPhanMoiTruongControl.RegisterEventsThanhPhanMoiTruong();
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

    LoadDatatable: function (opts) {
        var self = this;
        self.table = SetDataTable({
            table: $('#tbl'),
            url: localStorage.getItem("API_URL") + "/ThanhPhanMoiTruong/GetAllPaging",
            dom: "rtip",
            data: {
                "requestData": function () {
                    return {
                        "keyword": $('#txtKEY_WORD').val(),
                    }
                },
                "processData2": function (res) {
                    var json = jQuery.parseJSON(res);
                    json.recordsTotal = json.Data.TotalCount;
                    json.recordsFiltered = json.Data.TotalCount;
                    json.data = json.Data.Items;
                    return JSON.stringify(json);
                },
                "columns": [
                    {
                        "class": "stt-control",
                        "data": "RN",
                        "defaultContent": "1",
                        render: function (data, type, row, meta) {
                            var tables = $('#tbl').DataTable();
                            var info = tables.page.info();
                            return info.start + meta.row + 1;
                        }
                    },
                    {
                        "class": "name-control",
                        "defaultContent": "",
                        render: function (data, type, row) {
                            var thaotac = "<a href='ThanhPhanMoiTruong/ThanhPhanMoiTruongChiTiet/" + row.IdThanhPhanMoiTruong + "'>" + row.TenThanhPhanMoiTruong + "</i></a>";
                            return thaotac;
                        }
                    },
                    {
                        "class": "name-control",
                        "data": "Nam",
                        "defaultContent": "",
                    },
                    {
                        "class": "name-control",
                        "data": "Lan",
                        "defaultContent": "",
                    },
                    {
                        "class": "name-control",
                        "data": "GhiChu",
                        "defaultContent": "",
                    },
                    {
                        "class": "function-control",
                        "orderable": false,
                        "defaultContent": "",
                        render: function (data, type, row) {
                            var thaotac = "<div class='hstn-func' style='text-align: center;' data-type='" + JSON.stringify(row) + "'>" +
                                "<a href='javascript:;' class='edit-ThanhPhanMoiTruong' data-id='" + row.IdThanhPhanMoiTruong + "'><i class='fas fa-edit' title='Sửa'></i></a>&nbsp" +
                                "<a href='javascript:;' class='remove-ThanhPhanMoiTruong text-danger' data-id='" + row.IdThanhPhanMoiTruong + "'><i class='fas fa-trash-alt' title='Xóa' ></i></a>" +
                                "</div>";
                            return thaotac;
                        }
                    }
                ]
            },
            callback: function () {

                $('#tbl tbody .edit-ThanhPhanMoiTruong').off('click').on('click', function (e) {
                    var $popup = $('#popup-form-thanh-phan-moi-truong');
                    var id = $(this).attr('data-id');
                    Get({
                        url: localStorage.getItem("API_URL") + '/ThanhPhanMoiTruong/GetById',
                        data: {
                            idThanhPhanMoiTruong: id
                        },
                        callback: function (res) {
                            if (res.Success) {
                                $popup.modal('show');
                                self.ResetPopup();
                                $popup.find('.modal-title').text("Chỉnh sửa thông tin thành phần môi trường");
                                FillFormData('#FormThanhPhanMoiTruong', res.Data);
                                $.each(res.Data.FileTaiLieu, function (i, item) {
                                    var $tr = $popup.find("#tempFileTable").html();
                                    console.log($tr)
                                    $popup.find("#tblFileThanhPhanMoiTruong tbody").append($tr);
                                    $popup.find("#tblFileThanhPhanMoiTruong tbody tr:last").find('[data-name="MoTa"]').val(item.MoTa);
                                    $popup.find("#tblFileThanhPhanMoiTruong tbody tr:last").find('[data-name="LoaiFileTaiLieu"]').val(item.LoaiFileTaiLieu);
                                    $popup.find("#tblFileThanhPhanMoiTruong tbody tr:last td:first").append('<a href = "' + localStorage.getItem('API_URL').replace("api", "") + item.File.LinkFile + '" data-id="' + item.IdFileTaiLieu + '" data-IdFile = "' + item.IdFile + '" target="_blank">' + item.File.TenFile + '</a>');
                                    $popup.find(".tr-remove").off('click').on('click', function () {
                                        $(this).parents('tr:first').remove();
                                    });
                                });
                                self.RegisterEventsPopupThanhPhanMoiTruong();
                            }
                        }
                    });
                });

                $("#tbl tbody .remove-ThanhPhanMoiTruong").off('click').on('click', function (e) {
                    var $y = $(this);
                    var id = $y.attr('data-id');
                    if (id != "0") {
                        if (confirm("Xác nhận xóa?") == true) {
                            $.ajax({
                                url: localStorage.getItem("API_URL") + "/ThanhPhanMoiTruong/Delete?idThanhPhanMoiTruong=" + $y.attr('data-id') + "&Type=1",
                                headers: {
                                    'Authorization': 'Bearer ' + localStorage.getItem("ACCESS_TOKEN")
                                },
                                dataType: 'json',
                                contentType: "application/json-patch+json",
                                type: "Delete",
                                success: function (res) {
                                    if (res.Success) {
                                        toastr.success('Thực hiện thành công', 'Thông báo')
                                        self.table.ajax.reload();
                                    }
                                    else {
                                        toastr.error(res.Message, 'Có lỗi xảy ra')
                                    }
                                }
                            });
                        }

                    }
                });

            }
        });

    },

    ResetPopup: function () {
        var $popup = $('#popup-form-thanh-phan-moi-truong');
        $popup.find('.modal-header').text("Thêm mới thành phần môi trường");
        ResetForm("#FormThanhPhanMoiTruong");
        $popup.find('[data-name="IdThanhPhanMoiTruong"]').val(0);
        $popup.find('[data-name="Lan"]').val(1);
        $popup.find("#tblFileThanhPhanMoiTruong tbody").html('');
    },
    LoadChiTietThanhPhanMoiTruong: function () {
        var self = this;
        var $popup = $('#popup-form-thanh-phan-moi-truong');
        Get({
            "url": localStorage.getItem("API_URL") + "/ThanhPhanMoiTruong/GetById?id=" + idGiayPhep,
            callback: function (res) {
            }
        });
    },
    InsertUpdateThanhPhanMoiTruong: function () {
        console.log(123)
        var self = this;
        var isValidate = ValidateForm($('#FormThanhPhanMoiTruong'));
        if (isValidate) {
            var $popup = $('#popup-form-thanh-phan-moi-truong');
            var data = LoadFormData("#FormThanhPhanMoiTruong");
            var listFileTaiLieu = [];
            $('#tblFileThanhPhanMoiTruong tbody tr').each(function () {
                var $tr = $(this);
                listFileTaiLieu.push({
                    IdFileTaiLieu: $(this).find("td:first a").attr("data-id"),
                    IdFile: $(this).find("td:first a").attr("data-idfile"),
                    NhomTaiLieu: "ThanhPhanMoiTruong",
                    LoaiFileTaiLieu: $(this).find('[data-name="LoaiFileTaiLieu"] option:selected').val(),
                    MoTa: $(this).find('[data-name="MoTa"]').val(),
                })

            });
            data.FileTaiLieu = listFileTaiLieu;
            Post({
                "url": localStorage.getItem("API_URL") + "/ThanhPhanMoiTruong/InsertUpdate",
                "data": data,
                callback: function (res) {
                    if (res.Success) {
                        toastr.success('Thực hiện thành công', 'Thông báo')
                        self.table.ajax.reload();
                        $popup.find('.btn-danger').trigger('click');
                    }
                    else {
                        toastr.error(res.Message, 'Có lỗi xảy ra')
                    }
                }
            });
        }
    },

    RegisterEventsThanhPhanMoiTruong: function () {
        var self = this;
        var $popup = $('#popup-form-thanh-phan-moi-truong');
        $('#btnCreateThanhPhanMoiTruong').off('click').on('click', function () {
            console.log(2213)
            $popup.modal('show');
            self.ResetPopup();
            self.RegisterEventsPopupThanhPhanMoiTruong();
        });
        self.LoadDatatable();
        $(document).on('keypress', function (e) {
            if (e.which == 13) {
                $("#btnSearchThanhPhanMoiTruong").trigger('click');
            }
        });
        $("#btnSearchThanhPhanMoiTruong").off('click').on('click', function () {
            self.table.ajax.reload();
        });
    },
    RegisterEventsPopupThanhPhanMoiTruong: function () {
        var self = this;
        var $popup = $('#popup-form-thanh-phan-moi-truong');
        // Phần xử lý file
        $popup.find('#btnSelectFileThanhPhanMoiTruong').off('click').on('click', function () {
            $popup.find('#fileThanhPhanMoiTruong').trigger("click");
        });

        if ($popup.find('#fileThanhPhanMoiTruong').length > 0) {
            $popup.find('#fileThanhPhanMoiTruong')[0].value = "";
            $popup.find('#fileThanhPhanMoiTruong').off('change').on('change', function (e) {
                var $this = this;
                var file = $popup.find('#fileThanhPhanMoiTruong')[0].files.length > 0 ? $popup.find('#fileThanhPhanMoiTruong')[0].files : null;
                if (file != null) {
                    var dataFile = new FormData();
                    dataFile.append("NhomTaiLieu", "ThanhPhanMoiTruong");
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
                                    $popup.find("#tblFileThanhPhanMoiTruong tbody").append($tr);
                                    $popup.find("#tblFileThanhPhanMoiTruong tbody tr:last td:first").append('<a href = "' + localStorage.getItem("API_URL").replace("api", "") + res.Data[i].LinkFile + '" data-id="0" data-IdFile = "' + res.Data[i].IdFile + '"target="_blank">' + file[i].name + '</a>');
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
            console.log(12322)
            self.InsertUpdateThanhPhanMoiTruong();
        });
    },
}

$(document).ready(function () {
    ThanhPhanMoiTruongControl.Init();
});


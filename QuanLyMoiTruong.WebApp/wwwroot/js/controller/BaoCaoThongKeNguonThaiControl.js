if (typeof (BaoCaoThongKeNguonThaiControl) == "undefined") BaoCaoThongKeNguonThaiControl = {};
BaoCaoThongKeNguonThaiControl = {
    Init: function () {
        BaoCaoThongKeNguonThaiControl.RegisterEventsBaoCaoThongKeNguonThai();
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
            url: localStorage.getItem("API_URL") + "/BaoCaoThongKeNguonThai/GetAllPaging",
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
                            var thaotac = "<a href='BaoCaoThongKeNguonThai/BaoCaoThongKeNguonThaiChiTiet/" + row.IdBaoCaoThongKeNguonThai + "'>" + row.TenBaoCao + "</i></a>";
                            return thaotac;
                        }
                    },
                    {
                        "class": "name-control",
                        "data": "NgayBaoCao",
                        "defaultContent": "",
                    },
                    {
                        "class": "name-control",
                        "data": "Nam",
                        "defaultContent": "",
                    },
                    {
                        "class": "function-control",
                        "orderable": false,
                        "defaultContent": "",
                        render: function (data, type, row) {
                            var thaotac = "<div class='hstn-func' style='text-align: center;' data-type='" + JSON.stringify(row) + "'>" +
                                "<a href='javascript:;' class='edit-BaoCaoThongKeNguonThai' data-id='" + row.IdBaoCaoThongKeNguonThai + "'><i class='fas fa-edit' title='Sửa'></i></a>&nbsp" +
                                "<a href='javascript:;' class='remove-BaoCaoThongKeNguonThai text-danger' data-id='" + row.IdBaoCaoThongKeNguonThai + "'><i class='fas fa-trash-alt' title='Xóa' ></i></a>" +
                                "</div>";
                            return thaotac;
                        }
                    }
                ]
            },
            callback: function () {

                $('#tbl tbody .edit-BaoCaoThongKeNguonThai').off('click').on('click', function (e) {
                    var $popup = $('#popup-form-bao-cao-thong-ke-nguon-thai');
                    var id = $(this).attr('data-id');
                    Get({
                        url: localStorage.getItem("API_URL") + '/BaoCaoThongKeNguonThai/GetById',
                        data: {
                            idBaoCaoThongKeNguonThai: id
                        },
                        callback: function (res) {
                            if (res.Success) {
                                $popup.modal('show');
                                self.ResetPopup();
                                console.log(111)
                                $popup.find('.modal-title').text("Chỉnh sửa báo cáo thống kê nguồn thải");
                                FillFormData('#FormBaoCaoThongKeNguonThai', res.Data);
                                $.each(res.Data.FileTaiLieu, function (i, item) {
                                    var $tr = $popup.find("#tempFileTable").html();
                                    console.log($tr)
                                    $popup.find("#tblFileBaoCaoThongKeNguonThai tbody").append($tr);
                                    $popup.find("#tblFileBaoCaoThongKeNguonThai tbody tr:last").find('[data-name="MoTa"]').val(item.MoTa);
                                    $popup.find("#tblFileBaoCaoThongKeNguonThai tbody tr:last").find('[data-name="LoaiFileTaiLieu"]').val(item.LoaiFileTaiLieu);
                                    $popup.find("#tblFileBaoCaoThongKeNguonThai tbody tr:last td:first").append('<a href = "' + localStorage.getItem('API_URL').replace("api", "") + item.File.LinkFile + '" data-id="' + item.IdFileTaiLieu + '" data-IdFile = "' + item.IdFile + '" target="_blank">' + item.File.TenFile + '</a>');
                                    $popup.find(".tr-remove").off('click').on('click', function () {
                                        $(this).parents('tr:first').remove();
                                    });
                                });
                                self.RegisterEventsPopupBaoCaoThongKeNguonThai();
                            }
                        }
                    });
                });

                $("#tbl tbody .remove-BaoCaoThongKeNguonThai").off('click').on('click', function (e) {
                    var $y = $(this);
                    var id = $y.attr('data-id');
                    if (id != "0") {
                        if (confirm("Xác nhận xóa?") == true) {
                            $.ajax({
                                url: localStorage.getItem("API_URL") + "/BaoCaoThongKeNguonThai/Delete?idBaoCaoThongKeNguonThai=" + $y.attr('data-id') + "&Type=1",
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
        var $popup = $('#popup-form-bao-cao-thong-ke-nguon-thai');
        $popup.find('.modal-header').text("Thêm mới báo cáo thống kê nguồn thải");
        ResetForm("#FormBaoCaoThongKeNguonThai");
        $popup.find('[data-name="IdBaoCaoThongKeNguonThai"]').val(0);
        $popup.find("#tblFileBaoCaoThongKeNguonThai tbody").html('');
    },
    LoadChiTietBaoCaoThongKeNguonThai: function () {
        var self = this;
        var $popup = $('#popup-form-bao-cao-thong-ke-nguon-thai');
        Get({
            "url": localStorage.getItem("API_URL") + "/BaoCaoThongKeNguonThai/GetById?id=" + idGiayPhep,
            callback: function (res) {
            }
        });
    },
    InsertUpdateBaoCaoThongKeNguonThai: function () {
        console.log(123)
        var self = this;
        var isValidate = ValidateForm($('#FormBaoCaoThongKeNguonThai'));
        if (isValidate) {
            var $popup = $('#popup-form-bao-cao-thong-ke-nguon-thai');
            var data = LoadFormData("#FormBaoCaoThongKeNguonThai");
            var listFileTaiLieu = [];
            $('#tblFileBaoCaoThongKeNguonThai tbody tr').each(function () {
                var $tr = $(this);
                listFileTaiLieu.push({
                    IdFileTaiLieu: $(this).find("td:first a").attr("data-id"),
                    IdFile: $(this).find("td:first a").attr("data-idfile"),
                    NhomTaiLieu: "BaoCaoThongKeNguonThai",
                    LoaiFileTaiLieu: $(this).find('[data-name="LoaiFileTaiLieu"] option:selected').val(),
                    MoTa: $(this).find('[data-name="MoTa"]').val(),
                })

            });
            data.FileTaiLieu = listFileTaiLieu;
            Post({
                "url": localStorage.getItem("API_URL") + "/BaoCaoThongKeNguonThai/InsertUpdate",
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

    RegisterEventsBaoCaoThongKeNguonThai: function () {
        var self = this;
        var $popup = $('#popup-form-bao-cao-thong-ke-nguon-thai');
        $('#btnCreateBaoCaoThongKeNguonThai').off('click').on('click', function () {
            console.log(2213)
            $popup.modal('show');
            self.ResetPopup();
            self.RegisterEventsPopupBaoCaoThongKeNguonThai();
        });
        self.LoadDatatable();
        $(document).on('keypress', function (e) {
            if (e.which == 13) {
                $("#btnSearchBaoCaoThongKeNguonThai").trigger('click');
            }
        });
        $("#btnSearchBaoCaoThongKeNguonThai").off('click').on('click', function () {
            self.table.ajax.reload();
        });
    },
    RegisterEventsPopupBaoCaoThongKeNguonThai: function () {
        var self = this;
        var $popup = $('#popup-form-bao-cao-thong-ke-nguon-thai');
        // Phần xử lý file
        $popup.find('#btnSelectFileBaoCaoThongKeNguonThai').off('click').on('click', function () {
            $popup.find('#fileBaoCaoThongKeNguonThai').trigger("click");
        });

        if ($popup.find('#fileBaoCaoThongKeNguonThai').length > 0) {
            $popup.find('#fileBaoCaoThongKeNguonThai')[0].value = "";
            $popup.find('#fileBaoCaoThongKeNguonThai').off('change').on('change', function (e) {
                var $this = this;
                var file = $popup.find('#fileBaoCaoThongKeNguonThai')[0].files.length > 0 ? $popup.find('#fileBaoCaoThongKeNguonThai')[0].files : null;
                if (file != null) {
                    var dataFile = new FormData();
                    dataFile.append("NhomTaiLieu", "BaoCaoThongKeNguonThai");
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
                                    $popup.find("#tblFileBaoCaoThongKeNguonThai tbody").append($tr);
                                    $popup.find("#tblFileBaoCaoThongKeNguonThai tbody tr:last td:first").append('<a href = "' + localStorage.getItem("API_URL").replace("api", "") + res.Data[i].LinkFile + '" data-id="0" data-IdFile = "' + res.Data[i].IdFile + '"target="_blank">' + file[i].name + '</a>');
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
            self.InsertUpdateBaoCaoThongKeNguonThai();
        });
    },
}

$(document).ready(function () {
    BaoCaoThongKeNguonThaiControl.Init();
});


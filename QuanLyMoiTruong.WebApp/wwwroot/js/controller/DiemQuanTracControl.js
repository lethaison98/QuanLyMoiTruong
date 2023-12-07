if (typeof (DiemQuanTracControl) == "undefined") DiemQuanTracControl = {};
var url = window.location.pathname;
var type = "";
if (url.indexOf("BanDoCacDiemXaThai") != -1) {
    type = "DiemXaThai";
}

if (url.indexOf("BanDoCacThanhPhanMoitruong") != -1) {
    type = "DiemQuanTrac";
}
DiemQuanTracControl = {
    Init: function () {
        DiemQuanTracControl.RegisterEventsDiemQuanTrac();
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
            url: localStorage.getItem("API_URL") + "/DiemQuanTrac/GetAllPaging",
            dom: "rtip",
            data: {
                "requestData": function () {
                    return {
                        "keyword": $('#txtKEY_WORD').val(),
                        "type": type
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
                        "data": "TenDiemQuanTrac",
                        "defaultContent": "",
                    },
                    {
                        "class": "name-control",
                        "data": "DiaChi",
                        "defaultContent": "",
                    },
                    {
                        "class": "name-control",
                        "data": "KinhDo",
                        "defaultContent": "",
                    },
                    {
                        "class": "name-control",
                        "data": "ViDo",
                        "defaultContent": "",
                    },
                    {
                        "class": "name-control",
                        "data": "Loai",
                        "defaultContent": "",
                    },
                    {
                        "class": "function-control",
                        "orderable": false,
                        "defaultContent": "",
                        render: function (data, type, row) {
                            var thaotac = "<div class='hstn-func' style='text-align: center;' data-type='" + JSON.stringify(row) + "'>" +
                                "<a href='javascript:;' class='edit-DiemQuanTrac' data-id='" + row.IdDiemQuanTrac + "'><i class='fas fa-edit' title='Sửa'></i></a>&nbsp" +
                                "<a href='javascript:;' class='remove-DiemQuanTrac text-danger' data-id='" + row.IdDiemQuanTrac + "'><i class='fas fa-trash-alt' title='Xóa' ></i></a>" +
                                "</div>";
                            return thaotac;
                        }
                    }
                ]
            },
            callback: function () {

                $('#tbl tbody .edit-DiemQuanTrac').off('click').on('click', function (e) {
                    var $popup = $('#popup-form-diem-quan-trac');
                    var id = $(this).attr('data-id');
                    self.RegisterEventsPopupDiemQuanTrac();
                    Get({
                        url: localStorage.getItem("API_URL") + '/DiemQuanTrac/GetById',
                        data: {
                            idDiemQuanTrac: id
                        },
                        callback: function (res) {
                            if (res.Success) {
                                $popup.modal('show');
                                self.ResetPopup();
                                $popup.find('.modal-title').text("Chỉnh sửa điểm quan trắc");
                                FillFormData('#FormDiemQuanTrac', res.Data);
                                setTimeout(function () {
                                    $('[data-name="IdDuAn"]').val(res.Data.IdDuAn).change();
                                }, 500)
                            }
                        }
                    });
                });

                $("#tbl tbody .remove-DiemQuanTrac").off('click').on('click', function (e) {
                    var $y = $(this);
                    var id = $y.attr('data-id');
                    if (id != "0") {
                        if (confirm("Xác nhận xóa?") == true) {
                            $.ajax({
                                url: localStorage.getItem("API_URL") + "/DiemQuanTrac/Delete?idDiemQuanTrac=" + $y.attr('data-id') + "&Type=1",
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
        var $popup = $('#popup-form-diem-quan-trac');
        $popup.find('.modal-header').text("Thêm mới điểm quan trắc");
        ResetForm("#FormDiemQuanTrac");
        $popup.find('[data-name="IdDiemQuanTrac"]').val(0);
        $popup.find("#tblFileDiemQuanTrac tbody").html('');
    },

    InsertUpdateDiemQuanTrac: function () {
        var self = this;
        var isValidate = ValidateForm($('#FormDiemQuanTrac'));
        if (isValidate) {
            var $popup = $('#popup-form-diem-quan-trac');
            var data = LoadFormData("#FormDiemQuanTrac");
            var listFileTaiLieu = [];
            $('#tblFileDiemQuanTrac tbody tr').each(function () {
                var $tr = $(this);
                listFileTaiLieu.push({
                    IdFileTaiLieu: $(this).find("td:first a").attr("data-id"),
                    IdFile: $(this).find("td:first a").attr("data-idfile"),
                    NhomTaiLieu: "DiemQuanTrac",
                    LoaiFileTaiLieu: $(this).find('[data-name="LoaiFileTaiLieu"] option:selected').val(),
                    MoTa: $(this).find('[data-name="MoTa"]').val(),
                })

            });
            if (type == "DiemXaThai") {
                data.KhuKinhTe = false;
                data.IdDuAn = $('.ddDuAn option:selected').val()
            } else {
                data.IdDuAn = null;
                data.KhuKinhTe = true;
            }
            data.FileTaiLieu = listFileTaiLieu;
            Post({
                "url": localStorage.getItem("API_URL") + "/DiemQuanTrac/InsertUpdate",
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

    RegisterEventsDiemQuanTrac: function () {
        var self = this;
        var $popup = $('#popup-form-diem-quan-trac');
        $('#btnCreateDiemQuanTrac').off('click').on('click', function () {
            $popup.modal('show');
            self.ResetPopup();
            self.RegisterEventsPopupDiemQuanTrac();
        });
        self.LoadDatatable();
        $(document).on('keypress', function (e) {
            if (e.which == 13) {
                $("#btnSearchDiemQuanTrac").trigger('click');
            }
        });
        $("#btnSearchDiemQuanTrac").off('click').on('click', function () {
            self.table.ajax.reload();
        });
    },
    RegisterEventsPopupDiemQuanTrac: function () {
        var self = this;
        var $popup = $('#popup-form-diem-quan-trac');
        // Phần xử lý file
        $popup.find('#btnSelectFileDiemQuanTrac').off('click').on('click', function () {
            $popup.find('#fileDiemQuanTrac').trigger("click");
        });
        if (type == "DiemXaThai") {
            $popup.find(".ddDuAn").parent().parent().show();
            $popup.find('.ddDuAn').html("");
            $popup.find('[data-name="Loai"]').html("");
            $popup.find('[data-name="Loai"]').append('<option value="KT">Điểm quan trắc khí thải tại cơ sở (KT)</option>');
            $popup.find('[data-name="Loai"]').append('<option value="NT">Điểm quan trắc nước thải tại cơ sở (NT)</option>');
            $popup.find('[data-name="Loai"]').append('<option value="OTHER">Điểm quan trắc khác</option>');
            Get({
                url: localStorage.getItem("API_URL") + "/DuAn/GetAll",
                callback: function (res) {
                    if (res.Success) {
                        $.each(res.Data, function (i, item) {
                            $popup.find('.ddDuAn').append('<option value=' + item.IdDuAn + '>' + item.TenDuAn + '</option>');
                        })
                        $('.select2').select2({
                            dropdownParent: $('#popup-form-diem-quan-trac')
                        });
                    }
                }
            });
        } else {
            $popup.find('[data-name="Loai"]').html("");
            $popup.find('[data-name="Loai"]').append('<option value="K">Điểm quan trắc không khí (K)</option>');
            $popup.find('[data-name="Loai"]').append('<option value="M">Điểm quan trắc nước mặt (M)</option>');
            $popup.find('[data-name="Loai"]').append('<option value="N">Điểm quan trắc nước ngầm (N)</option>');
            $popup.find('[data-name="Loai"]').append('<option value="B">Điểm quan trắc nước biển (B)</option>');
        }

        if ($popup.find('#fileDiemQuanTrac').length > 0) {
            $popup.find('#fileDiemQuanTrac')[0].value = "";
            $popup.find('#fileDiemQuanTrac').off('change').on('change', function (e) {
                var $this = this;
                var file = $popup.find('#fileDiemQuanTrac')[0].files.length > 0 ? $popup.find('#fileDiemQuanTrac')[0].files : null;
                if (file != null) {
                    var dataFile = new FormData();
                    dataFile.append("NhomTaiLieu", "DiemQuanTrac");
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
                                    $popup.find("#tblFileDiemQuanTrac tbody").append($tr);
                                    $popup.find("#tblFileDiemQuanTrac tbody tr:last td:first").append('<a href = "' + localStorage.getItem("API_URL").replace("api", "") + res.Data[i].LinkFile + '" data-id="0" data-IdFile = "' + res.Data[i].IdFile + '"target="_blank">' + file[i].name + '</a>');
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
            self.InsertUpdateDiemQuanTrac();
        });
    },
}

$(document).ready(function () {
    DiemQuanTracControl.Init();
});


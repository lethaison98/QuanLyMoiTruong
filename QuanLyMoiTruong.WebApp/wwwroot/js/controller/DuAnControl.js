if (typeof (DuAnControl) == "undefined") DuAnControl = {};
DuAnControl = {
    Init: function () {
        DuAnControl.RegisterEvents();
    },

    LoadDatatable: function (opts) {
        var self = this;
        self.table = SetDataTable({
            table: $('#tbl'),
            url: localStorage.getItem("API_URL") + "/DuAn/GetAllPaging",
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
                            var thaotac = "<a href='DuAn/DuAnChiTiet/" + row.IdDuAn + "'>" + row.TenDuAn + "</i></a>";
                            return thaotac;
                        }
                    },
                    {
                        "class": "name-control",
                        "data": "TenDoanhNghiep",
                        "defaultContent": ""
                    },
                    {
                        "class": "name-control",
                        "data": "GiayPhepDKKD",
                        "defaultContent": "",
                    },
                    {
                        "class": "function-control",
                        "orderable": false,
                        "defaultContent": "",
                        render: function (data, type, row) {
                            var thaotac = "<div class='hstn-func' style='text-align: center;' data-type='" + JSON.stringify(row) + "'>" +
                                "<a href='javascript:;' class='edit-DuAn' data-id='" + row.IdDuAn + "'><i class='fas fa-edit' title='Sửa'></i></a>&nbsp" +
                                "<a href='javascript:;' class='remove-DuAn text-danger' data-id='" + row.IdDuAn + "'><i class='fas fa-trash-alt' title='Xóa' ></i></a>" +
                                "</div>";
                            return thaotac;
                        }
                    }
                ]
            },
            callback: function () {

                $('#tbl tbody .edit-DuAn').off('click').on('click', function (e) {
                    var id = $(this).attr('data-id');
                    Get({
                        url: localStorage.getItem("API_URL") + '/DuAn/GetById',
                        data: {
                            idDuAn: id
                        },
                        callback: function (res) {
                            if (res.Success) {
                                $('#popup-form-du-an').modal('show');
                                $('#popup-form-du-an .modal-title').text("Chỉnh sửa thông tin dự án");
                                FillFormData('#FormDuAn', res.Data);
                                $("#popup-form-du-an .btn-primary").off('click').on('click', function () {
                                    self.InsertUpdate();
                                });
                            }
                        }
                    });
                });

                $("#tbl tbody .remove-DuAn").off('click').on('click', function (e) {
                    var $y = $(this);
                    var id = $y.attr('data-id');
                    if (id != "0") {
                        if (confirm("Xác nhận xóa?") == true) {
                            $.ajax({
                                url: localStorage.getItem("API_URL") + "/DuAn/Delete?idDuAn=" + $y.attr('data-id') + "&Type=1",
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

    InsertUpdate: function () {
        var self = this;
        var isValidate = ValidateForm($('#FormDuAn'));
        if (isValidate) {
            var data = LoadFormData("#FormDuAn");
            Post({
                "url": localStorage.getItem("API_URL") + "/DuAn/InsertUpdate",
                "data": data,
                callback: function (res) {
                    if (res.Success) {
                        toastr.success('Thực hiện thành công', 'Thông báo')
                        self.table.ajax.reload(null, false);
                        ResetForm("#FormDuAn");
                        $('#btnCloseDuAn').trigger('click');
                    }
                    else {
                        toastr.error(res.Message, 'Có lỗi xảy ra')
                    }
                }
            });
        } else {
            toastr.error("Vui lòng không bỏ trống thông tin có đánh dấu *", 'Có lỗi xảy ra')
        }
    },

    RegisterEvents: function () {
        var self = this;
        self.LoadDatatable();
        $('#btnCreateDuAn').off('click').on('click', function () {
            ResetPopup("#FormDuAn");
            $('#popup-form-du-an').modal('show');
            $("#popup-form-du-an .btn-primary").off('click').on('click', function () {
                console.log(1);
                self.InsertUpdate();
            });
        });
        $(document).on('keypress', function (e) {
            if (e.which == 13) {
                $("#btnSearchDuAn").trigger('click');
            }
        });
        $("#btnSearchDuAn").off('click').on('click', function () {
            self.table.ajax.reload();
        });
    },
}

$(document).ready(function () {
    DuAnControl.Init();
});


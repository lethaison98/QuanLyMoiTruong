if (typeof (UserControl) == "undefined") UserControl = {};
UserControl = {
    Init: function () {
        UserControl.RegisterEvents();
    },

    LoadDatatable: function (opts) {
        var self = this;
        self.table = SetDataTable({
            table: $('#tbl'),
            url: localStorage.getItem("API_URL") + "/User/GetAllPaging",
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
                            var thaotac = "<a href='User/UserChiTiet/" + row.IdUser + "'>" + row.UserName + "</i></a>";
                            return thaotac;
                        }
                    },
                    {
                        "class": "name-control",
                        "data": "FullName",
                        "defaultContent": ""
                    },
                    {
                        "class": "name-control",
                        "data": "Email",
                        "defaultContent": "",
                    },
                    {
                        "class": "function-control",
                        "orderable": false,
                        "defaultContent": "",
                        render: function (data, type, row) {
                            var thaotac = "<div class='hstn-func' style='text-align: center;' data-type='" + JSON.stringify(row) + "'>" +
                                "<a href='javascript:;' class='edit-User' data-id='" + row.IdUser + "'><i class='fas fa-edit' title='Sửa'></i></a>&nbsp" +
                                "<a href='javascript:;' class='remove-User text-danger' data-id='" + row.IdUser + "'><i class='fas fa-trash-alt' title='Xóa' ></i></a>" +
                                "</div>";
                            return thaotac;
                        }
                    }
                ]
            },
            callback: function () {

                $('#tbl tbody .edit-User').off('click').on('click', function (e) {
                    var id = $(this).attr('data-id');
                    Get({
                        url: localStorage.getItem("API_URL") + '/User/GetById',
                        data: {
                            idUser: id
                        },
                        callback: function (res) {
                            if (res.Success) {
                                $('#popup-form-user').modal('show');
                                $('#popup-form-user .modal-title').text("Chỉnh sửa thông tin tài khoản");
                                FillFormData('#FormUser', res.Data);
                                $("#popup-form-user .btn-primary").off('click').on('click', function () {
                                    self.InsertUpdate();
                                });
                            }
                        }
                    });
                });

                $("#tbl tbody .remove-User").off('click').on('click', function (e) {
                    var $y = $(this);
                    var id = $y.attr('data-id');
                    if (id != "0") {
                        if (confirm("Xác nhận xóa?") == true) {
                            $.ajax({
                                url: localStorage.getItem("API_URL") + "/User/Delete?idUser=" + $y.attr('data-id') + "&Type=1",
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
        var isValidate = ValidateForm($('#FormUser'));
        if (isValidate) {
            var data = LoadFormData("#FormUser");
            Post({
                "url": localStorage.getItem("API_URL") + "/User/InsertUpdate",
                "data": data,
                callback: function (res) {
                    if (res.Success) {
                        toastr.success('Thực hiện thành công', 'Thông báo')
                        self.table.ajax.reload(null, false);
                        $('#btnCloseUser').trigger('click');
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
        console.log(123);
        var self = this;
        self.LoadDatatable();
        $('#btnCreateUser').off('click').on('click', function () {
            ResetForm("#FormUser");
            $('#popup-form-user').find('[data-name="IdUser"]').val("00000000-0000-0000-0000-000000000000");
            $('#popup-form-user').modal('show');
            $("#popup-form-user .btn-primary").off('click').on('click', function () {
                console.log(1);
                self.InsertUpdate();
            });
        });
        $(document).on('keypress', function (e) {
            if (e.which == 13) {
                $("#btnSearchUser").trigger('click');
            }
        });
        $("#btnSearchUser").off('click').on('click', function () {
            self.table.ajax.reload();
        });
    },
}

$(document).ready(function () {
    UserControl.Init();
});


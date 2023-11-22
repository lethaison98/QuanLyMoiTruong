if (typeof (KhuCongNghiepControl) == "undefined") KhuCongNghiepControl = {};
KhuCongNghiepControl = {
    Init: function () {
        KhuCongNghiepControl.RegisterEvents();
    },

    LoadDatatable: function (opts) {
        var self = this;
        self.table = SetDataTable({
            table: $('#tbl'),
            url: localStorage.getItem("API_URL") + "/KhuCongNghiep/GetAllPaging",
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
                            var thaotac = "<a href='KhuCongNghiep/KhuCongNghiepChiTiet/" + row.IdKhuCongNghiep + "'>" + row.TenKhuCongNghiep + "</i></a>";
                            return thaotac;
                        }
                    },
                    {
                        "class": "name-control",
                        "data": "DiaDiem",
                        "defaultContent": "",
                    },
                    {
                        "class": "name-control",
                        "data": "TenChuDauTu",
                        "defaultContent": "",
                    },
                    {
                        "class": "name-control",
                        render: function (data, type, row) {
                            if (row.ThuocKhuKinhTe)
                                return "Có";
                            else
                                return "Không";
                        }
                    },
                    {
                        "class": "function-control",
                        "orderable": false,
                        "defaultContent": "",
                        render: function (data, type, row) {
                            var thaotac = "<div class='hstn-func' style='text-align: center;' data-type='" + JSON.stringify(row) + "'>" +
                                "<a href='javascript:;' class='edit-KhuCongNghiep' data-id='" + row.IdKhuCongNghiep + "'><i class='fas fa-edit' title='Sửa'></i></a>&nbsp" +
                                "<a href='javascript:;' class='remove-KhuCongNghiep text-danger' data-id='" + row.IdKhuCongNghiep + "'><i class='fas fa-trash-alt' title='Xóa' ></i></a>" +
                                "</div>";
                            return thaotac;
                        }
                    }
                ]
            },
            callback: function () {

                $('#tbl tbody .edit-KhuCongNghiep').off('click').on('click', function (e) {
                    var id = $(this).attr('data-id');
                    Get({
                        url: localStorage.getItem("API_URL") + '/KhuCongNghiep/GetById',
                        data: {
                            idKhuCongNghiep: id
                        },
                        callback: function (res) {
                            if (res.Success) {
                                $('#popup-form-khu-cong-nghiep').modal('show');
                                $('#popup-form-khu-cong-nghiep .modal-title').text("Chỉnh sửa thông tin khu công nghiệp");
                                FillFormData('#FormKhuCongNghiep', res.Data);
                                $("#popup-form-khu-cong-nghiep .btn-primary").off('click').on('click', function () {
                                    self.InsertUpdate();
                                });
                            }
                        }
                    });
                });

                $("#tbl tbody .remove-KhuCongNghiep").off('click').on('click', function (e) {
                    var $y = $(this);
                    var id = $y.attr('data-id');
                    if (id != "0") {
                        if (confirm("Xác nhận xóa?") == true) {
                            $.ajax({
                                url: localStorage.getItem("API_URL") + "/KhuCongNghiep/Delete?idKhuCongNghiep=" + $y.attr('data-id') + "&Type=1",
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
        var isValidate = ValidateForm($('#FormKhuCongNghiep'));
        if (isValidate) {
            var data = LoadFormData("#FormKhuCongNghiep");
            console.log(data);
            Post({
                "url": localStorage.getItem("API_URL") + "/KhuCongNghiep/InsertUpdate",
                "data": data,
                callback: function (res) {
                    if (res.Success) {
                        toastr.success('Thực hiện thành công', 'Thông báo')
                        self.table.ajax.reload(null, false);
                        $('#btnCloseKhuCongNghiep').trigger('click');
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
        $('#btnCreateKhuCongNghiep').off('click').on('click', function () {
            ResetForm("#FormKhuCongNghiep");
            $('#popup-form-khu-cong-nghiep').find('[data-name="IdKhuCongNghiep"]').val(0);
            $('#popup-form-khu-cong-nghiep').modal('show');
            $("#popup-form-khu-cong-nghiep .btn-primary").off('click').on('click', function () {
                console.log(1);
                self.InsertUpdate();
            });
        });
        $(document).on('keypress', function (e) {
            if (e.which == 13) {
                $("#btnSearchKhuCongNghiep").trigger('click');
            }
        });
        $("#btnSearchKhuCongNghiep").off('click').on('click', function () {
            self.table.ajax.reload();
        });
    },
}

$(document).ready(function () {
    KhuCongNghiepControl.Init();
});


﻿using QuanLyMoiTruong.Application.Requests;
using QuanLyMoiTruong.Application.ViewModels;
using QuanLyMoiTruong.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyMoiTruong.Application.Interfaces
{
    public interface IHoSoKiemTraXuPhatService:IBaseService<HoSoKiemTraXuPhat, int, HoSoKiemTraXuPhatViewModel, HoSoKiemTraXuPhatRequest>
    {
        public Task<ApiResult<IList<HoSoKiemTraXuPhatViewModel>>> GetListHoSoKiemTraXuPhatByDuAn(int idDuAn);
        public Task<ApiResult<IList<HoSoKiemTraXuPhatViewModel>>> GetListHoSoKiemTraXuPhatByKhuCongNghiep(int idKhuCongNghiep);

    }
}

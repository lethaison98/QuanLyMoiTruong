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
    public interface IBaoCaoBaoVeMoiTruongHangNamService:IBaseService<BaoCaoBaoVeMoiTruongHangNam, int, BaoCaoBaoVeMoiTruongHangNamViewModel, BaoCaoBaoVeMoiTruongHangNamRequest>
    {
        public Task<ApiResult<IList<BaoCaoBaoVeMoiTruongHangNamViewModel>>> GetListBaoCaoBaoVeMoiTruongHangNamByDuAn(int idDuAn);
      
    }
}

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
    public interface IBaoCaoBaoVeMoiTruongService:IBaseService<BaoCaoBaoVeMoiTruong, int, BaoCaoBaoVeMoiTruongViewModel, BaoCaoBaoVeMoiTruongRequest>
    {
        public Task<ApiResult<IList<BaoCaoBaoVeMoiTruongViewModel>>> GetListBaoCaoBaoVeMoiTruongByDuAn(int idDuAn);
        public Task<ApiResult<IList<BaoCaoBaoVeMoiTruongViewModel>>> GetListBaoCaoBaoVeMoiTruongByKhuCongNghiep(int idKhuCongNghiep);
        public Task<ApiResult<IList<BaoCaoBaoVeMoiTruongViewModel>>> GetListBaoCaoBaoVeMoiTruongByKhuKinhTe();
        public Task<ApiResult<BaoCaoBaoVeMoiTruong>> UpdateKetQuaBaoCaoBaoVeMoiTruong(int id);
      
    }
}

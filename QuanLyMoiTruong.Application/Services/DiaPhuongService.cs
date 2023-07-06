using QuanLyMoiTruong.Application.Interfaces;
using QuanLyMoiTruong.Data.Entities;
using QuanLyMoiTruong.UnitOfWork;
using Microsoft.EntityFrameworkCore;
using QuanLyMoiTruong.Application.ViewModels;
using QuanLyMoiTruong.UnitOfWork.Collections;
using System;
using System.Globalization;
using System.Reflection.Metadata;
using QuanLyMoiTruong.Application.Requests;
using System.Linq.Expressions;
using QuanLyMoiTruong.Common.Expressions;
using Microsoft.AspNetCore.Http.Features;

namespace QuanLyMoiTruong.Application.Services
{

    public class DiaPhuongService : IDiaPhuongService
    {
        private readonly IUnitOfWork _unitOfWork;
        public DiaPhuongService(IUnitOfWork unitOfWork)
        {
            _unitOfWork= unitOfWork;
        }

        public async Task<ApiResult<List<PhuongXa>>> GetAllPhuongXa()
        {
            var result = new List<PhuongXa>();
            var entities = await _unitOfWork.GetRepository<PhuongXa>().GetAllAsync();
            result = entities.ToList();
            return new ApiSuccessResult<List<PhuongXa>>() { Data = result };
        }

        public async Task<ApiResult<List<QuanHuyen>>> GetAllQuanHuyen()
        {
            var result = new List<QuanHuyen>();
            var entities = await _unitOfWork.GetRepository<QuanHuyen>().GetAllAsync();
            result = entities.ToList();
            return new ApiSuccessResult<List<QuanHuyen>>() { Data = result };
        }

        public async Task<ApiResult<List<TinhThanh>>> GetAllTinhThanh()
        {
            var result = new List<TinhThanh>();
            var entities = await _unitOfWork.GetRepository<TinhThanh>().GetAllAsync();
            result = entities.ToList();
            return new ApiSuccessResult<List<TinhThanh>>() { Data = result };
        }

        public async Task<ApiResult<DiaPhuongViewModel>> GetDiaPhuongById(int idDiaPhuong)
        {
            var result = new DiaPhuongViewModel();
            if(idDiaPhuong < 1000)
            {
                var data = await _unitOfWork.GetRepository<TinhThanh>().GetFirstOrDefaultAsync(predicate: x => x.IdTinhThanh == idDiaPhuong, include : x=> x.Include(x=> x.DSQuanHuyen).ThenInclude(x=> x.DSPhuongXa));
                result = MapTinhThanhToDiaPhuongViewModel(data);
            }
            else if(idDiaPhuong < 10000)
            {
                var data = await _unitOfWork.GetRepository<QuanHuyen>().GetFirstOrDefaultAsync(predicate: x => x.IdQuanHuyen == idDiaPhuong, include: x => x.Include(x => x.DSPhuongXa));
                result = MapQuanHuyenToDiaPhuongViewModel(data);
            }
            else
            {
                var data = await _unitOfWork.GetRepository<PhuongXa>().GetFirstOrDefaultAsync(predicate: x => x.IdPhuongXa == idDiaPhuong);
                result = MapPhuongXaToDiaPhuongViewModel(data);
            }
            return new ApiSuccessResult<DiaPhuongViewModel>() { Data = result };
        }

        public Task<ApiResult<List<DiaPhuongViewModel>>> GetDsDiaPhuongByIds(string idsDiaPhuong)
        {
            throw new NotImplementedException();
        }
        public DiaPhuongViewModel MapTinhThanhToDiaPhuongViewModel(TinhThanh entity)
        {
            var result = new DiaPhuongViewModel();
            result.IdDiaPhuong = entity.IdTinhThanh;
            result.TenDiaPhuong = entity.TenTinhThanh;
            result.TenDayDu = entity.TenTinhThanh;
            result.IdDiaPhuongCha = 0;
            result.Cap = entity.Cap;
            result.Type = "TinhThanh";
            result.DiaPhuongCon = entity.DSQuanHuyen.Select(MapQuanHuyenToDiaPhuongViewModel).ToList();
            return result;
        }
        public DiaPhuongViewModel MapQuanHuyenToDiaPhuongViewModel(QuanHuyen entity)
        {
            var result = new DiaPhuongViewModel();
            result.IdDiaPhuong = entity.IdQuanHuyen;
            result.TenDiaPhuong = entity.TenQuanHuyen;
            result.TenDayDu =  entity.TenQuanHuyen + " - " + entity.TenTinhThanh;
            result.IdDiaPhuongCha = entity.IdTinhThanh;
            result.Cap = entity.Cap;
            result.Type = "QuanHuyen";
            result.DiaPhuongCon = entity.DSPhuongXa.Select(MapPhuongXaToDiaPhuongViewModel).ToList();
            return result;
        }
        public DiaPhuongViewModel MapPhuongXaToDiaPhuongViewModel(PhuongXa entity)
        {
            var result = new DiaPhuongViewModel();
            result.IdDiaPhuong = entity.IdPhuongXa;
            result.TenDiaPhuong = entity.TenPhuongXa;
            result.TenDayDu = entity.TenPhuongXa + " - " + entity.TenQuanHuyen + " - " + entity.TenTinhThanh;
            result.IdDiaPhuongCha = entity.IdQuanHuyen;
            result.Cap = entity.Cap;
            result.Type = "PhuongXa";
            return result;
        }
    }

}

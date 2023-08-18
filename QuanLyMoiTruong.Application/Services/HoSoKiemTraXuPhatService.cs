﻿using QuanLyMoiTruong.Application.Interfaces;
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
using QuanLyMoiTruong.Common.Enums;
using QuanLyMoiTruong.Application.Request;

namespace QuanLyMoiTruong.Application.Services
{

    public class HoSoKiemTraXuPhatService : IHoSoKiemTraXuPhatService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IFileTaiLieuService _fileTaiLieuService;

        public HoSoKiemTraXuPhatService(IUnitOfWork unitOfWork, IFileTaiLieuService fileTaiLieuService)
        {
            _unitOfWork= unitOfWork;
            _fileTaiLieuService= fileTaiLieuService;
        }

        public async Task<ApiResult<bool>> Delete(int id)
        {
            var entity =  await _unitOfWork.GetRepository<HoSoKiemTraXuPhat>().FindAsync(id);
            if (entity != null)
            {
                entity.IsDeleted= true;
                await _unitOfWork.SaveChangesAsync();
                return new ApiSuccessResult<bool>() {};
            }
            else
            {
                return new ApiErrorResult<bool>("Không tồn tại dữ liệu cần xóa") ;
            }
        } 

        public async Task<ApiResult<IList<HoSoKiemTraXuPhatViewModel>>> GetAll()
        {
            var result = new List<HoSoKiemTraXuPhatViewModel>();
            var entities =  await _unitOfWork.GetRepository<HoSoKiemTraXuPhat>().GetAllAsync(predicate: x => !x.IsDeleted);
            result = entities.Select(MapEntityToViewModel).ToList();
            return new ApiSuccessResult<IList<HoSoKiemTraXuPhatViewModel>>() { Data = result };
        }

        public async Task<ApiResult<HoSoKiemTraXuPhatViewModel>> GetById(int id)
        {
            var result = new HoSoKiemTraXuPhatViewModel();
            var data = await _unitOfWork.GetRepository<HoSoKiemTraXuPhat>().GetFirstOrDefaultAsync(predicate: x => x.IdHoSoKiemTraXuPhat == id, include: x => x.Include(y => y.DuAn).Include(y=> y.KhuCongNghiep));
            result = MapEntityToViewModel(data);
            var dsFile = await _fileTaiLieuService.GetByTaiLieu(data.IdHoSoKiemTraXuPhat, NhomTaiLieuEnum.HoSoKiemTraXuPhat.ToString());
            if (dsFile.Success)
            {
                result.FileTaiLieu = dsFile.Data.ToList();
            }
            return new ApiSuccessResult<HoSoKiemTraXuPhatViewModel>() {Data = result };
        }

        public async Task<ApiResult<HoSoKiemTraXuPhat>> Insert(HoSoKiemTraXuPhatViewModel obj)
        {
            var entity = new HoSoKiemTraXuPhat();
            entity = MapViewModelToEntity(obj, entity);
            await _unitOfWork.GetRepository<HoSoKiemTraXuPhat>().InsertAsync(entity);
            await _unitOfWork.SaveChangesAsync();

            var fileTaiLieuRequest = new FileTaiLieuRequest();
            fileTaiLieuRequest.IdTaiLieu = entity.IdHoSoKiemTraXuPhat;
            fileTaiLieuRequest.NhomTaiLieu = NhomTaiLieuEnum.HoSoKiemTraXuPhat.ToString();
            fileTaiLieuRequest.FileTaiLieu = obj.FileTaiLieu;
            await _fileTaiLieuService.UpdateAll(fileTaiLieuRequest);
            return new ApiSuccessResult<HoSoKiemTraXuPhat>() { Data = entity };
        }

        public async Task<ApiResult<bool>> Remove(int id)
        {
            var entity = await _unitOfWork.GetRepository<HoSoKiemTraXuPhat>().FindAsync(id);
            if (entity != null)
            {
                _unitOfWork.GetRepository<HoSoKiemTraXuPhat>().Remove(id);
                return new ApiSuccessResult<bool>() { };
            }
            else
            {
                return new ApiErrorResult<bool>("Không tồn tại dữ liệu cần xóa");
            }
        }

        public async Task<ApiResult<IPagedList<HoSoKiemTraXuPhatViewModel>>> GetAllPaging(HoSoKiemTraXuPhatRequest request)
        {
            Expression<Func<HoSoKiemTraXuPhat, bool>> filter = x => true;
            if (!string.IsNullOrWhiteSpace(request.FullTextSearch))
            {
                var fullTextSearch = request.FullTextSearch.ToLowerInvariant();
                filter = filter.And(p => p.TenHoSo.ToLower().Contains(fullTextSearch));
            }
            filter = filter.And(p => !p.IsDeleted);
            var data = await _unitOfWork.GetRepository<HoSoKiemTraXuPhat>().GetPagedListAsync(predicate: filter, pageIndex: request.PageIndex, pageSize: request.PageSize);
            data.Items.Select(MapEntityToViewModel);
            var result = new PagedList<HoSoKiemTraXuPhatViewModel>();

            result.PageIndex = data.PageIndex;
            result.PageSize = data.PageSize;
            result.IndexFrom= data.IndexFrom;
            result.TotalCount = data.TotalCount;
            result.TotalPages = data.TotalPages;    
            result.Items = data.Items.Select(MapEntityToViewModel).ToList();
            return new ApiSuccessResult<IPagedList<HoSoKiemTraXuPhatViewModel>>() { Data = result };
        }

        public async Task<ApiResult<HoSoKiemTraXuPhat>> Update(HoSoKiemTraXuPhatViewModel obj)
        {
            var entity = await _unitOfWork.GetRepository<HoSoKiemTraXuPhat>().GetFirstOrDefaultAsync(predicate: x => x.IdHoSoKiemTraXuPhat == obj.IdHoSoKiemTraXuPhat);
            entity = MapViewModelToEntity(obj, entity);
            _unitOfWork.GetRepository<HoSoKiemTraXuPhat>().Update(entity);
            await _unitOfWork.SaveChangesAsync();

            var fileTaiLieuRequest = new FileTaiLieuRequest();
            fileTaiLieuRequest.IdTaiLieu = entity.IdHoSoKiemTraXuPhat;
            fileTaiLieuRequest.NhomTaiLieu = NhomTaiLieuEnum.HoSoKiemTraXuPhat.ToString();
            fileTaiLieuRequest.FileTaiLieu = obj.FileTaiLieu;
            await _fileTaiLieuService.UpdateAll(fileTaiLieuRequest);

            return new ApiSuccessResult<HoSoKiemTraXuPhat>() { Data = entity };
        }
        public async Task<ApiResult<IList<HoSoKiemTraXuPhatViewModel>>> GetListHoSoKiemTraXuPhatByDuAn(int idDuAn)
        {
            var result = new List<HoSoKiemTraXuPhatViewModel>();
            var entities = await _unitOfWork.GetRepository<HoSoKiemTraXuPhat>().GetAllAsync(predicate: x => !x.IsDeleted && x.IdDuAn == idDuAn, include: x => x.Include(x => x.DuAn));
            result = entities.Select(MapEntityToViewModel).ToList();
            foreach (var item in result)
            {
                var dsFile = await _fileTaiLieuService.GetByTaiLieu(item.IdHoSoKiemTraXuPhat, NhomTaiLieuEnum.HoSoKiemTraXuPhat.ToString());
                if (dsFile.Success)
                {
                    item.FileTaiLieu = dsFile.Data.ToList();
                }
            }
            return new ApiSuccessResult<IList<HoSoKiemTraXuPhatViewModel>>() { Data = result };
        }
        public async Task<ApiResult<IList<HoSoKiemTraXuPhatViewModel>>> GetListHoSoKiemTraXuPhatByKhuCongNghiep(int idKhuCongNghiep)
        {
            var result = new List<HoSoKiemTraXuPhatViewModel>();
            var entities = await _unitOfWork.GetRepository<HoSoKiemTraXuPhat>().GetAllAsync(predicate: x => !x.IsDeleted && x.IdKhuCongNghiep == idKhuCongNghiep, include: x => x.Include(x => x.KhuCongNghiep));
            result = entities.Select(MapEntityToViewModel).ToList();
            foreach (var item in result)
            {
                var dsFile = await _fileTaiLieuService.GetByTaiLieu(item.IdHoSoKiemTraXuPhat, NhomTaiLieuEnum.HoSoKiemTraXuPhat.ToString());
                if (dsFile.Success)
                {
                    item.FileTaiLieu = dsFile.Data.ToList();
                }
            }
            return new ApiSuccessResult<IList<HoSoKiemTraXuPhatViewModel>>() { Data = result };
        }
        public HoSoKiemTraXuPhatViewModel MapEntityToViewModel(HoSoKiemTraXuPhat entity) {
            var result = new HoSoKiemTraXuPhatViewModel();
            result.IdHoSoKiemTraXuPhat = entity.IdHoSoKiemTraXuPhat;
            result.TenHoSo = entity.TenHoSo;
            //result.NgayBaoCao = entity.NgayBaoCao != null ? entity.NgayBaoCao.Value.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture) : "";
            result.IdDuAn = entity.IdDuAn;
            result.TenDuAn = entity.IdDuAn != null ? entity.DuAn.TenDuAn : "";
            result.TenDoanhNghiep = entity.IdDuAn != null ? entity.DuAn.TenDoanhNghiep : "";
            result.IdKhuCongNghiep = entity.IdKhuCongNghiep;
            result.TenKhuCongNghiep = entity.IdKhuCongNghiep != null ? entity.KhuCongNghiep.TenKhuCongNghiep : "";
            result.TenChuDauTu = entity.IdKhuCongNghiep != null ? entity.KhuCongNghiep.TenChuDauTu : "";

            return result;
        }
        public HoSoKiemTraXuPhat MapViewModelToEntity(HoSoKiemTraXuPhatViewModel viewModel, HoSoKiemTraXuPhat entity)
        {
            entity.IdHoSoKiemTraXuPhat = viewModel.IdHoSoKiemTraXuPhat;
            entity.TenHoSo = viewModel.TenHoSo;
            entity.IdDuAn = viewModel.IdDuAn == 0 ? null : viewModel.IdDuAn;
            entity.IdKhuCongNghiep = viewModel.IdKhuCongNghiep == 0 ? null : viewModel.IdKhuCongNghiep;

            return entity;
        }
    }

}

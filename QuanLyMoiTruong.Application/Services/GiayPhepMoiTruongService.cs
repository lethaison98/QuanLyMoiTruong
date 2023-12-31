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
using QuanLyMoiTruong.Application.Request;
using QuanLyMoiTruong.Common.Enums;

namespace QuanLyMoiTruong.Application.Services
{

    public class GiayPhepMoiTruongService : IGiayPhepMoiTruongService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IFileTaiLieuService _fileTaiLieuService;
        public GiayPhepMoiTruongService(IUnitOfWork unitOfWork, IFileTaiLieuService fileTaiLieuService)
        {
            _unitOfWork = unitOfWork;
            _fileTaiLieuService = fileTaiLieuService;
        }

        public async Task<ApiResult<bool>> Delete(int id)
        {
            var entity = await _unitOfWork.GetRepository<GiayPhepMoiTruong>().FindAsync(id);
            if (entity != null)
            {
                entity.IsDeleted = true;
                await _unitOfWork.SaveChangesAsync();
                return new ApiSuccessResult<bool>() { };
            }
            else
            {
                return new ApiErrorResult<bool>("Không tồn tại dữ liệu cần xóa");
            }
        }

        public async Task<ApiResult<IList<GiayPhepMoiTruongViewModel>>> GetAll()
        {
            var result = new List<GiayPhepMoiTruongViewModel>();
            var entities = await _unitOfWork.GetRepository<GiayPhepMoiTruong>().GetAllAsync(predicate: x => !x.IsDeleted);
            result = entities.Select(MapEntityToViewModel).ToList();
            return new ApiSuccessResult<IList<GiayPhepMoiTruongViewModel>>() { Data = result };
        }
        public async Task<ApiResult<GiayPhepMoiTruongViewModel>> GetById(int id)
        {
            var result = new GiayPhepMoiTruongViewModel();
            var data = await _unitOfWork.GetRepository<GiayPhepMoiTruong>().GetFirstOrDefaultAsync(predicate: x => x.IdGiayPhepMoiTruong == id, include: x => x.Include(y => y.DuAn).Include(y => y.KhuCongNghiep));
            result = MapEntityToViewModel(data);
            var dsFile = await _fileTaiLieuService.GetByTaiLieu(data.IdGiayPhepMoiTruong, NhomTaiLieuEnum.GiayPhepMoiTruong.ToString());
            if (dsFile.Success)
            {
                result.FileTaiLieu = dsFile.Data.ToList();
            }
            return new ApiSuccessResult<GiayPhepMoiTruongViewModel>() { Data = result };
        }

        public async Task<ApiResult<GiayPhepMoiTruong>> Insert(GiayPhepMoiTruongViewModel obj)
        {
            var entity = new GiayPhepMoiTruong();
            entity = MapViewModelToEntity(obj);
            _unitOfWork.GetRepository<GiayPhepMoiTruong>().Update(entity);
            await _unitOfWork.SaveChangesAsync();

            var fileTaiLieuRequest = new FileTaiLieuRequest();
            fileTaiLieuRequest.IdTaiLieu = entity.IdGiayPhepMoiTruong;
            fileTaiLieuRequest.NhomTaiLieu = NhomTaiLieuEnum.GiayPhepMoiTruong.ToString();
            fileTaiLieuRequest.FileTaiLieu = obj.FileTaiLieu;
            await _fileTaiLieuService.UpdateAll(fileTaiLieuRequest);

            return new ApiSuccessResult<GiayPhepMoiTruong>() { Data = entity };
        }

        public async Task<ApiResult<bool>> Remove(int id)
        {
            var entity = await _unitOfWork.GetRepository<GiayPhepMoiTruong>().FindAsync(id);
            if (entity != null)
            {
                _unitOfWork.GetRepository<GiayPhepMoiTruong>().Remove(id);
                return new ApiSuccessResult<bool>() { };
            }
            else
            {
                return new ApiErrorResult<bool>("Không tồn tại dữ liệu cần xóa");
            }
        }

        public async Task<ApiResult<IPagedList<GiayPhepMoiTruongViewModel>>> GetAllPaging(GiayPhepMoiTruongRequest request)
        {
            Expression<Func<GiayPhepMoiTruong, bool>> filter = x => true;
            if (!string.IsNullOrWhiteSpace(request.FullTextSearch))
            {
                var fullTextSearch = request.FullTextSearch.ToLowerInvariant();
                filter = filter.And(p => p.SoGiayPhep.ToLower().Contains(fullTextSearch) || p.TenGiayPhep.ToLower().Contains(fullTextSearch));
            }
            filter = filter.And(p => !p.IsDeleted);
            var data = await _unitOfWork.GetRepository<GiayPhepMoiTruong>().GetPagedListAsync(predicate: filter, pageIndex: request.PageIndex, pageSize: request.PageSize);
            data.Items.Select(MapEntityToViewModel);
            var result = new PagedList<GiayPhepMoiTruongViewModel>();

            result.PageIndex = data.PageIndex;
            result.PageSize = data.PageSize;
            result.IndexFrom = data.IndexFrom;
            result.TotalCount = data.TotalCount;
            result.TotalPages = data.TotalPages;
            result.Items = data.Items.Select(MapEntityToViewModel).ToList();
            return new ApiSuccessResult<IPagedList<GiayPhepMoiTruongViewModel>>() { Data = result };
        }

        public async Task<ApiResult<GiayPhepMoiTruong>> Update(GiayPhepMoiTruongViewModel obj)
        {
            var entity = await _unitOfWork.GetRepository<GiayPhepMoiTruong>().GetFirstOrDefaultAsync(predicate: x => x.IdGiayPhepMoiTruong == obj.IdGiayPhepMoiTruong);
            entity = MapViewModelToEntity(obj);
            _unitOfWork.GetRepository<GiayPhepMoiTruong>().Update(entity);
            await _unitOfWork.SaveChangesAsync();

            var fileTaiLieuRequest = new FileTaiLieuRequest();
            fileTaiLieuRequest.IdTaiLieu = entity.IdGiayPhepMoiTruong;
            fileTaiLieuRequest.NhomTaiLieu = NhomTaiLieuEnum.GiayPhepMoiTruong.ToString();
            fileTaiLieuRequest.FileTaiLieu = obj.FileTaiLieu;
            await _fileTaiLieuService.UpdateAll(fileTaiLieuRequest);

            return new ApiSuccessResult<GiayPhepMoiTruong>() { Data = entity };
        }
        public async Task<ApiResult<IList<GiayPhepMoiTruongViewModel>>> GetListGiayPhepMoiTruongByDuAn(int idDuAn)
        {
            var result = new List<GiayPhepMoiTruongViewModel>();
            var entities = await _unitOfWork.GetRepository<GiayPhepMoiTruong>().GetAllAsync(predicate: x => !x.IsDeleted && x.IdDuAn == idDuAn, include: x => x.Include(x => x.DuAn), orderBy: o => o.OrderByDescending(s=> s.NgayCap));
            result = entities.Select(MapEntityToViewModel).ToList();
            foreach (var item in result)
            {
                var dsFile = await _fileTaiLieuService.GetByTaiLieu(item.IdGiayPhepMoiTruong, NhomTaiLieuEnum.GiayPhepMoiTruong.ToString());
                if (dsFile.Success)
                {
                    item.FileTaiLieu = dsFile.Data.ToList();
                }
            }
            return new ApiSuccessResult<IList<GiayPhepMoiTruongViewModel>>() { Data = result };
        }
        public async Task<ApiResult<IList<GiayPhepMoiTruongViewModel>>> GetListGiayPhepMoiTruongByKhuCongNghiep(int idKhuCongNghiep)
        {
            var result = new List<GiayPhepMoiTruongViewModel>();
            var entities = await _unitOfWork.GetRepository<GiayPhepMoiTruong>().GetAllAsync(predicate: x => !x.IsDeleted && x.IdKhuCongNghiep == idKhuCongNghiep, include: x => x.Include(x => x.KhuCongNghiep));
            result = entities.Select(MapEntityToViewModel).ToList();
            foreach (var item in result)
            {
                var dsFile = await _fileTaiLieuService.GetByTaiLieu(item.IdGiayPhepMoiTruong, NhomTaiLieuEnum.GiayPhepMoiTruong.ToString());
                if (dsFile.Success)
                {
                    item.FileTaiLieu = dsFile.Data.ToList();
                }
            }
            return new ApiSuccessResult<IList<GiayPhepMoiTruongViewModel>>() { Data = result };
        }
        public async Task<ApiResult<IList<BaoCaoCapGiayPhepMoiTruongViewModel>>> GetListByKhoangThoiGian(GiayPhepMoiTruongRequest request)
        {
            var entities = await _unitOfWork.GetRepository<GiayPhepMoiTruong>().GetAllAsync(predicate: x => !x.IsDeleted && x.IdDuAn != null, include: x=> x.Include(x=> x.DuAn).Include(x=> x.DuAn.KhuCongNghiep));
            if (!String.IsNullOrEmpty(request.TenGiayPhep))
            {
                entities = entities.Where(x => x.TenGiayPhep == request.TenGiayPhep).ToList();
            }
            if (request.IdKhuCongNghiep != 0)
            {
                entities = entities.Where(x => x.DuAn.IdKhuCongNghiep == request.IdKhuCongNghiep).ToList();
            }
            if (request.TuNgay != null)
            {
                entities = entities.Where(x => x.NgayCap >= request.TuNgay).ToList();
            }
            if (request.DenNgay != null)
            {
                entities = entities.Where(x => x.NgayCap <= request.DenNgay).ToList();
            }
            var result = new List<BaoCaoCapGiayPhepMoiTruongViewModel>();
            foreach(var item in entities)
            {
                var data = new BaoCaoCapGiayPhepMoiTruongViewModel();
                data.IdGiayPhepMoiTruong = item.IdGiayPhepMoiTruong;
                data.TenGiayPhep = item.TenGiayPhep;
                data.SoGiayPhep = item.SoGiayPhep;
                data.CoQuanCap = item.CoQuanCap;
                data.NgayCap = item.NgayCap != null ? item.NgayCap.Value.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture) : "";
                data.IdDuAn = item.IdDuAn;
                data.TenDuAn = item.DuAn != null ? item.DuAn.TenDuAn : "";
                data.DiaChi = item.DuAn != null ? item.DuAn.DiaChi : "";
                data.LoaiHinhSanXuat = item.DuAn != null ? item.DuAn.LoaiHinhSanXuat : "";
                data.QuyMo = item.DuAn != null ? item.DuAn.QuyMo : "";
                data.TenNguoiDaiDien = item.DuAn != null ? item.DuAn.TenNguoiDaiDien : "";
                data.TenDoanhNghiep = item.DuAn != null ? item.DuAn.TenDoanhNghiep : "";
                data.TenKhuCongNghiep = item.DuAn.KhuCongNghiep != null ? item.DuAn.KhuCongNghiep.TenKhuCongNghiep : "";
                result.Add(data);
            }
            return new ApiSuccessResult<IList<BaoCaoCapGiayPhepMoiTruongViewModel>>() { Data = result };
        }
        public GiayPhepMoiTruongViewModel MapEntityToViewModel(GiayPhepMoiTruong entity)
        {
            var result = new GiayPhepMoiTruongViewModel();
            result.IdGiayPhepMoiTruong = entity.IdGiayPhepMoiTruong;
            result.TenGiayPhep = entity.TenGiayPhep;
            result.SoGiayPhep = entity.SoGiayPhep;
            result.CoQuanCap = entity.CoQuanCap;
            result.NgayCap = entity.NgayCap != null ? entity.NgayCap.Value.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture) : "";
            result.IdDuAn = entity.IdDuAn;
            result.TenDuAn = entity.DuAn != null ? entity.DuAn.TenDuAn : "";
            result.TenDoanhNghiep = entity.DuAn != null ? entity.DuAn.TenDoanhNghiep : "";
            result.IdKhuCongNghiep = entity.IdKhuCongNghiep;
            result.TenKhuCongNghiep = entity.KhuCongNghiep != null ? entity.KhuCongNghiep.TenKhuCongNghiep : "";
            result.TenChuDauTu = entity.KhuCongNghiep != null ? entity.KhuCongNghiep.TenChuDauTu : "";

            return result;
        }
        public GiayPhepMoiTruong MapViewModelToEntity(GiayPhepMoiTruongViewModel viewModel)
        {
            var entity = new GiayPhepMoiTruong();
            entity.IdGiayPhepMoiTruong = viewModel.IdGiayPhepMoiTruong;
            entity.TenGiayPhep = viewModel.TenGiayPhep;
            entity.SoGiayPhep = viewModel.SoGiayPhep;
            entity.CoQuanCap = viewModel.CoQuanCap;
            entity.NgayCap = string.IsNullOrEmpty(viewModel.NgayCap) ? null : DateTime.Parse(viewModel.NgayCap, new CultureInfo("vi-VN"));
            entity.IdDuAn = viewModel.IdDuAn == 0 ? null : viewModel.IdDuAn;
            entity.IdKhuCongNghiep = viewModel.IdKhuCongNghiep == 0 ? null : viewModel.IdKhuCongNghiep;
            return entity;
        }
    }

}
